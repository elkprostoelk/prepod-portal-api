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
