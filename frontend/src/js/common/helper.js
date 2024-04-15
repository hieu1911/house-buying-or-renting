import emitter from 'tiny-emitter/instance';

// import router from '../router/router';

const common = {
    showToastMessage(type, title, content) {
        emitter.emit('showToastMessage', type, title, content);
    },

    showDialog(type, title, desc, action, del=false) {
        emitter.emit('showDialog', type, title, desc, action, del);
    },
    
    handleError(err) {
        console.log(err);
        this.showLoading(false);

        // const res = resource[publicStore().langCode];
        //const errorData = err.response?.data;
        // errors = errorData?.Errors;
        // let dialogType = MISAEnum.statusType.WARNING;
        // let dialogTitle = resource.VN.dialog.warning;
        // let dialogContent= '';
        // let action = null;
        // let confirmLabel = res.button.confirm

        let code = err.response?.status;
        if (!code) {
            code = err.response?.data?.ErrorCode
        }

        switch(code) {
            case 400: 
                //if (errors) {
                    // dialogTitle = resource[MISAEnum.langCode.VN].emulation.dialog.warningData.title;
                    // dialogContent = [];
                    // errors.forEach(error => dialogContent.push(error.ErrorMessage));
                    // dialogContent.reverse();
                //} else {
                    //dialogContent = [resource.VN.public.errorMsg500];
                //}
                break;
            case 401: 
                // dialogTitle = res.dialog.warning;
                // dialogContent = [res.dialog.sessionEnd];
                // action = () => router.push('/login');
                // confirmLabel = res.dialog.reLogin;
                // publicStore().isLogin = false;
                break;
            case 404: 
                return;
            case 409: 
                //dialogTitle = res.dialog.warning;
                //dialogContent = [err.response.data.UserMessage];
                break;
            case 412: 
                throw err;
            case 500: 
                //dialogContent = [resource.VN.public.errorMsg500];
                break;
            default:
               //dialogType = MISAEnum.statusType.ERROR;
                //dialogContent = [resource.VN.public.errorMsg500];
                break;
        }

        // this.showDialog(
        //     dialogType,
        //     dialogTitle,
        //     dialogContent,
        //     action,
        //     confirmLabel
        // );
    },
    
    showLoading(showed) {
        emitter.emit('showLoading', showed)
    }
};

export default common;