namespace Orc.SystemInfo.Win32
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class WmiNetUtils
    {
        private const string UnmanagedWmiLibraryDllName = "wminet_utils.dll";

        #region Description
        /// <summary>
        /// Sets the authentication information that will be used to make calls on the specified proxy.
        /// </summary>
        #endregion
        internal static readonly CoSetProxyBlanketForIWbemServicesFunction CoSetProxyBlanketForIWbemServices;

        internal static readonly CoSetProxyBlanketForIWbemClassObjectEnumeratorFunction CoSetProxyBlanketForIWbemClassObjectEnumerator;

        internal static readonly ExecQueryFunction ExecQueryWmi;

        static WmiNetUtils()
        {
            string dllPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + Path.DirectorySeparatorChar + UnmanagedWmiLibraryDllName;
            dllPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\\wminet_utils.dll";

            IntPtr loadLibrary = Kernel32.LoadLibrary(dllPath);

            if (loadLibrary != IntPtr.Zero)
            {
                IntPtr procAddr = Kernel32.GetProcAddress(loadLibrary, "BlessIWbemServices");

                CoSetProxyBlanketForIWbemServices = (CoSetProxyBlanketForIWbemServicesFunction)Marshal.GetDelegateForFunctionPointer(procAddr, typeof(CoSetProxyBlanketForIWbemServicesFunction));

                CoSetProxyBlanketForIWbemClassObjectEnumerator = (CoSetProxyBlanketForIWbemClassObjectEnumeratorFunction)Marshal.GetDelegateForFunctionPointer(procAddr, typeof(CoSetProxyBlanketForIWbemClassObjectEnumeratorFunction));

                ExecQueryWmi = (ExecQueryFunction)Marshal.GetDelegateForFunctionPointer(Kernel32.GetProcAddress(loadLibrary, "ExecQueryWmi"), typeof(ExecQueryFunction));
            }
            else
            {
                throw new TypeInitializationException("wminet_utils.net (native)", null);
            }
        }

        internal delegate HResult CoSetProxyBlanketForIWbemClassObjectEnumeratorFunction(
            [In, MarshalAs(UnmanagedType.Interface)] IWbemClassObjectEnumerator wbemClassObjectEnumerator,
            [In, MarshalAs(UnmanagedType.BStr)] string userName,
            [In, MarshalAs(UnmanagedType.BStr)] string password,
            [In, MarshalAs(UnmanagedType.BStr)] string authority,
            [In] ImpersonationLevel impersonationLevel,
            [In] AuthenticationLevel authenticationLevel);

        // Set Authentication for wbem services
        internal delegate HResult CoSetProxyBlanketForIWbemServicesFunction(
            [In, MarshalAs(UnmanagedType.Interface)]
            IWbemServices wbemServices,
            [In, MarshalAs(UnmanagedType.BStr)]
            string userName,
            [In, MarshalAs(UnmanagedType.BStr)]
            string password,
            [In, MarshalAs(UnmanagedType.BStr)]
            string authority,
            [In]
            ImpersonationLevel impersonationLevel,
            [In]
            AuthenticationLevel authenticationLevel);

        internal delegate HResult ExecQueryFunction(
            [In][MarshalAs(UnmanagedType.BStr)]
            string queryLanguage,
            [In][MarshalAs(UnmanagedType.BStr)]
            string query,
            [In]
            WbemClassObjectEnumeratorBehaviorOption enumeratorBehaviorOption,
            [In]
            IWbemContext ctx,
            [Out]
            out IWbemClassObjectEnumerator enumerator,
            [In]
            AuthenticationLevel impersonationLevel,
            [In]
            ImpersonationLevel authenticationLevel,
            [In]
            IWbemServices wbemService,
            [In][MarshalAs(UnmanagedType.BStr)]
            string userName,
            [In][MarshalAs(UnmanagedType.BStr)]
            string password,
            [In][MarshalAs(UnmanagedType.BStr)]
            string authority);
    }
}
