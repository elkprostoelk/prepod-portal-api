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
    public class MonographController : ControllerBase
    {
        private readonly IMonographService _service;
        private readonly IValidator<NewMonographDto> _addMonographValidator;

        public MonographController(
            IMonographService service,
            IValidator<NewMonographDto> addMonographValidator)
        {
            _service = service;
            _addMonographValidator = addMonographValidator;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMonograph(NewMonographDto newMonographDto)
        {
            var validationResult = await _addMonographValidator.ValidateAsync(newMonographDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (!newMonographDto.AuthorsIds.Contains(User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var added = await _service.AddMonographAsync(newMonographDto);
            return added ? Ok() : Conflict();
        }
    }
}
