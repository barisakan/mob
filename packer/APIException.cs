namespace Packer;


public static class Messages
{
    public static string MaxWeight = "Weight of an item should be <=100";
    public static string MaxCost = "Cost of an item should be <=100";
    public static string MaxItem = "Maximum 15 items can be evaluated";
    public static string MaxBoxWeigth = "Box weight can not be more than 100";
    public static string LineParsingError = "Malformatted data within file";
    public static string FileOpenError = "Error while trying to read file";
}

public class APIException : Exception
{
    public APIException(string? message, Exception innerException) : base(message, innerException)
    {

    }
    public APIException(Exception innerException) : base(innerException.Message, innerException)
    {

    }
    public APIException(string? message) : base(message)
    {

    }
}