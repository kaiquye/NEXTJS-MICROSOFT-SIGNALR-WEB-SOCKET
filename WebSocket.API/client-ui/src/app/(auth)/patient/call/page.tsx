"use client";
import React from "react";
import styles from "./styles.module.css";
import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { IPatient } from "@/app/(auth)/patient/dashboard/types/patient-register-interface";

export default function Call() {
  const [patients, setPatients] = React.useState<Array<IPatient>>();
  const [conn, setConnection] = React.useState<HubConnection>(null);

  React.useEffect(() => {
    async function start() {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl("http://localhost:5244/patient", {
            skipNegotiation: true,
            transport: HttpTransportType.WebSockets,
          })
          .configureLogging(LogLevel.Information)
          .build();
        setConnection(connection);

        connection.on("success:findAll", (listPatients) => {
          setPatients(listPatients);
        });

        await connection.start();
        await connection.invoke("ListAllPatient");

        console.log("SignalR connection established.");
      } catch (error) {
        console.log(error);
        console.log("SignalR error connecting.");
      }
    }

    start();
  }, []);

  return (
    <section className={styles.main}>
      <h1>NEXT PATIENT</h1>
      <div className={styles.patientInfo}>
        <div>
          <label>Name:</label>
          <p>{patients !== undefined ? patients[0]?.name : ""}</p>
        </div>
        <div>
          {" "}
          <label>Room:</label>
          <p>{patients !== undefined ? patients[0]?.room : ""}</p>
        </div>
        <div>
          <label>Number:</label>
          <p>{patients !== undefined ? patients[0]?.number : ""}</p>
        </div>
      </div>
    </section>
  );
}
