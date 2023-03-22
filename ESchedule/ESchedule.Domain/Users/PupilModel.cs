namespace ESchedule.Domain.Users
{
    public record PupilModel : BaseUserModel
    {
        public Guid GroupId { get; set; }

        public GroupModel Group { get; set; } = null!;
    }
}
