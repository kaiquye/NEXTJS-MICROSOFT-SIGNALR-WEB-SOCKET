import "./globals.css";
import { Inter } from "next/font/google";
import { GlobalErrorContextProvider } from "@/contexts/globalError.context";
import { UserInfosContextProvider } from "@/contexts/user-infos.context";
import { HeaderAuth } from "@/components/header/auth";

const inter = Inter({ subsets: ["latin"] });

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <GlobalErrorContextProvider>
          <UserInfosContextProvider>{children}</UserInfosContextProvider>
        </GlobalErrorContextProvider>
      </body>
    </html>
  );
}
