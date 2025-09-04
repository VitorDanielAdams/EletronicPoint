namespace EletronicPoint.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
