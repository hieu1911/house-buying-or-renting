import axios from './base';
import config from "../config";

export const getByUser = async (senderId, receiverId) => {
    const response = await axios.get(`${config.API}/Message/user`, {
        params: {
            senderId,
            receiverId
        }
    });
    return response;
}

export const getContacts = async (id) => {
    const response = await axios.get(`${config.API}/Message/contact/${id}`);
    return response;
}