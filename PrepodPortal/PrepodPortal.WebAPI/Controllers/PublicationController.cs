using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _service;
        private readonly IUserService _userService;

        public PublicationController(
            IPublicationService service,
            IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetAllUserPublicationsAsync(string userId)
        {
            var userExists = await _userService.UserExistsAsync(userId);
            if (!userExists)
            {
                return NotFound("User does not exist!");
            }

            return Ok(await _service.GetAllAsync(userId));
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public async Task<IActionResult> DeletePublication(long id)
        {
            var publicationExists = await _service.ExistsAsync(id);
            if (!publicationExists)
            {
                return NotFound();
            }

            var result = await _service.DeleteAsync(id);
            return result.IsSuccessful
                ? NoContent()
                : BadRequest(result.Errors);
        }
    }
}
