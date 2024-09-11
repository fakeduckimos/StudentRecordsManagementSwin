namespace StudentRecordManagement.Models.ViewModels
{
    public class AddStudentRequest
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PreferredName { get; set; }
        public string Year { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public char Gender { get; set; }
    }
}
