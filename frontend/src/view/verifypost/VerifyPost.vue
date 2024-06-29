<template>
    <div>
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
                                <span v-if="realEstate.Longtitude > 0 && realEstate.Latitude > 0" @click="showGoogleMap = true">
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
                    <div class="verify-btn-group">
                        <div>
                            <v-button
                                label="Duyệt bài"
                                type="primary"    
                                w100
                            ></v-button>
                        </div>
                        <div>
                            <v-button
                                label="Gỡ bỏ"
                                type="secondary"
                                w100
                            >
                            </v-button>
                        </div>
                    </div>
            </div>
            
            <div v-if="showGoogleMap" class="google-map-wrapper" :style="topStyle">
                <div class="google-map-content">
                    <div class="google-map-header">
                        <div></div>
                        <h5>{{ $t('post.choosePosition') }}</h5>
                        <v-icon type="close" @click="showGoogleMap = false"></v-icon>
                    </div>
                    <div class="google-map-center">
                        <GoogleMap
                            api-key="AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg"
                            style="width: 1200px; height: 500px"
                            :center="positionLatLng"
                            :zoom="15"
                            @click="handleClick"
                        >
                            <Marker :options="{ position: positionLatLng }" />
                        </GoogleMap>
                    </div>
                    <div class="google-map-footer">
                        <v-button
                            label="Đóng"
                            type="primary"
                            @click="showGoogleMap = false"
                        ></v-button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { onBeforeMount, ref, reactive, computed, onMounted, onUnmounted } from 'vue';
import { useRoute } from 'vue-router';
import { Carousel, Slide, Pagination, Navigation } from 'vue3-carousel';
import { GoogleMap, Marker } from 'vue3-google-map';
import 'vue3-carousel/dist/carousel.css';

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
const showGoogleMap = ref(false);
const topStyle = ref('top: 20px;');
const positionLatLng = reactive({
    lat: 0,
    lng: 0
})

onBeforeMount(async () => {
    features.splice(0, features.length);

    const realEstateId = route.params.id;
    const userInfo = (await getUserInfo()).data;
    Object.assign(user, userInfo)

    const realEstateCurr = await getRecord('RealEstate', realEstateId);
    Object.assign(realEstate, realEstateCurr.data);

    positionLatLng.lng = realEstate.Longtitude;
    positionLatLng.lat = realEstate.Latitude;

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

onMounted(() => {
    window.addEventListener("scroll", onScroll);
})

onUnmounted(() => {
    window.removeEventListener("scroll", onScroll)
})

function onScroll() {
    topStyle.value = `top: ${Math.floor(window.top.scrollY) + 350}px`
}

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
@import url(./verify-post.css);
</style>