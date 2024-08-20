using System;
using System.Globalization;
using NumberValidator.Helpers;
using NumberValidator.Validators.AD;
using System.Text.RegularExpressions;


namespace NumberValidator.Validators.PAN
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
            string panPattern = @"^[A-Z]{5}\d{4}[A-Z]$";
            if (pan.Length != 10)
            {
                throw new InvalidLengthException();
            }
            if (pan.Substring(4, 4) == "0000") // Change index from 5 to 4
            {
                throw new InvalidComponentException();
            }
            Regex panRegex = new Regex(panPattern);
            if (!panRegex.IsMatch(pan))
            {
                throw new InvalidFormatException();
            }
        }
    }
}
