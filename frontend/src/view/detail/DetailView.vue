<template>
    <div>
        
    </div>
</template>

<script setup>
import { onBeforeMount } from 'vue';
import { useRoute } from 'vue-router';

onBeforeMount(() => {
    const router = useRoute();
    const realEstateId = router.params.id;
    console.log(realEstateId);
})
</script>

<style scoped>
@import url(./detail-view.css);
</style>