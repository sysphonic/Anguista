/* 
 * ConfConfig.cs
 * 
 * Copyright: (c) 2007-2018, MORITA Shintaro, Sysphonic. All rights reserved.
 * License: Modified BSD License (See LICENSE file)
 * URL: http://sysphonic.com/
 */
using System;
using System.IO;
using System.Xml.Serialization;
using Sysphonic.Common;

namespace Anguista.Main
{
    /// <summary>Configuration class for AnguistaConf.</summary>
    public class ConfConfig : Anguista.Lib.ConfigBase
    {
        /// <summary>Configuration file name.</summary>
        protected override string CONFIG_FILE_NAME { get { return @"conf.config"; } }

        /// <summary>Width of the Settings window.</summary>
        protected double _settingsWidth = 450;

        /// <summary>Height of the Settings window.</summary>
        protected double _settingsHeight = 400;


        /// <summary>Constructor</summary>
        public ConfConfig()
        {
        }

        /// <summary>Width of the Settings window.</summary>
        public double SettingsWidth
        {
            get { return _settingsWidth; }
            set { _settingsWidth = value; }
        }

        /// <summary>Height of the Settings window.</summary>
        public double SettingsHeight
        {
            get { return _settingsHeight; }
            set { _settingsHeight = value; }
        }

        /// <summary>Loads Configuration for AnguistaConf.</summary>
        /// <returns>Configuration for AnguistaConf.</returns>
        static public ConfConfig Load()
        {
            return (ConfConfig)Anguista.Lib.ConfigBase.Load(new ConfConfig());
        }
    }
}
