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
                    <input type="radio" id="buy" name="postType" value="1" v-model="postType">
                    <label for="html">Bán</label><br>
                </div>
                <div>
                    <input type="radio" id="rent" name="postType" value="0" v-model="postType">
                    <label for="html">Cho thuê</label><br>
                </div>
            </div>
        </div>
        <div class="filter-realestate-type">
            <h5>Loại hình bất động sản</h5>
            <div>
                <v-checkbox
                    label="Chung cư"
                ></v-checkbox>
                <v-checkbox
                    label="Nhà mặt đất"
                ></v-checkbox>
                <v-checkbox
                    label="Đất"
                ></v-checkbox>
                <v-checkbox v-if="postType == 0"
                    label="Phòng trọ"
                ></v-checkbox>
            </div>
        </div>
        <div class=filter-combobox>
            <h5>Khoảng giá</h5>
            <div>
                <v-combobox
                    label="Từ"
                ></v-combobox>
                <v-combobox
                    label="Đến"
                ></v-combobox>
            </div>
        </div>
        <div class="filter-combobox">
            <h5>Diện tích</h5>
            <div>
                <v-combobox
                    label="Từ"
                ></v-combobox>
                <v-combobox
                    label="Đến"
                ></v-combobox>
            </div>
        </div>
        <hr style="margin: 16px 0"/>
        <div class="filter-footer">
            <v-button type="secondary" label="Đóng" @click="$emit('close')"></v-button>
            <v-button type="primary" label="Lọc"></v-button>
        </div>
    </div>
</template>

<script setup>
import { ref, defineEmits } from 'vue';

defineEmits(['close'])
const postType = ref(1);
</script>

<style scoped>
@import url(./filter-card.css);
</style>