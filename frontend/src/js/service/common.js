import axios from './base';
import config from "../config";

export const getAddress = async () => {
    const response = await axios.get(`${config.API}/Common/address`);
    return response;
}