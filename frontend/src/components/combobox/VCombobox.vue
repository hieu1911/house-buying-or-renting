<template>
    <div ref="wref">
        <label 
            v-if="label" 
            class="combobox__title" 
            :class="{'combobox--required': required}"
            @click="showCombobox=true;"
        >
            {{ label }}
        </label>
        <div class="combobox__body"
            :class="{'combobox--small': comboboxSmall}"
            @keydown.arrow-down="handleDownArrow"
            @keydown.arrow-up="handleUpArrow"
            @keydown.enter="handleEnter"
        >
            <div>
                <v-input
                    ref="inputRef"
                    combobox
                    :w100="!comboboxSmall"
                    hasIcon
                    iconType="combobox-down"
                    iconAfter
                    v-model="selectedTitle"
                    :iconFilter="iconFilter"
                    :small="comboboxSmall"
                    :readonly="readonly || selectOnly"
                    :disable="disable"
                    :placeholder="placeholder"
                    @clickIcon="toggleCombobox"
                    @focus="showCombobox = true"
                ></v-input>
            </div>
            <div 
                v-show="showCombobox" 
                class="combobox__result" 
                :class="{
                    'combobox__result-in-top': showInTop, 
                    'combobox__result-in-bottom': !showInTop, 
                    'combobox__result--small': comboboxSmall
                }"
                
            >
                <div v-if="options?.length > 0">
                    <div 
                        v-for="(item) in options" 
                        class="combobox__item"
                        :key="item?.value" 
                        :value="item?.value" 
                        :class="{'combobox__item--selected': item?.value == selectedValue}"
                        @click="handleSelect(item)"
                    >
                        {{ item?.title + (item?.subTitle ? ' - ' + item.subTitle : '')}}
                        <v-icon v-if="item?.value == selectedValue" type="tick"></v-icon>
                    </div>
                </div>
                <div v-else class="combobox__item combobox-no-data">
                    <!-- {{ resource.public.noData }} -->
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { defineProps, defineEmits, ref, onMounted } from 'vue';
import { useOnClickOutside } from '@/js/composable/click-outside';
// import { publicStore } from '@/js/store/public';

const props = defineProps({
    'label': String,
    'errorDesc': String,
    'errorDescDup': String,
    'required': Boolean,
    'options': Array,
    'showInTop': Boolean,
    'comboboxSmall': Boolean,
    'modelValue': [Number, String],
    'titleGray': Boolean,
    'disable': Boolean,
    'readonly': Boolean,
    'placeholder': String,
    'iconFilter': Boolean,
    'wref': String,
    'selectOnly': Boolean
});

const emit = defineEmits(['update:modelValue', 'selectedItem']);

const showCombobox = ref(false)
const selectedValue = ref(props.modelValue)
const selectedTitle = ref(props.options?.find(option => props.modelValue == option.value)?.title)
const wref = ref(null);

onMounted(() => {
    if (wref.value) {
        useOnClickOutside(wref.value, () => {
            showCombobox.value = false;
        });
    }
})
  
function handleSelect(item) {
    selectedValue.value = item.value;
    selectedTitle.value = props.options.find(option => item.value == option.value)?.title;
    emit('update:modelValue', item.value);
    emit('selectedItem', item)
    showCombobox.value = false;
}

function toggleCombobox() {
    if (!props.disable) {
        showCombobox.value = !showCombobox.value;
    }
}
</script>

<style scoped>
@import url(./v-combobox.css);
</style>