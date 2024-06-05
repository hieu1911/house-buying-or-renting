import axios from './base';
import config from "../config";

export const getForCarousel = async () => {
    const response = await axios.get(`${config.API}/RealEstate/carousel`);
    return response;
}

export const getListRealEstate = async (provinceId, pageSize, pageNumber) => {
    const response = await axios.get(`${config.API}/RealEstate/list`, {
        params: {
            provinceId,
            pageSize,
            pageNumber
        }
    });
    return response;
}

export const getDetailRealEstate = async (type, realEstateId) => {
    const response = await axios.get(`${config.API}/${type}/RealEstate/${realEstateId}`);
    return response;
}