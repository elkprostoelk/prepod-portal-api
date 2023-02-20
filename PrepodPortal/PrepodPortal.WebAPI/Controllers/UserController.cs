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
    public class UserController : ControllerBase
    {
        private readonly IValidator<NewTeacherDto> _newTeacherValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;
        private readonly IUserService _service;
        private readonly IValidator<ChangeUserAvatarDto> _changeAvatarValidator;

        public UserController(
            IValidator<NewTeacherDto> newTeacherValidator,
            IValidator<ChangePasswordDto> changePasswordValidator,
            IUserService service,
            IValidator<ChangeUserAvatarDto> changeAvatarValidator)
        {
            _newTeacherValidator = newTeacherValidator;
            _changePasswordValidator = changePasswordValidator;
            _service = service;
            _changeAvatarValidator = changeAvatarValidator;
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

            var result = await _service.RegisterTeacherAsync(newTeacherDto);
            return result.IsSuccessful
                ? StatusCode(201, result.Container)
                : BadRequest(new { errors = result.Errors });
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

            var result = await _service.ChangePasswordAsync(changePasswordDto);
            return result.IsSuccessful
                ? Ok()
                : BadRequest(new { errors = result.Errors });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userExists = await _service.UserExistsAsync(id);
            if (!userExists)
            {
                return NotFound("User does not exist");
            }

            if (id != User.GetUserId() && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.DeleteUserAsync(id);
            return result.IsSuccessful
                ? NoContent()
                : BadRequest(new { errors = result.Errors });
        }

        [Authorize]
        [HttpPatch("change-avatar")]
        public async Task<IActionResult> ChangeUserAvatar([FromForm] ChangeUserAvatarDto changeUserAvatarDto)
        {
            var validationResult = await _changeAvatarValidator.ValidateAsync(changeUserAvatarDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
                return BadRequest(errors);
            }

            if (User.GetUserId() != changeUserAvatarDto.UserId
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.ChangeAvatarAsync(changeUserAvatarDto);
            return result.IsSuccessful
                ? NoContent()
                : BadRequest(result.Errors);
        }
    }
}
