import axios from './base';
import config from "../config";

export const login = async (user) => {
    const response = await axios.post(`${config.API}/Auth/login`, user);
    return response;
}

export const register = async (user) =>  {
    const response = await axios.post(`${config.API}/Auth/register`, user);
    return response;
}

export const getUserInfo = async () =>  {
    const response = await axios.get(`${config.API}/Auth/info`);
    return response;
}