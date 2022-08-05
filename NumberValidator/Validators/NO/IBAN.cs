// iban.py - functions for handling Norwegian IBANs
// coding: utf-8

// Copyright (C) 2018 Arthur de Jong
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

using System;
using System.Globalization;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.NO
{
    public class IBAN : IValidator
    {
        public bool IsValid(string iban)
        {
            try
            {
                Validate(iban);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string ToKontonr(string iban)
        {
            return (iban.Substring(4, 11));
        }

        public void Validate(string iban)
        {
            if (iban.Length != 15)
            {
                throw new InvalidLengthException();
            }

            if (!iban.StartsWith("NO"))
            {
                throw new InvalidComponentException();
            }

            if(!ToKontonr(iban).IsDigits())
            {
                throw new InvalidFormatException();
            }
        }
    }
}