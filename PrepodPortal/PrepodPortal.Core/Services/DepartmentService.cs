using Microsoft.Extensions.Logging;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(
        IDepartmentRepository repository,
        ILogger<DepartmentService> logger)
    {
        _repository = repository;
        _logger = logger;
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
}