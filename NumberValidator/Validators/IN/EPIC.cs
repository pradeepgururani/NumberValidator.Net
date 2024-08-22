/*EPIC (Electoral Photo Identity Card, Indian Voter ID).

The Electoral Photo Identity Card (EPIC) is an identity document issued by
the Election Commission of India (ECI) only to the India citizens who have
reached the age of 18.

Each EPIC contains an unique 10 digit alphanumeric identifier known as EPIC
number or Voter ID number.

Every EPIC number begins with a Functional Unique Serial Number (FUSN), a 3
letter unique identifier for each Assembly Constituency. FUSN is followed by
a 6 digit serial number and 1 check digit of the serial number calculated
using Luhn algorithm.*/

using System.Text.RegularExpressions;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.IN
{
    public class EPIC : IValidator
    {
        public bool IsValid(string epic)
        {
            try
            {
                Validate(epic);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Validate(string epic)
        {
            var epicPattern = @"^[A-Z]{3}[0-9]{7}$";

            epic = epic.RemoveSpace();

            if (epic.Length != 10)
            {
                throw new InvalidLengthException();
            }

            Regex epicReg = new Regex(epicPattern);

            if (!epicReg.IsMatch(epic))
            {
                throw new InvalidFormatException();
            }
         
            Luhn.Validate(epic.Substring(3));
        }

    }
}
