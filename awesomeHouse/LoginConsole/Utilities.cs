using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LoginComponent
{
    class Utilities
    {
        internal static void CheckEmail(string email)
        {
            if (!new EmailAddressAttribute().IsValid(email))
                throw new Exception("Invalid email format");

            if (!Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new Exception("Invalid characters in the email");
            }
        }

        internal static void CheckPassword(string password, string confirmPassword)
        {
            CheckPasswordLength(password);
            CheckIfPasswordsAreIdentical(password, confirmPassword);
            CheckIfPasswordContainsLettersAndNumbers(password);
        }

        private static void CheckIfPasswordContainsLettersAndNumbers(string password)
        {
            Regex regex = new Regex(@"^.*(?=.{4,10})(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).*$");
            if (!regex.Match(password).Success)
                throw new Exception("Password needs at least one small letter, one capitalized letter and one number");
        }

        static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static void CheckIfPasswordsAreIdentical(string password, string confirmPassword)
        {
            if (password != confirmPassword)
                throw new Exception("Passwords are not identical");
        }

        private static void CheckPasswordLength(string password)
        {
            if (password.Length < 6)
                throw new Exception("Password too short");

            if (password.Length > 16)
                throw new Exception("Password too long");
        }

        internal static string HashPassword(string password)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(password));
            return Encoding.Default.GetString(hash);
        }


    }
}
