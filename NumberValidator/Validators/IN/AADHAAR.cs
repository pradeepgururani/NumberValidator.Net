using System;
using System.Linq;
using NumberValidator.Helpers;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.IN
{
    public class Aadhaar : IValidator
    {
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
            String aadhaarPattern = @"^[2-9][0-9]{11}$";

            if (aadhaar.Length != 12)
            {
                throw new InvalidLengthException();
            }

            if (aadhaar.SequenceEqual(aadhaar.Reverse()))
            {
                throw new InvalidFormatException();
            }

            Regex aadhaarRegex = new Regex(aadhaarPattern);
            if (!aadhaarRegex.IsMatch(aadhaar))
            {
                throw new InvalidFormatException();
            }
        }
    }
}