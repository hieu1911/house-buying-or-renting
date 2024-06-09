import { defineStore } from "pinia";

export const userStore = defineStore('userStore', {
    state: () => {
        return {
            userName: '',
            id: ''
        }
    },
    actions: {
        setUser(user) {
            this.userName = user.userName;
            this.id = user.id;
        }
    }
});