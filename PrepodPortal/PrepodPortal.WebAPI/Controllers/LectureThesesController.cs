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
    public class LectureThesesController : ControllerBase
    {
        private readonly IValidator<NewLectureThesesDto> _addLectureThesesValidator;
        private readonly ILectureThesesService _service;

        public LectureThesesController(
            IValidator<NewLectureThesesDto> addLectureThesesValidator,
            ILectureThesesService service)
        {
            _addLectureThesesValidator = addLectureThesesValidator;
            _service = service;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLectureTheses(NewLectureThesesDto newLectureThesesDto)
        {
            var validationResult = await _addLectureThesesValidator.ValidateAsync(newLectureThesesDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (!newLectureThesesDto.AuthorsIds.Contains(User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var added = await _service.AddLectureThesesAsync(newLectureThesesDto);
            return added ? Ok() : Conflict();
        }
    }
}
