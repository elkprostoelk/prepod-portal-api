namespace PrepodPortal.Core.Interfaces;

public interface IPublicationService
{
    Task<bool> ExistsAsync(long id);
    Task<bool> DeleteAsync(long id);
}