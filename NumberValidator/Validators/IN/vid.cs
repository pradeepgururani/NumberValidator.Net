using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace VidHandling
{
    public class InvalidLengthException : Exception { }
    public class InvalidFormatException : Exception { }
    public class InvalidChecksumException : Exception { }

    public static class Vid
    {
        private static readonly Regex _vidRegex = new(@"^[2-9][0-9]{15}$");

        public static string Compact(string number) =>
            number?.Replace(" ", "").Replace("-", "").Trim() ?? string.Empty;

        public static string Validate(string number)
        {
            number = Compact(number);

            if (number.Length != 16 || !_vidRegex.IsMatch(number) || IsPalindrome(number))
                throw new InvalidFormatException();

            Verhoeff.Validate(number);

            return number;
        }

        public static bool IsValid(string number)
        {
            try
            {
                return !string.IsNullOrEmpty(Validate(number));
            }
            catch
            {
                return false;
            }
        }

        public static string Format(string number)
        {
            number = Compact(number);
            return $"{number.Substring(0, 4)} {number.Substring(4, 4)} {number.Substring(8, 4)} {number.Substring(12)}";
        }

        public static string Mask(string number)
        {
            number = Compact(number);
            return $"XXXX XXXX XXXX {number.Substring(12)}";
        }

        private static bool IsPalindrome(string number) =>
            number == new string(number.Reverse().ToArray());

        public static string GenerateValidVid()
        {
            string vid;
            do
            {
                var rand = new Random();
                var baseNumber = rand.Next(2, 10).ToString() + new string(Enumerable.Range(0, 14).Select(_ => rand.Next(0, 10).ToString()[0]).ToArray());
                var checksum = Verhoeff.Validate(baseNumber);
                vid = baseNumber + checksum.ToString();
            } while (IsPalindrome(vid));
            return vid;
        }
    }

    public static class Verhoeff
    {
        private static readonly int[,] D = {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
            {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
            {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
            {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
            {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
            {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
            {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
            {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
            {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
        };

        private static readonly int[,] P = {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 5, 7, 6, 4, 8, 9, 0, 3, 2},
            {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
            {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
            {9, 4, 5, 3, 1, 2, 7, 8, 6, 0},
            {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
            {2, 7, 9, 3, 8, 1, 6, 4, 5, 0},
            {7, 0, 4, 6, 9, 3, 1, 2, 5, 8}
        };

        private static readonly int[] Inverse = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

        public static void Validate(string number)
        {
            if (CalculateChecksum(number) != 0)
                throw new InvalidChecksumException();
        }

        public static int CalculateChecksum(string number)
        {
            var reversedDigits = number.Reverse().Select(digit => int.Parse(digit.ToString())).ToArray();
            var c = 0;

            for (var i = 0; i < reversedDigits.Length; i++)
                c = D[c, P[i % 8, reversedDigits[i]]];

            return Inverse[c];
        }
    }
}
