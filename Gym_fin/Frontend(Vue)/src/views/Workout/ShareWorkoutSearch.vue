<script setup lang="ts">
import { ref } from 'vue'
import WorkoutSearch from '@/components/WorkoutSearch.vue'


</script>

<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Search for Workouts</h1>
    <WorkoutSearch
      />
  </div>
</template>

<style scoped>
.accordion-button {
  cursor: pointer;
}
.accordion-button.collapsed {
  background-color: #f8f9fa;
}
.accordion-body {
  overflow: hidden;
}

/* Transition animation */
.accordion-enter-active,
.accordion-leave-active {
  transition: all 0.3s ease;
  max-height: 1000px;
  opacity: 1;
}
.accordion-enter-from,
.accordion-leave-to {
  max-height: 0;
  opacity: 0;
}
.card {
  margin-bottom: 20px;
}
.card-outline-success {
  border: 3px solid green;
  border-radius: 0.5rem;
  box-shadow: 0 8px 16px rgba(0, 128, 0, 0.2);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}
</style>
