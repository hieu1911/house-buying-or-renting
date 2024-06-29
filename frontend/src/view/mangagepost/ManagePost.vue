<template>
  <div>
        <div class="list-wrapper">
            <div class="list-content">
                <div v-for="(item, idx) in realEstate" :key="idx" class="item-card">
                    <div @click="router.push(`/verify-post/${item.Id}`)">
                        <img :src="item.ImageUrls[0].Url"/>
                    </div>
                    <div class="item-wrapper">
                        <p class="item-title" @click="router.push(`/verify-post/${item.Id}`)">{{ item.Title }}</p>
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
import { onBeforeMount, reactive } from 'vue';
import { getUserInfo } from '@/js/service/auth';
import { getRecords } from '@/js/service/base';

import { numberToWord } from '@/js/common/helper';
import router from '@/js/router/router';

const realEstate= reactive([]);

onBeforeMount(async () => {
    const user = await getUserInfo();
    console.log(user.data);
    if (!user.data || user.data.Role != 1) {
        router.push('/login?returnUrl=manage-user');
        return;
    }

    realEstate.splice(0, realEstate.length);
    const res = await getRecords('RealEstate');
    res.data.forEach(r => {
        if (r.IsAccepted == 0) realEstate.push(r);
    });
});
</script>

<style scoped>
@import url(./manage-post.css);
</style>