using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class ResearchWorkService : IResearchWorkService
{
    private readonly IResearchWorkRepository _repository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ResearchWorkService> _logger;
    private readonly IMapper _mapper;

    public ResearchWorkService(
        IResearchWorkRepository repository,
        IPublicationRepository publicationRepository,
        UserManager<ApplicationUser> userManager,
        ILogger<ResearchWorkService> logger,
        IMapper mapper)
    {
        _repository = repository;
        _publicationRepository = publicationRepository;
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<ServiceResult<long>> AddResearchWorkAsync(NewResearchWorkDto newResearchWorkDto)
    {
        var result = new ServiceResult<long>();
        const string commonError = "Failed to add a research work";
        try
        {
            var researchWork = _mapper.Map<ResearchWork>(newResearchWorkDto);
            researchWork.Performers = new List<ApplicationUser>();
            foreach (var performerId in newResearchWorkDto.PerformerIds)
            {
                researchWork.Performers.Add(await _userManager.FindByIdAsync(performerId));
            }

            if (newResearchWorkDto.PublicationIds is not null)
            {
                researchWork.Publications = new List<Publication>();
                foreach (var publicationId in newResearchWorkDto.PublicationIds)
                {
                    researchWork.Publications.Add(await _publicationRepository.GetAsync(publicationId));
                }
            }

            var added = await _repository.AddAsync(researchWork);
            if (added)
            {
                result.Container = researchWork.Id;
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

    public async Task<ResearchWorkDto?> GetAsync(long id)
    {
        try
        {
            var researchWork = await _repository.GetAsync(id);
            return _mapper.Map<ResearchWorkDto>(researchWork);
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
        const string commonError = "Failed to delete the research work!";
        try
        {
            var researchWork = await _repository.GetAsync(id);
            if (researchWork is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("Research work does not exist");
            }
            else
            {
                var deleted = await _repository.RemoveAsync(researchWork);
                if (!deleted)
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