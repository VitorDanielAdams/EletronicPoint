namespace EletronicPoint.Application.DTOs.User
{
    public record GetUserResponseDTO(int Id, string Name, string Email, bool IsActive);
}
