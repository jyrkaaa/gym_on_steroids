<script setup lang="ts">
import { onMounted, reactive, ref, computed } from 'vue'
import { ExerciseCategoryService } from '@/services/ExerciseCategoryService.ts'
import type { IResultObject } from '@/types.ts'
import type { IExerciseCategory } from '@/domain/IExerciseCategory.ts'
import { RouterLink } from 'vue-router'
import { ExerciseService } from '@/services/ExerciseService.ts'
import { useAuthStore } from '@/stores/auth.ts'

const requestIsOngoing = ref(false)
const data = reactive<IResultObject<IExerciseCategory[]>>({})
const searchQuery = ref('')
let errors = ref<string[]>([]);
const store = useAuthStore();

const fetchPageData = async () => {
  requestIsOngoing.value = true
  try {
    const service = new ExerciseCategoryService()
    const result = await service.getAllAsync()

    data.data = result.data
    data.errors = result.errors

    requestIsOngoing.value = false
  } catch (error) {
    console.error(error)
  } finally {
    requestIsOngoing.value = false
  }
}
onMounted(async () => {
  await fetchPageData()
})

const filteredData = computed(() => {
  const q = searchQuery.value.toLowerCase().trim()
  if (!q) {
    return data.data
  }

  return data.data!
    .map((cat) => {
      const filteredExercises = cat.exercises?.filter(
        (ex) => cat.name.toLowerCase().includes(q) || ex.name.toLowerCase().includes(q),
      )
      return filteredExercises && filteredExercises.length > 0
        ? { ...cat, exercises: filteredExercises }
        : null
    })
    .filter(Boolean)
});
const deleteExercise = async (id: string) => {
  console.log("Deleting exercise with id:" + id)
  await store.refreshJwtIfNeeded();
  const exerciseService = new ExerciseService();
  const delResult = await exerciseService.removeAsync2(id)
  console.log(delResult);
  if (delResult.errors) {
    errors.value = delResult.errors;
  } else {
    errors.value = [];
    await fetchPageData();
  }
}
</script>

<template>
  <div class="alert alert-danger" v-if="errors.length != 0 && errors">Can't delete Exercise because the exercise isn't created by you.</div>
  <div class="container mt-5">
    <h1>All Exercises</h1>

    <router-link to="/makeExercise">
      <button type="button" class="btn btn-success mb-4">Add New Exercise</button>
    </router-link>
    <div class="mb-4">
      <input
        v-model="searchQuery"
        type="text"
        class="form-control"
        placeholder="Search by category or exercise name"
      />
    </div>
    <div v-if="data.data">
      <div v-for="category in filteredData" :key="category!.name" class="category">
        <h2 class="category-title">{{ category!.name }}</h2>
        <div class="row">
          <div
            v-for="exercise in category!.exercises"
            :key="exercise.name"
            class="col-md-4 exercise-card"
          >
            <div class="card">
              <div class="card-body">
                <h5 class="card-title">{{ exercise.name }}</h5>
                <p class="card-text">
                  {{ exercise.desc || 'No description provided' }}
                </p>
                <button @click.prevent="deleteExercise(exercise.id)" class="btn btn-danger">Delete Exercise</button>
              </div>
            </div>
          </div>
        </div>
      </div>
      </div>
    <div v-else>
      No Exercises found.
    </div>
  </div>
</template>

<style scoped>
.category-title {
  margin-top: 30px;
  border-bottom: 2px solid #ccc;
  padding-bottom: 5px;
}

.card {
  margin-bottom: 20px;
  transition:
    transform 0.3s ease,
    box-shadow 0.3s ease;
}

.card:hover {
  transform: scale(1.05);
  box-shadow: 0 8px rgba(0, 0, 0, 0.2);
}

.btn-outline-light {
  margin-left: 10px;
}
</style>

<style scoped></style>
