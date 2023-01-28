using PrepodPortal.DataAccess.Entities;

namespace PrepodPortal.DataAccess.Interfaces;

public interface IArticleRepository
{
    Task<bool> AddAsync(Article article);
    Task<Article?> GetAsync(Article article);
    Task<bool> UpdateAsync(Article article);
}