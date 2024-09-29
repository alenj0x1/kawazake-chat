using System.Security.Cryptography;
using System.Text;

namespace KawasakeChat.Shared;

public static class Hasher
{
    private const char Separator = ';';
    
    public static string Hash(string value)
    {
        try
        {
            var salt = RandomSalt();
            var hashData = SHA256.HashData(Encoding.UTF8.GetBytes(value + salt));
            var base64 = Convert.ToBase64String(hashData);

            return $"{base64}{Separator}{salt}";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string Hash(string value, string salt)
    {
        try
        {
            var hashData = SHA256.HashData(Encoding.UTF8.GetBytes(value + salt));
            var base64 = Convert.ToBase64String(hashData);

            return $"{base64}{Separator}{salt}";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public static bool Compare(string value, string hashedValue)
    {
        try
        {
            var salt = hashedValue.Split(Separator)[1];
            return Hash(value, salt) == hashedValue;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private static string RandomSalt()
    {
        try
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128 / 8));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}