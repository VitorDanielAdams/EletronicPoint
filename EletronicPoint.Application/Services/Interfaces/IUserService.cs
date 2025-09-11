using EletronicPoint.Application.DTOs.Auth;
using EletronicPoint.Application.DTOs.User;
using EletronicPoint.Domain.Enums;

namespace EletronicPoint.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default);
        Task<GetUserResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<GetUserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<GetUserResponse>> GetAllActiveAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<GetUserResponse>> GetUsersByRoleAsync(Role role, CancellationToken cancellationToken = default);
        //Task<GetUserResponseDTO> CreateAsync(CreateUserRequestDTO createUserRequest, CancellationToken cancellationToken = default);
        //Task<GetUserResponseDTO> UpdateAsync(int id, UpdateUserRequestDTO updateUserRequest, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task RestoreAsync(int id, CancellationToken cancellationToken = default);
        //Task ChangePasswordAsync(int id, ChangePasswordRequestDTO changePasswordRequest, CancellationToken cancellationToken = default);
        Task<bool> EmailExistsAsync(string email, int? excludeUserId = null, CancellationToken cancellationToken = default);
    }
}
