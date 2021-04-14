using System;
using System.Globalization;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.NO
{
    public class IBAN : IValidator
    {
        public bool IsValid(string iban)
        {
            try
            {
                Validate(iban);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string ToKontonr(string iban)
        {
            

            return (iban.Substring(4, 11));
        }

        public void Validate(string iban)
        {
            
        
            if (iban.Length != 15)
            {
                throw new InvalidLengthException();
            }

            if (!iban.StartsWith("NO"))
            {
                throw new InvalidComponentException();
            }

            if(!ToKontonr(iban).IsDigits() ||
                ToKontonr(iban).Length != 11)
            {
                throw new InvalidFormatException();
            }
        }
    }
}