<template>
  <div class="app-wrapper">
    <TheHeader v-if="!store.isAuthPage"></TheHeader>
    <TheContent></TheContent>
  </div>
</template>

<script setup>
import { defineComponent, onErrorCaptured, inject } from 'vue';
import TheHeader from '@/layout/header/TheHeader.vue';
import TheContent from '@/layout/content/TheContent.vue';
import { publicStore } from './js/store/publicStore';

const common = inject('$common')
const store = publicStore();

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
