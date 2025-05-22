<script setup lang="ts">
import { ref, reactive, watch, onMounted, computed } from 'vue'
import dayjs from 'dayjs'

import type { IResultObject } from '@/types.ts'
import type { IUserWeight } from '@/domain/IUserWeight.ts'
import type { IWorkout } from '@/domain/IWorkout'

import { UserWeightService } from '@/services/UserWeightService.ts'
import { WorkoutService } from '@/services/WorkoutService'

import WeightChart from '@/components/WeightChart.vue'
import { ExerciseService } from '@/services/ExerciseService.ts'
import type { IExercise } from '@/domain/IExercise.ts'
import { ExerciseCategoryService } from '@/services/ExerciseCategoryService.ts'
import type { IExerciseCategory } from '@/domain/IExerciseCategory.ts'
import router from '@/router'

const exerciseInput = ref("");
const data = reactive<IResultObject<IUserWeight[]>>({
  data: [],
  errors: undefined
})
const exerciseData = reactive<IResultObject<IExerciseCategory[]>>({
  data: [],
  errors: undefined
})

const selectedDate = ref(new Date()) // The day selected by the user
const displayedMonth = ref(dayjs(selectedDate.value).startOf('month').toDate()) // The month currently shown in calendar
const workouts = ref<IWorkout[]>([])

const workoutService = new WorkoutService()

// Fetch workouts for the displayed month
const fetchWorkouts = async () => {
  const current = dayjs(displayedMonth.value)
  const fromDate = current.startOf('month').format('YYYY-MM-DD')
  const toDate = current.endOf('month').format('YYYY-MM-DD')

  const result: IResultObject<IWorkout[]> = await workoutService.getAllAsyncModified(undefined, fromDate, toDate)
  workouts.value = result.data || []
}

// Helper: compare two dates by year and month
const isSameYearMonth = (date1: Date, date2: Date) => {
  const d1 = dayjs(date1)
  const d2 = dayjs(date2)
  return d1.year() === d2.year() && d1.month() === d2.month()
}

// Update displayedMonth only if month changed, then fetch new data
const handleMonthChange = (pages: any[]) => {
  if (!pages || pages.length === 0) return

  const newYear = pages[0].year
  const newMonth = pages[0].month // Usually 1-based month (1=Jan)

  const newDate = dayjs(`${newYear}-${String(newMonth).padStart(2, '0')}-01`).toDate()

  if (!isSameYearMonth(newDate, displayedMonth.value)) {
    displayedMonth.value = newDate
  }
};

const seeStats = async (exerciseAddedId: string) => {
  await router.push(`/statistics/${exerciseAddedId}`)
}

const exerciseAddedId = computed(() => {
  for (const cat of exerciseData.data || []) {
    const match = cat.exercises.find(ex => ex.name === exerciseInput.value);
    if (match) return match.id;
  }
  return '';
});

// Watch displayedMonth to fetch workouts whenever month changes
watch(displayedMonth, fetchWorkouts, { immediate: true })

// Fetch user weight data on mount
const fetchPageData = async () => {
  const exerciseService = new ExerciseCategoryService();
  const userService = new UserWeightService();
  const exerResponse = await exerciseService.getAllAsync()
  const userResponse = await userService.getAllAsync()
  exerciseData.data = exerResponse.data
  exerciseData.errors = exerResponse.errors
  data.data = userResponse.data;
  data.errors = userResponse.errors;
}

onMounted(async () => {
  await fetchPageData()
})
</script>

<template>
  <h3 class="mt-4">See Exercise Stats</h3>
  <div class="mb-3">
    <label for="exercise_id" class="form-label">Select Exercise:</label>
    <div class="d-flex flex-column">
      <input list="exercises" class="form-control" name="exercise_id" id="exercise_id" required v-model="exerciseInput" placeholder="Choose Exercise">
      <datalist id="exercises">
        <option value="" disabled>Choose an exercise</option>
        <template v-for="category in exerciseData.data" :key="category.name">
          <option disabled class="category-option">{{ category.name }}</option>
          <option
            v-for="exercise in category.exercises"
            :key="exercise.id"
            :value="exercise.name"
            :data-category="category.name"
          >
            {{ exercise.name }}
          </option>
        </template>
      </datalist>
      <button @click.prevent="seeStats(exerciseAddedId)" class="btn btn-dark">View Stats</button>
    </div>
  </div>
  <RouterLink to="/addWeight" class="btn btn-primary">Add Weight</RouterLink>
  <h2 v-if="data.data && data.data.length" class="mb-3">Weight Progress</h2>
  <div class="container">
    <WeightChart
      v-if="data.data && data.data.length"
      :data="data.data.map(w => ({ date: w.date, weightKg: w.weightKg }))"
    />
  </div>
  <div>
    <h2>Workout Calendar</h2>

    <vc-calendar
      v-model="selectedDate"
      is-expanded
      @update:pages="handleMonthChange"
      :attributes="[
        {
          key: 'workouts',
          dates: workouts.map(w => new Date(w.date)),
          dot: 'green'
        }
      ]"
    />

    <div class="mt-4">
      <h4>Workouts this month: {{ workouts.length }}</h4>
      <ul>
        <li v-for="w in workouts" :key="w.id">
          {{ w.name }} - {{ new Date(w.date).toLocaleDateString() }}
        </li>
      </ul>
    </div>
  </div>
</template>

<style scoped>
ul {
  list-style-type: disc;
  padding-left: 1.5rem;
}
</style>
