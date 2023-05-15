import { UseFormRegister, UseFormReturn } from "react-hook-form";
import styles from "./style.module.css";
import React from "react";

export interface IPropsInput {
  label?: string;
  placeholder: string;
  value?: string;
  type?: string;
  errors: any;
  prop_error: string;
  onChange?: (value: any) => void;
  register: any;
}

export default function Input(props: IPropsInput) {
  return (
    <div className={styles.main}>
      <label>{props.label}</label>
      <input
        {...props.register}
        type={props.type}
        value={props.value}
        onChange={props.onChange}
        placeholder={props.placeholder}
      />
      <p className={styles.errorMsg}>
        {props.errors[props.prop_error]?.message}
      </p>
    </div>
  );
}
