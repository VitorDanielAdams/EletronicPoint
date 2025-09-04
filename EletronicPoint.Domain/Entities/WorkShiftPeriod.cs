using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class WorkShiftPeriod : BaseEntity
    {
        public int WorkShiftId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int BreakMinutes { get; set; }
        public int Order { get; set; }

        public virtual WorkShift WorkShift { get; set; }
    }
}
