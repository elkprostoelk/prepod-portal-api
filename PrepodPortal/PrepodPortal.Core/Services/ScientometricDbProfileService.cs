using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class ScientometricDbProfileService : IScientometricDbProfileService
{
    private readonly ILogger<ScientometricDbProfileService> _logger;
    private readonly IMapper _mapper;
    private readonly IScientometricDbProfileRepository _repository;

    public ScientometricDbProfileService(
        ILogger<ScientometricDbProfileService> logger,
        IMapper mapper,
        IScientometricDbProfileRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<ServiceResult<ICollection<long>>> AddProfilesAsync(ICollection<NewScientometricDbProfileDto> scientometricDbProfiles,
        string userId)
    {
        const string commonError = "Failed to add scientometric DB profiles!";
        var result = new ServiceResult<ICollection<long>>();
        try
        {
            var profiles = _mapper.Map<ICollection<ScientometricDbProfile>>(scientometricDbProfiles);
            profiles.ToList().ForEach(profile => profile.UserId = userId);
            var added = await _repository.AddRangeAsync(profiles);
            if (added)
            {
                result.Container = profiles.Select(profile => profile.Id).ToList();
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
}