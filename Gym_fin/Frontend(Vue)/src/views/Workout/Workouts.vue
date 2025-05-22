<script setup lang="ts">
import { computed, nextTick, onMounted, reactive, ref } from 'vue'
import type { IResultObject } from '@/types.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'
import { WorkoutService } from '@/services/WorkoutService.ts'
import { useAuthStore } from '@/stores/auth.ts'
import { UsersInWorkoutService } from '@/services/UsersInWorkoutService.ts'

const requestIsOngoing = ref(false);
const data = reactive<IResultObject<IWorkout[]>>({});
const searchQuery = ref('')
const openSections = ref<string[]>([]); // Tracks open month keys
const store = useAuthStore();
const curUserid = store.userId;

// Current year period
const now = new Date();
const fromDate = ref(new Date(now.getFullYear(), 0, 1).toISOString().substring(0, 10)); // Jan 1st
const toDate = ref(new Date(now.getFullYear(), 11, 31).toISOString().substring(0, 10)); // Dec 31st



const fetchPageData = async () => {
  requestIsOngoing.value = true;
  try {
    const service = new WorkoutService();
    const result = await service.getAllAsyncModified(undefined, fromDate.value, toDate.value);

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
  let firstWorkout = data.data?.[0];
  if (firstWorkout) {
    let workoutDate = new Date(firstWorkout.date);
    let monthString = workoutDate.toLocaleString('default', { month: 'long', year: 'numeric' });
    openSections.value = [monthString];
  }
});

// Group by month-year and filter
const filteredByMonth = computed(() => {
  const groups: Record<string, IWorkout[]> = {}
  const query = searchQuery.value.trim().toLowerCase()

  for (const workout of data.data || []) {
    const workoutDate = new Date(workout.date)
    const formattedDate = workoutDate.toLocaleDateString()
    const formattedMonth = workoutDate.toLocaleString('default', { month: 'long', year: 'numeric' })

    const matchQuery =
      !query ||
      workout.name.toLowerCase().includes(query) ||
      formattedDate.toLowerCase().includes(query) ||
      formattedMonth.toLowerCase().includes(query)

    if (!matchQuery) continue

    if (!groups[formattedMonth]) {
      groups[formattedMonth] = []
    }

    groups[formattedMonth].push(workout)
  }

  return groups
})
const toggleSection = (month: string) => {
  if (openSections.value.includes(month)) {
    openSections.value = openSections.value.filter(m => m !== month)
  } else {
    openSections.value.push(month)
  }
}
const isOpen = (month: string) => openSections.value.includes(month)
const toggleActive = (workoutId: string) => {
  if (store.currentWorkoutId === workoutId) {
    store.clearCurrentWorkoutId();
  } else store.setCurrentWorkoutId(workoutId)
}
const showModal = ref(false);
const workoutToDelete = ref<{ id: string; name: string } | null>(null);

const confirmDelete = (id: string, name: string) => {
  store.refreshJwtIfNeeded();
  workoutToDelete.value = { id, name };
  showModal.value = true;
};

const deleteWorkout = async (id : string | undefined) => {
  if (id == undefined) return;
  console.log(id);
  const uiwService = new UsersInWorkoutService();
  await uiwService.removeAsync(id!);
  await fetchPageData();
  showModal.value = false;

}
const updateDates = async () =>
{
  await store.refreshJwtIfNeeded();
  await fetchPageData();
}

</script>

<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Your Workouts</h1>

    <router-link to="/makeWorkout">
      <button class="btn btn-success mb-3">Create Workout</button>
    </router-link>

    <div class="row mb-4 align-items-end">
      <div class="col-md-4">
        <label class="form-label">Search</label>
        <input v-model="searchQuery" type="text" class="form-control" placeholder="Name or date..." />
      </div>
      <div class="col-md-3">
        <label class="form-label">From</label>
        <input v-model="fromDate" type="date" class="form-control" />
      </div>
      <div class="col-md-3">
        <label class="form-label">To</label>
        <input v-model="toDate" type="date" class="form-control" />
      </div>
      <div class="col-md-3">
        <button @click.prevent="updateDates()" class="btn btn-outline-primary">Update</button>
      </div>
    </div>

    <div class="accordion" id="workoutsAccordion">
      <div v-for="(workouts, month) in filteredByMonth" :key="month" class="accordion-item">
        <h2 class="accordion-header">
          <button
            class="accordion-button"
            :class="{ collapsed: !isOpen(month) }"
            @click="toggleSection(month)"
          >
            {{ month }} ({{ workouts.length }} workouts)
          </button>
        </h2>
        <transition name="accordion">
          <div v-show="isOpen(month)" class="accordion-body">
            <div class="row">
              <div v-for="w in workouts" :key="w.id" class="col-12 col-md-4 d-flex">
                <div :class="['card workout-card flex-fill', store.currentWorkoutId == w.id ? 'card-outline-success' : '']">
                  <div class="card-body">
                    <h5 class="card-title d-flex align-items-center">
                      {{ w.name }}
                      <span
                        class="ms-2 rounded-circle"
                        :style="{
        width: '12px',
        height: '12px',
        backgroundColor: w.public ? 'green' : 'red',
        display: 'inline-block'
      }"
                        :title="w.public ? 'Public' : 'Private'"
                      ></span>
                    </h5>
                    <p class="card-text"><strong>Date:</strong> {{ new Date(w.date).toLocaleDateString() }}</p>
                    <router-link :to="`/workouts/${w.id}`">
                      <button class="btn btn-primary"><strong>Modify</strong></button>
                    </router-link>
                    <button @click.prevent="toggleActive(w.id)"
                            :class="['btn', store.currentWorkoutId === w.id ? 'btn-success' : 'btn-outline-success']">
                      {{ store.currentWorkoutId === w.id ? 'Active' : 'Set Actice'}}
                    </button>
                    <button class="btn btn-danger" @click="confirmDelete(w.users?.find(u => u.netUserId == curUserid)!.id!, w.name)">
                      <strong>Delete</strong>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </transition>
      </div>
    </div>
  </div>
  <!-- Delete Confirmation Modal -->
  <div class="modal fade show" tabindex="-1" style="display: block;" v-if="showModal">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Confirm Deletion</h5>
          <button type="button" class="btn-close" @click="showModal = false"></button>
        </div>
        <div class="modal-body">
          <p>Are you sure you want to delete <strong>{{ workoutToDelete?.name }}</strong>?</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" @click="showModal = false">Close</button>
          <button type="button" class="btn btn-danger" @click="deleteWorkout(workoutToDelete?.id)">Delete</button>
        </div>
      </div>
    </div>
  </div>
  <div class="modal-backdrop fade show" v-if="showModal"></div>

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
