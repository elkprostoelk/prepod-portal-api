using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using PrepodPortal.Common.DTO;
using PrepodPortal.Common.Enums;
using PrepodPortal.Core.Interfaces;
using PrepodPortal.DataAccess.Entities;
using PrepodPortal.DataAccess.Interfaces;

namespace PrepodPortal.Core.Services;

public class ArticleService : IArticleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ArticleService> _logger;
    private readonly IArticleRepository _repository;
    private readonly IMapper _mapper;

    public ArticleService(
        UserManager<ApplicationUser> userManager,
        ILogger<ArticleService> logger,
        IArticleRepository repository,
        IMapper mapper)
    {
        _userManager = userManager;
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<bool> AddArticlesWithCsvFileAsync(AddPublicationsWithCsvDto addPublicationsWithCsvDto)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(addPublicationsWithCsvDto.UserId);
            if (user is null)
            {
                return false;
            }
            
            var userFolderPath = $"{Environment.CurrentDirectory}/Users/{user.Email}";
            var csvFilePath = $"{userFolderPath}/{addPublicationsWithCsvDto.CsvFile.FileName}";
            await using (var stream = new FileStream(userFolderPath, FileMode.Create))
            {
                await addPublicationsWithCsvDto.CsvFile.CopyToAsync(stream);
            }

            using var parser = new TextFieldParser(csvFilePath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadFields(); // skipping the headers' line
            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                
                var article = new Article
                {
                    Authors = new List<ApplicationUser> { user },
                    ArticleType = ArticleType.WoSOrScopusForeign,
                    Title = fields[1],
                    EditionName = fields[2],
                    Tome = fields[3],
                    Number = fields[4],
                    PageNumbers = fields[5],
                    PublishedLocation = fields[7],
                    PublishedYear = String.IsNullOrEmpty(fields[6])
                        ? null : Convert.ToInt32(fields[6])
                };

                var existingArticle = await _repository.GetAsync(article);
                if (existingArticle is null)
                {
                    var added = await _repository.AddAsync(article);
                    if (!added) return false;
                }
                else
                {
                    existingArticle.Authors?.Add(user);
                    var updated = await _repository.UpdateAsync(existingArticle);
                    if (!updated) return false;
                }
            }

            File.Delete(csvFilePath);
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }

    public async Task<bool> AddArticleAsync(NewArticleDto newArticleDto)
    {
        try
        {
            var article = _mapper.Map<Article>(newArticleDto);
            article.Authors = new List<ApplicationUser>();
            foreach (var id in newArticleDto.AuthorsIds)
            {
                article.Authors.Add(await _userManager.FindByIdAsync(id));
            }
            return await _repository.AddAsync(article);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "An exception occured when executing the service");
            return false;
        }
    }
}