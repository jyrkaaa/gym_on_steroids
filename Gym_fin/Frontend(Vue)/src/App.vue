<script>
import { defineComponent } from 'vue'
import Header from '@/nav/Header.vue'
import Footer from './nav/Footer.vue'

export default defineComponent({
  components: { Header, Footer },
})
</script>
<style>
.footer {
  position: absolute;
  bottom: 0;
  width: 100%;
  white-space: nowrap;
  line-height: 60px;
}
</style>
<template>
  <Header />

  <div class="container container-tall">
    <RouterView />
  </div>

  <Footer />
</template>
<style scoped>
.container-tall {
  min-height: 87vh;
}
.footer {
  position: relative;
}
</style>/
