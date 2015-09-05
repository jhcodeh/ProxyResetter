//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="code Herrenkind - www.codeh.de">
//     Copyright (c) 2015 code Herrenkind. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows.Forms;

namespace ProxyResetter
{
    /// <summary>
    ///     Program class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Shows the system tray icon.
            using (var pi = new ProcessIcon())
            {
                // Create daemon and give to backend.
                Daemon deamon = new Daemon(3000);
                pi.Display(deamon);

                // Make sure the application runs!
                Application.Run();
            }

            Application.Exit();
        }
    }
}
