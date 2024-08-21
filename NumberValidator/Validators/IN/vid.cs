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
        private static readonly Regex VidRegex = new Regex(@"^[2-9][0-9]{15}$");

        public static string Compact(string number)
        {
            return number?.Replace(" ", "").Replace("-", "").Trim() ?? string.Empty;
        }

        public static string Validate(string number)
        {
            var compactedNumber = Compact(number);

            if (compactedNumber.Length != 16 || !VidRegex.IsMatch(compactedNumber) || IsPalindrome(compactedNumber))
            {
                throw new InvalidFormatException();
            }

            Verhoeff.Validate(compactedNumber);
            return compactedNumber;
        }

        public static bool IsValid(string number)
        {
            try
            {
                Validate(number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Format(string number)
        {
            var compactedNumber = Compact(number);
            return $"{compactedNumber.Substring(0, 4)} {compactedNumber.Substring(4, 4)} {compactedNumber.Substring(8, 4)} {compactedNumber.Substring(12)}";
        }

        public static string Mask(string number)
        {
            var compactedNumber = Compact(number);
            return $"XXXX XXXX XXXX {compactedNumber.Substring(12)}";
        }

        private static bool IsPalindrome(string number)
        {
            var reversed = new string(number.Reverse().ToArray());
            return number == reversed;
        }

        public static string GenerateValidVid()
        {
            string vid;
            do
            {
                vid = GenerateRandomVid();
            } while (IsPalindrome(vid) || !IsValid(vid));
            return vid;
        }

        private static string GenerateRandomVid()
        {
            var rand = new Random();
            var baseNumber = rand.Next(2, 10).ToString() +
                string.Concat(Enumerable.Range(0, 14).Select(_ => rand.Next(0, 10).ToString()));

            var checksum = Verhoeff.CalculateChecksum(baseNumber);
            return baseNumber + checksum;
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
            {
                throw new InvalidChecksumException();
            }
        }

        public static int CalculateChecksum(string number)
        {
            var reversedDigits = number.Reverse().Select(digit => int.Parse(digit.ToString())).ToArray();
            int c = 0;

            for (int i = 0; i < reversedDigits.Length; i++)
            {
                c = D[c, P[(i + 1) % 8, reversedDigits[i]]];
            }

            return Inverse[c];
        }
    }
}
