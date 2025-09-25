using EletronicPoint.Application.DTOs.User;

namespace EletronicPoint.Application.DTOs.Auth
{
    public record LoginResponse (string Token, UserResponse User);
}
