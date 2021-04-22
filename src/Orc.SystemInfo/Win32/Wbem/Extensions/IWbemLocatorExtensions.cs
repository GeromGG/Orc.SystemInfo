namespace Orc.SystemInfo.Win32
{
    using System;
    using Catel.Logging;

    internal static class IWbemLocatorExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        internal static IWbemServices ConnectServer(this IWbemLocator wbemLocator, string networkResource, string userName, string userPassword, string locale,
            WbemConnectOption wbemConnectOption, string authority, IWbemContext ctx)
        {
            HResult hr = wbemLocator.ConnectServer(networkResource, userName, userPassword, locale, wbemConnectOption, authority, ctx, out IWbemServices services);

            if (hr.Failed)
            {
                var exception = (Exception)hr;
                Log.Error(exception);
                throw exception;
            }

            return services;
        }
    }
}
