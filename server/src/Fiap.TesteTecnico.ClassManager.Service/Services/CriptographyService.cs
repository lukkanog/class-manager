using System.Security.Cryptography;
using System.Text;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;

public static class CriptographyService
{
    public static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}
