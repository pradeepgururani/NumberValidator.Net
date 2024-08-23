using System;

namespace NumberValidator.Helpers
{
    public class Luhn
    {
        public static void Validate(string input)
        {
            var sum = 0;
            bool alternate = false;

            for (var i = input.Length - 1; i >= 0; i--)
            {
                var n = int.Parse(input[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }

                sum += n;
                alternate = !alternate;
            }

            if (sum % 10 != 0)
            {
                throw new InvalidChecksumException();
            }
        }
    }
}
