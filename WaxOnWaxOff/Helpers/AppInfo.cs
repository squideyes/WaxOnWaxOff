// Copyright 2018 SquidEyes, LLC.
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
using System.Text;

namespace WaxOnWaxOff
{
    public class AppInfo
    {
         public AppInfo(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            Product = GetProduct(assembly);
            Version = assembly.GetName().Version;
        }

        public string Product { get; private set; }
        public Version Version { get; private set; }

        public string Title
        {
            get
            {
                var sb = new StringBuilder();

                sb.Append(Product);

                sb.Append(" v");
                sb.Append(Version.Major);
                sb.Append('.');
                sb.Append(Version.Minor);

                if ((Version.Build != 0) || (Version.Revision != 0))
                {
                    sb.Append('.');
                    sb.Append(Version.Build);
                }

                if (Version.Revision != 0)
                {
                    sb.Append('.');
                    sb.Append(Version.Revision);
                }

                return sb.ToString();
            }
        }

        private static string GetProduct(Assembly assembly) =>
            assembly.GetAttribute<AssemblyProductAttribute>().Product;
    }
}
