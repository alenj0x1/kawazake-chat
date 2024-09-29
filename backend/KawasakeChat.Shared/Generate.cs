using System.Text;

namespace KawasakeChat.Shared;

public static class Generate
{
    private const string Characters = "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNORPQRSTUVXYWZ0123456789!@#$%^&*()_+{}[]<>|";
    
    public static string RandomString(int length = 50)
    {
        try
        {
            var chars = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                chars.Append(Characters[new Random().Next(0, Characters.Length)]);
            }

            return chars.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}