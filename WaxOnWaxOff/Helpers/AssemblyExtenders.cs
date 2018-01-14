﻿// Copyright 2018 SquidEyes, LLC.
//
// This file is part of WaxOnWaxOff.
//
// WaxOnWaxOff is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation, either version 3 of the License, 
// or (at your option) any later version.
//
// WaxOnWaxOff is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with WaxOnWaxOff.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Reflection;

namespace WaxOnWaxOff
{
    public static partial class Extenders
    {
        public static T GetAttribute<T>(this Assembly callingAssembly)
            where T : Attribute
        {
            T result = null;

            var configAttributes = Attribute.
                GetCustomAttributes(callingAssembly, typeof(T), false);

            if (!configAttributes.IsNullOrEmpty())
                result = (T)configAttributes[0];

            return result;
        }
    }
}
