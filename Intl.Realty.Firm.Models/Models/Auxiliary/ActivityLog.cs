namespace Intl.Realty.Firm.Models.Models.Auxiliary
{
    public class ActivityLog : BaseModel
    {
        public ActivityLog() { }
        public ActivityLog(int moduleId, ActivityType activity, string oldValue, string newValue, int userAction)
        {
            ModuleId = moduleId;
            Activity = Enum.GetName(typeof(ActivityType), activity);
            OldValues = oldValue;
            NewValues = newValue;
            IsActive = true;
            CreatedBy = userAction;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string? Activity { get; set; }
        public string? OldValues { get; set; }
        public string NewValues { get; set; }
        public Module Module { get; set; } = null!;
    }
}
