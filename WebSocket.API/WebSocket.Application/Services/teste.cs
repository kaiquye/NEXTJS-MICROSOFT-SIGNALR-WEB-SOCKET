using WebSocket.Domain.Interfaces.WebSocket;

namespace WebSocket.Api;

public class Classteste
{
    public List<Patient> patients = new List<Patient>();
    private static Classteste Instance = null;

    private Classteste()
    {
    }

    public static Classteste getInstance()
    {
        if (Instance == null)
        {
            Instance = new Classteste();
        }

        return Instance;
    }
}