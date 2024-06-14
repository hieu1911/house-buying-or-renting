<template>
    <div class="filter-body">
        <div class="filter-header">
            <h4>Bộ lọc</h4>
            <div>
                <v-icon type="close" desc="Đóng" @click="$emit('close')"></v-icon>
            </div>
        </div>
        <hr style="margin: 16px 0;"/>
        <div class="filter-post-type">
            <h5>Loại bài đăng</h5>
            <div>
                <div>
                    <input type="radio" id="buy" name="postType" value="2" v-model="postType">
                    <label for="html">Bán</label><br>
                </div>
                <div>
                    <input type="radio" id="rent" name="postType" value="1" v-model="postType">
                    <label for="html">Cho thuê</label><br>
                </div>
            </div>
        </div>
        <div class="filter-realestate-type">
            <h5>Loại hình bất động sản</h5>
            <div>
                <v-checkbox
                    label="Chung cư"
                    v-model="apartmentChecked"
                ></v-checkbox>
                <v-checkbox
                    label="Nhà mặt đất"
                    v-model="hosueChecked"
                ></v-checkbox>
                <v-checkbox
                    label="Đất"
                    v-model="landChecked"
                ></v-checkbox>
                <v-checkbox v-if="postType == 1"
                    label="Phòng trọ"
                    v-model="boardingHouseChecked"
                ></v-checkbox>
            </div>
        </div>
        <div class=filter-combobox>
            <h5>Khoảng giá</h5>
            <div>
                <v-combobox
                    v-model="minPriceValue"
                    :options="minPrice"
                    label="Từ"
                ></v-combobox>
                <v-combobox
                    v-model="maxPriceValue"
                    :options="maxPrice"
                    label="Đến"
                ></v-combobox>
            </div>
        </div>
        <div class="filter-combobox">
            <h5>Diện tích</h5>
            <div>
                <v-combobox
                    v-model="minArea"
                    :options="minAreaOptions"
                    label="Từ"
                ></v-combobox>
                <v-combobox
                    v-model="maxArea"
                    :options="maxAreaOptions"
                    label="Đến"
                ></v-combobox>
            </div>
        </div>
        <hr style="margin: 16px 0"/>
        <div class="filter-footer">
            <v-button type="secondary" label="Đóng" @click="$emit('close')"></v-button>
            <v-button type="primary" label="Lọc" @click="handleFilter()"></v-button>
        </div>
    </div>
</template>

<script setup>
import { ref, defineEmits, reactive, watch, onBeforeMount } from 'vue';

defineEmits(['close']);

const postType = ref(2);
const apartmentChecked = ref(false);
const boardingHouseChecked = ref(false);
const hosueChecked = ref(false);
const landChecked = ref(false);
const minPriceValue = ref(0);
const maxPriceValue = ref(100000000000000);
const minArea = ref(0);
const maxArea = ref(100000);

const minPriceRent = [{
        value: 0,
        title: 'Không giới hạn'
    },
    {
        value: 4000000,
        title: '4 triệu'
    },
    {
        value: 8000000,
        title: '8 triệu'
    },
    {
        value: 12000000,
        title: '12 triệu'
    },
    {
        value: 20000000,
        title: '20 triệu'
    }
];

const maxPriceRent = [
    {
        value: 4000000,
        title: '4 triệu'
    },
    {
        value: 8000000,
        title: '8 triệu'
    },
    {
        value: 12000000,
        title: '12 triệu'
    },
    {
        value: 20000000,
        title: '20 triệu'
    },
    {
        value: 100000000000000,
        title: 'Không giới hạn'
    }
];

const minPriceBuy = [
    {
        value: 0,
        title: 'Không giới hạn'
    },
    {
        value: 1000000000,
        title: '1 tỷ'
    },
    {
        value: 3000000000,
        title: '3 tỷ'
    },
    {
        value: 6000000000,
        title: '6 tỷ'
    },
    {
        value: 10000000000,
        title: '10 tỷ'
    }
];

const maxPriceBuy = [
    {
        value: 1000000000,
        title: '1 tỷ'
    },
    {
        value: 3000000000,
        title: '3 tỷ'
    },
    {
        value: 6000000000,
        title: '6 tỷ'
    },
    {
        value: 10000000000,
        title: '10 tỷ'
    },
    {
        value: 100000000000000,
        title: 'Không giới hạn'
    }
];

const minAreaConst = [
    {
        value: 0,
        title: 'Không giới hạn'
    },
    {
        value: 30,
        title: '30m²'
    },
    {
        value: 60,
        title: '60m²'
    },
    {
        value: 90,
        title: '90m²'
    },
    {
        value: 120,
        title: '120m²'
    },
    {
        value: 150,
        title: '150m²'
    }
];

const maxAreaConst = [
    {
        value: 30,
        title: '30m²'
    },
    {
        value: 60,
        title: '60m²'
    },
    {
        value: 90,
        title: '90m²'
    },
    {
        value: 120,
        title: '120m²'
    },
    {
        value: 150,
        title: '150m²'
    },
    {
        value: 100000,
        title: 'Không giới hạn'
    }
];

const minPrice = reactive([]);
const maxPrice = reactive([]);
const minAreaOptions = reactive([]);
const maxAreaOptions = reactive([]);

onBeforeMount(() => {
    updatePrice();
    Object.assign(minAreaOptions, minAreaConst);
    Object.assign(maxAreaOptions, maxAreaConst);
})

watch(postType, () => {
    updatePrice();
    minPriceValue.value = 0;
    maxPriceValue.value = 100000000000000;
})

watch(minPriceValue, () => {
    maxPrice.splice(0, maxPrice.length);
    console.log(minPriceValue.value);
    if (postType.value == 1) {
        maxPriceRent.forEach(p => {
            if (p.value > minPriceValue.value) maxPrice.push(p);
        })
    } else {
        maxPriceBuy.forEach(p => {
            if (p.value > minPriceValue.value) maxPrice.push(p);
        })
    }
})

watch(maxPriceValue, () => {
    minPrice.splice(0, maxPrice.length);
    if (postType.value == 1) {
        minPriceRent.forEach(p => {
            if (p.value < maxPriceValue.value) minPrice.push(p);
        })
    } else {
        maxPriceBuy.forEach(p => {
            if (p.value < maxPriceValue.value) minPrice.push(p);
        })
    }
})

watch(minArea, () => {
    maxAreaOptions.splice(0, maxAreaOptions.length);
    maxAreaConst.forEach(a => {
        if (a.value > minArea.value) maxAreaOptions.push(a);
    })
})

watch(maxArea, () => {
    minAreaOptions.splice(0, minAreaOptions.length);
    minAreaConst.forEach(a => {
        if (a.value < maxArea.value) minAreaOptions.push(a);
    })
})

function handleFilter() {
    let realEstateType = '';
    if (apartmentChecked.value) realEstateType += 3;
    if (hosueChecked.value) realEstateType += 1;
    if (boardingHouseChecked.value) realEstateType += 2;
    if (landChecked.value) realEstateType += 4;

    window.location.href = `/list?postType=${postType.value}&realEstateType=${realEstateType}&minPrice=${minPriceValue.value}&maxPrice=${maxPriceValue.value}&minArea=${minArea.value}&maxArea=${maxArea.value}`;
}

function updatePrice() {
    if (postType.value == 1) {
        Object.assign(minPrice, minPriceRent);
        Object.assign(maxPrice, maxPriceRent);
    } else {
        Object.assign(minPrice, minPriceBuy);
        Object.assign(maxPrice, maxPriceBuy);
    }
}
</script>

<style scoped>
@import url(./filter-card.css);
</style>