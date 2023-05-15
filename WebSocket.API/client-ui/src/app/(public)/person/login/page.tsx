"use client";
import React from "react";
import { useRouter } from "next/navigation";
import styles from "./styles.module.css";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "@/components/input/input";
import { Button } from "@/components/button/button";
import Link from "next/link";
import { LoginUserService } from "@/app/(public)/person/login/services/login-user.service";
import ErrorComponent from "@/components/error/error.component";
import { ILoginUserIn } from "@/app/(public)/person/login/types/login.types";
import { useGlobalError } from "@/contexts/globalError.context";
import { UserInfosContext } from "@/contexts/user-infos.context";

const schema = yup
  .object({
    email: yup.string().required(),
    password: yup.string().required(),
  })
  .required();
type FormData = yup.InferType<typeof schema>;

export default function Login() {
  const { setEmail } = React.useContext(UserInfosContext);
  const { setError } = useGlobalError();
  const router = useRouter();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
  });
  const onSubmit = async (data: ILoginUserIn) => {
    const logged = await LoginUserService(data, setError);
    if (logged) return router.push("/patient/call");
  };

  return (
    <section className={styles.main}>
      <ErrorComponent />
      <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
        <img
          width={50}
          src="https://upload.wikimedia.org/wikipedia/commons/9/99/Sample_User_Icon.png"
        />
        <h1 className={styles.title}>Login</h1>
        <div className={styles.input}>
          <Input
            label="E-mail"
            type="string"
            register={register("email")}
            errors={errors}
            prop_error="email"
            placeholder="E-mail"
          />
        </div>
        <Input
          label="Password"
          type="password"
          register={register("password")}
          errors={errors}
          prop_error="password"
          placeholder="password"
        />
        <div className={styles.Menu}>
          <Button type="submit">Login</Button>
          <Link href="/person/register">Register</Link>
        </div>
      </form>
    </section>
  );
}
