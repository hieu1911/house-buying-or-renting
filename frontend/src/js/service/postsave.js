import axios from './base';
import config from "../config";

export const getByUserIdAndRealEstateId = async (userId, realEstateId) => {
    const response = await axios.get(`${config.API}/PostSave/user-realestate`, {
        params: {
            userId,
            realEstateId
        }
    });
    return response;
}