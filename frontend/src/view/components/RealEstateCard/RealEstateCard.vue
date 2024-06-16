<template>
    <div>
        <img :src="imgUrl" class="img-thumbnail" @click="viewDetail()"/>
        <p class="card-title" @click="viewDetail()">{{ realEstate.Title }}</p>
        <div class="card-info">
            <span class="card-area">{{ realEstate.Area }} m²</span>
            <span class="card-mid">-</span>
            <span class="card-price">{{ numberToWord(realEstate.Price) }}</span>
        </div>
        <div class="card-info">
            <span class="card-time">{{ timePosted }}</span>
            <div>
                <v-icon type="pin" nowrap></v-icon>
                <span class="card-address">{{ realEstate.District.Province.Name }}</span>
            </div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, defineProps, ref } from 'vue';
import { numberToWord, convertMilliseconds } from '@/js/common/helper';
import router from '@/js/router/router';

const imgUrl = ref('');
const timePosted = ref('');

const props = defineProps({
    'realEstate': Object
})

onBeforeMount(() => {
    imgUrl.value = props.realEstate.ImageUrls[0]?.Url;

    const createdTime = new Date(props.realEstate.CreatedDate);
    const timeObj = convertMilliseconds(Date.now() - createdTime);
    timePosted.value = '';
    if (timeObj.days > 0) {
        timePosted.value += `${timeObj.days} ngày `;
    }

    if (timeObj.hours > 0) {
        timePosted.value += `${timeObj.hours} giờ `;
    }

    if (timeObj.days == 0 && timeObj.minutes > 0) {
        timePosted.value += `${timeObj.minutes} phút `;
    }

    if (timeObj.days == 0 && timeObj.hours == 0 && timeObj.minutes == 0) {
        timePosted.value += `${timeObj.seconds} giây `;
    }

    timePosted.value += 'trước';
});

function viewDetail() {
    router.push(`/detail/${props.realEstate.Id}`)
}

</script>

<style scoped>
@import url(./real-estate-card.css);
</style>