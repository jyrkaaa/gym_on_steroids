<script setup lang="ts">
import { reactive, ref, watch } from 'vue'
import type { IResultObject } from '@/types'
import { WorkoutService } from '@/services/WorkoutService.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import { useAuthStore } from '@/stores/auth.ts'
import { UsersInWorkoutService } from '@/services/UsersInWorkoutService.ts'
import type { IUserWeight } from '@/domain/IUserWeight.ts'
import type { IUsersInWorkout } from '@/domain/IUsersInWorkout.ts'
import router from '@/router'

const searchQuery = ref('')
const service = new WorkoutService()
const store = useAuthStore();
const userId = store.userId
const data = reactive<IResultObject<IWorkout[]>>({
  data : [],
});


// Optional: debouncing
let debounceTimeout: number | undefined = 100;

watch(searchQuery, (newQuery) => {
  clearTimeout(debounceTimeout)

  if (newQuery.trim() === '') {

    return
  }

  debounceTimeout = window.setTimeout(async () => {
    const response: IResultObject<IWorkout[]> = await service.getAllAsyncModified(newQuery, undefined, undefined)
    data.data = response.data || [];
    data.errors = response.errors;
  }, 300) // 300ms debounce
});
const addWorkout = async (id: string) => {
  const uiwService = new UsersInWorkoutService();
  let uiwEntity : IUsersInWorkout = {
    id: '',
    workoutId: id,
    netUserId: userId!
  }
  await uiwService.addAsync(uiwEntity)
}
const removeWorkout = async (id: string | undefined) => {
  if (id == undefined) return;
  const uiwService = new UsersInWorkoutService();
  await uiwService.removeAsync(id!)
  await router.push("/shareWorkout");
}
</script>

<template>
    <div class="search-container mx-auto">
      <input
        type="text"
        v-model="searchQuery"
        placeholder="Search workouts..."
        class="form-control mb-4"
      />

      <div v-if="data.data?.length === 0" class="text-muted text-center">No workouts found.</div>

      <div class="row g-3">
        <div
          v-for="w in data.data"
          :key="w.id"
          class="col-12 col-md-6 col-lg-4"
        >
          <div class="card h-100 shadow-sm">
            <div class="card-body d-flex flex-column justify-content-between">
              <div>
                <div class="d-flex flex-col justify-content-between">
                  <h5 class="card-title mb-2">{{ w.name }}</h5>
                  <router-link :to="`/workouts/${w.id}`" class="btn btn-outline-primary">
                    <i class="bi bi-box-arrow-in-left me-1"></i>
                  </router-link>
                </div>
                <p class="card-text mb-1"><strong>Date:</strong> {{ new Date(w.date).toLocaleDateString() }}</p>
                <p class="card-text mb-2">
                  <strong>Users:</strong>
                  <span v-if="w.users?.length">
                  <span
                    v-for="(u, index) in w.users"
                    :key="u.id"
                    class="badge bg-secondary me-1"
                  >
                    {{ u.netUser?.username || 'Unknown' }}
                  </span>
                </span>
                  <span v-else class="text-muted">No users</span>
                </p>
              </div>
              <button
                v-if="!w.users?.some(u => u.netUserId === userId)"
                class="btn btn-primary mt-2"
                @click="addWorkout(w.id)"
              >
                Add
              </button>
              <span v-else-if="w.users?.length == 1">Only You are in this workout, if you want to delete, do it in the workout page.</span>
              <button
                v-else
                class="btn btn-danger mt-2"
                @click="removeWorkout(w.users?.find(u => u.netUserId == userId)?.id)"
              >
                Remove
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<style scoped>
.search-container {
  max-width: 800px;
  padding: 1rem;
}
.card-title {
  font-weight: 600;
}
</style>
