import { defineStore } from "pinia";

export const publicStore = defineStore('publicStore', {
    state: () => {
        return {
            isAuthPage: false,
            isHomePage: false,
            isRenting: false
        }
    },
    actions: {
        setAuthPageStatus(isAuthPage) {
            this.isAuthPage = isAuthPage;
        },

        setIsHomePage(isHomePage) {
            this.isHomePage = isHomePage;
        },

        setIsRenting(isRenting) {
            this.isRenting = isRenting;
        }
    }
});