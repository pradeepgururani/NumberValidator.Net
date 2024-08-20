using System;
using System.Globalization;
using NumberValidator.Helpers;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NumberValidator.Validators.IN;


namespace NumberValidator.Validators.IN
{
    public class GSTIN : IValidator
    {
        private static readonly HashSet<string> StateCodes = new HashSet<string>
        {
            "01" , "02" ,"03" ,"04" ,"05" ,"06" ,"07" , "08" ,
            "09" ,"10" ,"11" ,"12" ,"13" ,"14" ,"15" ,"16" ,"17" ,
            "18" ,"19" ,"20" ,"21" ,"22" ,"23" ,"24" ,"25" ,"26" ,
            "27" ,"28" ,"29" ,"30" ,"31" ,"32" ,"33" ,"34" ,"35" ,
            "36" ,"37"
        };

        private readonly PAN panValidator = new PAN();

        public bool IsValid(string gst)
        {
            try
            {
                Validate(gst);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Validate(string gst)
        {
            gst = gst.RemoveSpace();

            string gstPattern = @"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z][0-9A-Z]{3}$";

            if (gst.Length != 15)
            {
                throw new InvalidLengthException();
            }

            if (!StateCodes.Contains(gst.Substring(0, 2)) || gst[12] == 0 || gst[13] != 'Z')
            {
                throw new InvalidFormatException();
            }

            Regex gstRegex = new Regex(gstPattern);

            if (!gstRegex.IsMatch(gst))
            {
                throw new InvalidFormatException();
            }

            string panNumber = gst.Substring(2, 10);

            panValidator.Validate(panNumber);
        }
    }
}