import ServerApi from "@/services/api";
import { ILoginUserIn } from "@/app/(public)/person/login/types/login.types";
import { AxiosError } from "axios";
import { addEmail, addToken } from "@/services/storage.service";
import { ISetError } from "@/contexts/types/context.types";

export const LoginUserService = async (
  data: ILoginUserIn,
  setError: ISetError
): Promise<string> => {
  try {
    const response = await ServerApi.post("/login", data);
    if (response.data.acess_token) {
      addToken(response.data?.acess_token);
      addEmail("admin@admin.com"); // trocar pelo e-mail
    }
    return "admin@admin.com";
  } catch (error: AxiosError | any) {
    if (error instanceof AxiosError) {
      if (
        error.response?.data?.message === "[Error] Invalid email or password"
      ) {
        return setError("E-mail ou senha invalido.");
      }
      return setError("Tente mais tarde.");
    }
    return setError("Tente mais tarde.");
  }
};
