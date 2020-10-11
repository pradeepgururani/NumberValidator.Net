﻿using System.Text.RegularExpressions;

namespace NumberValidator.Helpers
{
    public static class StringExtensions
    {
        public static string Clean(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            
            var removeAllNonDigits = new Regex(@"\D");

            return removeAllNonDigits.Replace(input, "");
        }

        public static bool IsDigit(this string input)
        {
            var digitsRegularExpression = new Regex("^[0-9]+$");

            return digitsRegularExpression.Match(input).Success;
        }
    }
}