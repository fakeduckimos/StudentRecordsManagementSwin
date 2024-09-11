namespace StudentRecordManagement.Models.Entities.People
{
    public enum StaffGroupBit
    {
        Teacher = 1, TeacherAide = 2, TeacherReplacement = 4, HOD = 8, Admin = 16, MedicalOfficer = 32
    }
    public class Staff
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int StaffGroupMask { get; set; } = (int) StaffGroupBit.Teacher;
    }
}
