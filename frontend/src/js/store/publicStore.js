import { defineStore } from "pinia";

export const publicStore = defineStore('publicStore', {
    state: () => {
        return {
            isAuthPage: false,
            isHomePage: false
        }
    },
    actions: {
        setAuthPageStatus(isAuthPage) {
            this.isAuthPage = isAuthPage;
        },

        setIsHomePage(isHomePage) {
            this.isHomePage = isHomePage;
        }
    }
});