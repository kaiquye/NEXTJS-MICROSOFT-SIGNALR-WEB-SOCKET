import axios, { AxiosInstance } from "axios";
import { logout } from "@/services/storage.service";

function ServerApi(): AxiosInstance {
  axios.interceptors.response.use(
    (response) => response,
    function (error) {
      if (error.response.status === 401) {
        logout();
        return window.location.replace("/person/login");
      }
      return alert(error);
    }
  );

  return axios.create({
    baseURL: "http://localhost:5244/",
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
      "Cache-Control": "no-cache",
    },
  });
}

export default ServerApi();
