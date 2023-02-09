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
    public class AcademicDegreeController : ControllerBase
    {
        private readonly IValidator<NewAcademicDegreeDto> _newAcademicDegreeValidator;
        private readonly IAcademicDegreeService _service;

        public AcademicDegreeController(
            IValidator<NewAcademicDegreeDto> newAcademicDegreeValidator,
            IAcademicDegreeService service)
        {
            _newAcademicDegreeValidator = newAcademicDegreeValidator;
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAcademicDegree(NewAcademicDegreeDto newAcademicDegreeDto)
        {
            var validationResult = await _newAcademicDegreeValidator.ValidateAsync(newAcademicDegreeDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (User.GetUserId() != newAcademicDegreeDto.UserId
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.AddAcademicDegreeAsync(newAcademicDegreeDto);
            return result.IsSuccessful ? StatusCode(201, result.Container)
                : BadRequest(new { errors = result.Errors });
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> DeleteAcademicDegree(long id)
        {
            var degree = await _service.GetAsync(id);
            if (degree is null)
            {
                return NotFound();
            }

            if (User.GetUserId() != degree.UserId
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.DeleteAsync(id);
            return result.IsSuccessful ? NoContent()
                : BadRequest(new { errors = result.Errors });
        }
    }
}
