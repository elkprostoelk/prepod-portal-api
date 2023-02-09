using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class LectureThesesService : ILectureThesesService
{
    private readonly ILectureThesesRepository _repository;
    private readonly ILogger<LectureThesesService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public LectureThesesService(
        ILectureThesesRepository repository,
        ILogger<LectureThesesService> logger,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<ServiceResult<long>> AddLectureThesesAsync(NewLectureThesesDto newLectureThesesDto)
    {
        var result = new ServiceResult<long>();
        const string commonError = "Failed to add lecture theses";
        try
        {
            var lectureTheses = _mapper.Map<LectureTheses>(newLectureThesesDto);
            lectureTheses.Authors = new List<ApplicationUser>();
            foreach (var id in newLectureThesesDto.AuthorsIds)
            {
                lectureTheses.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            var added = await _repository.AddAsync(lectureTheses);
            if (added)
            {
                result.Container = lectureTheses.Id;
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