using Microsoft.AspNetCore.SignalR;
using WebSocket.Api;
using WebSocket.Domain.Interfaces.WebSocket;

namespace WebSocket.Application.Services;

public class PatientHub: Hub
{
    public List<Patient> patients;

    public PatientHub()
    {
        var instance =  Classteste.getInstance();
        this.patients = instance.patients;       
    }
    
    public void AddPatient(Patient patient)
    {
        if (this.patients.Contains(patient))
        {
            Clients.All.SendAsync("error:AddPatient", new
            {
                code="PT:CD409"
            });
        }
        this.patients.Add(patient);
        Clients.All.SendAsync("success:AddPatient", true);
        Clients.All.SendAsync("success:findAll", patients);
    }
    
    public void NextPatient()
    {
        var lastPatient = this.patients[0];
        Console.WriteLine(lastPatient.name);
        this.removePatient(lastPatient);
        this.ListAllPatient();
    }

    public void ListAllPatient()
    {
        Clients.All.SendAsync("success:findAll", this.patients);
    }
    
    public void removePatient(Patient? patient)
    {
        if (patient != null)
        {
            var item = this.patients.Find(ptn => ptn.name == patient.name);
            this.patients.Remove(item);
            this.ListAllPatient();
        }
    }
    
    
}