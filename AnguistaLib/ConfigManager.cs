// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System;
using System.IO;
using System.Xml.Serialization;
using Sysphonic.Common;

namespace Anguista.Lib
{
    /// <summary>Configuration Manager class.</summary>
    public class ConfigManager : ConfigBase
    {
        /// <summary>Configuration file name.</summary>
        protected override string CONFIG_FILE_NAME { get { return @"settings.xml"; } }

        /// <summary>Max. of the Information Items.</summary>
        protected int _maxItemsOnPanel = 30;


        /// <summary>Constructor</summary>
        public ConfigManager()
        {
        }

        /// <summary>Max. of the Information Items.</summary>
        public int MaxItemsOnPanel
        {
            get { return _maxItemsOnPanel; }
            set { _maxItemsOnPanel = value; }
        }

        /// <summary>Loads Configuration.</summary>
        /// <returns>Configuration Manager.</returns>
        static public ConfigManager Load()
        {
            return (ConfigManager)Anguista.Lib.ConfigBase.Load(new ConfigManager());
        }
    }
}
