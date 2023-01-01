namespace PrepodPortal.Core.Interfaces;

public interface IPasswordGenerator
{
    public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters);
}