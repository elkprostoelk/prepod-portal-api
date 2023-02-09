using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
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

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var result = new ServiceResult();
        const string commonError = "Failed to delete a publication";
        try
        {
            var publication = await _repository.GetAsync(id);
            if (publication is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("Publication was not found!");
            }
            else
            {
                var added = await _repository.RemoveAsync(publication);
                if (!added)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add(commonError);
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors.Add(commonError);
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }
}