using EletronicPoint.Application.Services.Interfaces;

namespace EletronicPoint.Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; private set; }
        public string Role { get; private set; }
        public Guid TenantId { get; private set; }

        public void SetUser(Guid userId, string role, Guid tenantId)
        {
            UserId = userId;
            Role = role;
            TenantId = tenantId;
        }
    }
}
