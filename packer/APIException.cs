namespace Packer;


public static class Messages
{
    public static string MaxItemWeight = "Weight of an item excessing limits";
    public static string MaxItemCost = "Cost of an item excessing limits";
    public static string MaxItem = "Can not add more than maximum allowed items to the package";
    public static string MaxPackageWeigth = "Box weight can not be more than allowed limits";
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