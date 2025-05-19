<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router';
import { useAuthStore } from '@/stores/auth.ts';
import { computed, ref } from 'vue'
import AccountService from '@/services/AccountService.ts'

const userStore = useAuthStore();
const router = useRouter();
const authStore = useAuthStore();



let loginIsOngoing = ref(false);
let errors = ref<string[]>([]);

const doLogout = async () => {
  const refreshToken = authStore.refreshToken;
  const jwt = authStore.jwt;
  loginIsOngoing.value = true;
  const res = await AccountService.logoutAsync(jwt!, refreshToken!);

  if (res.data) {
    authStore.clearTokens();
    errors.value = [];
    await router.push('/');
  } else {
    errors.value = res.errors!;
  }

  loginIsOngoing.value = false;
}

const isLoggedIn = computed(() => userStore.isAuthenticated)

</script>


<template>
  <header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
      <div class="container">
        <a class="navbar-brand" href="/">WebApp</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse"
                aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
          <ul v-if="isLoggedIn" class="navbar-nav flex-grow-1">
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/workouts">Workouts</RouterLink>
            </li>

            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/exercises">Exercises</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/statistics">Statistics</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/shareWorkout">Search for workouts</RouterLink>
            </li>

          </ul>

          <ul v-if="!isLoggedIn" class="navbar-nav">
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/register">Register</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" to="/login">Login</RouterLink>
            </li>
          </ul>

          <ul v-else class="navbar-nav">
            <li class="nav-item">
              <RouterLink class="nav-link text-dark" @click="doLogout()" to="/login">Logout</RouterLink>
            </li>
          </ul>

        </div>
      </div>
    </nav>
  </header>
</template>
