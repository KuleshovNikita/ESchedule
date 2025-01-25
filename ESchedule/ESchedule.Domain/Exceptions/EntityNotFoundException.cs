using ESchedule.Domain.Properties;

namespace ESchedule.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    private static readonly string DefaultMessage = Resources.TheItemDoesntExist;

    public EntityNotFoundException() : base(DefaultMessage)
    {

    }

    public EntityNotFoundException(string? message) : base(message ?? DefaultMessage)
    {

    }
}
