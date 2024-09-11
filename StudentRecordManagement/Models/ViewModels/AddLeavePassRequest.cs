using StudentRecordManagement.Models.Entities.Forms;
using System.ComponentModel.DataAnnotations;

namespace StudentRecordManagement.Models.ViewModels
{
    public class AddLeavePassRequest
    {
        //public DateTime RecordDate { get; set; }
        //public DateTime Created { get; set; }
        //public DateTime Modified { get; set; }
        public Guid StudentId { get; set; }
        public AbsentReason Reason { get; set; }
        public string? Details { get; set; } // appear when select Other

        [Required, EnumDataType(typeof(AbsentReason))]
        public DateTime SignOutDateTime { get; set; }
    }
}
