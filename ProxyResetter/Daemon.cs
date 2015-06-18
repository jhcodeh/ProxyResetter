//-----------------------------------------------------------------------
// <copyright file="Daemon.cs" company="code Herrenkind - www.codeh.de">
//     Copyright (c) 2015 code Herrenkind. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading;

namespace ProxyResetter
{
    /// <summary>
    /// The daemon class.
    /// </summary>
    class Daemon
    {
        /// <summary>
        /// The _interval
        /// </summary>
        private readonly int _interval;

        /// <summary>
        /// The _thread
        /// </summary>
        private Thread _thread;

        /// <summary>
        /// The _auto run
        /// </summary>
        private bool _autoRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="Daemon"/> class.
        /// </summary>
        /// <param name="interval">The interval.</param>
        public Daemon(int interval)
        {
            this._interval = interval;
            this._autoRun = true;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this._autoRun = true;
            this._thread = new Thread(DisableProxyEvent);
            this._thread.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this._thread.Abort(this);
            this._autoRun = false;
        }

        /// <summary>
        /// Disables the proxy event.
        /// </summary>
        private void DisableProxyEvent()
        {
            if (this._autoRun)
            {
                RegistryHandler registryHandler = new RegistryHandler();
                registryHandler.DisableProxy();
                Thread.Sleep(this._interval);
                DisableProxyEvent();
            }
        }
    }
}
