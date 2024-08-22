using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.IN
{
    public class AadhaarValidator : IValidator
    {
        private static readonly Regex aadhaarRegex = new Regex(@"^\d{12}$");

        public bool IsValid(string aadhaar)
        {
            return TryValidate(aadhaar);
        }
        public void Validate(string aadhaar)
        {
            if (!TryValidate(aadhaar))
            {
                throw new FormatException("Aadhaar number is invalid.");
            }
        }
        private bool TryValidate(string aadhaar)
        {
            if (string.IsNullOrWhiteSpace(aadhaar))
            {
                return false;
            }
            return aadhaarRegex.IsMatch(aadhaar);
        }
    }
}