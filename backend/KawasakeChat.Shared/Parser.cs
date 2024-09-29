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
}