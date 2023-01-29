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
    
    public async Task<bool> AddLectureThesesAsync(NewLectureThesesDto newLectureThesesDto)
    {
        try
        {
            var monograph = _mapper.Map<LectureTheses>(newLectureThesesDto);
            monograph.Authors = new List<ApplicationUser>();
            foreach (var id in newLectureThesesDto.AuthorsIds)
            {
                monograph.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            return await _repository.AddAsync(monograph);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}