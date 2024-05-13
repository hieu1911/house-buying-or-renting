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
            this.userName = user.UserName;
            this.id = user.id;
        }
    }
});