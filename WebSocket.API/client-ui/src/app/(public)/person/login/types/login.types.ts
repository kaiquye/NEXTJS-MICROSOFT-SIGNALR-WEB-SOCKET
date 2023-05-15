import React from "react";

export interface ILoginUserIn {
  email: string;
  password: string;
}

export interface ILoginUserOut {
  access_token: string;
  error: boolean;
}
