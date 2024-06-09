<template>
    <div>
        <hr style="margin-top: 0"/>
        <div class="detail-wrapper">
            <div class="detail-top">
                <div class="detail-top-left">
                    <img :src="imgUrlLarge" class="img-large"/>
                    <div class="carousel-wrapper">
                        <Carousel :items-to-show="realEstate?.ImageUrls?.length > 6 ? 6 : realEstate?.ImageUrls?.length">
                            <Slide v-for="(image, idx) in realEstate?.ImageUrls" :key="idx">
                                <img :src="image?.Url" class="img-small" :class="{'img-active': image.Url == imgUrlLarge}" @click="imgUrlLarge = image.Url"/>
                            </Slide>
                            <template #addons>
                                <Navigation />
                                <Pagination />
                            </template>
                        </Carousel> 
                    </div>
                </div>
                <div class="detail-bottom" style="margin-top: 20px;">
                    <div>
                        <div class="info-title">
                            <h4>{{ realEstate.Title }}</h4>
                            <v-icon :type="isSaved ? 'saved' : 'not-save'" :desc="isSaved ? 'Bỏ lưu' : 'Lưu tin'" v-show="showOwnerInfo" @click="changePostSaveHistory"></v-icon>
                        </div>
                        <div class="info-price-area">
                            <span>{{ numberToWord(realEstate.Price) }}</span>
                            <span>-</span>
                            <span>{{ realEstate.Area }} m²</span>
                        </div>
                        <div class="info-address">
                            <v-icon type="location-detail" nowrap></v-icon>
                            <p>{{ realEstate.Address }}
                                <span v-if="realEstate.Longtitude > 0 && realEstate.Latitude > 0">
                                    Xem bản đồ
                                    <v-icon type="next" nowrap></v-icon>
                                </span>
                            </p>
                        </div>
                        <div class="info-time">
                            <v-icon type="time" nowrap></v-icon>
                            <p>Đăng {{ timePosted }}</p>
                        </div>
                    </div>
                    <hr style="margin: 28px 0"/>
                    <div class="info-feature">
                        <h4>Đặc điểm bất động sản</h4>
                        <div>
                            <div v-for="(item, index) in features" :key="index" class="feature-item">
                                <v-icon :type="item.icon"></v-icon>
                                <span>{{ item.title }}</span>
                            </div>
                        </div>
                    </div>
                    <hr style="margin: 28px 0"/>
                    <div class="info-desc">
                        <h4>Mô tả chi tiết</h4>
                        <p>{{ realEstate.Description }}</p>
                    </div>
                </div>
            </div>
            <div class="detail-top-right" v-if="showOwnerInfo">
                    <div class="detail-owner">
                        <div class="avt">{{ owner.FullName?.split(' ')[owner.FullName?.split(' ').length - 1][0] }}</div>
                        <div class="info">
                            <p>{{ owner.FullName }}</p>
                            <div>
                                <v-icon type="bag"></v-icon>
                                <p>{{ realEstate.IsPersonal ? 'Cá nhân' : 'Môi giới' }}</p>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="detail-contact">
                        <h4>Liên hệ với người đăng</h4>
                        <div>
                            <span v-for="(item, index) in contactMsg" :key="index">{{ item }}</span>
                        </div>
                    </div>
                    <div class="detail-send-msg">
                        <v-button
                            type="hasIconPrimary"
                            label="Nhắn tin cho người đăng"
                            icon="messager"
                            w100
                        ></v-button>
                    </div>
                    <div class="detail-more-contact-wrapper">
                        <div class="detail-more-contact">
                            <h4>Bạn cần tư vấn thêm?</h4>
                            <p>Để lại thông tin để người đăng tin liên hệ với bạn ngay.</p>
                            <div>
                                <v-input 
                                    w100
                                    placeholder="Họ và tên"
                                ></v-input>
                                <v-input 
                                    w100
                                    placeholder="Số điện thoại"
                                ></v-input>
                                <textarea
                                    placeholder="Lời nhắn"
                                ></textarea>
                                <v-button
                                    label="Gửi thông tin"
                                    type="primary"
                                ></v-button>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, ref, reactive, computed } from 'vue';
import { useRoute } from 'vue-router';
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel'
import 'vue3-carousel/dist/carousel.css'

import { getRecord, createRecord, deleteRecord } from '@/js/service/base';
import { getDetailRealEstate } from '@/js/service/realEstate';
import { numberToWord, convertMilliseconds } from '@/js/common/helper';
import { getUserInfo } from '@/js/service/auth';
import { getByUserIdAndRealEstateId } from '@/js/service/postsave';
import enums from '@/js/common/enum';
import common from '@/js/common/helper';
import router from '@/js/router/router';

const route = useRoute();
const timePosted = ref("");
const imgUrlLarge = ref("");
const realEstateType = ref(0);
const realEstate = reactive({
    ImageUrls: []
})
const apartment = reactive({});
const boardingHouse = reactive({});
const house = reactive({});
const land = reactive({});
const owner = reactive({});
const user = reactive({});
const features = reactive([]);
const showOwnerInfo = ref(true);
const currentRouteName = computed(() => route.path);
const isSaved = ref(false);
const saveHistoryInfo = reactive({});

const contactMsg = ["Bất động sản này còn không?", "Giá bán hiện tại?", "Diện tích sử dụng?",
    "Có sổ đỏ/sổ hồng không?", "Có nằm trong khu vực quy hoạch không?", "Hướng nhà là hướng nào?", "An ninh tốt không?",
    "Về cơ sở hạ tầng", "Hẹn xem trực tiếp", "Có chỗ để xe ô tô không?", "Có phí quản lý không?", "Thời gian bàn giao",
    "Các tiện ích công cộng"
]

onBeforeMount(async () => {
    features.splice(0, features.length);

    const realEstateId = route.params.id;
    const userInfo = (await getUserInfo()).data;
    Object.assign(user, userInfo)

    const realEstateCurr = await getRecord('RealEstate', realEstateId);
    Object.assign(realEstate, realEstateCurr.data);
    features.push.apply(features, [{ 
        icon: 'area',
        title: `Diện tích đất: ${realEstate.Area} m²`
    },
    {
        icon: 'price',
        title: `Giá: ${numberToWord(realEstate.Price)}`
    }]);

    if (userInfo) {
        const postSaved = await getByUserIdAndRealEstateId(userInfo.Id, realEstate.Id);
        if (postSaved.status != 204) {
            isSaved.value = true;
            Object.assign(saveHistoryInfo, postSaved.data);
        }
        else isSaved.value = false;
    }

    if (userInfo && userInfo.Id == realEstate.OwnerId) showOwnerInfo.value = false;
    else showOwnerInfo.value = true;

    getTime(realEstate.CreatedDate);

    const posterCurr = await getRecord('Auth', realEstate.OwnerId);
    Object.assign(owner, posterCurr.data);

    imgUrlLarge.value = realEstate?.ImageUrls[0]?.Url;
    
    realEstateType.value = realEstateCurr.data.RealEstateType;
    switch (realEstateType.value) {
        case enums.realEstateEnum.APARTMENT: 
            Object.assign(apartment, (await getDetailRealEstate('Apartment', realEstateId)).data);
            features.push.apply(features, [{
                icon: 'bed-room', 
                title: `Số phòng ngủ: ${apartment.NumberOfBedRoom}`
            },
            {
                icon: 'toilet', 
                title: `Số phòng vệ sinh: ${apartment.NumberOfToilet}`
            },
            {
                icon: 'floor', 
                title: `Tầng: ${apartment.Floor}`
            },
            {
                icon: 'funiture', 
                title: `Nội thất: ${apartment.Funiture}`
            },
            {
                icon: 'document', 
                title: `Giấy tờ pháp lý: ${apartment.LegalDocument ? 'Sổ hồng riếng' : 'Chưa có sổ'}`
            }])
            break;
        case enums.realEstateEnum.BOARDINGHOUSE: 
            Object.assign(boardingHouse, (await getDetailRealEstate('BoardingHouse', realEstateId)).data);
            features.push.apply(features, [{
                icon: 'funiture', 
                title: `Nội thất: ${boardingHouse.Funiture}`
            },
            {
                icon: 'bed-room', 
                title: `Loại hình: ${boardingHouse.SeftContained ? 'Khép kín' : 'Chung chủ'}`
            }])
            break;
        case enums.realEstateEnum.HOUSE: 
            Object.assign(house, (await getDetailRealEstate('House', realEstateId)).data);
            features.push.apply(features, [{
                icon: 'bed-room', 
                title: `Số phòng ngủ: ${house.NumberOfBedRoom}`
            },
            {
                icon: 'toilet', 
                title: `Số phòng vệ sinh: ${house.NumberOfToilet}`
            },
            {
                icon: 'floor', 
                title: `Số tầng: ${house.NumberOfFloor}`
            },
            {
                icon: 'funiture', 
                title: `Nội thất: ${house.Funiture}`
            },
            {
                icon: 'document', 
                title: `Giấy tờ pháp lý: ${house.RedBook ? 'Sổ đỏ' : 'Chưa có sổ'}`
            }])
            break;
        case enums.realEstateEnum.Land: 
            Object.assign(land, (await getDetailRealEstate('Land', realEstateId)).data);
            features.push.apply(features, [{
                icon: 'earth', 
                title: `Loại hình đất: ${land.LandType}`
            },
            {
                icon: 'document', 
                title: `Giấy tờ pháp lý: ${land.LegalDocument ? 'Đã có sổ' : 'Chưa có sổ'}`
            }])
            break;
        default: 
            break;
    }
})

function getTime(time) {
    const timeObj = convertMilliseconds(Date.now() - new Date(time));
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
    return timePosted.value;
}

async function changePostSaveHistory() {
    if (user.Id) {
        if (isSaved.value) {
            common.showDialog(enums.statusEnum.WARNING, 'Cảnh báo', ['Xóa bài đăng khỏi danh sách đã lưu?'], async () => {
                await deleteRecord('PostSave', saveHistoryInfo.Id);
                isSaved.value = false;
                common.showToastMessage('success', 'Thông báo', 'Bỏ lưu bài đăng thành công!')
            })
        } else {
            await createRecord('PostSave', {
                UserId: user.Id,
                RealEstateId: realEstate.Id
            })
            const postSaved = await getByUserIdAndRealEstateId(user.Id, realEstate.Id);
            Object.assign(saveHistoryInfo, postSaved.data);
            isSaved.value = true;
            common.showToastMessage('success', 'Thông báo', 'Lưu bài đăng thành công!')
        }
    } else {
        router.push(`/login?returnUrl=${currentRouteName.value.slice(1)}`)
    }
}

</script>

<style scoped>
@import url(./detail-view.css);
</style>