using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        
        public UserController(
            IValidator<NewTeacherDto> newTeacherValidator,
            IValidator<ChangePasswordDto> changePasswordValidator,
            IUserService service)
        {
            _newTeacherValidator = newTeacherValidator;
            _changePasswordValidator = changePasswordValidator;
            _service = service;
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

            var registeredTeacher = await _service.RegisterTeacherAsync(newTeacherDto);
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

            var passwordChanged = await _service.ChangePasswordAsync(changePasswordDto);
            return passwordChanged ? Ok() : Conflict();
        }

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

            var deleted = await _service.DeleteUserAsync(id);
            return deleted ? NoContent() : Conflict();
        }
    }
}
