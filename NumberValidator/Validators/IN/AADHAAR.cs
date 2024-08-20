using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators
{
    public class AadhaarValidator : IValidator
    {
        private static readonly Regex AadhaarRegex = new(@"^[1-9][0-9]{11}$");

        public bool IsValid(string aadhaar)
        {
            try
            {
                Validate(aadhaar);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Validate(string aadhaar)
        {
            // Check if aadhaar is null
            if (aadhaar == null)
            {
                throw new ArgumentException("Aadhaar number is invalid.");
            }

            // Check length and regex pattern
            if (aadhaar.Length != 12 || !AadhaarRegex.IsMatch(aadhaar))
            {
                throw new ArgumentException("Aadhaar number is invalid.");
            }
        }
    }
}
