<template>
    <div>
        <hr/>
        <div v-if="realEstate.length == 0" style="font-size: 14px;">Chưa có bất động sản nào trong danh sách.</div>
        <div class="list-wrapper">
            <div class="filter" v-show="showFilter">
                <h3>Chọn lọc theo</h3>
                <div class="filter-price">
                    <h4>Khoảng giá</h4>
                    <ul v-if="isBuy">
                        <v-checkbox label="Giá dưới 1 tỷ"></v-checkbox>
                        <v-checkbox label="Giá 1 - 3 tỷ"></v-checkbox>
                        <v-checkbox label="Giả 3 - 6 tỷ"></v-checkbox>
                        <v-checkbox label="Giả 6 - 10 tỷ"></v-checkbox>
                        <v-checkbox label="Giá trên 10 tỷ"></v-checkbox>
                    </ul>
                    <ul v-else>
                        <v-checkbox label="Giá dưới 4 triệu"></v-checkbox>
                        <v-checkbox label="Giá 4 - 8 triệu"></v-checkbox>
                        <v-checkbox label="Giả 8 - 12 triệu"></v-checkbox>
                        <v-checkbox label="Giả 12 - 20 triệu"></v-checkbox>
                        <v-checkbox label="Giá trên 20 triệu"></v-checkbox>
                    </ul>
                </div>
                <div>
                    <h4>{{ isBuy ? "Mua bán" : "Cho thuê" }} bất động sản</h4>
                    <ul>
                        <v-checkbox label="Hà Nội"></v-checkbox>
                        <v-checkbox label="TP Hồ Chí Minh"></v-checkbox>
                        <v-checkbox label="Đà Nẵng"></v-checkbox>
                        <v-checkbox label="Cần Thơ"></v-checkbox>
                        <v-checkbox label="Bình Dương"></v-checkbox>
                    </ul>
                </div>
            </div>
            <div class="list-content">
                <div v-for="(item, idx) in realEstate" :key="idx" class="item-card">
                    <div @click="router.push(`/detail/${item.Id}`)">
                        <img :src="item.ImageUrls[0].Url"/>
                    </div>
                    <div class="item-wrapper">
                        <p class="item-title" @click="router.push(`/detail/${item.Id}`)">{{ item.Title }}</p>
                        <div class="card-info">
                            <span class="card-area">{{ item.Area }} m²</span>
                            <span class="card-mid">-</span>
                            <span class="card-price">{{ numberToWord(item.Price) }}</span>
                        </div>
                        <div class="item-desc">
                            <p>{{ item.Description }}</p>
                        </div>
                        <div class="card-address-wrapper">
                            <v-icon type="pin" nowrap></v-icon>
                            <span class="card-address">{{ item.District.Province.Name }}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, reactive, ref, watch } from 'vue';
import { useRoute } from 'vue-router';
import router from '@/js/router/router';

import { numberToWord } from '@/js/common/helper';
import { getListRealEstate, getByOwner, getSavedHistory, filter, search } from '@/js/service/realEstate';
import { getUserInfo } from '@/js/service/auth';

const realEstate= reactive([]);
const isBuy = ref(true);
const showFilter = ref(true);

onBeforeMount(async () => {
    realEstate.splice(0, realEstate.length);
    const router = useRoute();
    const provinceId = router.query.provinceId;
    const ownerId = router.query.ownerId;
    const saved = router.query.saved;
    const searchValue = router.query.search;
    const postType = router.query.postType;
    const realEstateType = router.query.realEstateType;
    const minPrice = router.query.minPrice;
    const maxPrice = router.query.maxPrice;
    const minArea = router.query.minArea;
    const maxArea = router.query.maxArea;

    if (postType && realEstateType && minPrice && maxPrice && minArea && maxArea) {
        const res = await filter(postType, realEstateType, minPrice, maxPrice, minArea, maxArea);
        res.data.forEach(r => realEstate.push(r));
        return;
    }

    if (searchValue) {
        const res = await search(searchValue, postType);
        res.data.forEach(r => realEstate.push(r));
        return;
    }


    if (saved) {
        const user = await getUserInfo();
        const res = await getSavedHistory(user.data.Id);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;        
        return;
    }

    if (ownerId) {
        const res = await getByOwner(ownerId);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;
        return;
    }

    const res = await getListRealEstate(provinceId, 10, 1);
    res.data.forEach(r => realEstate.push(r));
});

watch(() => useRoute().query, async () => {
    realEstate.splice(0, realEstate.length);
    const router = useRoute();
    const provinceId = router.query.provinceId;
    const ownerId = router.query.ownerId;
    const saved = router.query.saved;

    if (saved) {
        const user = await getUserInfo();
        const res = await getSavedHistory(user.data.Id);
        console.log(res);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;        
        return;
    }

    if (ownerId) {
        const res = await getByOwner(ownerId);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;
        return;
    }

    const res = await getListRealEstate(provinceId, 10, 1);
    res.data.forEach(r => realEstate.push(r));
})
</script>

<style scoped>
@import url(./list-view.css);
</style>