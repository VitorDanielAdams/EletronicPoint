namespace EletronicPoint.Application.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Role { get; }
        Guid TenantId { get; }
        void SetUser(Guid userId, string role, Guid tenantId);
    }
}
