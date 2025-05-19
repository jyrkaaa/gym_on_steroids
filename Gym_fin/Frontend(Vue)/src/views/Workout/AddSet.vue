<script setup lang="ts">
import { useRoute } from 'vue-router'
import { onMounted, reactive, ref } from 'vue'
import { SetinExercService } from '@/services/SetinExercService.ts'
import type { ISetInExerc } from '@/domain/ISetInExerc.ts'
import router from '@/router'
import type { IResultObject } from '@/types.ts'
import type { IWorkout } from '@/domain/IWorkout.ts'

const props = defineProps<{id: string, eiwId: string, exercId:string}>();
const data = reactive<IResultObject<ISetInExerc[]>>({});
const route = useRoute();
const weight = ref('0');
const reps = ref('0');

const fetchPageData = async () => {
  const setService = new SetinExercService();
  let result = await setService.getBestSet(props.exercId);
  data.data = result.data;
  data.errors = result.errors;
  console.log(data)
}

const addSet = async () => {
  const setService = new SetinExercService();
  let entity: ISetInExerc = {
    id: "",
    weight: parseFloat(weight.value),
    reps: parseFloat(reps.value),
    exerInWorkoutId: props.eiwId,
  };
  const response = await setService.addAsync(entity);
  if (response.errors) {
    console.log('User Not appart of workout.');
  }
  await router.push(`/workouts/${props.id}`)
}
onMounted(async () => {
  await fetchPageData()
});
const insertStats = (weightInsert: number | null, repsInsert: number | null) => {
  if (weightInsert != null) weight.value = weightInsert.toString();
  if (repsInsert != null) reps.value = repsInsert.toString();
}
const modifyStats = (weightInsert: number, repsInsert: number) => {
  weight.value = (parseFloat(weight.value) + weightInsert).toString();
  reps.value = (parseFloat(reps.value) + repsInsert).toString();
}
</script>

<template>
  <div class="container">
    <div class="card shadow-sm">
      <div class="card-body">
        <h1 class="card-title text-center">Add A Rep to </h1>
        <p class="text-center text-muted">Enter the weight and number of reps for your exercise.</p>
        <!-- Display Personal Best -->
        <div v-if="data.data" class="personal-best-container text-center">
          <p><strong>Personal best is rep(s) {{data.data.reps }} at {{data.data.weight}} kg</strong></p>
          <!-- Button to insert personal best values into form -->
          <button @click.prevent="insertStats(data.data.weight, data.data.reps)" class="btn btn-warning">Use Personal Best</button>
        </div>

        <form method="POST" class="mt-4">
          <div class="mb-3">
            <label for="weight" class="form-label">Weight (kg):</label>
            <input type="number" step="0.1" min="0" class="form-control" v-model="weight"  placeholder="Enter weight" required>
            <div class="mt-2 d-flex justify-content-between">
              <div>
                <button @click.prevent="modifyStats(5, 0)" class="btn btn-success">+5</button>
                <button @click.prevent="modifyStats(2.5, 0)" class="btn btn-success mx-1">+2.5</button>
              </div>
              <div>
                <button @click.prevent="modifyStats(-2.5, 0)" class="btn btn-danger mx-1">-2.5</button>
                <button @click.prevent="modifyStats(-5, 0)" class="btn btn-danger">-5</button>
              </div>
            </div>

          </div>

          <!-- Reps Input -->
          <div class="mb-3">
            <label for="reps" class="form-label">Number of Reps:</label>
            <input type="number" min="1" class="form-control" v-model="reps" placeholder="Enter number of reps" required>
            <div class="mt-2 d-flex justify-content-between">
              <div>
                <button @click.prevent="insertStats(null, 10)" class="btn btn-success">10</button>
                <button @click.prevent="insertStats(null, 6)" type="button" class="btn btn-success mx-1">6</button>
                <button @click.prevent="modifyStats(0, 1)" type="button" class="btn btn-success mx-1">+1</button>
              </div>
              <div>
                <button @click.prevent="modifyStats(0, -1)" type="button" class="btn btn-danger mx-1">-1</button>
                <button @click.prevent="modifyStats(0, -2)" type="button" class="btn btn-danger">-2</button>
              </div>
            </div>
          </div>

          <!-- Submit Button -->
          <button @click.prevent="addSet()" class="btn btn-dark w-100">Submit</button>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>
