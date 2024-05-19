<template>
    <div>
        
    </div>
</template>

<script setup>
import { onBeforeMount, defineProps } from 'vue';

const props = defineProps({
    'realEstate': Object
})

onBeforeMount(() => {
    console.log(123)
});

</script>

<style scoped>
@import url(./real-estate-card.css);
</style>