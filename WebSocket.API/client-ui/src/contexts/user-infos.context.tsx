"use client";
import React from "react";
import { IUserInfosContext } from "@/contexts/types/context.types";

export const UserInfosContext = React.createContext<IUserInfosContext>({
  user_id: "",
  email: "",
  setUserId: () => {},
  setEmail: () => {},
});

export const UserInfosContextProvider = function ({ children }) {
  const [email, setEmail] = React.useState<string | null>();
  const [user_id, setUserId] = React.useState<string | null>();

  return (
    <UserInfosContext.Provider value={{ email, user_id, setEmail, setUserId }}>
      {children}
    </UserInfosContext.Provider>
  );
};

export const useUserInfosContext = () => React.useContext(UserInfosContext);
