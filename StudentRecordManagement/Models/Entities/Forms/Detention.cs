using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentRecordManagement.Models.Entities.Forms
{
    public enum PredefinedReasons
    {
        Behaviour = 1,
        Late = 2,
        Uniform = 3,
        OtherReason = 4,
        [Description("Unexplained Left Early")]
        UnexplainedLeftEarly = 5
    }

    public enum DetentionTime
    {
        BeforeSchool = 1,
        AM_Periods = 2,
        LunchTime = 3,
        PM_Periods = 4,
        AfterSchool = 5
    }

    public enum DetentionStatus
    {
        [Description("Non Processed")]
        NonProcessed = 1,
        [Description("Detention Completed")]
        DetentionCompleted = 2
    }

    public class Detention : FormRecord
    {
        public DetentionStatus Status { get; set; } = DetentionStatus.NonProcessed;
        public PredefinedReasons PredefinedReasons { get; set; }
        public string? BreachReason { get; set; }
        public DetentionTime DetentionTime { get; set; }
    }
}
