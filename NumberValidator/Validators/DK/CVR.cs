// CVR.cs - Validator for Danish CVR number
//
// Copyright (C) 2012-2015 Arthur de Jong
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

using System;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.DK
{
    /// <summary>
    /// This is Danish VAT number
    /// The CVR (Momsregistreringsnummer, VAT) is an 8 digit number with a  straightforward check mechanism.
    /// </summary>
    public class CVR : IValidator
    {
        public bool IsValid(string input)
        {
            try
            {
                Validate(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validates a given CVR.
        /// </summary>
        /// <param name="cvr"></param>
        /// <returns>True if CVR is valid</returns>
        /// <exception cref="InvalidFormatException">Thrown when CVR contains non digit or starts with 0.</exception>
        /// <exception cref="InvalidLengthException">Thrown when CVR does not contain 8 digits.</exception>
        /// <exception cref="InvalidChecksumException">Thrown when CVR checksum validation fails.</exception>
        public void Validate(string cvr)
        {
            cvr = cvr.Clean();

            ValidateFormat(cvr);
        }

        private void ValidateFormat(string cvr)
        {
            if (!cvr.IsDigits() || cvr.StartsWith("0") || cvr.Length != 8)
            {
                throw new InvalidFormatException();
            }

            if (Checksum(cvr) != 0)
            {
                throw new InvalidChecksumException();
            }
        }

        private int Checksum(string cvr)
        {
            var weights = new[] {2, 7, 6, 5, 4, 3, 2, 1};

            var sum = 0;
            for (var index = 0; index < 8; index++)
            {
                sum += int.Parse(cvr[index].ToString()) * weights[index];
            }

            return sum % 11;
        }
    }
}