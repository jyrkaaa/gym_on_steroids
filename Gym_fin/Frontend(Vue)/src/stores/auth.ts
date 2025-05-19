import { ref, computed } from 'vue'
import { defineStore } from 'pinia'
import type { stringOrNull } from '@/types.ts'
import { jwtDecode } from 'jwt-decode'
import AccountService from '@/services/AccountService.ts'

interface JwtPayload {
  exp: number;
  userid: number// Unix timestamp
  [key: string]: any;
}

export const useAuthStore = defineStore('auth', () => {
  const jwt = ref<stringOrNull>(null);
  const refreshToken = ref<stringOrNull>(null);
  const userId = ref<stringOrNull>(null);
  const currentWorkoutId = ref<stringOrNull>(null);

  function setCurrentWorkoutId(id: stringOrNull) {
    currentWorkoutId.value = id;
  }

  function clearCurrentWorkoutId() {
    currentWorkoutId.value = null;
  }

  const isAuthenticated = computed<boolean>(() => {
    if (!jwt.value) return false;

    try {
      const decoded = jwtDecode<JwtPayload>(jwt.value);
      const now = Math.floor(Date.now() / 1000);
      return decoded.exp > now;
    } catch (e) {
      return false;
    }
  });
  function saveTokens(jwtToken: string, refresh: string) {
    jwt.value = jwtToken;
    refreshToken.value = refresh;
    let decoded = jwtDecode<JwtPayload>(jwtToken);
    userId.value = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    localStorage.setItem('jwt', jwtToken);
    localStorage.setItem('refreshToken', refresh);
  }
  function loadTokensFromStorage() {
    jwt.value = localStorage.getItem('jwt');
    refreshToken.value = localStorage.getItem('refreshToken');
  }

  function clearTokens() {
    jwt.value = null;
    refreshToken.value = null;
    currentWorkoutId.value = null;

    localStorage.removeItem('jwt');
    localStorage.removeItem('refreshToken');
  }
  async function refreshJwtIfNeeded() {
    if (!jwt.value || !refreshToken.value) return;

    try {
      const decoded = jwtDecode<JwtPayload>(jwt.value);
      const now = Math.floor(Date.now() / 1000);
      const buffer = 10; // seconds before expiry to trigger refresh

      if (decoded.exp - now < buffer) {
        const result = await AccountService.refreshAsyncJWT(jwt.value ,refreshToken.value);
        if (result.data) {
          saveTokens(result.data.jwt, result.data.refreshToken);
        } else {
          clearTokens();
        }
      }
    } catch (err) {
      clearTokens();
    }
  }

  return {
    jwt,
    refreshToken,
    isAuthenticated,
    refreshJwtIfNeeded,
    clearTokens,
    saveTokens,
    loadTokensFromStorage,
    userId,
    currentWorkoutId,
    clearCurrentWorkoutId,
    setCurrentWorkoutId,
  }
});

