import ServerApi from "@/services/api";
import { ILoginUserIn } from "@/app/(public)/person/login/types/login.types";
import { AxiosError } from "axios";
import { ISetError } from "@/contexts/types/context.types";

export const RegisterUserService = async (
  data: ILoginUserIn,
  setError: ISetError
) => {
  try {
    await ServerApi.post("/api/v1/person", data);
    return true;
  } catch (error: AxiosError | any) {
    if (
      error.response.data.message ===
      "[Error] The email entered is already registered."
    ) {
      return setError("[Erro] O e-mail digitado já está cadastrado.");
    }
    return setError("Tente mais tarde.");
  }
};
