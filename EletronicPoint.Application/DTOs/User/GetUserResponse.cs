namespace EletronicPoint.Application.DTOs.User
{
    public record GetUserResponse(int Id, string Name, string Email, bool IsActive);
}
