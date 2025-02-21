namespace PowerInfrastructure.Exceptions;

public class ItemNotFoundException : Exception
{
    private static readonly string DefaultMessage = "Item not found";

    public ItemNotFoundException() : base(DefaultMessage)
    {

    }

    public ItemNotFoundException(string? message) : base(message ?? DefaultMessage)
    {

    }
}