import { defineStore } from "pinia";

export const publicStore = defineStore('publicStore', {
    state: () => {
        return {
            isAuthPage: false
        }
    },
    actions: {
        setAuthPageStatus(isAuthPage) {
            this.isAuthPage = isAuthPage;
        }
    }
});