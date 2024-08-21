


/*DNI (Documento Nacional de Identidad, Argentinian national identity nr.).

The DNI number is the number that appears on the Argentinian national
identity document and is used to identify citizen and foreigners residing in
the country.

Each DNI contains 7 or 8 digits

*/




using System;
using System.Globalization;
using NumberValidator.Helpers;
using System.Text.RegularExpressions;
using NumberValidator.Validators.PAN;

namespace NumberValidator.Validators.AR
{
     public class DNI : IValidator
    {
        public bool IsValid(string dni)
        {
            try
            {
                Validate(dni);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public void  Validate(string dni)
        {
          dni=dni.Clean();

            if (dni.Length != 7  || dni.Length != 8)
            { 
                throw new InvalidLengthException();
            }
            if (!dni.IsDigits())
            {
                throw new InvalidFormatException();
             }
         
              
           
        }

        
     }
}

