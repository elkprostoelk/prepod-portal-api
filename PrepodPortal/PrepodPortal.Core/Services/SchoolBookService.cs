using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class SchoolBookService : ISchoolBookService
{
    private readonly ISchoolBookRepository _repository;
    private readonly ILogger<SchoolBookService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public SchoolBookService(
        ISchoolBookRepository repository,
        ILogger<SchoolBookService> logger,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<ServiceResult<long>> AddSchoolBookAsync(NewSchoolBookDto newSchoolBookDto)
    {
        var result = new ServiceResult<long>();
        const string commonError = "Failed to delete a publication";
        try
        {
            var schoolBook = _mapper.Map<SchoolBook>(newSchoolBookDto);
            schoolBook.Authors = new List<ApplicationUser>();
            foreach (var id in newSchoolBookDto.AuthorsIds)
            {
                schoolBook.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            var added = await _repository.AddAsync(schoolBook);
            if (added)
            {
                result.Container = schoolBook.Id;
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