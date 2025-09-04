using System.ComponentModel.DataAnnotations;

namespace EletronicPoint.WebApi.Models.Auth
{
    public record LoginRequest(
        [Required(ErrorMessage = "LoginRequired")]
        string Login,

        [Required(ErrorMessage = "PasswordRequired")]
        string Password
    );
}
