using System;
using System.Globalization;
using NumberValidator.Helpers;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.IN
{
    public class PAN : IValidator
    {
        public bool IsValid(string pan)
        {
            try
            {
                Validate(pan);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Validate(string pan)
        {
            pan = pan.Compact();

            if (pan.Length != 10)
            {
                throw new InvalidLengthException();
            }

            if (pan.Substring(5, 4) == "0000")
            {
                throw new InvalidComponentException();
            }

            if (!Regex.IsMatch(pan, @"^[A-Z]{5}[0-9]{4}[A-Z]$"))
            {
                throw new InvalidFormatException();
            }
        }
    }
}