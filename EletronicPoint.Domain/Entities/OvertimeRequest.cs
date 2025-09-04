using EletronicPoint.Domain.Entities.Common;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Domain.Entities
{
    public class OvertimeRequest : BaseEntity
    {
        public int UserId { get; set; }
        public DateTimeOffset RequestDate { get; set; }
        public DateTimeOffset OvertimeDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Reason { get; set; }
        public RequestStatus Status { get; set; }
        public DateTimeOffset? ApprovalDate { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
