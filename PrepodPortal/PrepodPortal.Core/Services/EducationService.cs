using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class EducationService : IEducationService
{
    private readonly ILogger<EducationService> _logger;
    private readonly IMapper _mapper;
    private readonly IEducationRepository _repository;

    public EducationService(
        ILogger<EducationService> logger,
        IMapper mapper,
        IEducationRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ServiceResult<long>> AddAsync(NewEducationDto newEducationDto)
    {
        const string commonError = "Failed to add an education";
        var result = new ServiceResult<long>();
        try
        {
            var education = _mapper.Map<Education>(newEducationDto);
            var added = await _repository.AddAsync(education);
            if (added)
            {
                result.Container = education.Id;
            }
            else
            {
                result.IsSuccessful = false;
                result.Errors.Add(commonError);
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

    public async Task<EducationDto?> GetAsync(long id)
    {
        try
        {
            var education = await _repository.GetAsync(id);
            return _mapper.Map<EducationDto>(education);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return null;
        }
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        var result = new ServiceResult();
        try
        {
            var education = await _repository.GetAsync(id);
            if (education is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("Education was not found");
            }
            else
            {
                var deleted = await _repository.RemoveAsync(education);
                if (!deleted)
                {
                    result.IsSuccessful = false;
                    result.Errors.Add($"Failed to delete the education {id}");
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            result.IsSuccessful = false;
            result.Errors = result.Errors.Distinct().ToList();
        }

        return result;
    }
}