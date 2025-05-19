import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import Dashboard from '../views/Dashboard.vue';
import { useAuthStore } from '@/stores/auth';
import Register from '@/views/Register.vue'
import Exercises from '@/views/Exercise/Exercises.vue'
import MakeExercise from '@/views/Exercise/MakeExercise.vue'
import Workouts from '@/views/Workout/Workouts.vue'
import EditWorkout from '@/views/Workout/EditWorkout.vue'
import MakeWorkout from '@/views/Workout/MakeWorkout.vue'
import AddSet from '@/views/Workout/AddSet.vue'
import Statistics from '@/views/Statistics/Statistics.vue'
import ShareWorkoutSearch from '@/views/Workout/ShareWorkoutSearch.vue'
import AddUserWeight from '@/views/Statistics/AddUserWeight.vue'

const routes = [
  { path: '/', name: 'Dashboard', component: Dashboard, meta: { requiresAuth: true } },
  { path: '/login', name: 'Login', component: Login },
  { path: '/register', name: 'Register', component: Register },
  { path: '/exercises', name: 'Exercises', component: Exercises , meta: { requiresAuth: true } },
  { path: '/makeExercise', name: 'MakeExercise', component: MakeExercise , meta: { requiresAuth: true } },
  { path: '/workouts', name: 'Workouts', component: Workouts , meta: { requiresAuth: true } },
  { path: '/workouts/:id', name: 'EditWorkout', component: EditWorkout, props: true , meta: { requiresAuth: true } },
  { path: '/makeWorkout', name: 'MakeWorkout', component: MakeWorkout, meta: { requiresAuth: true } },
  { path: '/workouts/:id/:eiwId/:exercId', name: 'AddSet', component: AddSet, props: true, meta: { requiresAuth: true } },
  { path: '/statistics', name: 'Statistics', component: Statistics, meta: { requiresAuth: true } },
  { path: '/shareWorkout', name: 'ShareWorkout', component: ShareWorkoutSearch, meta: { requiresAuth: true } },
  { path: '/addWeight', name: 'AddWeight', component: AddUserWeight, meta: { requiresAuth: true } },

];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// Protect routes with requiresAuth meta
router.beforeEach(async (to, from, next) => {
  const auth = useAuthStore();

  // Optionally: refresh JWT if needed
  await auth.refreshJwtIfNeeded();

  if (to.meta.requiresAuth && !auth.isAuthenticated) {
    next({ path: '/login' });
  } else {
    next();
  }
});


export default router;
