namespace ESchedule.Domain.Exceptions;

public class NoSuchRuleException(string message) : Exception(message)
{
}
