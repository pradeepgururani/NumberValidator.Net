// fodselsnummer.py - functions for handling Norwegian birth numbers
// coding: utf-8

// Copyright (C) 2018 Ilya Vihtinsky
// Copyright (C) 2018 Arthur de Jong
// Copyright (C) 2020 Leon Sandøy
// Copyright (C) 2021 Krishna Israni

// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.

// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA
// 02110-1301 USA

// Fødselsnummer (Norwegian birth number, the national identity number).
// The Fødselsnummer is an eleven-digit number that is built up of the date of
// birth of the person, a serial number and two check digits.
// More information:
// * https://no.wikipedia.org/wiki/F%C3%B8dselsnummer
// * https://en.wikipedia.org/wiki/National_identification_number#Norway

using System;
using System.Globalization;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.NO
{
    public class FSN : IValidator
    {
        public bool IsValid(string fsn)
        {
            try
            {
                Validate(fsn);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string CheckDigit1(string fsn)
        {
            var weights = new[] { 3, 7, 6, 1, 8, 9, 4, 5, 2 };

            var sum = 0;
            for (var index = 0; index < 9; index++)
            {
                sum += int.Parse(fsn[index].ToString()) * weights[index];
            }
            sum = (11 - sum) % 11;

            return (sum.ToString());
        }

        private string CheckDigit2(string fsn)
        {
            var weights = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            var sum = 0;
            for (var index = 0; index < 10; index++)
            {
                sum += int.Parse(fsn[index].ToString()) * weights[index];
            }
            sum = (11 - sum) % 11;


            return (sum.ToString());
        }
        private char GetGender(string fsn)
        {
            fsn = fsn.Clean();
            var temp = int.Parse(fsn[6].ToString());
            if (temp % 2 == 0)
            {
                return 'M';
            }
            else
            {
                return 'F';
            }
        }
        private bool BirthdateInFuture(string fsn)
        {
            var day = int.Parse(fsn.Substring(0, 2));
            var month = int.Parse(fsn.Substring(2, 2));
            var year = int.Parse(fsn.Substring(4, 2));
            var individual_digits = int.Parse(fsn.Substring(6, 3));

            if (day >= 80)
            {
                throw new InvalidComponentException();
            }
            if (day > 40)
            {
                day = day - 40;
            }
            if (month > 40)
            {
                month = month - 40;
            }
            if (individual_digits < 500)
            {
                year = year + 1900;
            }
            else if (individual_digits >= 500 &&
                individual_digits < 750 &&
                year > 54)
            {
                year = year + 1800;
            }
            else if (individual_digits >= 500 &&
                individual_digits < 1000 &&
                year < 40)
            {
                year = year + 2000;
            }
            else if (individual_digits >= 900 && 
                individual_digits < 1000 && 
                year >= 40)
            {
                year = year + 1900;
            }


            var dob = new DateTime(year, month, day);

            return dob > DateTime.Today;
        }
        public void Validate(string fsn)
        {
            fsn = fsn.Clean();

            if (!fsn.IsDigits())
            {
                throw new InvalidFormatException();
            }

            if (fsn.Length != 11)
            {
                throw new InvalidLengthException();
            }

            if (BirthdateInFuture(fsn))
            {
                throw new InvalidComponentException();
            }

            if (fsn[fsn.Length - 2].Equals(Checkdigit1(fsn)) == false ||
                fsn[fsn.Length - 1].Equals(Checkdigit2(fsn)) == false)
            {
                throw new InvalidChecksumException();
            }
        }
    }