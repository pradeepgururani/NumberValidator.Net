using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace NumberValidator
{
    public static class Vid
    {
        private static readonly Regex vidRegex = new Regex(@"^[2-9][0-9]{15}$");

        public static string Clear(string number) =>
            number?.Replace(" ", "").Replace("-", "").Trim() ?? string.Empty;

        public static string Validate(string number)
        {
            var cleanedNumber = Clear(number);

            if (cleanedNumber.Length != 16 || !vidRegex.IsMatch(cleanedNumber) || IsPalindrome(cleanedNumber))
            {
                throw new InvalidFormatException();
            }

            VerhoeffAlgorithm.Validate(cleanedNumber);
            return cleanedNumber;
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
            var cleanedNumber = Clear(number);
            return $"{cleanedNumber.Substring(0, 4)} {cleanedNumber.Substring(4, 4)} {cleanedNumber.Substring(8, 4)} {cleanedNumber.Substring(12)}";
        }

        public static string Mask(string number)
        {
            var cleanedNumber = Clear(number);
            return $"XXXX XXXX XXXX {cleanedNumber.Substring(12)}";
        }

        private static bool IsPalindrome(string number) =>
            number == new string(number.Reverse().ToArray());

        public static string GenerateValidVid()
        {
            var validVid = string.Empty;
            do
            {
                validVid = GenerateRandomVid();
            } while (IsPalindrome(validVid) || !IsValid(validVid));
            return validVid;
        }

        private static string GenerateRandomVid()
        {
            var randomGenerator = new Random();
            var baseNumber = randomGenerator.Next(2, 10).ToString() +
                string.Concat(Enumerable.Range(0, 14).Select(_ => randomGenerator.Next(0, 10).ToString()));

            var checksum = VerhoeffAlgorithm.CalculateChecksum(baseNumber);
            return baseNumber + checksum;
        }
    }

    public static class VerhoeffAlgorithm
    {
        private static readonly int[,] MultiplicationTable = {
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

        private static readonly int[,] PermutationTable = {
            {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
            {1, 5, 7, 6, 4, 8, 9, 0, 3, 2},
            {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
            {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
            {9, 4, 5, 3, 1, 2, 7, 8, 6, 0},
            {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
            {2, 7, 9, 3, 8, 1, 6, 4, 5, 0},
            {7, 0, 4, 6, 9, 3, 1, 2, 5, 8}
        };

        private static readonly int[] InverseTable = { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

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
            var checksum = 0;

            for (var index = 0; index < reversedDigits.Length; index++)
            {
                checksum = MultiplicationTable[checksum, PermutationTable[(index + 1) % 8, reversedDigits[index]]];
            }

            return InverseTable[checksum];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var generatedVid = Vid.GenerateValidVid();
                Console.WriteLine($"Generated VID: {generatedVid}");
                Console.WriteLine($"Formatted VID: {Vid.Format(generatedVid)}");
                Console.WriteLine($"Masked VID: {Vid.Mask(generatedVid)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
