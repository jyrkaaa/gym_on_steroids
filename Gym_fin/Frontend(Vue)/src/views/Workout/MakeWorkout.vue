<script setup lang="ts">
import {useAuthStore} from '@/stores/auth.ts'
import { ref } from 'vue'
import { WorkoutService } from '@/services/WorkoutService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import router from '@/router'

const workoutName = ref('');
const date = ref('');
const status = ref('0');


const store = useAuthStore();
const userId = store.userId;

const fillToday = () => {
  const today = new Date();
  const yyyy = today.getFullYear();
  const mm = String(today.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
  const dd = String(today.getDate()).padStart(2, '0');
  const hh = String(today.getHours()).padStart(2, '0');
  const min = String(today.getMinutes()).padStart(2, '0');
  date.value = `${yyyy}-${mm}-${dd}T${hh}:${min}`;

}
const makeWorkout = async () => {
  const workoutService = new WorkoutService();
  const chosenDate = new Date(date.value);
  const entity:IWorkout = {
    name: workoutName.value,
    public: !!status,
    date: chosenDate.toISOString(),
    id: '',
    exercises: null,
    users: null,
  }
  const data = await workoutService.addAsync(entity);
  if (data.data) {
    await router.push('/workouts');
  }

}
</script>

<template>
  <div class="container mt-5">
    <div class="card shadow-sm">
      <div class="card-body">
        <h1 class="card-title text-center mb-4">Create a New Workout</h1>
        <form method="POST">
          <!-- Input for workout name -->
          <div class="mb-3">
            <label for="workout_name" class="form-label">Workout Name:</label>
            <input v-model="workoutName"  type="text" class="form-control" id="workout_name" name="workout_name" placeholder="Enter workout name" required>
          </div>

          <!-- Input for workout date -->
          <div class="mb-3">
            <label for="workout_date" class="form-label">Workout Date:</label>
            <input v-model="date" type="datetime-local" class="form-control" id="workout_date" name="workout_date" required>
            <button @click.prevent="fillToday" class="btn btn-warning mt-2" id="fill_today">Today</button>
          </div>
          <div class="mb-3">
            <label for="workout_status" class="form-label">Workout Status:</label>
            <select v-model="status" class="form-select" required>
              <option value="1">Public</option>
              <option value="0">Private</option>
            </select>
          </div>
          <!-- Checkbox section for exercises
          <div class="mb-3">
            <label class="form-label">Select Exercises:</label>
            <div class="row">
              <div class="col-md-4">  Adjust column width using Bootstrap grid classes
                <div class="form-check">
                  <input type="checkbox" class="form-check-input" id="exercise_8" name="exercises[]" value="8">
                  <label class="form-check-label" for="exercise_8">
                    Squat                                            </label>
                </div>
              </div>
              <div class="col-md-4">  Adjust column width using Bootstrap grid classes
                <div class="form-check">
                  <input type="checkbox" class="form-check-input" id="exercise_9" name="exercises[]" value="9">
                  <label class="form-check-label" for="exercise_9">
                    Bench Press                                            </label>
                </div>
              </div>
            </div>
          </div>
  -->
          <!-- Submit button -->
          <div class="text-center">
            <button @click.prevent="makeWorkout" class="btn btn-dark">Create Workout</button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>
