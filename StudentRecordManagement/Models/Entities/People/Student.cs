using Microsoft.Extensions.Hosting;
using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Models.Entities.People
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public string PreferredName { get; set; }
        public string Year { get; set; }
        public DateTime DOB { get; set; }
        public char Gender { get; set; }
        public ParentContact? ParentContact { get; set; }

        public ICollection<FormRecord> Records { get; set; }
        public Student() {
            Records = new List<FormRecord>();
        }
    }
}
