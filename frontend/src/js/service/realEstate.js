import axios from './base';
import config from "../config";

export const getForCarousel = async () => {
    const response = await axios.get(`${config.API}/RealEstate/carousel`);
    return response;
}