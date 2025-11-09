using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared.DTOs.IdentityModule;
using Shared.DTOs.OrderModule;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDTO>> RegisterAsync(RegisterDTO registerDTO)
            => Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDTO));

        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDTO>> LoginAsync(LoginDTO loginDTO)
            => Ok(await serviceManager.AuthenticationService.LoginAsync(loginDTO));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
            => Ok(await serviceManager.AuthenticationService.CheckEmailExistAsync(email));

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDTO>> GetCurrentUserAsync()
            => Ok(await serviceManager.AuthenticationService.GetCurrentUserAsync(GetUserEmail()));

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
            => Ok(await serviceManager.AuthenticationService.GetUserAddressAsync(GetUserEmail()));

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
            => Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(GetUserEmail(), addressDto));
    }
}
