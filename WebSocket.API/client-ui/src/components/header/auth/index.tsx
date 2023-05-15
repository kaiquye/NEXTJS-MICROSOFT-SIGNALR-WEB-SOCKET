"use client";
import styles from "./styles.module.css";
import React from "react";
import { useRouter } from "next/navigation";
export function HeaderAuth() {
  const { push } = useRouter();
  return (
    <header className={styles.main}>
      <div className={styles.logo}>
        <div style={{ paddingLeft: "10px" }}>
          <img
            width="50"
            src="https://upload.wikimedia.org/wikipedia/commons/7/7c/Logo-Pluang.png"
          />
        </div>
        <div style={{ paddingLeft: "10px" }}>
          <label>Sistema de chamada</label>
        </div>
      </div>
      <div className={styles.menu}>
        <button onClick={() => push("/person/login")}>PATIENTS</button>
        <button onClick={() => push("/person/register")}>
          REGISTER EMPLOYEE
        </button>
        <button>
          <a
            className={styles.registerNewPatient}
            target="_blank"
            href="/patient/dashboard"
          >
            REGISTER PATIENT
          </a>
        </button>
      </div>
    </header>
  );
}
