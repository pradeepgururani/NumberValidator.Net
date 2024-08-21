using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators
{
    public class AadhaarValidator
    {
        private static readonly Regex AadhaarRegex = new Regex(@"^[1-9][0-9]{11}$");

        public bool IsValid(string aadhaar) => TryValidate(aadhaar);

        public void Validate(string aadhaar)
        {
            if (!TryValidate(aadhaar))
            {
                throw new ArgumentException("Aadhaar number is invalid.");
            }
        }

        private bool TryValidate(string aadhaar) => !string.IsNullOrEmpty(aadhaar) && AadhaarRegex.IsMatch(aadhaar);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var validator = new AadhaarValidator();
            Console.WriteLine(validator.IsValid("123456789012"));
            Console.WriteLine(validator.IsValid("12345678901"));
            try
            {
                validator.Validate("123456789012");
                Console.WriteLine("Aadhaar number is valid.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}