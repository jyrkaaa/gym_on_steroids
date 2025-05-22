<script setup lang="ts">

import { onMounted, reactive, ref } from 'vue'
import type { IResultObject } from '@/types.ts'
import type { IExerciseCategory } from '@/domain/IExerciseCategory.ts'
import { ExerciseCategoryService } from '@/services/ExerciseCategoryService.ts'
import { ExerciseService } from '@/services/ExerciseService.ts'
import type { IExerciseCreate } from '@/domain/Create/IExerciseCategoryCreate.ts'
import router from '@/router';
import type { IExercise } from '@/domain/IExercise.ts'

const requestIsOngoing = ref(false);
const data = reactive<IResultObject<IExerciseCategory[]>>({});

let catName = ref('');
let catId = ref('');
let desc = ref('');

const fetchPageData = async () => {
  requestIsOngoing.value = true;
  try {
    const service = new ExerciseCategoryService();
    const result = await service.getAllAsync();

    data.data = result.data;
    data.errors = result.errors;

    requestIsOngoing.value = false;
  } catch (error) {
    console.error(error)
  } finally {
    requestIsOngoing.value = false;
  }
}
onMounted(async () => {
  await fetchPageData()
});
const addExercise = async () => {
  const entity: IExercise = {
    name: catName.value,
    date: new Date().toISOString(),
    desc: desc.value,
    exerTargetId: null,
    exerGuideId: null,
    exerciseCategoryId: catId.value,
    id: ""
  }
  const exerciseService = new ExerciseService();
  const res = await exerciseService.addAsync(entity);
  await router.push('/exercises')
}

</script>

<template>
  <div class="container mt-5">
    <h1>Add New Exercise</h1>
    <form method="POST">
      <div class="mb-3">
        <label for="name" class="form-label">Exercise Name</label>
        <input v-model="catName" type="text" class="form-control" id="name" name="name" required>
      </div>

      <div class="mb-3">
        <label for="category" class="form-label">Category</label>
        <select v-model="catId" class="form-select" id="category" name="category_id" required>
          <option v-for="category in data.data" :value="category.id">{{ category.name }}</option>
        </select>
      </div>

      <div class="mb-3">
        <label for="description" class="form-label">Description (Optional)</label>
        <textarea v-model="desc" class="form-control" id="description" name="description"></textarea>
      </div>

      <button class="btn btn-dark" @click.prevent="addExercise">Add Exercise</button>
    </form>
  </div>
</template>

<style scoped>

</style>
