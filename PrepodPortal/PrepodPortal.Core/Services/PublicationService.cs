using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class PublicationService : IPublicationService
{
    private readonly IPublicationRepository _repository;
    private readonly ILogger<PublicationService> _logger;
    private readonly IMapper _mapper;

    public PublicationService(
        IPublicationRepository repository,
        ILogger<PublicationService> logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
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

    public async Task<List<object>> GetAllAsync(string userId)
    {
        List<object> result = new List<object>();

        try
        {
            var publications = await _repository.GetAllAsync(userId);
            var dtos = _mapper.Map<ICollection<PublicationDto>>(publications);
            var objectDtos = dtos.Select(dto => (object)dto).ToList();
            return objectDtos;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
        }

        return result;
    }
}