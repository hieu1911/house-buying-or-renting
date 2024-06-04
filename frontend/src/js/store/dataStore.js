import { defineStore } from "pinia";

export const dataStore = defineStore('dataStore', {
    state: () => {
        return {
            listRealEstate: null
        }
    },
    actions: {
        setListRealEstate(listRealEstate) {
            this.listRealEstate = listRealEstate;
        }
    }
});