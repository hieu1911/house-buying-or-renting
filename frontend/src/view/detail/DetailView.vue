<template>
    <div>
        <hr/>

    </div>
</template>

<script setup>
import { onBeforeMount, ref, reactive } from 'vue';
import { useRoute } from 'vue-router';

import { getRecord } from '@/js/service/base';
import { getDetailRealEstate } from '@/js/service/realEstate';
import enums from '@/js/common/enum';

const realEstateType = ref(0);
const apartment = reactive({});
const boardingHouse = reactive({});
const house = reactive({});
const land = reactive({});

onBeforeMount(async () => {
    const router = useRoute();
    const realEstateId = router.params.id;

    const realEstate = await getRecord('RealEstate', realEstateId);
    realEstateType.value = realEstate.data.RealEstateType;
    switch (realEstateType.value) {
        case enums.realEstateEnum.APARTMENT: 
            Object.assign(apartment, (await getDetailRealEstate('Apartment', realEstateId)).data);
            break;
        case enums.realEstateEnum.BOARDINGHOUSE: 
            Object.assign(boardingHouse, (await getDetailRealEstate('BoardingHouse', realEstateId)).data);
            break;
        case enums.realEstateEnum.HOUSE: 
            Object.assign(house, (await getDetailRealEstate('House', realEstateId)).data);
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