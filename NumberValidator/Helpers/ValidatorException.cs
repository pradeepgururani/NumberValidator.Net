using System;

namespace NumberValidator.Helpers
{
    [Serializable]
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException() 
            : base("The number has an invalid format.")
        { }
    }

    [Serializable]
    public class InvalidChecksumException : Exception
    {
        public InvalidChecksumException()
            : base("The number's checksum or check digit is invalid.")
        { }   
    }

    [Serializable]
    public class InvalidLengthException : Exception
    {
        public InvalidLengthException()
            : base("The number has an invalid length.") 
        { }
    }

    /// <summary>
    /// Some part of the number refers to some external entity like a country
    /// code, a date or a predefined collection of values. The number contains
    /// some invalid reference.
    /// </summary>
    [Serializable]
    public class InvalidComponentException : Exception
    {
        public InvalidComponentException() 
            : base("One of the parts of the number are invalid or unknown.")
        { }
    }
}