using System.ComponentModel.DataAnnotations;

namespace Manzili.Api.DTOs.Auth
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MinLength(6), MaxLength(16)]
        public string Password { get; set; } = null!;
    }
}
