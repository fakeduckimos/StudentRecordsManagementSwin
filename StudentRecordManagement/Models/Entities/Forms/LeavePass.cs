namespace StudentRecordManagement.Models.Entities.Forms
{
    public enum AbsentReason
    {
        Sick = 1,
        Family = 2,
        Unexplained = 3,
        Other = 4
    }

    public class LeavePass : FormRecord
    {
        public AbsentReason Reason { get; set; }
        public string? Details { get; set; }
        public DateTime SignOutDateTime { get; set; }
    }
}
