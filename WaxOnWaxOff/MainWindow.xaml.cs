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
using System.Management;
using System.Threading.Tasks;
using System.Windows;

namespace WaxOnWaxOff
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetEnabled(true, false);
        }

        private void ReleaseClick(object sender, RoutedEventArgs e) =>
            Task.Run(() => Manage(ReleaseOrRestore.Release));

        private void RestoreClick(object sender, RoutedEventArgs e) =>
            Task.Run(() => Manage(ReleaseOrRestore.Restore));

        private void SetEnabled(bool dropEnabled, bool restoreEnabled)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ReleaseButton.IsEnabled = dropEnabled;
                RestoreButton.IsEnabled = restoreEnabled;
            }));
        }

        private void Manage(ReleaseOrRestore dropOrRestore)
        {
            try
            {
                SetEnabled(false, false);

                var searcher = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_NetworkAdapterConfiguration");

                foreach (ManagementObject mo in searcher.Get())
                {
                    var check = Convert.ToString(mo["DHCPLeaseObtained"]);

                    if (!string.IsNullOrEmpty(check))
                    {
                        var method = dropOrRestore == ReleaseOrRestore.Release ?
                            "ReleaseDHCPLease" : "RenewDHCPLease";

                        mo.InvokeMethod(method, null, null);
                    }
                }

                if (dropOrRestore == ReleaseOrRestore.Release)
                    SetEnabled(false, true);
                else
                    SetEnabled(true, false);
            }
            catch (ManagementException error)
            {
                if (dropOrRestore == ReleaseOrRestore.Release)
                    SetEnabled(true, false);
                else
                    SetEnabled(false, true);

                Modal.WarningDialog(this, $"{dropOrRestore} Error: {error.Message}");
            }
        }

        protected override void OnSourceInitialized(EventArgs e) => IconRemover.Remove(this);
    }
}
