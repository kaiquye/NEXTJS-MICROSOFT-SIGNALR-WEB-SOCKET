import Link from "next/link";

export default function Page() {
  return (
    <section>
        <main>
           <h1>Wellcome.</h1>
            <div>
              <Link href="/patient/call">Patient call</Link>
              <Link href="/patient/register">Patient register</Link>
              <Link href="/person/register">Person register</Link>
              <Link href="/person/login">Person login</Link>
            </div>
        </main>
    </section>
  )
}
