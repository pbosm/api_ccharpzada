using System.Security.Cryptography;
using System.Text;
using DotNetEnv;

namespace SystemCcharpzinho.Request.Helpers;

public class CryptoHelper
{
    static CryptoHelper()
    {
        Env.Load();
    }

    public static string CryptS(string input)
    {
        var hash1 = GetMd5Hash(Env.GetString("HASH1"));
        var hash2 = GetMd5Hash(Env.GetString("HASH2"));
        var hash3 = GetMd5Hash(Env.GetString("HASH3"));
        var hash4 = GetMd5Hash(Env.GetString("HASH4"));

        var encodedInput = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));

        var count1 = encodedInput.Length;
        var count2 = count1 / 2;

        var split1 = encodedInput[..count2];
        var split2 = encodedInput[count2..count1];

        return hash1[..8] + split1 + hash2[..4] + hash3[..4] + split2 + hash4[..8];
    }

    public static string DescryptS(string input)
    {
        try
        {
            var hash1 = GetMd5Hash(Env.GetString("HASH1"));
            var hash2 = GetMd5Hash(Env.GetString("HASH2"));
            var hash3 = GetMd5Hash(Env.GetString("HASH3"));
            var hash4 = GetMd5Hash(Env.GetString("HASH4"));

            input = input.Replace(hash1[..8], string.Empty);
            input = input.Replace(hash2[..4], string.Empty);
            input = input.Replace(hash3[..4], string.Empty);
            input = input.Replace(hash4[..8], string.Empty);

            var decodedBytes = Convert.FromBase64String(input);
            
            return Encoding.UTF8.GetString(decodedBytes);
        }
        catch (Exception e)
        {
            throw new Exception("Erro no HASH: " + e.Message);
        }
    }

    private static string GetMd5Hash(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);

        var hashBytes = MD5.HashData(inputBytes);

        var sb = new StringBuilder();

        for (var i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();
    }
}