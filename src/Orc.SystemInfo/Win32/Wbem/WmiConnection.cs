namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Runtime.InteropServices;
    using Catel;
    using Catel.Logging;

    public class WmiConnection : IDisposable
    {
        private const string DefaultLocalRootPath = @"\\.\root\cimv2";

        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly object _lock = new object();

        private bool _isDisposed;
        private bool _isConnected;
        private IWbemServices _wbemServices;
#pragma warning disable IDE0044 // Add readonly modifier
        private IWbemContext _context = null;
#pragma warning restore IDE0044 // Add readonly modifier

        public void Open()
        {
            try
            {
                if (_isDisposed)
                {
                    throw Log.ErrorAndCreateException<ObjectDisposedException>($"Access to disposed instance of {typeof(WmiConnection).FullName}");
                }

                if (!_isConnected)
                {
                    IWbemLocator locator = new WbemLocator();

                    lock (_lock)
                    {
                        if (!_isConnected)
                        {
                            AuthenticationLevel authLevel = AuthenticationLevel.PacketIntegrity;

                            _wbemServices = locator.ConnectServer(DefaultLocalRootPath, null, null, null, WbemConnectOption.None, null, _context);
                            _wbemServices.SetProxy(ImpersonationLevel.Impersonate, authLevel);

                            _isConnected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _context = null;
                Dispose();
                Log.Error(ex);
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(typeof(WmiConnection).FullName);
            }

            if (_isConnected)
            {
                lock (_lock)
                {
                    if (_wbemServices is not null)
                    {
                        Marshal.ReleaseComObject(_wbemServices);
                        _wbemServices = null;
                    }

                    _isConnected = false;
                }
            }

            _isDisposed = true;
        }

        public WmiQuery CreateQuery(string wql)
        {
            Argument.IsNotNull(() => wql);

            return new WmiQuery(this, wql);
        }

        public WmiObjectEnumerator ExecuteQuery(WmiQuery query)
        {
            Argument.IsNotNull(() => query);

            return new WmiObjectEnumerator(InternalExecuteQuery(query));
        }

        internal IWbemClassObjectEnumerator InternalExecuteQuery(WmiQuery query)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(typeof(WmiConnection).FullName);
            }

            Open();

            IWbemClassObjectEnumerator enumerator;

            WbemClassObjectEnumeratorBehaviorOption behaviorOption = (WbemClassObjectEnumeratorBehaviorOption)query.EnumeratorBehaviorOption;

            enumerator = _wbemServices.ExecQuery(query.Wql, behaviorOption, _context);

            return enumerator;
        }
    }
}
