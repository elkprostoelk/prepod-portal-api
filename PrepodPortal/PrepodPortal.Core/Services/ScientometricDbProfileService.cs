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
    
    public async Task<bool> AddProfilesAsync(ICollection<NewScientometricDbProfileDto> scientometricDbProfiles,
        long userProfileId)
    {
        try
        {
            var profiles = _mapper.Map<ICollection<ScientometricDbProfile>>(scientometricDbProfiles);
            profiles.ToList().ForEach(profile => profile.UserProfileId = userProfileId);
            return await _repository.AddRangeAsync(profiles);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}