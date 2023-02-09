using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class MonographService : IMonographService
{
    private readonly IMonographRepository _repository;
    private readonly ILogger<MonographService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public MonographService(
        IMonographRepository repository,
        ILogger<MonographService> logger,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<ServiceResult<long>> AddMonographAsync(NewMonographDto newMonographDto)
    {
        var result = new ServiceResult<long>();
        const string commonError = "Failed to add a monograph";
        try
        {
            var monograph = _mapper.Map<Monograph>(newMonographDto);
            monograph.Authors = new List<ApplicationUser>();
            foreach (var id in newMonographDto.AuthorsIds)
            {
                monograph.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            var added = await _repository.AddAsync(monograph);
            if (added)
            {
                result.Container = monograph.Id;
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