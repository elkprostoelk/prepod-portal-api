using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class PublicationService : IPublicationService
{
    private readonly IPublicationRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<PublicationService> _logger;

    public PublicationService(
        IPublicationRepository repository,
        UserManager<ApplicationUser> userManager,
        ILogger<PublicationService> logger)
    {
        _repository = repository;
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<bool> ExistsAsync(long id)
    {
        try
        {
            return await _repository.ExistsAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var publication = await _repository.GetAsync(id);
            if (publication is null)
            {
                return false;
            }

            return await _repository.RemoveAsync(publication);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}