// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Anguista.Main
{
    public partial class App : Application
    {
        private bool _needLicKey = false;

        public bool NeedLicKey
        {
            get { return _needLicKey; }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            foreach (string arg in e.Args)
            {
                switch (arg)
                {
                    case @"/needLicKey":
                        _needLicKey = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
