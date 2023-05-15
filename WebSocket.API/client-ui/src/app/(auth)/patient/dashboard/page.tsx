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
import Input from "@/components/input/input";
import * as yup from "yup";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import ErrorComponent from "@/components/error/error.component";
import { useGlobalError } from "@/contexts/globalError.context";
import { Button } from "@/components/button/button";

const schema = yup
  .object({
    name: yup.string().required(),
    number: yup.string().required(),
    room: yup.string().required(),
  })
  .required();

type FormData = yup.InferType<typeof schema>;

export default function Dashboard() {
  const { setError } = useGlobalError();

  const [patients, setPatients] = React.useState<Array<IPatient>>();
  const [conn, setConnection] = React.useState<HubConnection>(null);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
  });

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

        connection.on("error:AddPatient", (code) => {
          if (code === "PT:CD409") {
            setError("Patient not found");
          }
        });

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

  const callNextPatient = async () => {
    try {
      await conn.invoke("NextPatient");
    } catch (exception) {
      console.log(exception);
    }
  };
  const remove = async (patient_name: string) => {
    try {
      if (patients) {
        await conn.invoke("removePatient", {
          name: patient_name,
          room: null,
          number: null,
        });
      }
    } catch (exception) {
      console.log(exception);
    }
  };
  const registerNewPatient = async (patient: IPatient) => {
    try {
      await conn.invoke("AddPatient", patient);
    } catch (exception) {
      console.log(exception);
    }
  };

  return (
    <div>
      <ErrorComponent />
      <section className={styles.main}>
        <div className={styles.call_screen}>
          <div className={styles.current_user}>
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
          <div>
            <Button onClick={callNextPatient} color={"#1c8c18"}>
              CALL THE NEXT PATIENT
            </Button>
          </div>
        </div>
        <div className={styles.main_patients}>
          <div className={styles.table_patients}>
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Number</th>
                  <th>Room</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {patients &&
                  patients.map((patient: IPatient) => (
                    <tr key={patient.name}>
                      <td>{patient.name}</td>
                      <td>{patient.number}</td>
                      <td>{patient.room}</td>
                      <div className={styles.btnCallPatient}>
                        <Button
                          color={"#de2837"}
                          onClick={async () => {
                            await remove(patient.name);
                          }}
                        >
                          REMOVE
                        </Button>
                      </div>
                    </tr>
                  ))}
              </tbody>
            </table>
          </div>
        </div>
        <div className={styles.form_patients}>
          <form onSubmit={handleSubmit(registerNewPatient)}>
            <Input
              label={"name: "}
              type="string"
              register={register("name")}
              errors={errors}
              prop_error="name"
              placeholder="Name"
            />
            <Input
              label={"number: "}
              type="string"
              register={register("number")}
              errors={errors}
              prop_error="number"
              placeholder="Number"
            />
            <Input
              label={"room: "}
              type="string"
              register={register("room")}
              errors={errors}
              prop_error="room"
              placeholder="Room"
            />
            <div className={styles.btnsForm}>
              <Button type="submit">REGISTER</Button>
            </div>
          </form>
        </div>
      </section>
    </div>
  );
}
