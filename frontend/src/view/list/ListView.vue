<template>
    <div>
        <div class="list-wrapper">
            <div class="filter" v-show="showFilter">
                <h3>Chọn lọc theo</h3>
                <div class="filter-price">
                    <h4>Khoảng giá</h4>
                    <ul v-if="isBuy">
                        <v-checkbox label="Giá dưới 1 tỷ" @change="filterList()" v-model="price1T"></v-checkbox>
                        <v-checkbox label="Giá 1 - 3 tỷ" @change="filterList()" v-model="price13T"></v-checkbox>
                        <v-checkbox label="Giả 3 - 6 tỷ" @change="filterList()" v-model="price36T"></v-checkbox>
                        <v-checkbox label="Giả 6 - 10 tỷ" @change="filterList()" v-model="price610T"></v-checkbox>
                        <v-checkbox label="Giá trên 10 tỷ" @change="filterList()" v-model="price10T"></v-checkbox>
                    </ul>
                    <ul v-else>
                        <v-checkbox label="Giá dưới 4 triệu" @change="filterList()" v-model="price4Tr"></v-checkbox>
                        <v-checkbox label="Giá 4 - 8 triệu" @change="filterList()" v-model="price48Tr"></v-checkbox>
                        <v-checkbox label="Giả 8 - 12 triệu" @change="filterList()" v-model="price812Tr"></v-checkbox>
                        <v-checkbox label="Giả 12 - 20 triệu" @change="filterList()" v-model="price1220Tr"></v-checkbox>
                        <v-checkbox label="Giá trên 20 triệu" @change="filterList()" v-model="price20Tr"></v-checkbox>
                    </ul>
                </div>
                <div>
                    <h4>{{ isBuy ? "Mua bán" : "Cho thuê" }} bất động sản</h4>
                    <ul>
                        <v-checkbox label="Hà Nội" @change="filterList()" v-model="addressHN"></v-checkbox>
                        <v-checkbox label="TP Hồ Chí Minh" @change="filterList()" v-model="addressHCM"></v-checkbox>
                        <v-checkbox label="Đà Nẵng" @change="filterList()" v-model="addressDN"></v-checkbox>
                        <v-checkbox label="Cần Thơ" @change="filterList()" v-model="addressCT"></v-checkbox>
                        <v-checkbox label="Bình Dương" @change="filterList()" v-model="addressBD"></v-checkbox>
                    </ul>
                </div>
            </div>
            <div class="list-content">
                <div v-if="realEstate.length == 0" style="font-size: 14px;">Chưa có bất động sản nào trong danh sách.</div>
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

                    <div v-if="isShowHistory" class="item-card-status" :style="item.IsAccepted == 0 ? 'color: #00af00;' : item.IsAccepted == 1 ? 'color: #007AFF;' : 'color: #DE3618;'">
                       {{ item.IsAccepted == 0 ? 'Đang chờ duyệt' : item.IsAccepted == 1 ? 'Đã được duyệt' : 'Bị từ chối' }}
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, onMounted, reactive, ref, watch } from 'vue';
import { useRoute } from 'vue-router';
import router from '@/js/router/router';

import { numberToWord } from '@/js/common/helper';
import { getListRealEstate, getByOwner, getSavedHistory, filter, search, searchByDisctrict } from '@/js/service/realEstate';
import { getUserInfo } from '@/js/service/auth';
import { publicStore } from '@/js/store/publicStore';
import { getRecords } from '@/js/service/base';

const realEstate= reactive([]);
const realEstateConst = reactive([]);
const isBuy = ref(true);
const showFilter = ref(true);
const isShowHistory = ref(false);

const addressHN = ref(false);
const addressHCM = ref(false);
const addressDN = ref(false);
const addressCT = ref(false);
const addressBD = ref(false);

const price1T = ref(false);
const price13T = ref(false);
const price36T = ref(false);
const price610T = ref(false);
const price10T = ref(false);

const price4Tr = ref(false);
const price48Tr = ref(false);
const price812Tr = ref(false);
const price1220Tr = ref(false);
const price20Tr = ref(false);

onBeforeMount(async () => {
    realEstate.splice(0, realEstate.length);
    const router = useRoute();
    const provinceId = router.query.provinceId;
    const districtId = router.query.districtId;
    const ownerId = router.query.ownerId;
    const saved = router.query.saved;
    const searchValue = router.query.search;
    let postType = router.query.postType;
    const realEstateType = router.query.realEstateType;
    const minPrice = router.query.minPrice;
    const maxPrice = router.query.maxPrice;
    const minArea = router.query.minArea;
    const maxArea = router.query.maxArea;

    if (!postType) postType = 1;

    isBuy.value = postType == 2;

    if (postType == 1) publicStore().setIsRenting(true);
    else publicStore().setIsRenting(false);

    if (postType && realEstateType && minPrice && maxPrice && minArea && maxArea) {
        const res = await filter(postType, realEstateType, minPrice, maxPrice, minArea, maxArea);
        res.data.forEach(r => realEstate.push(r));
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));
        return;
    }

    if (searchValue && postType) {
        const res = await search(searchValue, postType);
        res.data.forEach(r => realEstate.push(r));
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));
        return;
    }


    if (saved) {
        const user = await getUserInfo();
        const res = await getSavedHistory(user.data.Id);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;    
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));    
        return;
    }

    if (ownerId) {
        const res = await getByOwner(ownerId);
        res.data.forEach(r => realEstate.push(r));
        showFilter.value = false;
        isShowHistory.value = true;
        console.log(realEstate);
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));
        return;
    }

    if (provinceId) {
        const res = await getListRealEstate(provinceId, 10, 1, postType);
        res.data.forEach(r => realEstate.push(r));
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));
        return;
    }

    if (districtId) {
        const res = await searchByDisctrict(districtId, postType);
        res.data.forEach(r => realEstate.push(r));
        realEstateConst.splice(0, realEstateConst.length);
        realEstate.forEach(r => realEstateConst.push(r));
        return;
    }

    const res = await getRecords("RealEstate");
    console.log(res);
    res.data.forEach(r => realEstate.push(r));
});

onMounted(() => {
    console.log(realEstate);
})

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
});

function filterList() {
    let listClone = [];
    realEstateConst.forEach(r => listClone.push(r));
    listClone = listClone.filter(item => {
        if (addressHN.value && item.District.Province.Id == "6e52efd1-769c-2ea0-eedc-845b5dcdad45") return true;
        if (addressHCM.value && item.District.Province.Id == "1235c88b-5f2a-78d9-e612-ebbbcf065d1c") return true;
        if (addressDN.value && item.District.Province.Id == "471530a2-44fe-7395-b1ad-cb989f2177da") return true;
        if (addressCT.value && item.District.Province.Id == "3d7f2a35-42d8-229a-e429-16c01537719a") return true;
        if (addressBD.value && item.District.Province.Id == "1ce192a3-6b70-712d-fa74-9471face0ab4") return true;

        if (!addressHN.value && !addressHCM.value && !addressDN.value && !addressCT.value && !addressBD.value) return true;

        return false;
    });

    console.log(listClone);

    listClone = listClone.filter(item => {
        if (isBuy.value) {
            if (price1T.value && item.Price < 1000000000) return true;
            if (price13T.value && item.Price > 1000000000 && item.Pirce < 3000000000) return true;
            if (price36T.value && item.Price > 3000000000 && item.Pirce < 6000000000) return true;
            if (price610T.value && item.Price > 6000000000 && item.Pirce < 10000000000) return true;
            if (price10T.value && item.Price > 10000000000) return true;

            if (!price1T.value && !price13T.value && !price36T.value && !price610T.value && !price10T.value) return true;

            return false;
        } else {
            if (price4Tr.value && item.Price < 4000000) return true;
            if (price48Tr.value && item.Price > 4000000 && item.Pirce < 8000000) return true;
            if (price812Tr.value && item.Price > 8000000 && item.Pirce < 12000000) return true;
            if (price1220Tr.value && item.Price > 12000000 && item.Pirce < 20000000) return true;
            if (price20Tr.value && item.Price > 20000000) return true;

            if (!price4Tr.value && !price48Tr.value && !price48Tr.value && !price1220Tr.value && !price20Tr.value) return true;

            return false;
        }
    });

    console.log(listClone);
    realEstate.splice(0, realEstate.length);
    Object.assign(realEstate, listClone);
    console.log(realEstate);
}
</script>

<style scoped>
@import url(./list-view.css);
</style>