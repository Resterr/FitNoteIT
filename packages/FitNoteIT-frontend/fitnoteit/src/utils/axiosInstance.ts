import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "https://fitnoteit.azurewebsites.net",
});

export default axiosInstance;
