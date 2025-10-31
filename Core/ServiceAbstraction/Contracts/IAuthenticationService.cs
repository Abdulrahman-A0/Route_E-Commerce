using Shared.DTOs.IdentityModule;

namespace ServiceAbstraction.Contracts
{
    public interface IAuthenticationService
    {
        Task<UserResultDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserResultDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
