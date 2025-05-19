import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import router from './router';
import VCalendar from 'v-calendar';
import 'v-calendar/style.css';
import 'bootstrap-icons/font/bootstrap-icons.css';



const app = createApp(App);
const pinia =createPinia();

app.use(pinia);
app.use(VCalendar, {
  componentPrefix: 'vc', // registers components like <vc-calendar />
}) // Registers <vc-calendar> and others globally


import { useAuthStore} from '@/stores/auth.ts';
const authStore = useAuthStore();
authStore.loadTokensFromStorage();

app.use(router);
app.mount('#app');
