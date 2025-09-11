using EletronicPoint.Application.Repositories.Common;
using EletronicPoint.Domain.Entities;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<List<User>> ListByIsActiveAsync(bool isActive, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetUsersByRoleAsync(Role role, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null, CancellationToken cancellationToken = default);
    }
}
