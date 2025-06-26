using API_Layer.Extensions;
using Core.DTOs.ExternalAuth;
using Core.DTOs.People;
using Core.DTOs.RefreshTokens;
using Core.Services.Interfaces;
using Core.Services.Interfaces.RefreshTokens;
using ExternalAuthentication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.DTOs.People.UsersDTOs;

namespace API_Layer.Controllers.People
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        IRefreshTokenService _refreshTokenService;
        private readonly IExternalAuthProviderFactory _authProviderFactory;

        public UsersController(IUserService userService, 
            ILogger<UsersController> logger,
            IRefreshTokenService refreshTokenService,
            IExternalAuthProviderFactory authProviderFactory)
        {
            _userService = userService;
            _logger = logger;
            _refreshTokenService = refreshTokenService;
            _authProviderFactory = authProviderFactory;
        }




        [HttpPost("signup")]
        public async Task<ActionResult<UsersDTOs.SendUserDTO>> Signup(AddUserDTO addUserDTO)
        {
            var user = await _userService.CreateUserAsync(addUserDTO);
            return StatusCode(201, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RefreshTokenDTOs.LoginResponseDto>> Login(LoginDTO loginDTO)
        {
            var user = await _userService.Login(loginDTO);
            if (user == null)
                return BadRequest("Wrong username or password");

            var tokens = await _refreshTokenService.GenerateTokensForUserAsync(user.UserId);
            return Ok(tokens);
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<SendUserDTO>> UpdateUserInfo(JsonPatchDocument<AddUserDTO> UpdatedItem)
        {
            var userId = User.GetUserId();

            var user = await _userService.PatchUser(UpdatedItem, userId);

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUser()
        {
            int userId = User.GetUserId();
            var user = await _userService.GetUser(userId);
            return Ok(user);
        }
        [HttpPost("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDTOs.RefreshTokenDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
                return BadRequest(new { message = "Refresh token is required" });

            var success = await _refreshTokenService.LogoutAsync(request.Token);

            if (!success)
                return Unauthorized(new { message = "Invalid or already revoked token" });

            return Ok(new { message = "Logged out successfully" });
        }

        //[HttpDelete]
        //[Authorize]
        //public async Task<ActionResult> Delete()
        //{
        //    return Ok(await _userService.DeleteUserAsync(clsToken.GetUserID(HttpContext)));
        //}


        [HttpPost("external-login")]
        public async Task<ActionResult<ExternalAuthResponseDTO>> ExternalLogin([FromBody] ExternalAuthDTOs.ExternalLoginRequestDTO request)
        {
            var provider = _authProviderFactory.GetProvider(request.Provider);

            var info = await provider.AuthenticateAsync(request.IdToken);

            if (info == null || info?.Email == null)
            {
                _logger.LogWarning("External authentication failed for provider: {Provider}", request.Provider);
                return BadRequest($"Authentication failed. Please try again. {info?.ErrorMessage}");
            }

            var result = await _userService.GetExternalAuthResponse(info.Email, info.FullName);
            if (result == null)
            {
                _logger.LogWarning("User not found or created during external authentication for email: {Email}", info.Email);
                return BadRequest("User not found or created during external authentication.");
            }

            return Ok(result);

        }
    }
}
