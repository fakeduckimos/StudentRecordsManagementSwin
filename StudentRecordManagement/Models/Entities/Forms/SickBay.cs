using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Models.Entities.Forms
{
    public enum SickBayOutAction
    {
        ReturnToClass = 1,
        GoingHome = 2
    }

    public enum SickBayReason
    {
        Injured = 1,
        Sick = 2,
        OtherReason=3
    }

    public enum SickBayStatus
    {
        NonProcessed = 0,
        SickBayIn = 1,
        SickBayOut = 2
    }

    public class SickBay : FormRecord
    {
        public SickBayStatus Status { get; set; } = SickBayStatus.NonProcessed;
        public DateTime? TimeIn { get; set; }

        public SickBayReason SickBayReason { get; set; }

        public string? OtherReasons { get; set; }
        public string? Treatment { get; set; }

        public DateTime? TimeOut { get; set; }

        public SickBayOutAction? SickBayOutAction { get; set; }

        public bool? ParentContacted { get; set; }
        public Guid? MedicalOfficerId { get; set; }
        public Staff? MedicalOfficer { get; set; }
    }
}
