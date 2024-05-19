<template>
    <div>
        <div class="search-wrapper">
            <h4>{{ $t('home.searchTitle') }}</h4>
            <div class="search-body">
                <div class="search-type">
                    <span class="active">{{ $t('header.buy') }}</span>
                    <span>{{ $t('header.rent') }}</span>
                    <span>{{ $t('home.address') }}</span>
                </div>
                <hr/>
                <div class="search-content">
                    <div class="search-content-left">
                        <v-icon type="search-home"></v-icon>
                        <input class="search-input" :placeholder="$t('home.searchTitle')"/>
                    </div>
                    <div>
                        <v-button
                            type="hasIconSecondary"
                            :label="$t('home.filter')"
                            icon="filter"
                        ></v-button>
                        <v-button
                            type="primary"
                            :label="$t('home.search')"
                        ></v-button>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <h4 class="region-title">{{ $t('home.recomendTitle') }}</h4>
            <h3 class="region-sub-title">{{ $t('home.recomendSubTitle') }}</h3>
            <div class="recomend">
                <div style="width: 49%;" class="card-wrapper" @click="navigateByCityName('Da Lat')">
                    <div class="recomend-card">
                        <img src="../../assets/image/dalat.jpg" alt="">
                    </div>
                    <h3>Đà Lạt</h3>
                </div>
                <div style="width: 49%;" class="card-wrapper" @click="navigateByCityName('Ho Chi Minh City')">
                    <div class="recomend-card">
                        <img src="../../assets/image/hcm.jpg" alt="">
                    </div>
                    <h3>Thành phố Hồ Chí Minh</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('Nha Trang')">
                    <div class="recomend-card">
                        <img src="../../assets/image/nhatrang.jpg" alt="">
                    </div>
                    <h3>Nha Trang</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('Da Nang')">
                    <div class="recomend-card">
                        <img src="../../assets/image/danang.jpg" alt="">
                    </div>
                    <h3>Đà Nẵng</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('Hoi An')">
                    <div class="recomend-card">
                        <img src="../../assets/image/hoian.jpg" alt="">
                    </div>
                    <h3>Hội An</h3>
                </div>
            </div>
        </div>
        <div>
            <h4 class="region-title">{{ $t('home.rentRealestateTtile') }}</h4>
            <Carousel :items-to-show="4">
                <Slide v-for="slide in 10" :key="slide">
                    <div class="carousel__item">{{ slide }}</div>
                </Slide>

                <template #addons>
                    <Navigation />
                    <Pagination />
                </template>
            </Carousel>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, reactive, inject } from 'vue';
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'

import { getForCarousel } from '@/js/service/realEstate';
const postEnums = inject('$enums').postEnum;
const realEstateRent = reactive([]);
const realEstateBuy = reactive([]);

onBeforeMount(async () => {
    const realEstates = await getForCarousel();
    realEstates.data.forEach(realEstate => {
        if (realEstate.Type == postEnums.RENT) {
            realEstateRent.push(realEstate)
        } else {
            realEstateBuy.push(realEstate)
        }
    })
})

</script>

<style scoped>
@import url(./home-view.css);
.carousel__item {
  min-height: 200px;
  width: 100%;
  background-color: var(--color-primary);
  color: #fff;
  font-size: 20px;
  border-radius: 8px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.carousel__slide {
  padding: 10px;
}

.carousel__prev,
.carousel__next {
  box-sizing: content-box;
  border: 5px solid white;
}
</style>