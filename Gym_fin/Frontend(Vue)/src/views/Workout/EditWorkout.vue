<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue'
import type { IResultObject } from '@/types.ts'
import { ExerciseCategoryService } from '@/services/ExerciseCategoryService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import { WorkoutService } from '@/services/WorkoutService.ts'
import type { IExerciseCategory } from '@/domain/IExerciseCategory.ts'
import { ExerInWorkoutService } from '@/services/ExerInWorkoutService.ts'
import type { IExerInWorkout } from '@/domain/IExerInWorkout.ts'
import router from '@/router'
import { SetinExercService } from '@/services/SetinExercService.ts'
import type { ISetInExerc } from '@/domain/ISetInExerc.ts'
import { format, parseISO } from 'date-fns'

const requestIsOngoing = ref(false);
const data = reactive<IResultObject<IWorkout[]>>({});
const categoryData = reactive<IResultObject<IExerciseCategory[]>>({});
const props = defineProps<{id: string}>();


const exerciseInput = ref('');
const exerciseAddedId = computed(() => {
  for (const cat of categoryData.data || []) {
    const match = cat.exercises.find(ex => ex.name === exerciseInput.value);
    if (match) return match.id;
  }
  return '';
});
const fetchPageData = async (id: string) => {
  requestIsOngoing.value = true;
  try {
    const workoutService = new WorkoutService();
    const categoryService = new ExerciseCategoryService();
    const workoutResult = await workoutService.findAsync(id);
    const categoryResult = await categoryService.getAllAsync();

    //For Workout info
    data.data = workoutResult.data;
    data.errors = workoutResult.errors;
    //Exercise Select
    categoryData.data = categoryResult.data;
    categoryData.errors = categoryResult.errors;

    requestIsOngoing.value = false;
  } catch (error) {
    console.error(error)
  } finally {
    requestIsOngoing.value = false;
  }
}
onMounted(async () => {
  await fetchPageData(props.id)
});

const addExercise = async (workoutId: string, exerciseAddedId : string) => {
  try {
    const exerciseInWorkoutService = new ExerInWorkoutService();
    let entity : IExerInWorkout = {
      id: "",
      exerciseId: exerciseAddedId,
      workoutId: workoutId,
      desc: '',
      exercise: undefined,
      Sets: undefined
    };
    const exerciseResponse = await exerciseInWorkoutService.addAsync(entity);
    if (exerciseResponse.data) {
      exerciseInput.value = '';
      await fetchPageData(props.id);
    }
  } catch (e) {
    console.error(e)
  }
};
const deleteSet = async (setId: string) => {
  requestIsOngoing.value = true;
  try {
    const setService = new SetinExercService();
    const addRepResponse = await setService.removeAsync(setId);
    await fetchPageData(props.id);
    requestIsOngoing.value = false;
  } catch (e) {
    console.error(e)
    requestIsOngoing.value = false;

  }
}
const deleteExercise = async (exercId: string) => {
  try {
    const exerciseInWorkoutService = new ExerInWorkoutService();
    await exerciseInWorkoutService.removeAsync(exercId);

    await fetchPageData(props.id);

  } catch (e) {
    console.error(e)
  }
};
const duplicateSet = async (exerciseinWorkoutId: string, dupeWeight: number, dupeReps: number) => {
    try {
      const setService = new SetinExercService();
      let entity : ISetInExerc = {
        id: "",
        weight: dupeWeight,
        reps: dupeReps,
        exerInWorkoutId: exerciseinWorkoutId
      };
      await setService.addAsync(entity);
      await fetchPageData(props.id);
    } catch (e) {
      console.error(e)
    }
}
const editPublic = async (state: boolean) => {
  try {
    const workoutService = new WorkoutService();
    let boolEntity: IWorkout = {
      id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      date: "2025-05-15T15:57:44.680Z",
      name: "string",
      public: !state,
      exercises: null,
      users: null
    };
    let publicResponse = await workoutService.updateAsync(props.id, boolEntity);
    await fetchPageData(props.id);
  }catch (e) {
    console.error(e)
  }

}

</script>

<template>
  <div v-if="requestIsOngoing">Loading...</div>
    <div class="container mt-5" v-if="data.data">
      <div class="card shadow-sm">
        <div class="card-body">
          <h1 class="card-title text-center mb-4">{{ data.data.name }}</h1>
          <h3><strong>Date:</strong> {{ format(parseISO(data.data.date), 'PPPpp') }}</h3>
          <h3><strong>Public: {{ data.data.public }}</strong>  <button class="btn btn-outline-warning" @click.prevent="editPublic(data.data.public)">Change</button></h3>
          <h3 class="mt-4">Current Exercises</h3>
          <form method="post">
            <div class="row g-3">
              <div
                v-for="exercise in data.data.exercises"
                :key="exercise.id"
                class="col-md-6"
              >
                <div class="card border-dark mb-3">
                  <div class="card-header bg-dark text-white">
                    <strong>Exercise Name:</strong> {{ exercise.exercise.name }}
                  </div>
                  <div class="card-body">
                    <ul class="list-group">
                      <li
                        v-for="set in exercise.sets"
                        :key="set.id"
                        class="list-group-item d-flex justify-content-between align-items-center"
                      >
                        <div>
                          <strong>Weight:</strong> {{ set.weight }} kg,
                          <strong>Reps:</strong> {{ set.reps }}
                        </div>
                        <div>
                          <button @click.prevent="deleteSet(set.id)" class="btn btn-danger btn-sm mr-1">Delete</button>
                          <button @click.prevent="duplicateSet(exercise.id, set.weight, set.reps)" class="btn btn-primary btn-sm"
                                  :value="`${set.id},${set.reps},${set.weight}`"
                                  name="copyRep">Duplicate</button>
                        </div>
                      </li>
                    </ul>
                  </div>
                  <div class="card-footer d-flex justify-content-between">
                    <RouterLink :to="`/workouts/${id}/${exercise.id}/${exercise.exercise.id}`">
                      <button class="btn btn-primary btn-sm">Add Rep</button>
                    </RouterLink>
                    <button @click.prevent="deleteExercise(exercise.id)" :value="exercise.id" class="btn btn-danger btn-sm">Delete Exercise</button>
                  </div>
                </div>
              </div>
            </div>
          </form>

          <h3 class="mt-4">Add an Exercise</h3>
            <div class="mb-3">
              <label for="exercise_id" class="form-label">Select Exercise:</label>
              <input list="exercises" class="form-select" name="exercise_id" id="exercise_id" required v-model="exerciseInput" placeholder="Choose Exercise">
              <datalist id="exercises">
                  <option value="" disabled>Choose an exercise</option>
                  <template v-for="category in categoryData.data" :key="category.name">
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
            </div>
            <button @click.prevent="addExercise(id, exerciseAddedId)" class="btn btn-dark">Add Exercise</button>
        </div>
      </div>
    </div>

    <div v-else class="container mt-5 text-center">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
</template>

<style scoped>

</style>
