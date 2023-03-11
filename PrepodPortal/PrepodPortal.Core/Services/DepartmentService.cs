using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILogger<DepartmentService> _logger;
    private readonly IMapper _mapper;

    public DepartmentService(
        IDepartmentRepository repository,
        ILogger<DepartmentService> logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        try
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    public async Task<ICollection<DepartmentDto>> GetAllAsync()
    {
        try
        {
            var departments = await _repository.GetAllAsync();
            return _mapper.Map<ICollection<DepartmentDto>>(departments);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return new List<DepartmentDto>();
        }
    }
}