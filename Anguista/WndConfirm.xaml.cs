// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System.Windows;

namespace Anguista.Main
{
    /// <summary>Confirmation of the connection result dialog class.</summary>
    public partial class WndConfirm : Window
    {
        /// <summary>Constructor.</summary>
        public WndConfirm()
        {
            InitializeComponent();
        }

        /// <summary>Click event handler of the Yes button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            if (tbxTitle.Text.Length > 0)
                ((WndTarget)Owner).tbxTitle.Text = tbxTitle.Text;
            Close();
        }

        /// <summary>Click event handler of the No button.</summary>
        /// <param name="sender">Sender Object.</param>
        /// <param name="e">Event parameters.</param>
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
