<template>
  <div class="app-wrapper">
    <TheHeader></TheHeader>
    <TheContent></TheContent>
  </div>
</template>

<script setup>
import { defineComponent, onErrorCaptured, inject } from 'vue';
import TheHeader from '@/layout/header/TheHeader.vue';
import TheContent from '@/layout/content/TheContent.vue';

const common = inject('$common')

defineComponent({
  TheHeader,
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
