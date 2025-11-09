using Shared.DTOs.IdentityModule;
using Shared.DTOs.OrderModule;

namespace ServiceAbstraction.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserResultDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserResultDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<UserResultDTO> GetCurrentUserAsync(string userEmail);
        Task<bool> CheckEmailExistAsync(string userEmail);
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
    }
}
