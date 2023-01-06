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
    
    public async Task<bool> AddAsync(NewEducationDto newEducationDto)
    {
        try
        {
            var education = _mapper.Map<Education>(newEducationDto);
            return await _repository.AddAsync(education);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
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

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var education = await _repository.GetAsync(id);
            if (education is null)
            {
                return false;
            }
            return await _repository.RemoveAsync(education);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}