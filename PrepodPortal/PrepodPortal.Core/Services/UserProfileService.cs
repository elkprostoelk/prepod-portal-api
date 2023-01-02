using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class UserProfileService : IUserProfileService
{
    private readonly ILogger<UserProfileService> _logger;
    private readonly IUserProfileRepository _repository;
    private readonly IMapper _mapper;

    public UserProfileService(
        ILogger<UserProfileService> logger,
        IUserProfileRepository repository,
        IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ICollection<BriefUserProfileDto>> GetAllAsync(string? searchByName)
    {
        try
        {
            var profiles = await _repository.GetAllAsync(searchByName);
            return _mapper.Map<ICollection<BriefUserProfileDto>>(profiles);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return new List<BriefUserProfileDto>();
        }
    }
}