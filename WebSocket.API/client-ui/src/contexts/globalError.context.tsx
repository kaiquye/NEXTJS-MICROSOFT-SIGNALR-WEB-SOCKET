"use client";
import React from "react";
import { ContextProps } from "@/contexts/types/context.types";

export const GlobalErrorContext = React.createContext<ContextProps>({
  error: null,
  setError: () => null,
});

export const GlobalErrorContextProvider = function ({ children }) {
  const [error, setError] = React.useState<string | null>();

  return (
    <GlobalErrorContext.Provider value={{ error, setError }}>
      {children}
    </GlobalErrorContext.Provider>
  );
};

export const useGlobalError = () => React.useContext(GlobalErrorContext);
