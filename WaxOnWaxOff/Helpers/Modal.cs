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

using WaxOnWaxOff;
using System.Reflection;
using System.Windows;

namespace WaxOnWaxOff
{
    public static class Modal
    {
        private static AppInfo AppInfo { get; } =
            new AppInfo(Assembly.GetEntryAssembly());

        public static void WarningDialog(
            Window owner, string format, params object[] args)
        {
            MessageBox.Show(owner, string.Format(format, args),
                AppInfo.Title,
                MessageBoxButton.OK, MessageBoxImage.Warning,
                MessageBoxResult.OK, MessageBoxOptions.None);
        }

        public static void FailureDialog(
            string format, params object[] args)
        {
            MessageBox.Show(string.Format(format, args),
                AppInfo.Title, MessageBoxButton.OK,
                MessageBoxImage.Exclamation,
                MessageBoxResult.OK, MessageBoxOptions.None);
        }
    }
}
