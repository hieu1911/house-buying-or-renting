<template>
    <div class="header-wrapper">
        <div class="header-left">
            <v-icon type='logo'></v-icon>
            <h3>Real estate</h3>
            <div class="header-link">
                <a>{{ $t('header.buy') }}</a>
                <a>{{ $t('header.rent') }}</a>
            </div>
        </div>
        <div class="header-right">
            <v-icon type='notify'></v-icon>
            <v-icon type='user'></v-icon>
        </div>
    </div>
</template>

<script setup>

</script>

<style scoped>
@import url(./the-header.css);
</style>