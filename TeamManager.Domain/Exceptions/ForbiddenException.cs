namespace TeamManager.Domain.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("Acesso proibido ao recurso") { }

    public ForbiddenException(string message)
        : base(message) { }

    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException) { }
}
