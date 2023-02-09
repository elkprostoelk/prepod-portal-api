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
    
    public async Task<ServiceResult<long>> AddAcademicDegreeAsync(NewAcademicDegreeDto newAcademicDegreeDto)
    {
        const string commonError = "Failed to add an academic degree!";
        var result = new ServiceResult<long>();
        try
        {
            var academicDegree = _mapper.Map<AcademicDegree>(newAcademicDegreeDto);
            var added = await _repository.AddAsync(academicDegree);
            result.IsSuccessful = added;
            if (added)
            {
                result.Container = academicDegree.Id;
            }
            else
            {
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

    public async Task<AcademicDegreeDto?> GetAsync(long id)
    {
        try
        {
            var academicDegree = await _repository.GetAsync(id);
            return academicDegree is null
                ? null
                : _mapper.Map<AcademicDegreeDto>(academicDegree);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return null;
        }
    }

    public async Task<ServiceResult> DeleteAsync(long id)
    {
        const string commonError = "Failed to delete an academic degree!";
        var result = new ServiceResult();
        try
        {
            var academicDegree = await _repository.GetAsync(id);
            if (academicDegree is null)
            {
                result.IsSuccessful = false;
                result.Errors.Add("The academic degree does not exist!");
            }
            else
            {
                var isRemoved = await _repository.RemoveAsync(academicDegree);
                result.IsSuccessful = isRemoved;
                if (!isRemoved)
                {
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