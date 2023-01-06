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
    public class EducationController : ControllerBase
    {
        private readonly IValidator<NewEducationDto> _newEducationValidator;
        private readonly IEducationService _service;

        public EducationController(
            IValidator<NewEducationDto> newEducationValidator,
            IEducationService service)
        {
            _newEducationValidator = newEducationValidator;
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddEducation(NewEducationDto newEducationDto)
        {
            var validationResult = await _newEducationValidator.ValidateAsync(newEducationDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (User.GetUserId() != newEducationDto.UserId
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var added = await _service.AddAsync(newEducationDto);
            return added ? StatusCode(201) : Conflict();
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> DeleteEducation(long id)
        {
            var education = await _service.GetAsync(id);
            if (education is null)
            {
                return NotFound();
            }

            if (education.UserId != User.GetUserId()
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : Conflict();
        }
    }
}
