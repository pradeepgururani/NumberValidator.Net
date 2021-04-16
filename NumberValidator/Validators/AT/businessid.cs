// businessid.py - functions for handling Austrian company register numbers
// Copyright (C) 2015 Holvi Payment Services Oy
// Copyright (C) 2012-2019 Arthur de Jong
// Copyright (C) 2012-2019 MURARI YADAV
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA
// 02110-1301 USA

using NumberValidator.Helpers;
using System.Text.RegularExpressions;

namespace NumberValidator.Validators.AT
{
    public class BusinessId : IValidator
    {
        public bool IsValid(string businessid)
        {
            try
            {
                Validate(businessid);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Validate(string businessid)
        {
            if (!Regex.IsMatch(businessid, "^[0-9]+[a-z]$"))
            {
                throw new InvalidFormatException();
            }
            return;
        }
    }
}
