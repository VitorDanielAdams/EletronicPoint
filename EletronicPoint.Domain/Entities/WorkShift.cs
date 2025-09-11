using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class WorkShift : AuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int BreakMinutes { get; set; }

        public virtual ICollection<WorkShiftPeriod> Periods { get; set; } = new List<WorkShiftPeriod>();
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
