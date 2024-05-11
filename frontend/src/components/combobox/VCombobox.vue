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
// const isFocus = ref(false)
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

// function handleDownArrow() {
//     if (!this.disable) {
//         if (!this.showCombobox && !this.disable) {
//             this.showCombobox = true;
//         } else if (!this.itemTarget) {
//             this.itemTarget = this.listItem[0];
//         } else {
//             for (let i = 0; i < this.listItem.length; ++i) {
//                 if (this.listItem[i].value == this.itemTarget.value) {
//                     if (i == this.listItem.length - 1) {
//                         this.itemTarget = this.listItem[0];
//                     } else {
//                         this.itemTarget = this.listItem[i + 1];
//                     }
//                     break;
//                 }
//             }
//         }
//     }
// }

// function handleUpArrow() {
//     if (!this.disable) {
//         if (this.showCombobox) {
//             for (let i = 0; i < this.listItem.length; ++i) {
//                 if (this.listItem[i].value == this.itemTarget.value) {
//                     if (i == 0) {
//                         this.itemTarget = this.listItem[this.listItem.length - 1];
//                     } else {
//                         this.itemTarget = this.listItem[i - 1];
//                     }
//                     break;
//                 }
//             }
//         }
//     }
// }

// function handleEnter() {
//     if (!this.modelValue) return;

//     if (this.itemTarget) {
//         this.handleSelect(this.itemTarget);
//     } else {
//         this.handleSelect(this.options.find(option => this.modelValue == option.value)[0]);
//     }
//     this.listItem = this.options;
// }

function toggleCombobox() {
    if (!props.disable) {
        showCombobox.value = !showCombobox.value;
        console.log(showCombobox.value)
    }
}

// function focus() {
//     this.$refs.inputRef.focus();
// }

// watch(selectedTitle,(newValue, oldValue) => {
//     if (!this.enterPress) {
//         if (!this.showCombobox) {
//             this.showCombobox = true;
//         }
//         let newListItem = [];
//         this.options.forEach(option => {
//             if (option.title.toLowerCase().includes(newValue.toLowerCase().trim())) {
//                 newListItem.push(option);
//             }   
//         });
        
//         this.itemTarget = newListItem[0];
//         this.listItem = newListItem;
//     } else {
//         this.enterPress = false;
//     }
// })
</script>

<style scoped>
@import url(./v-combobox.css);
</style>