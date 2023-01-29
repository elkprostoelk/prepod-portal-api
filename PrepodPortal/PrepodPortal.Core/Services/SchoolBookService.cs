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
    
    public async Task<bool> AddSchoolBookAsync(NewSchoolBookDto newSchoolBookDto)
    {
        try
        {
            var schoolBook = _mapper.Map<SchoolBook>(newSchoolBookDto);
            schoolBook.Authors = new List<ApplicationUser>();
            foreach (var id in newSchoolBookDto.AuthorsIds)
            {
                schoolBook.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            return await _repository.AddAsync(schoolBook);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}