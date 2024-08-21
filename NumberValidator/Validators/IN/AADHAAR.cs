using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators
{
    public class AadhaarValidator : IValidator
    {
        private static readonly Regex AadhaarRegex = new Regex(@"^[1-9][0-9]{11}$");

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
            if (string.IsNullOrEmpty(aadhaar) || !AadhaarRegex.IsMatch(aadhaar))
            {
                throw new ArgumentException("Aadhaar number is invalid.");
            }
        }
    }
}
