//-----------------------------------------------------------------------
// <copyright file="ProcessIcon.cs" company="code Herrenkind - www.codeh.de">
//     Copyright (c) 2015 code Herrenkind. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using ProxyResetter.Properties;

namespace ProxyResetter
{
    /// <summary>
    ///     The Process icon class.
    /// </summary>
    internal class ProcessIcon : IDisposable
    {
        /// <summary>
        ///     The notify icon
        /// </summary>
        private readonly NotifyIcon _notifyIcon;

        /// <summary>
        /// The registry handler
        /// </summary>
        private RegistryHandler _registryHandler = new RegistryHandler();

        /// <summary>
        /// The _daemon
        /// </summary>
        private Daemon _daemon;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessIcon" /> class.
        /// </summary>
        public ProcessIcon()
        {
            this._notifyIcon = new NotifyIcon();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            this._daemon.Stop();
            this._notifyIcon.Dispose();
        }

        /// <summary>
        /// Displays the specified daemon.
        /// </summary>
        /// <param name="daemon">The daemon.</param>
        public void Display(Daemon daemon)
        {
            this._daemon = daemon;

            // Put the icon in the system tray and allow it react to mouse clicks.
            this._notifyIcon.MouseClick += notifyIcon_MouseClick;
            this._notifyIcon.Icon = Resources.SystemTrayApp;
            this._notifyIcon.Text = "Reset system proxy settings daemon.";
            this._notifyIcon.Visible = true;

            // Attach a context menu.
            this._notifyIcon.ContextMenuStrip = new ContextMenus().Create(this._registryHandler, this._daemon);

            // Start daemon process.
            this._daemon.Start();
        }

        /// <summary>
        /// Handles the MouseClick event of the _notifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._registryHandler.DisableProxy();
            }
        }
    }
}