namespace Packer;


public static class Exceptions
{
    public static string MaxWeight = "Weight of an item should be <=100";
    public static string MaxCost = "Cost of an item should be <=100";
    public static string MaxItem = "Maximum 15 items can be evaluated";
    public static string MaxBoxWeigth = "Box weight can not be more than 100";
}

public class APIException : Exception
{
    public APIException(string? message, Exception innerException) : base(message, innerException)
    {

    }
    public APIException(string? message) : base(message)
    {

    }
}