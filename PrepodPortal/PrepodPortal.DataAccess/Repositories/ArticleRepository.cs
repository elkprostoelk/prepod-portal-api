using Microsoft.EntityFrameworkCore;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.DataAccess.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly PrepodPortalDbContext _context;

    public ArticleRepository(
        PrepodPortalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> AddAsync(Article article)
    {
        await _context.Articles.AddAsync(article);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Article?> GetAsync(Article article) =>
        await _context.Articles
            .AsNoTracking()
            .Include(a => a.Authors)
            .FirstOrDefaultAsync(a =>
                a.ArticleType == article.ArticleType
                && a.EditionName == article.EditionName
                && a.Title == article.Title
                && a.Tome == article.Tome
                && a.Number == article.Number
                && a.PageNumbers == article.PageNumbers
                && a.PublishedLocation == article.PublishedLocation
                && a.PublishedYear == article.PublishedYear);

    public async Task<bool> UpdateAsync(Article article)
    {
        _context.Articles.Update(article);
        return await _context.SaveChangesAsync() > 0;
    }
}