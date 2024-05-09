<template>
    <div>
        <h4>{{ $t('post.title') }}</h4>
        <div>
            <v-combobox
                :label="$t('post.realestateType')"
            ></v-combobox>
        </div>
    </div>
</template>

<script setup>

</script>

<style scoped>
@import url(./post-view.css);
</style>