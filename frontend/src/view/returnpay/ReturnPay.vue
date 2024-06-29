<template>
    <div class="return-pay">
        <h4>Kết quả thanh toán</h4>
        <div class="return-pay-wrapper">
            <img src="../../assets/image/target.png" />
            <h5>Thanh toán thành công!</h5>
            <p>Bạn đã trả phí bài đăng thành công, vui lòng đợi xác nhận từ quản trị viên.</p>
            <div style="width: 160px;">
                <v-button
                    w100
                    type="primary"
                    label="Về trang chủ"
                    @click="router.push('/')"
                ></v-button>
            </div>
        </div>
    </div>
</template>

<script setup>
import router from '@/js/router/router';


</script>

<style scoped>
@import url(./return-pay.css);
</style>