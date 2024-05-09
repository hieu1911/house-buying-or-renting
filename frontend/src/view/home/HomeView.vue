<template>
    <div>
        <div class="search-wrapper">
            <h4>{{ $t('home.searchTitle') }}</h4>
            <div>
                <div>
                    <span>{{ $t('header.buy') }}</span>
                    <span>{{ $t('header.rent') }}</span>
                    <span>{{ $t('home.address') }}</span>
                </div>
                <div>
                    <div>
                        <v-icon></v-icon>
                        <input />
                    </div>
                    <div>
                        <v-button
                            :label="$t('home.filter')"
                            icon="filter"
                        ></v-button>
                        <v-button
                            :label="$t('home.search')"
                        ></v-button>
                    </div>
                </div>
            </div>
        </div>
        <div>

        </div>
    </div>
</template>

<script setup>

</script>

<style scoped>
@import url(./home-view.css);
</style>