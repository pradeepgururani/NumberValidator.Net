// nrt.py - functions for handling Andorra NRT numbers
// coding: utf-8
//
// Copyright (C) 2019 Leandro Regueiro
// Copyright (C) 2021 Mayank Deopa
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

// """NRT (Número de Registre Tributari, Andorra tax number).
// The Número de Registre Tributari (NRT) is an identifier of legal and natural
// entities for tax purposes.
// This number consists of one letter indicating the type of entity, then 6
// digits, followed by a check letter.
// More information:
// *https://www.oecd.org/tax/automatic-exchange/crs-implementation-and-assistance/tax-identification-numbers/Andorra-TIN.pdf



using NumberValidator.Helpers;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.AD
{
    public class NRT : IValidator
    {
        public bool IsValid(string nrt)
        {
            try
            {
                Validate(nrt);
                return true;
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// NRT (Número de Registre Tributari, Andorra tax number)
        /// </summary>
        /// <param name="nrt"></param>
        /// <returns>True if NRT is valid</returns>
       
        public void Validate(string nrt)
        {

            if (nrt.Length != 8)
            {
                throw new InvalidLengthException();
            }
             
            if (!char.IsLetter(nrt[0]) ||
                !char.IsLetter(nrt[nrt.Length - 1]))
            {
                throw new InvalidFormatException();
            }

            if ((!Regex.IsMatch(nrt, "^[ACDEFGLOPU]")) ||
                (nrt[0] == 'F' && int.Parse(nrt.Substring(1, 6)) > 699999) || 
                ((nrt[0] == 'A' || nrt[0] == 'L') &&
                    !(699999 < int.Parse(nrt.Substring(1, 6)) &&
                        int.Parse(nrt.Substring(1, 6)) < 800000)))
            {
                throw new InvalidComponentException();
            }
        }
    }
}
