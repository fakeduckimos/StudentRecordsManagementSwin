using StudentRecordManagement.Models.Entities.People;

namespace StudentRecordManagement.Models.Entities.Forms
{
    public abstract class FormRecord
    {
        public Guid Id { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
        
    }
}
