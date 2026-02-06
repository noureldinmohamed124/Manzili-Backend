using Manzili.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Manzili.Api.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string FullName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MinLength(6), MaxLength(16)]
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }   // Buyer / Provider
    }
}
