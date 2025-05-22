<script setup lang="ts">
import { ref, reactive, onMounted, computed } from 'vue'

import type { IResultObject } from '@/types.ts'
import type { IWorkout } from '@/domain/IWorkout'

import { WorkoutService } from '@/services/WorkoutService'

import WeightChart from '@/components/WeightChart.vue'
import { useAuthStore } from '@/stores/auth.ts'

const props = defineProps<{exerciseId: string}>();
const store = useAuthStore();
const data = reactive<IResultObject<IWorkout[]>>({ data: []
});
const searchQuery = ref('');

// Filtered workouts by search
const filteredWorkouts = computed(() => {
  if (!searchQuery.value) return data.data
  return data.data?.filter(workout =>
    workout.date.startsWith(searchQuery.value)
  )
});
// Fetch workouts for the given exercise
const fetchPageData = async () => {
  await store.refreshJwtIfNeeded();
  const workoutService = new WorkoutService();
  const userResponse = await workoutService.getAllAsyncByExercise(props.exerciseId);
  data.data = userResponse.data ?? [];
  data.errors = userResponse.errors;
};
onMounted(async () => {
  await fetchPageData()
})
const chartData = computed(() => {
  if (data.data && data.data.length > 0) {
    return data.data.map(workout => {
      const allSets = workout.exercises?.flatMap(e => e.sets || []) || [];
      const maxWeight : number = allSets.reduce((max, set) => Math.max(max, set.weight || 0), 0);

      return {
        date: workout.date.split('T')[0],
        weightKg: maxWeight
      };
    }).filter(entry => entry.weightKg > 0).reverse(); // Exclude workouts with no weight sets
  } return [];
});

</script>

<template>
  <div class="container-fluid mt-4" v-if="data.data && data.data.length">
    <h1 class="text-center mb-4">Workouts for Selected Exercise</h1>

    <!-- Chart -->
    <div class="mb-5">
      <WeightChart  v-if="data.data && data.data.length"
                    :data="chartData"></WeightChart>
    </div>
    <!-- Search -->
    <div class="search-bar mb-4">
      <input v-model="searchQuery" type="text" class="form-control" placeholder="Search by date (e.g., 2025-01-01)" />
    </div>

    <!-- Exercise Cards -->
    <div class="row">
      <div class="col-md-4 mb-4" v-for="workout in filteredWorkouts" :key="workout.id">
        <div class="card h-100">
          <div class="card-body">
            <h5 class="card-title card-header bg-dark text-white">
                <RouterLink class="nav-link" :to="`/workouts/${workout.id}`" v-slot="{href, navigate, isActive, isExactActive }">
                  <a :href="href"
                     @click="navigate"
                     :class="{active: isActive, exact: isExactActive}"
                     class="custom-link">{{ workout.name }}
                  </a>
                </RouterLink>
            </h5>
            <p class="card-text"><strong>Date:</strong> {{ workout.date.split('T')[0] }}</p>

            <div v-for="exercise in workout.exercises" :key="exercise.id">
              <h6 class="mt-3 mb-2">{{ exercise.exercise?.name || 'Unnamed Exercise' }}</h6>

              <div
                v-for="(set, i) in exercise.sets"
                :key="set.id"
                class="border p-2 mb-2 rounded bg-light"
              >
                <p class="mb-1"><strong>Set {{ i + 1 }}</strong></p>
                <p class="mb-1">Weight: {{ set.weight }} kg</p>
                <p class="mb-1">Reps: {{ set.reps }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

  <div v-else class="text-center mt-5">
    <p>Loading or no data available...</p>
  </div>
</template>



<style scoped>
ul {
  list-style-type: disc;
  padding-left: 1.5rem;
}
.custom-link {
  color: white;
}
</style>
