using ESchedule.Domain.Properties;

namespace ESchedule.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base(Resources.TheItemDoesntExist)
        {

        }

        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}
