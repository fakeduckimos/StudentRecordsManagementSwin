using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Models.ViewModels
{
    public class AddLatePassRequest
    {
        public Guid StudentId { get; set; }
        public AbsentReason Reason { get; set; }
        public string? Details { get; set; } // appear when select Other
        public DateTime SignInDateTime { get; set; }

    }
}
