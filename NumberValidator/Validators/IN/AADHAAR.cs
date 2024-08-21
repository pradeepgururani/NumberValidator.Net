using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators
{
    public class AadhaarValidator
    {
        // Regex pattern for exactly 12 digits
        private static readonly Regex AadhaarRegex = new Regex(@"^\d{12}$");

        // Determines if the Aadhaar number is valid
        public bool IsValid(string aadhaar)
        {
            return TryValidate(aadhaar);
        }

        // Validates the Aadhaar number and throws an exception if invalid
        public void Validate(string aadhaar)
        {
            if (!TryValidate(aadhaar))
            {
                throw new FormatException("Aadhaar number is invalid.");
            }
        }

        // Checks if the Aadhaar number is valid based on length and format
        private bool TryValidate(string aadhaar)
        {
            // Null or empty Aadhaar numbers are invalid
            if (string.IsNullOrWhiteSpace(aadhaar))
            {
                return false;
            }

            // Check if the Aadhaar number matches the regex pattern
            return AadhaarRegex.IsMatch(aadhaar);
        }
    }
}
