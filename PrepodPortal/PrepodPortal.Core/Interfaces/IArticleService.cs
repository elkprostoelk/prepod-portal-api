using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IArticleService
{
    Task<ServiceResult<ICollection<long>>> AddArticlesWithCsvFileAsync(AddPublicationsWithCsvDto addPublicationsWithCsvDto);
    Task<ServiceResult<long>> AddArticleAsync(NewArticleDto newArticleDto);
}