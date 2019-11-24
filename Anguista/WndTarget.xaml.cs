// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Collections;
using Sysphonic.Common;

namespace Anguista.Main
{
    /// <summary>Target Settings window.</summary>
    public partial class WndTarget : Window
    {

        /// <summary>Zeptair Dist. flag.</summary>
        bool _isZeptDist = false;


        /// <summary>Constructor (For Add).</summary>
        /// <param name="isZeptDist">Zeptair Dist. flag.</param>
        public WndTarget(bool isZeptDist=false)
        {
            InitializeComponent();

            _isZeptDist = isZeptDist;
            if (_isZeptDist)
              tbxTitle.Text = Anguista.Lib.Properties.Resources.ZEPTAIR_DISTRIBUTION;

            grdAuth.Visibility = Visibility.Hidden;
            grdAuth.Height = 0;
        }

        /// <summary>Constructor (For Edit).</summary>
        /// <param name="info">Target RSS Information.</param>
        public WndTarget()
        {
            InitializeComponent();

        }

        /// <summary>Click event handler of the OK button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>Click event handler of the Cancel button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>Click event handler of the URL Test button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnUrlTest_Click(object sender, RoutedEventArgs e)
        {
            string url = tbxUrl.Text;
            string userName = tbxUserName.Text;
            string password = tbxPassword.Password;

            if (url.Length <= 0)
                return;

            WndProgress wndProgress = new WndProgress();
            wndProgress.Owner = this;
            wndProgress.lblStatus.Content = Properties.Resources.STATUS_TESTING_URL;
            wndProgress.Show();

        }

        /// <summary>Click event handler of the URL Test checkbox.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void chkAuth_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)chkAuth.IsChecked)
            {
                grdAuth.Visibility = Visibility.Visible;
                grdAuth.Height = 65;
            }
            else
            {
                grdAuth.Visibility = Visibility.Hidden;
                grdAuth.Height = 0;
            }
        }

        /// <summary>Value Changed event handler of the Polling Interval scrollbar.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void scbInterval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        /// <summary>Text Changed event handler of Polling Interval.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void tbxInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

    }
}
