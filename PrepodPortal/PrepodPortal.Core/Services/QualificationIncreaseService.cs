using AutoMapper;
using Microsoft.Extensions.Logging;
using PrepodPortal.Common.DTO;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services
{
    public class QualificationIncreaseService : IQualificationIncreaseService
    {
        private readonly IQualificationIncreaseRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<QualificationIncreaseService> _logger;

        public QualificationIncreaseService(
            IQualificationIncreaseRepository repository,
            IMapper mapper,
            ILogger<QualificationIncreaseService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ICollection<QualificationIncreaseDto>> GetAllAsync(string userId)
        {
            var result = new List<QualificationIncreaseDto>();

            try
            {
                var qualificationIncreases = await _repository.GetAllAsync(userId);
                return _mapper.Map<ICollection<QualificationIncreaseDto>>(qualificationIncreases);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An exception occured when executing the service");
            }

            return result;
        }
    }
}
