// # cbu.py - functions for handling Argentinian CBU numbers
// # coding: utf-8
// #
// # Copyright (C) 2016 Luciano Rossi
// #
// # This library is free software; you can redistribute it and/or
// # modify it under the terms of the GNU Lesser General Public
// # License as published by the Free Software Foundation; either
// # version 2.1 of the License, or (at your option) any later version.
// #
// # This library is distributed in the hope that it will be useful,
// # but WITHOUT ANY WARRANTY; without even the implied warranty of
// # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// # Lesser General Public License for more details.
// #
// # You should have received a copy of the GNU Lesser General Public
// # License along with this library; if not, write to the Free Software
// # Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA
// # 02110-1301 USA

// """CBU (Clave Bancaria Uniforme, Argentine bank account number).
// CBU it s a code of the Banks of Argentina to identify customer accounts. The
// number consists of 22 digits and consists of a 3 digit bank identifier,
// followed by a 4 digit branch identifier, a check digit, a 13 digit account
// identifier and another check digit.

using System;
using System.Globalization;
using NumberValidator.Helpers;

namespace NumberValidator.Validators.AR
{
    public class CBU : IValidator
    {
        public bool IsValid(string cbu)
        {
            try
            {
                Validate(cbu);
                return true;
            }
            catch
            {
                return false;
            }
        }

          public void Validate(string cbu)
        {
            cvr = cvr.Clean();

            ValidateFormat(cbu);
        }

         private int calc_check_digit(string cbu)
        {
        
        char[] ch = cbu.ToCharArray();
        Array.Reverse(ch, 0, cbu.Length);      
        int sum = 0;
        var weights = new[] { 3, 1, 7, 9 };
        for (var index = 0; index < ch.Length; index++)
        {
            int n = ch[index ]-'0';
            sum += n * weights[index%4];        
        }
        Console.WriteLine(sum.GetType());
        int final = 10 - sum; 
        Console.WriteLine(10%final);
        Console.WriteLine();
        }

        public void Validate(string cpr)
        {
            cbu = cbu.Clean();

            if (cpr.Length != 22)
            {
                throw new InvalidLengthException();
            }

            if (!cpr.IsDigits())
            {
                throw new InvalidFormatException();
            }


            if (calc_check_digit(cbu[..7] != CBU[7]))
            {
                throw new InvalidChecksumException();
            }

            if (calc_check_digit(cbu[8..-1] != CBU[-1]))
            {
                throw new InvalidChecksumException();
            }

        }

       
    }
}