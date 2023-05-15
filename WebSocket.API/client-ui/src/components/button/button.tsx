import { PropsWithChildren } from "react";
import styles from "./styles.module.css";

export interface IProps {
  type?: string;
  color?: string;

  onClick?: any;
}

export function Button(props: PropsWithChildren<IProps>) {
  const action = props.onClick ?? function () {};

  return (
    <button
      onClick={action}
      style={{ backgroundColor: props.color ?? "#2868de" }}
      className={styles.main}
    >
      {props.children}
    </button>
  );
}
