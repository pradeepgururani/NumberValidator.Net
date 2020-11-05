// CVR.cs - Validator for Danish CVR number
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
            catch
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
            var day = int.Parse(cpr.Substring(0, 2));
            var month = int.Parse(cpr.Substring(2, 2));
            var year = int.Parse(cpr.Substring(4, 2));

            if ("5678".Contains(cpr[6].ToString()) && year >= 58)
            {
                year += 1800;
            }
            else if ("0123".Contains(cpr[6].ToString()) ||
                     ("49".Contains(cpr[6].ToString()) && year >= 37))
            {
                year += 1900;
            }
            else
            {
                year += 2000;
            }
            
            var dob = new DateTime(year, month, day);

            return dob > DateTime.Today;
        }
    }
}
