using NumberValidator.Helpers;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.AT

{
    public class BUSINESSID : IValidator
    {
        

        public bool IsValid(string businessid)
        {
           
            try
            {
                Validate(businessid);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Validate(string businessid)
        {
            // var bid = new Regex("^[0-9]+[a-z]$");
            //businessid = businessid.RemoveSpecialCharacthers().Replace("FN", string.Empty).Replace("fn", string.Empty);
            if (!Regex.IsMatch(businessid, "^[0-9]+[a-z]$"))
            {
                throw new InvalidFormatException();
            }
            return;
        }
    }
}
