import emitter from 'tiny-emitter/instance';

import enums from './enum';
import router from '../router/router';
import { publicStore } from '../store/publicStore';

export const formatNumber = (num) => {
    if (!num && num !== 0) return;
    let n = num;
    let str = n.toLocaleString('en-US');
    str = str.replace(/\./, ',');
    str = str.replace(/,/g, '.');
    return str;
}

export const deFormatNumber = (numberString) => {
    let number;

    if (numberString === null || numberString === undefined) return 0;
    number = numberString.toString().replace(/\./g, "");
    number = parseInt(number);

    return number;
}

export const numberToWord = (number) => {
    if (number > 1000000000) {
        const n = number / 1000000000;
        if (n % 1 == 0) return `${number / 1000000000} tỷ`;
        else return `${(number / 1000000000).toFixed(1)} tỷ`;
    } else {
        const n = number / 1000000;
        if (n % 1 == 0) return `${number / 1000000} triệu`;
        else return `${(number / 1000000).toFixed(1)} triệu`
    }
}

export const convertMilliseconds = (ms) => {
    let seconds = Math.floor(ms / 1000);
    let minutes = Math.floor(seconds / 60);
    let hours = Math.floor(minutes / 60);
    let days = Math.floor(hours / 24);

    seconds = seconds % 60;
    minutes = minutes % 60;
    hours = hours % 24;

    return {
        days: days,
        hours: hours,
        minutes: minutes,
        seconds: seconds
    };
}

const common = {
    showToastMessage(type, title, content) {
        emitter.emit('showToastMessage', type, title, content);
    },

    showDialog(type, title, desc, action) {
        emitter.emit('showDialog', type, title, desc, action);
    },
    
    handleError(err) {
        console.log(err);
        this.showLoading(false);

        const errorData = err.response?.data;
        let errors = errorData?.Errors;
        let dialogType = enums.statusEnum.WARNING;
        let dialogTitle = "Cảnh báo";
        let dialogContent= '';
        let action = null;

        let code = err.response?.status;
        if (!code) {
            code = err.response?.data?.ErrorCode
        }

        switch(code) {
            case 400: 
                if (errors) {
                    dialogContent = [];
                    errors.forEach(error => dialogContent.push(error.ErrorMessage));
                    dialogContent.reverse();
                } else {
                    dialogContent = ["Yêu cầu API không hợp lệ hoặc có định dạng không chính xác."];
                }
                break;
            case 401: 
                dialogContent = ["Cần đăng nhập để thực hiện dịch vụ này."];
                action = () => router.push('/login');
                publicStore().isAuthPage = true;
                break;
            case 404: 
                return;
            case 409: 
                dialogContent = ["Dữ liệu không hợp lệ"];
                break;
            case 412: 
                throw err;
            case 500: 
                dialogContent = ["Có lỗi từ phía server."];
                break;
            default:
                dialogContent = ["Có lỗi từ phía server."];
                break;
        }

        this.showDialog(
            dialogType,
            dialogTitle,
            dialogContent,
            action
        );
    },
    
    showLoading(showed) {
        emitter.emit('showLoading', showed)
    }
};

export default common;