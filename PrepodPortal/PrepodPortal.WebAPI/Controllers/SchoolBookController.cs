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
    public class SchoolBookController : ControllerBase
    {
        private readonly ISchoolBookService _service;
        private readonly IValidator<NewSchoolBookDto> _addSchoolBookValidator;

        public SchoolBookController(
            ISchoolBookService service,
            IValidator<NewSchoolBookDto> addSchoolBookValidator)
        {
            _service = service;
            _addSchoolBookValidator = addSchoolBookValidator;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSchoolBook(NewSchoolBookDto newSchoolBookDto)
        {
            var validationResult = await _addSchoolBookValidator.ValidateAsync(newSchoolBookDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (!newSchoolBookDto.AuthorsIds.Contains(User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.AddSchoolBookAsync(newSchoolBookDto);
            return result.IsSuccessful
                ? StatusCode(201, result.Container)
                : BadRequest(result.Errors);
        }
    }
}
