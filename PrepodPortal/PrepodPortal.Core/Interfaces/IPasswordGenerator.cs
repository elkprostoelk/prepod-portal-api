namespace PrepodPortal.Core.Interfaces;

public interface IPasswordGenerator
{
    string GeneratePassword(int length, int requiredNumberOfNonAlphanumericCharacters,
        int requiredNumberOfDigits = 1);
}