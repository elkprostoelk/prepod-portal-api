using Microsoft.AspNetCore.Http;

namespace PrepodPortal.Common.DTO;

public class AddPublicationsWithCsvDto
{
    public IFormFile CsvFile { get; set; }
    
    public string UserId { get; set; }
}