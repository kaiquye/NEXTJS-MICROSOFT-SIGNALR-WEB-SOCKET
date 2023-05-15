using System.Security.Cryptography;

namespace WebSocket.Application.utility;

public class IResGenerate
{
    public string password_hash { get; set; }
    public string password_salt { get; set; }
}

public interface IPasswordHash
{
    public IResGenerate generate(string password);
    public bool verify(string password, string passwordHash);
}

public class PasswordHash : IPasswordHash
{
    private const int SaltSize = 128 / 8;
    private const int KetSize = 256 / 8;
    private const int Iterations = 500;
    private HashAlgorithmName hashAlgorithmType = HashAlgorithmName.SHA256;
    private const string Delimiter = ";";

    public IResGenerate generate(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmType, KetSize);

        var hash_ = String.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));

        return new IResGenerate { password_hash = hash_, password_salt = SaltSize.ToString() };
    }

    public bool verify(string password, string passwordHash)
    {
        var elements = passwordHash.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmType, KetSize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}