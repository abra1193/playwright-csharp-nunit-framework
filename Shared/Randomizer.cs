using System;
using System.Linq;

namespace qa_automation_exercise__mejiabritoabraham.Shared
{
    public static class Randomizer
    {
        private const string LowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string AlphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly Random Random = new();

        private static string GenerateRandomString(int length = 8)
        {
            const string chars = LowercaseCharacters + UppercaseCharacters;
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomEmail(string domain = "@testemail.com")
        {
            var username = GenerateRandomString();
            return username + domain;
        }

        public static string GenerateRandomProductId()
        {
            return new string(Enumerable.Repeat(AlphanumericCharacters, 10)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}