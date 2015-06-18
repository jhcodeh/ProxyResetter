//-----------------------------------------------------------------------
// <copyright file="RegistryHandler.cs" company="code Herrenkind - www.codeh.de">
//     Copyright (c) 2015 code Herrenkind. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ProxyResetter
{
    /// <summary>
    /// The registry handler class.
    /// </summary>
    class RegistryHandler
    {
        /// <summary>
        /// Internets the set option.
        /// </summary>
        /// <param name="hInternet">The h internet.</param>
        /// <param name="dwOption">The dw option.</param>
        /// <param name="lpBuffer">The lp buffer.</param>
        /// <param name="dwBufferLength">Length of the dw buffer.</param>
        /// <returns>Magic dragon egg.</returns>
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

        /// <summary>
        /// The interne t_ optio n_ setting s_ changed
        /// </summary>
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;

        /// <summary>
        /// The interne t_ optio n_ refresh
        /// </summary>
        public const int INTERNET_OPTION_REFRESH = 37;

        /// <summary>
        /// The settings return
        /// </summary>
        static bool settingsReturn, refreshReturn;

        /// <summary>
        /// Disables the proxy.
        /// </summary>
        public void DisableProxy()
        {
            // reset user settings
            RegistryKey registryUserPath = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            if (registryUserPath != null)
            {
                registryUserPath.SetValue("ProxyEnable", 0);
                registryUserPath.DeleteValue("AutoConfigURL", false);
                registryUserPath.Close();
            }

            // reset local machine settings
            RegistryKey registryMachinePath = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", true);
            if (registryMachinePath != null)
            {
                registryMachinePath.DeleteValue("WinHttpSettings", false);
                registryMachinePath.Close();
            }

            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
    }
}
