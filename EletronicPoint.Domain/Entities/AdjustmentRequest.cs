using EletronicPoint.Domain.Entities.Common;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Domain.Entities
{
    public class AdjustmentRequest : AuditableEntity
    {
        public int UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? ApprovedById { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AdjustmentRequestEntry> Entries { get; set; } = new List<AdjustmentRequestEntry>();
    }
}
