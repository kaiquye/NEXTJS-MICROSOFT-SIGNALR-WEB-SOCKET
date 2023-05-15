"use client";
import React from "react";
import styles from "./styles.module.css";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import Input from "@/components/input/input";
import { Button } from "@/components/button/button";
import Link from "next/link";
import { useGlobalError } from "@/contexts/globalError.context";
import { RegisterUserService } from "@/app/(public)/person/register/services/register-user.service";
import { IRegisterUserIn } from "@/app/(public)/person/register/types/register.types";
import ErrorComponent from "@/components/error/error.component";

const schema = yup
  .object({
    name: yup.string().required(),
    email: yup.string().required(),
    password: yup.string().required(),
  })
  .required();
type FormData = yup.InferType<typeof schema>;

export default function Register() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
  });

  const { setError } = useGlobalError();
  const router = useRouter();

  const onSubmit = async (data: IRegisterUserIn) => {
    const created = await RegisterUserService(data, setError);
    if (created) return router.push("person/login");
  };

  const [name, setName] = React.useState<string>();
  const [email, setEmail] = React.useState<string>();
  const [password, setPassword] = React.useState<string>();

  return (
    <section className={styles.main}>
      <ErrorComponent />
      <form className={styles.form} onSubmit={handleSubmit(onSubmit)}>
        <img
          width={50}
          src="https://upload.wikimedia.org/wikipedia/commons/9/99/Sample_User_Icon.png"
        />
        <h1 className={styles.title}>Register</h1>
        <Input
          label="Name"
          register={register("name")}
          onChange={setName}
          placeholder="name"
          errors={errors}
          prop_error="name"
        />
        <Input
          label="Email"
          type="email"
          register={register("email")}
          errors={errors}
          prop_error="email"
          onChange={setEmail}
          placeholder="email"
        />
        <Input
          label="Password"
          type="password"
          register={register("password")}
          errors={errors}
          prop_error="password"
          onChange={setPassword}
          placeholder="password"
        />
        <div className={styles.Menu}>
          <Button type="submit">REGISTER</Button>
          <Link href="/person/login">Back</Link>
        </div>
      </form>
    </section>
  );
}
