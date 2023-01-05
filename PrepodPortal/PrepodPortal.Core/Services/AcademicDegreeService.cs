using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class AcademicDegreeService : IAcademicDegreeService
{
    private readonly ILogger<AcademicDegreeService> _logger;
    private readonly IMapper _mapper;
    private readonly IAcademicDegreeRepository _repository;

    public AcademicDegreeService(
        ILogger<AcademicDegreeService> logger,
        IMapper mapper,
        IAcademicDegreeRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<bool> AddAcademicDegreeAsync(NewAcademicDegreeDto newAcademicDegreeDto)
    {
        try
        {
            var academicDegree = _mapper.Map<AcademicDegree>(newAcademicDegreeDto);
            return await _repository.AddAsync(academicDegree);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}