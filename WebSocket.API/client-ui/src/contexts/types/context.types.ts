import React from "react";

export interface ContextProps {
  error?: string | null;
  setError: ISetError;
}

export type ISetError =
  | React.Dispatch<React.SetStateAction<string | null>>
  | any;

export interface IUserInfosContext {
  email: string;
  user_id: string;
  setEmail: ISet;
  setUserId: ISet;
}

export type ISet = React.Dispatch<React.SetStateAction<string | null>> | any;
