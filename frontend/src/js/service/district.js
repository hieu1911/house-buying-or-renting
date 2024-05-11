import axios from './base';
import config from "../config";

export const getDistrictsByProvinceId = async (provinceId) => {
    const response = await axios.get(`${config.API}/District/by-province-id/${provinceId}`);
    return response;
}