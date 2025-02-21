using ESchedule.Domain.Properties;
using PowerInfrastructure.Exceptions;

namespace ESchedule.Domain.Exceptions;

public class EntityNotFoundException : ItemNotFoundException
{
    private static readonly string DefaultMessage = Resources.TheItemDoesntExist;

    public EntityNotFoundException() : base(DefaultMessage)
    {

    }

    public EntityNotFoundException(string? message) : base(message ?? DefaultMessage)
    {

    }
}