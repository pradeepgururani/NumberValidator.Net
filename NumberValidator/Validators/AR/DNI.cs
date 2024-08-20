
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

