<template>
  <div class="app-wrapper">
    <TheMenu></TheMenu>
    <div class="app-content">
      <TheHeader></TheHeader>
      <TheContent></TheContent>
    </div>
  </div>
</template>

<script setup>
import { defineComponent, onErrorCaptured, inject } from 'vue';
import TheHeader from '@/layout/header/TheHeader.vue';
import TheMenu from '@/layout/menu/TheMenu.vue';
import TheContent from '@/layout/content/TheContent.vue';

const common = inject('$common')

defineComponent({
  TheHeader,
  TheMenu,
  TheContent
})

onErrorCaptured((err) => {
  console.log(err);
  common.handleError(err);
})
</script>

<style>
@import url(./css/main.css);
</style>
