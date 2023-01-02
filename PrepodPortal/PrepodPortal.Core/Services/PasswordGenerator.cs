using System.Security.Cryptography;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.Core.Services;

public class PasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword(int length, int requiredNumberOfNonAlphanumericCharacters, int requiredNumberOfDigits = 1)
    {
        var punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        var digits = "1234567890".ToCharArray();
        if (length is < 1 or > 128)
        {
            throw new ArgumentException("Wrong password length!");
        }

        if (requiredNumberOfNonAlphanumericCharacters > length
            || requiredNumberOfNonAlphanumericCharacters < 0)
        {
            throw new ArgumentException(
                "Non-alphanumeric characters` count is more than a password`s length!");
        }

        var random = new Random();
        var passwordArray = new char[length];
        for (var i = 0; i < length; i++)
        {
            passwordArray[i] = letters[random.Next(letters.Length)];
        }

        var randomDigitsIndexes = new int[requiredNumberOfDigits];
        for (var i = 0; i < randomDigitsIndexes.Length; i++)
        {
            int currentRandomIndex;
            do
            {
                currentRandomIndex = random.Next(passwordArray.Length);
            } while (randomDigitsIndexes.Contains(currentRandomIndex));
            randomDigitsIndexes[i] = currentRandomIndex;
        }
        randomDigitsIndexes.ToList().ForEach(index =>
            passwordArray[index] = digits[random.Next(digits.Length)]);

        var randomNonAlphanumsIndexes = new int[requiredNumberOfNonAlphanumericCharacters];
        for (var i = 0; i < randomNonAlphanumsIndexes.Length; i++)
        {
            int currentRandomIndex;
            do
            {
                currentRandomIndex = random.Next(passwordArray.Length);
            } while (randomDigitsIndexes.Contains(currentRandomIndex)
                        || randomNonAlphanumsIndexes.Contains(currentRandomIndex));
            randomNonAlphanumsIndexes[i] = currentRandomIndex;
        }
        randomNonAlphanumsIndexes.ToList().ForEach(index =>
            passwordArray[index] = punctuations[random.Next(punctuations.Length)]);

        return new string(passwordArray);
    }
}