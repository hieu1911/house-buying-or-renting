import axios from "axios";
import common from "../common/helper";
import config from "../config";

axios.defaults.baseURL = config.API;
axios.defaults.withCredentials = true;

axios.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        common.handleError(error);
        return false;
    }
);

axios.interceptors.request.use(
    config => {
      config.headers['Content-Type'] = 'application/json';
      return config;
    },
    error => {
        common.handleError(error);
        return false;
    }
);

export default axios;

export const getRecordsLimit = async (object, limit, offset, sortType = null, sortField = null) => {
    let params;
    if (limit > 0 && offset >= 0) {
        params = {
            limit,
            offset,
            sortType,
            sortField
        }
    }
    const response = await getRecords(object, params);
    
    return response;
}

export const getRecords = async (object, params = {}) => {
    const response = await axios.get(`${object}`, {
        params
    });
    
    return response;
}

export const getByListIds = async (object, ids) => {
    const response = await axios.post(`${object}/list-ids`, ids);
    return response;
}

export const createRecord = async (object, record) => {
    const response = await axios.post(`${config.API}/${object}`, record);
    return response;
}

export const updateRecord = async (object, id, record) => {
    const response = await axios.put(`${config.API}/${object}/${id}`, record);
    return response;
}

export const deleteRecord = async (object, id) => {
    const response = await axios.delete(`${config.API}/${object}/${id}`);
    return response;
}

export const deleteMultiRecords = async (object, ids) => {
    const response = await axios.delete(`${config.API}/${object}`, {
        data: ids
    });
    return response;
}