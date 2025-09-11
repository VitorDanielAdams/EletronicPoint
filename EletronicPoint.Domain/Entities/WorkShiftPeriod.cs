using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class WorkShiftPeriod : AuditableEntity
    {
        public int WorkShiftId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Order { get; set; }

        public virtual WorkShift WorkShift { get; set; }
        public virtual ICollection<TimeSheetEntry> Entries { get; set; } = new List<TimeSheetEntry>();
        public virtual ICollection<AdjustmentRequestEntry> AdjustmentEntries { get; set; } = new List<AdjustmentRequestEntry>();
    }
}
