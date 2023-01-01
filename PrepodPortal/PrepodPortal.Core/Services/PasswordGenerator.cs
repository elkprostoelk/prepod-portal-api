using System.Security.Cryptography;
using PrepodPortal.Core.Interfaces;

namespace PrepodPortal.Core.Services;

public class PasswordGenerator : IPasswordGenerator
{
    public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
    {
        var punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
        if (length is < 1 or > 128)
        {
            throw new ArgumentException("Wrong password length!");
        }

        if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
        {
            throw new ArgumentException("Non-alphanumeric characters` count is more than a password`s length!");
        }

        string text;
        do
        {
            var array2 = new char[length];
            var num = 0;
            var array = RandomNumberGenerator.GetBytes(length);
            for (var i = 0; i < length; i++)
            {
                var num2 = array[i] % 87;
                switch (num2)
                {
                    case < 10:
                        array2[i] = (char)(48 + num2);
                        continue;
                    case < 36:
                        array2[i] = (char)(65 + num2 - 10);
                        continue;
                    case < 62:
                        array2[i] = (char)(97 + num2 - 36);
                        continue;
                    default:
                        array2[i] = punctuations[num2 - 62];
                        num++;
                        break;
                }
            }

            if (num < numberOfNonAlphanumericCharacters)
            {
                var random = new Random();
                for (var j = 0; j < numberOfNonAlphanumericCharacters - num; j++)
                {
                    int num3;
                    do
                    {
                        num3 = random.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(array2[num3]));
                    array2[num3] = punctuations[random.Next(0, punctuations.Length)];
                }
            }

            text = new string(array2);
        }
        while (IsDangerousString(text, out _));
        return text;
    }
    
    private static bool IsDangerousString(string s, out int matchIndex)
    {
        var startingChars = new[] { '<', '&' };
        matchIndex = 0;
        var startIndex = 0;
        while (true)
        {
            var num = s.IndexOfAny(startingChars, startIndex);
            if (num < 0)
            {
                return false;
            }

            if (num == s.Length - 1)
            {
                break;
            }

            matchIndex = num;
            switch (s[num])
            {
                case '<':
                    if (IsAtoZ(s[num + 1]) || s[num + 1] == '!' || s[num + 1] == '/' || s[num + 1] == '?')
                    {
                        return true;
                    }

                    break;
                case '&':
                    if (s[num + 1] == '#')
                    {
                        return true;
                    }

                    break;
            }

            startIndex = num + 1;
        }

        return false;
    }
    
    private static bool IsAtoZ(char c)
    {
        if (c is < 'a' or > 'z')
        {
            if (c >= 'A')
            {
                return c <= 'Z';
            }

            return false;
        }

        return true;
    }
}