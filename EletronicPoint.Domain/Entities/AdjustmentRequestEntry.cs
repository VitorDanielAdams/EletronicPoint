using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class AdjustmentRequestEntry : AuditableEntity
    {
        public int AdjustmentId { get; set; }
        public int? WorkShiftPeriodId { get; set; }
        public TimeSpan EntryTime { get; set; }
        public DateTimeOffset EntryDate { get; set; }

        public virtual AdjustmentRequest Adjustment { get; set; }
        public virtual WorkShiftPeriod WorkShiftPeriod { get; set; }
    }
}
