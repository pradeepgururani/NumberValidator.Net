// # cbu.py - functions for handling Argentinian CBU numbers
// # coding: utf-8
// #
// # Copyright (C) 2016 Luciano Rossi
// # Copyright (C) 2022 Sudhanshu Pant
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
    public class cbu : IValidator
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

        public void Validate(string a)
        {
            a = a.Clean();

            ValidateFormat(a);
        
        }

        
        public string Splitter(int i, int j,string cbu)
        {
            
            char[] cbu_array = new char[j];

            for (int k = 0; k < j; k++)
            {
                cbu_array[k] = cbu[i];
                i++;
            }

            string str = new string(cbu_array);

            return str;
        }

        private int CalcCheckDigit(string a)
        {
        
            char[] ch = a.ToCharArray();
            Array.Reverse(ch, 0, a.Length);      
            int sum = 0;
            var weights = new[] { 3, 1, 7, 9 };
            for (var index = 0; index < ch.Length; index++)
            {
                int n = ch[index ]-'0';
                sum += n * weights[index%4];        
            }
            
            
            return(10-(sum%10));
        
        }



        public void ValidateFormat(string cbu)
        {
            

            if (cbu.Length != 22)
            {
                throw new InvalidLengthException();
            }

            if (!cbu.IsDigits())
            {
                throw new InvalidFormatException();
            }


            if ((CalcCheckDigit(Splitter(0,7,cbu))) != (Convert.ToInt32(cbu[7]-'0')))
            {
                throw new InvalidChecksumException();
                
            }

            if (CalcCheckDigit(Splitter(8, 21 - 8, cbu)) != Convert.ToInt32(cbu[cbu.Length - 1] - '0'))
            {
                throw new InvalidChecksumException();
            }


        }

       
    }
}