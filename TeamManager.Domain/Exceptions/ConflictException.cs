namespace TeamManager.Domain.Exceptions;

public class ConflictException : Exception
{
    public ConflictException()
        : base("Conflito de dados detectado") { }

    public ConflictException(string message)
        : base(message) { }

    public ConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}
