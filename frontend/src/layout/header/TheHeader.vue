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
        <div v-if="!publicStore().isHomePage" class="header-mid">
            <v-input
                placeholder="Bất động sản"
                type="primary"
                w100
                hasIcon
                iconType="search"
            >
            </v-input>
            <v-button
                type="hasIconSecondary"
                :label="$t('home.filter')"
                icon="filter"
            ></v-button>
        </div>
        <div class="header-right">
            <v-button
                :label="$t('post.post')"
                type="hasIconPrimary"
                icon="checked"
            ></v-button>
            <v-icon type='notify'></v-icon>
            <v-icon type='user'></v-icon>
        </div>
    </div>
</template>

<script setup>
import { publicStore } from '@/js/store/publicStore';
</script>

<style scoped>
@import url(./the-header.css);
</style>