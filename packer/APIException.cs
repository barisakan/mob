namespace packer;

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