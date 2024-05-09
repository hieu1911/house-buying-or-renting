import authResource from './view/auth';
import iconResource from './components/icon';
import buttonResource from './components/button';
import headerResource from './layout/header';
import homeResource from './view/home';
import postResource from './view/post';

const messages = {
    vi: {
        auth: authResource.vi,
        icon: iconResource.vi,
        button: buttonResource.vi,
        header: headerResource.vi,
        home: homeResource.vi,
        post: postResource.vi
    },
    en: {
        auth: authResource.en,
        icon: iconResource.en,
        button: buttonResource.en,
        header: headerResource.en,
        home: homeResource.en,
        post: postResource.en
    }
}

export default messages