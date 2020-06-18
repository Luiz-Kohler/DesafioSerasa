using System;
using System.Text.RegularExpressions;

namespace Domain.Validators.ExtensionsValidators
{
    public static class Extensions
    {
        public static bool IsValid(this DateTime date)
        {
            var result = false;

            if (date < DateTime.UtcNow)
                result = true;

            return result;
        }

        public static string RemoveExtraWhiteSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ").Trim();
        }
    }
}
