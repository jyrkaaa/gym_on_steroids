<template>
  <main class="container my-4">
    <form method="get">
      <div class="text-center mt-4">
        <button @click.prevent="resumeWorkout()" class="btn btn-dark btn-lg mb-2">Resume an active Workout
        </button>
      </div>
      <div class="row g-3">
        <!-- Exercises Box -->
        <div class="col-12 col-md-4">
          <RouterLink class="box w-100 nav-link" to="/exercises">Exercises</RouterLink>
        </div>
        <!-- Workouts Box -->
        <div class="col-12 col-md-4">
          <RouterLink class="box w-100 nav-link" to="/workouts">Workouts</RouterLink>
        </div>
        <!-- Statistics Box -->
        <div class="col-12 col-md-4">
          <button type="submit" name="statistics" class="box w-100">Statistics</button>
        </div>
      </div>
    </form>
  </main>
</template>

<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth';

const authStore = useAuthStore();
const router = useRouter();

const resumeWorkout = async () => {
  const workoutId = authStore.currentWorkoutId;
  if (workoutId) {
    await router.push(`/workouts/${workoutId}`);
  } else await router.push(`/workouts`);
}
</script>

<style scoped>
.box {
  min-height: 400px; /* Adjusted for smaller screens */
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 10px;
  background-color: #f8f9fa;
  cursor: pointer;
  transition: transform 0.2s;
  font-size: 1.5rem; /* Slightly smaller for responsiveness */
  font-weight: 500;
  color: #000;
}

.box:hover {
  transform: scale(1.05);
  background-color: #e9ecef;
}

.h4 {
  color: white;
  margin-left: 2rem;
}

.btn-outline-light {
  margin-right: 1rem;
  margin-left: 1rem;

}

@media (max-width: 576px) {
  .box {
    min-height: 200px; /* Adjusted for smaller screens */
    font-size: 1.2rem; /* Further reduced for small screens */
    padding: 15px;
  }
}
</style>
