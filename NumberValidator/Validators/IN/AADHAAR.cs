using System;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators
{
    public class AadhaarValidator
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

    class Program
    {
        static void Main(string[] args)
        {
            var validator = new AadhaarValidator();

            Console.WriteLine("Enter an Aadhaar number:");
            var aadhaarNumber = Console.ReadLine();

            if (validator.IsValid(aadhaarNumber))
            {
                Console.WriteLine("Aadhaar number is valid.");
            }
            else
            {
                Console.WriteLine("Aadhaar number is invalid.");
            }
        }
    }
}
