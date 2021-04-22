namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Threading;

    public static class IWbemClassObjectEnumeratorExtensions
    {
        internal static IWbemClassObject Next(this IWbemClassObjectEnumerator wbemClassObjectEnumerator)
        {
            IWbemClassObject current = null;
            uint returnedCount;
            HResult hresult = wbemClassObjectEnumerator.Next(Timeout.Infinite, 1, out current, out returnedCount);

            if (hresult.Failed)
            {
                throw (Exception)hresult;
            }

            return current;
        }
    }
}
