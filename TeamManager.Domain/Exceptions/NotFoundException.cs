namespace TeamManager.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }

    public NotFoundException(string entity, object key)
        : base($"{entity} with key '{key}'  was not found.") { }
}
