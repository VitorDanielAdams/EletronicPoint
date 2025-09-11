using EletronicPoint.Application.DTOs.Auth;
using EletronicPoint.Application.DTOs.User;
using EletronicPoint.Application.Services.Interfaces;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Application.Services.Impl
{
    public class UserService : IUserService
    {
        public Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExistsAsync(string email, int? excludeUserId = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserResponse>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GetUserResponse>> GetUsersByRoleAsync(Role role, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
