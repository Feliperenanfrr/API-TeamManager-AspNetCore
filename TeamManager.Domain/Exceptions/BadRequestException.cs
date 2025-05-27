namespace TeamManager.Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("Requisição inválida")
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}