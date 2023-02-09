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
    public class ArticleController : ControllerBase
    {
        private readonly IValidator<AddPublicationsWithCsvDto> _addPublicationsWithCsvValidator;
        private readonly IValidator<NewArticleDto> _addArticleValidator;
        private readonly IArticleService _service;

        public ArticleController(
            IValidator<AddPublicationsWithCsvDto> addPublicationsWithCsvValidator,
            IValidator<NewArticleDto> addArticleValidator,
            IArticleService service)
        {
            _addPublicationsWithCsvValidator = addPublicationsWithCsvValidator;
            _addArticleValidator = addArticleValidator;
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddArticle(NewArticleDto newArticleDto)
        {
            var validationResult = await _addArticleValidator.ValidateAsync(newArticleDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (!newArticleDto.AuthorsIds.Contains(User.GetUserId())
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.AddArticleAsync(newArticleDto);
            return result.IsSuccessful
                ? StatusCode(201, result.Container)
                : BadRequest(new { errors = result.Errors });
        }
        
        [HttpPost("add-csv")]
        [Authorize]
        public async Task<IActionResult> AddArticlesWithCsv([FromForm] AddPublicationsWithCsvDto addPublicationsWithCsvDto)
        {
            var validationResult = await _addPublicationsWithCsvValidator.ValidateAsync(addPublicationsWithCsvDto);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            if (User.GetUserId() != addPublicationsWithCsvDto.UserId
                && !User.IsInRole("administrator"))
            {
                return Forbid();
            }

            var result = await _service.AddArticlesWithCsvFileAsync(addPublicationsWithCsvDto);
            return result.IsSuccessful
                ? StatusCode(201, result.Container)
                : BadRequest(new { errors = result.Errors });
        }
    }
}
