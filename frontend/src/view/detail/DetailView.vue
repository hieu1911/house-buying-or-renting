<template>
    <div>
        <hr/>
        <div>
            <div>
                <div>
                    <img :src="realEstate?.ImageUrls[0]?.Url"/>
                    <Carousel :items-to-show="4">
                        <Slide v-for="(image, idx) in realEstate?.ImageUrls" :key="idx">
                            <img :src="image?.Url"/>
                        </Slide>
                        <template #addons>
                            <Navigation />
                            <Pagination />
                        </template>
                    </Carousel> 
                </div>
                <div>

                </div>
            </div>
            <div></div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, ref, reactive } from 'vue';
import { useRoute } from 'vue-router';
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'

import { getRecord } from '@/js/service/base';
import { getDetailRealEstate } from '@/js/service/realEstate';
import enums from '@/js/common/enum';

const realEstateType = ref(0);
const realEstate = reactive({
    ImageUrls: []
})
const apartment = reactive({});
const boardingHouse = reactive({});
const house = reactive({});
const land = reactive({});

onBeforeMount(async () => {
    const router = useRoute();
    const realEstateId = router.params.id;

    const realEstateCurr = await getRecord('RealEstate', realEstateId);
    Object.assign(realEstate, realEstateCurr.data);
    
    realEstateType.value = realEstateCurr.data.RealEstateType;
    switch (realEstateType.value) {
        case enums.realEstateEnum.APARTMENT: 
            Object.assign(apartment, (await getDetailRealEstate('Apartment', realEstateId)).data);
            break;
        case enums.realEstateEnum.BOARDINGHOUSE: 
            Object.assign(boardingHouse, (await getDetailRealEstate('BoardingHouse', realEstateId)).data);
            break;
        case enums.realEstateEnum.HOUSE: 
            Object.assign(house, (await getDetailRealEstate('House', realEstateId)).data);
            console.log(house)
            break;
        case enums.realEstateEnum.Land: 
            Object.assign(land, (await getDetailRealEstate('Land', realEstateId)).data);
            break;
        default: 
            break;
    }
})
</script>

<style scoped>
@import url(./detail-view.css);
</style>