namespace KawasakeChat.Shared;

public static class Parser
{
    public static Guid? Guid(string value)
    {
        try
        {
            return System.Guid.Parse(value);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public static string WithoutWhiteSpaces(string value)
    {
        try
        {
            return value.Replace(" ", "");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}