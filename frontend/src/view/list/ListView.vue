<template>
    <div>
        <hr/>
        <div class="list-wrapper">
            <div class="filter">
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
import { onBeforeMount, reactive, ref } from 'vue';
import { useRoute } from 'vue-router';
import router from '@/js/router/router';

import { numberToWord } from '@/js/common/helper';
import { getListRealEstate } from '@/js/service/realEstate';

const realEstate= reactive([]);
const isBuy = ref(true)

onBeforeMount(async () => {
    const router = useRoute();
    const provinceId = router.query.provinceId;

    const res = await getListRealEstate(provinceId, 10, 1);
    res.data.forEach(r => realEstate.push(r));
});
</script>

<style scoped>
@import url(./list-view.css);
</style>