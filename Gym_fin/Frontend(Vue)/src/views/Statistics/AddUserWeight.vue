<script setup lang="ts">

import { ref } from 'vue';
import { UserWeightService } from '@/services/UserWeightService.ts';
import type { IUserWeight } from '@/domain/IUserWeight.ts';
import router from '@/router';

const weightinput = ref('');
const dateInput = ref('');
const desc = ref('');


const addWeight = async () => {
  const service = new UserWeightService();
  let formatedInput = new Date(dateInput.value);
  let entity : IUserWeight = {
    id: "",
    netUserId: null,
    weightKg: parseFloat(weightinput.value),
    date: formatedInput.toISOString(),
    desc: desc.value
  };
  await service.addAsync(entity);
  await router.push("/statistics");
}
</script>

<template>
  <div class="container container-tall mt-5">
    <h1 class="text-center mb-4">Add Your Weight</h1>

    <!-- Form for adding weight, time, and description -->
      <!-- Weight Input (float) -->
      <div class="mb-3">
        <label for="weight" class="form-label">Weight (kg)</label>
        <input v-model="weightinput" type="number" step="0.1" class="form-control" placeholder="Enter your weight" required>
      </div>

      <!-- Time Picker -->
      <div class="mb-3">
        <label for="time" class="form-label">Time</label>
        <input v-model="dateInput" type="datetime-local" class="form-control" required>
      </div>

      <!-- Optional Description -->
      <div class="mb-3">
        <label for="description" class="form-label">Description (Optional)</label>
        <textarea v-model="desc" class="form-control" placeholder="Overall wellbeing (optional)"></textarea>
      </div>

      <!-- Submit Button -->
      <div class="text-center">
        <button @click.prevent="addWeight()" class="btn btn-primary" >Add User Weight</button>
      </div>
  </div>
</template>

<style scoped>

</style>
