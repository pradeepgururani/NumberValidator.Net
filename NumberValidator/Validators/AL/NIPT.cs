// nipt.py - functions for handling Albanian VAT numbers
// coding: utf-8
//
// Copyright (C) 2008-2011 Cédric Krier
// Copyright (C) 2008-2011 B2CK
// Copyright (C) 2015 Arthur de Jong
// Copyright (C) 2021 Pradeep Gururani
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

using System.Text.RegularExpressions;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.AL
{
    public class NIPT : IValidator
    {
        private readonly Regex _niptRegularExpression = new Regex("^[JKL][0-9]{8}[A-Z]$");
        
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

        public void Validate(string input)
        {
            input = input.Clean();

            if (input.Length != 10)
            {
                throw new InvalidLengthException();
            }

            if (!_niptRegularExpression.Match(input).Success)
            {
                throw new InvalidFormatException();
            }
        }
    }
}