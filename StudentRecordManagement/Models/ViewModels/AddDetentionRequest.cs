using StudentRecordManagement.Models.Entities.Forms;

namespace StudentRecordManagement.Models.ViewModels
{
    public class AddDetentionRequest
    {
        public Guid StudentId { get; set; }
        public DetentionTime DetentionTime { get; set; }
        public PredefinedReasons PredefinedReasons { get; set; }
        public string? BreachReason { get; set; }
    }
}
