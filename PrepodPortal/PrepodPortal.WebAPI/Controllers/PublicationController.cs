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

        public PublicationController(
            IPublicationService service)
        {
            _service = service;
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

            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : Conflict();
        }
    }
}
