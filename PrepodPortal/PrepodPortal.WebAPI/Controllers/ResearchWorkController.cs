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
    public class ResearchWorkController : ControllerBase
    {
        private readonly IResearchWorkService _service;
        private readonly IValidator<NewResearchWorkDto> _validator;

        public ResearchWorkController(
            IResearchWorkService service,
            IValidator<NewResearchWorkDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddResearchWork(NewResearchWorkDto newResearchWorkDto)
        {
            var validationResult = await _validator.ValidateAsync(newResearchWorkDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            
            if (!newResearchWorkDto.PerformerIds.Contains(User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.AddResearchWorkAsync(newResearchWorkDto);
            return result.IsSuccessful
                ? StatusCode(201, result.Container)
                : BadRequest(new { errors = result.Errors });
        }

        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteResearchWork(long id)
        {
            var researchWorkDto = await _service.GetAsync(id);
            if (researchWorkDto is null)
            {
                return NotFound("Research work does not exist!");
            }

            if (!researchWorkDto.Performers.Any(user => user.Id == User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.DeleteAsync(id);
            return result.IsSuccessful
                ? NoContent()
                : BadRequest(new { errors = result.Errors });
        }
    }
}
