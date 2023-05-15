import { ELocalStorage } from "@/utility/enums/localStorage.enum";

export const getToken = () => localStorage.getItem(ELocalStorage.access_token);
export const getEmail = () => localStorage.getItem(ELocalStorage.user_email);
export const logout = () => localStorage.removeItem(ELocalStorage.access_token);
export const addToken = (token: string) =>
  localStorage.setItem(ELocalStorage.access_token, token);

export const addEmail = (email: string) =>
  localStorage.setItem(ELocalStorage.user_email, email);
