<template>
    <div class="post-wrapper">
        <h4 class="post-header">{{ $t('post.header') }}</h4>
        <hr/>
        <div>
            <h4 class="post-row-title">{{ $t('post.poster') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="personal" name="poster" value="0">
                    <label for="html">{{ $t('post.personal') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="agency" name="poster" value="1">
                    <label for="html">{{ $t('post.agency') }}</label><br>
                </div>
            </div>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.realestateType')"
                    :options="realestateOptions"
                    v-model="realestateType"
                    required
                    selectOnly
                ></v-combobox>
            </div>
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.postType')"
                    :options="postOptions"
                    required
                    selectOnly
                ></v-combobox>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.realestateName')"
            ></v-input>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.chooseProvince')"
                    :options="provincesOptions"
                    required
                    selectOnly
                ></v-combobox>
            </div>
            <div style="width: 47%">
                <v-combobox
                    :label="$t('post.chooseDistrict')"
                    :options="provincesOptions"
                    required
                    selectOnly
                ></v-combobox>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.addressDetail')"
            ></v-input>
        </div>
        <div class="post-row">
            <div style="width: 47%">
                <v-input
                    w100
                    :label="$t('post.area')"
                    required
                ></v-input>
            </div>
            <div style="width: 47%">
                <v-input
                    w100
                    :label="$t('post.price')"
                    required
                ></v-input>
            </div>
        </div>
        <div class="post-row">
            <v-input
                w100
                :label="$t('post.title')"
            ></v-input>
        </div>
        <!-- <div>
            <v-input
                w100
                :label="$t('post.feature')"
            ></v-input>
        </div> -->
        <div v-if="realestateType == realestateEnum.HOUSE">
            <div class="post-row">
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfBedRoom')"
                        required
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfToilet')"
                        required
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfFloor')"
                        required
                    ></v-input>
                </div>
            </div>
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.redBook') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="hasRedBook" name="redBook" value="0">
                    <label for="html">{{ $t('post.hasRedBook') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="agenoRedBookncy" name="redBook" value="1">
                    <label for="html">{{ $t('post.noRedBook') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.BOARDINGHOUSE">
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.selfContained') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="private" name="selfContained" value="0">
                    <label for="html">{{ $t('post.private') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="shard" name="selfContained" value="1">
                    <label for="html">{{ $t('post.shard') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.APARTMENT">
            <div class="post-row">
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfBedRoom')"
                        required
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.numberOfToilet')"
                        required
                    ></v-input>
                </div>
                <div style="width: 30%">
                    <v-input
                        w100
                        :label="$t('post.floor')"
                        required
                    ></v-input>
                </div>
            </div>
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.funiture')"
                    required
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.legalDocument') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="noHave" name="legalDocument" value="0">
                    <label for="html">{{ $t('post.noHave') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="pinkBook" name="legalDocument" value="1">
                    <label for="html">{{ $t('post.pinkBook') }}</label><br>
                </div>
            </div>
        </div>
        <div v-else-if="realestateType == realestateEnum.LAND">
            <div class="post-row">
                <v-input
                    w100
                    :label="$t('post.landType')"
                    required
                ></v-input>
            </div>
            <h4 class="post-row-title">{{ $t('post.legalDocument') }}</h4>
            <div class="radio-group">
                <div>
                    <input type="radio" id="noHave" name="legalDocumentLand" value="0">
                    <label for="html">{{ $t('post.noHave') }}</label><br>
                </div>
                <div>
                    <input type="radio" id="have" name="legalDocumentLand" value="1">
                    <label for="html">{{ $t('post.have') }}</label><br>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, inject } from 'vue';

const provincesOptions = [
    {title: 'bac', value: 0},
    {title: 'abc', value: 1},
];

const realestateEnum = inject('$enums').realEstateEnum;
const realestateType = ref(-1)
const realestateOptions = [
    {
        title: "Nhà mặt đất",
        value: realestateEnum.HOUSE
    },
    {
        title: "Phòng trọ",
        value: realestateEnum.BOARDINGHOUSE
    },
    {
        title: "Chung cư",
        value: realestateEnum.APARTMENT
    },
    {
        title: "Đất",
        value: realestateEnum.LAND
    }
]

const postEnum = inject('$enums').postEnum;
const postOptions = [
    {
        title: "Buôn bán",
        value: postEnum.BUY
    },
    {
        title: "Cho thuê",
        value: postEnum.RENT
    }
]
</script>

<style scoped>
@import url(./post-view.css);
</style>