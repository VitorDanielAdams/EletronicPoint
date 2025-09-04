using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class TimeSheet : BaseEntity
    {
        public int UserId { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalBreakHours { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual ICollection<TimeSheetEntry> Entries { get; set; } = new List<TimeSheetEntry>();
    }
}