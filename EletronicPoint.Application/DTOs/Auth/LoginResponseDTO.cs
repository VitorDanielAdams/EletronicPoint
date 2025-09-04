using EletronicPoint.Application.DTOs.User;

namespace EletronicPoint.Application.DTOs.Auth
{
    public record LoginResponseDTO (string Token, GetUserResponseDTO User);
}
