using System.Security.Cryptography;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.WebAPI.Extensions;

namespace PrepodPortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<NewTeacherDto> _newTeacherValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;
        private readonly IUserService _userService;

        public AuthController(
            IValidator<LoginDto> loginValidator,
            IUserService userService,
            IValidator<NewTeacherDto> newTeacherValidator,
            IValidator<ChangePasswordDto> changePasswordValidator)
        {
            _loginValidator = loginValidator;
            _userService = userService;
            _newTeacherValidator = newTeacherValidator;
            _changePasswordValidator = changePasswordValidator;
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
        
            var validatedUserTokenDto = await _userService.ValidateUserAsync(loginDto);
            return validatedUserTokenDto is not null
                ? Ok(validatedUserTokenDto)
                : Unauthorized("Auth error!");
        }

        [HttpPost("new-teacher")]
        [Authorize(Roles = "administrator, profiles creator")]
        public async Task<IActionResult> RegisterNewTeacher(NewTeacherDto newTeacherDto)
        {
            var validationResult = await _newTeacherValidator.ValidateAsync(newTeacherDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            var registeredTeacher = await _userService.RegisterTeacherAsync(newTeacherDto);
            return registeredTeacher ? StatusCode(201) : Conflict();
        }

        [HttpPatch("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var validationResult = await _changePasswordValidator.ValidateAsync(changePasswordDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (changePasswordDto.UserId != User.GetUserId()
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var passwordChanged = await _userService.ChangePasswordAsync(changePasswordDto);
            return passwordChanged ? Ok() : Conflict();
        }
    }
}
