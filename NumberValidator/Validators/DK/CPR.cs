﻿// CVR.cs - Validator for Danish CVR number
//
// Copyright (C) 2012-2019 Arthur de Jong
// Copyright (C) 2020 Leon Sandøy
// Copyright (C) 2020 Pradeep Gururani
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA
// 02110-1301 USA

// """CPR (personnummer, the Danish citizen number).
//
// The CPR is the national number to identify Danish citizens and is stored in
// the Det Centrale Personregister (Civil Registration System). The number
// consists of 10 digits in the format DDMMYY-SSSS where the first part
// represents the birth date and the second a sequence number. The first digit
// of the sequence number indicates the century.
//
// The numbers used to validate using a checksum but since the sequence numbers
// ran out this was abandoned in 2007. It is also not possible to use the
// checksum only for numbers that have a birth date before that because the
// numbers are also assigned to immigrants.
//
// More information:
//
// * https://en.wikipedia.org/wiki/Personal_identification_number_(Denmark)
// * https://da.wikipedia.org/wiki/CPR-nummer
// * https://cpr.dk/

using System;
using System.Globalization;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.DK
{
    public class CPR : IValidator
    {
        public bool IsValid(string cpr)
        {
            try
            {
                Validate(cpr);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public void Validate(string cpr)
        {
            cpr = cpr.Clean();

            if (!cpr.IsDigits())
            {
                throw new InvalidFormatException();
            }

            if (cpr.Length != 10)
            {
                throw new InvalidLengthException();
            }

            if (BirthdateInFuture(cpr))
            {
                throw new InvalidComponentException();
            }
        }

        private bool BirthdateInFuture(string cpr)
        {
            var dobPart = cpr.Substring(0, 6);

            if (DateTime.TryParseExact(dobPart, "ddMMyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dob))
            {
                return dob > DateTime.Today;
            }

            throw new InvalidComponentException();
        }

        /// <summary>
        /// checksum isn't actually used any more. Valid numbers used to have a checksum of 0
        /// </summary>
        /// <param name="cpr"></param>
        /// <returns></returns>
        private int Checksum(string cpr)
        {
            var weights = new[] {4, 3, 2, 7, 6, 5, 4, 3, 2, 1};

            var sum = 0;
            for (var index = 0; index < 10; index++)
            {
                sum += int.Parse(cpr[index].ToString()) * weights[index];
            }

            return sum % 11;
        }
    }
}