"use client";
import React from "react";
import { useRouter } from "next/navigation";
import { getToken } from "@/services/storage.service";

export default function AuthenticatedRoute({ children }) {
  const router = useRouter();
  let auth;

  React.useEffect(() => {
    auth = getToken();
    if (!auth) {
      return router.push("person/login");
    }
  }, []);

  return auth ?? children;
}
