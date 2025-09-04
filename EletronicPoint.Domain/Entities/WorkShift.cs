using EletronicPoint.Domain.Entities.Common;

namespace EletronicPoint.Domain.Entities
{
    public class WorkShift : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<WorkShiftPeriod> Periods { get; set; } = new List<WorkShiftPeriod>();
        public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
