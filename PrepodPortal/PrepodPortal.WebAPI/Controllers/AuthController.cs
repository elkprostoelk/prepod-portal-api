using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IUserService _userService;

        public AuthController(
            IValidator<LoginDto> loginValidator,
            IUserService userService)
        {
            _loginValidator = loginValidator;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var userExists = await _userService.UserExistsAsync(loginDto.Email);
            if (!userExists)
            {
                return NotFound($"User {loginDto.Email} was not found!");
            }
        
            var result = await _userService.ValidateUserAsync(loginDto);
            return result.IsSuccessful
                ? Ok(result)
                : Unauthorized(result.Errors);
        }
    }
}
