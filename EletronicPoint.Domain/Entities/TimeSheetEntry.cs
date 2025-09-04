using EletronicPoint.Domain.Entities.Common;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Domain.Entities
{
    public class TimeSheetEntry : BaseEntity
    {
        public int TimeSheetId { get; set; }
        public int? WorkShiftPeriodId { get; set; }
        public TimeSpan EntryTime { get; set; }    
        public DateTimeOffset EntryDate { get; set; }
        public TimeEntryType EntryType { get; set; }

        public virtual TimeSheet TimeSheet { get; set; }
        public virtual WorkShiftPeriod WorkShiftPeriod { get; set; }
    }
}
