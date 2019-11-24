// Copyright: (c) 2007-2019, MORITA Shintaro, Sysphonic. All rights reserved.
// License: MIT License (See LICENSE file)

using System;

namespace Anguista.Lib
{
    /// <summary>Interface of the IPC-Remoting service of Anguista.Main process.</summary>
    public interface IIpcConfService
    {
        /// <summary>Event of Task process closing.</summary>
        event EventHandler<TaskProcClosingEventArgs> TaskProcClosing;

        /// <summary>Fires event of updating list of the information items.</summary>
        void FireEventTaskProcClosing();
    }
}
