using EletronicPoint.WebApi.Models.User;

namespace EletronicPoint.WebApi.Models.Auth
{
    public record LoginResponse(string Token, GetPlayerResponse GetPlayerResponse);
}
