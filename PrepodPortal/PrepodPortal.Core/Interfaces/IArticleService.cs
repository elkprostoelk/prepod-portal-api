using PrepodPortal.Common.DTO;

namespace PrepodPortal.Core.Interfaces;

public interface IArticleService
{
    Task<bool> AddArticlesWithCsvFileAsync(AddPublicationsWithCsvDto addPublicationsWithCsvDto);
    Task<bool> AddArticleAsync(NewArticleDto newArticleDto);
}