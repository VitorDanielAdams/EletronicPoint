using EletronicPoint.Domain.Entities;

namespace EletronicPoint.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string>? GenerateToken(User player);
    }
}
