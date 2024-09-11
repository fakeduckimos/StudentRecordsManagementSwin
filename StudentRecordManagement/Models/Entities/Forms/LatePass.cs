namespace StudentRecordManagement.Models.Entities.Forms
{
    public class LatePass : FormRecord
    {
        public AbsentReason Reason { get; set; }
        public string? Details { get; set; } // appear when select Other
        public DateTime SignInDateTime { get; set; }
    }
}
