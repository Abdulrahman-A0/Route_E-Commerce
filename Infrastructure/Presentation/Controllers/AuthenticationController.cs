using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction.Contracts;
using Shared.DTOs.IdentityModule;

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
    }
}
