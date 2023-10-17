import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://fitnoteitdev.azurewebsites.net",
});

export default axiosInstance;
