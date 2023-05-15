import React from "react";
import styles from "./styles.module.css";
import { useGlobalError } from "@/contexts/globalError.context";
export default function ErrorComponent() {
  const { error, setError } = useGlobalError();
  const [open, setOpen] = React.useState<string>();

  if (error) {
    setTimeout(() => {
      setOpen("none");
      setError(undefined);
      setTimeout(() => {
        setOpen("flex");
      }, 10);
    }, 1500);
  }

  return (
    <>
      {error && (
        <>
          <div className={styles.main} style={{ display: open }}>
            <h1>{error}</h1>
          </div>
        </>
      )}
    </>
  );
}
