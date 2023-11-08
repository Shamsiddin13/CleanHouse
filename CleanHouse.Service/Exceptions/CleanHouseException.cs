namespace CleanHouse.Service.Exceptions;

public class CleanHouseException : Exception
{
    public int StatusCode { get; set; }
    public CleanHouseException(int code, string message) : base(message)
    {
        StatusCode = code;
    }
}
