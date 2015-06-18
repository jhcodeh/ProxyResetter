//-----------------------------------------------------------------------
// <copyright file="ContextMenus.cs" company="code Herrenkind - www.codeh.de">
//     Copyright (c) 2015 code Herrenkind. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using ProxyResetter.Properties;

namespace ProxyResetter
{
    /// <summary>
    /// The context menus.
    /// </summary>
    internal class ContextMenus
    {
        /// <summary>
        /// The _registry resetter
        /// </summary>
        private RegistryHandler _registryHandler;

        /// <summary>
        /// The _daemon
        /// </summary>
        private Daemon _daemon;

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create(RegistryHandler registryHandler, Daemon daemon)
        {
            this._registryHandler = registryHandler;
            this._daemon = daemon;

            // Add the default menu options.
            var menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            // Disable proxy
            item = new ToolStripMenuItem();
            item.Text = "Disable Proxy";
            item.Click += Explorer_Click;
            item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // Start daemon
            item = new ToolStripMenuItem();
            item.Text = "Start daemon";
            item.Click += StartDaemon_Click;
            item.Image = Resources.Start;
            menu.Items.Add(item);

            // Start daemon
            item = new ToolStripMenuItem();
            item.Text = "Stop daemon";
            item.Click += StopDaemon_Click;
            item.Image = Resources.Stop;
            menu.Items.Add(item);

            // Separator
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += Exit_Click;
            item.Image = Resources.Exit;
            menu.Items.Add(item);

            return menu;
        }

        /// <summary>
        ///     Handles the Click event of the Explorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Explorer_Click(object sender, EventArgs e)
        {
            this._registryHandler.DisableProxy();
        }

        /// <summary>
        /// Handles the Click event of the StartDaemon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StartDaemon_Click(object sender, EventArgs e)
        {
            this._daemon.Start();
        }

        /// <summary>
        /// Handles the Click event of the StopDaemon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void StopDaemon_Click(object sender, EventArgs e)
        {
            this._daemon.Stop();
        }

        /// <summary>
        ///     Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            this._daemon.Stop();
            Application.Exit();
        }
    }
}