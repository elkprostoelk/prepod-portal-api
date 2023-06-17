using Microsoft.AspNetCore.Mvc;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationIncreaseController : ControllerBase
    {
        private readonly IQualificationIncreaseService _service;
        private readonly IUserService _userService;

        public QualificationIncreaseController(
            IQualificationIncreaseService service,
            IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetUserQualificationIncreasesAsync(string userId)
        {
            var userExists = await _userService.UserExistsAsync(userId);
            if (!userExists)
            {
                return NotFound("User does not exist!");
            }

            var qualificationIncreases = await _service.GetAllAsync(userId);
            return Ok(qualificationIncreases);
        }
    } 
}
