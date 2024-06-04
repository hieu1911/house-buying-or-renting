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
            <h3 class="region-sub-title" style="margin: 0 0 16px 0">{{ $t('home.recomendSubTitle') }}</h3>
            <div class="recomend">
                <div style="width: 49%;" class="card-wrapper" @click="navigateByCityName('6e52efd1-769c-2ea0-eedc-845b5dcdad45')">
                    <div class="recomend-card">
                        <img src="../../assets/image/dalat.jpg" alt="">
                    </div>
                    <h3>Hà Nội</h3>
                </div>
                <div style="width: 49%;" class="card-wrapper" @click="navigateByCityName('1235c88b-5f2a-78d9-e612-ebbbcf065d1c')">
                    <div class="recomend-card">
                        <img src="../../assets/image/hcm.jpg" alt="">
                    </div>
                    <h3>Thành phố Hồ Chí Minh</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('405cd8be-1160-35ff-85c3-d3ede7a72094')">
                    <div class="recomend-card">
                        <img src="../../assets/image/nhatrang.jpg" alt="">
                    </div>
                    <h3>Nha Trang</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('79e2659b-1e78-7dc9-0528-030d4265475f')">
                    <div class="recomend-card">
                        <img src="../../assets/image/danang.jpg" alt="">
                    </div>
                    <h3>Đà Nẵng</h3>
                </div>
                <div style="width: 32%;" class="card-wrapper" @click="navigateByCityName('7b01963d-2bc0-5b15-82c3-d3ede7a72094')">
                    <div class="recomend-card">
                        <img src="../../assets/image/hoian.jpg" alt="">
                    </div>
                    <h3>Hội An</h3>
                </div>
            </div>
        </div>
        <div>
            <h4 class="region-title">{{ $t('home.rentRealestateTtile') }}</h4>
            <h3 class="region-sub-title" style="margin: 0;">{{ $t('home.rentRealestateSubTitle') }}</h3>
            <Carousel :items-to-show="5">
                <Slide v-for="(realEstate, idx) in realEstateRent" :key="idx">
                    <RealEstateCard
                        :realEstate="realEstate"
                    ></RealEstateCard>
                </Slide>
                <template #addons>
                    <Navigation />
                    <Pagination />
                </template>
            </Carousel>
        </div>

        <div>
            <h4 class="region-title">{{ $t('home.buyRealestateTitle') }}</h4>
            <h3 class="region-sub-title" style="margin: 0;">{{ $t('home.buyRealestateSubTitle') }}</h3>
            <Carousel :items-to-show="5">
                <Slide v-for="(realEstate, idx) in realEstateBuy" :key="idx">
                    <RealEstateCard
                        :realEstate="realEstate"
                    ></RealEstateCard>
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
import router from '@/js/router/router';
import { publicStore } from '@/js/store/publicStore';
import RealEstateCard from '../components/RealEstateCard/RealEstateCard.vue';

publicStore().setIsHomePage(true);

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

async function navigateByCityName(id) {
    router.push({
        path: '/list',
        query: {
            provinceId: id
        }
    })
}

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