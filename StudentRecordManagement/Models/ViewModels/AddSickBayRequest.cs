using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Models.Entities.People;
using System.Collections.Generic;
using System.Numerics;

namespace StudentRecordManagement.Models.ViewModels
{
    public class AddSickBayRequest
    {
        public Guid StudentId { get; set; }
        public DateTime TimeIn { get; set; }
        public SickBayReason SickBayReason { get; set; }
        public string OtherReasons { get; set; }
        public bool? ParentContacted { get; set; }
        public Guid? MedicalOfficerId { get; set; }
    }
}
