namespace TeamManager.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("Acesso não autorizado") { }

    public UnauthorizedException(string message)
        : base(message) { }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException) { }
}
