using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.IdentityModule
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        [Phone]
        public string? PhoneNumber { get; init; }
        public string UserName { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
    }
}
