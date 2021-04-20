﻿namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using Catel.Logging;
    using Catel.Services;
    using Orc.SystemInfo.Win32;

    public class Win32OperatingSystemSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public Win32OperatingSystemSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            var items = new List<SystemInfoElement>();
            var notAvailable = _languageService.GetString("SystemInfo_NotAvailable");

            var systemInfo = new Kernel32.SystemInfo();
            Kernel32.GetNativeSystemInfo(systemInfo);

            var osVersionInfo = new Kernel32.OSVersionInfoEx();
            Version OSVersion = null;
            if (Kernel32.GetVersionExA(osVersionInfo))
            {
                // Returns incorrect value depends on application manifest
                // note: https://docs.microsoft.com/en-us/windows/win32/api/sysinfoapi/nf-sysinfoapi-getversionexa
                OSVersion = osVersionInfo.GetOSVersion();
            }

            var processorArchitecture = systemInfo.wProcessorArchitecture.ToString();
            // items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_OsName"), wmi.GetValue("Caption", notAvailable)));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Architecture"), ProcessorArchitectureString(processorArchitecture)));
            // __cpuid, see: https://docs.microsoft.com/ru-ru/cpp/intrinsics/cpuid-cpuidex?view=msvc-160;
            // Not Implemented
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_ProcessorId"), notAvailable));
            items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_Build"), OSVersion?.Build.ToString() ?? notAvailable));
            // count from lpMaximumApplicationAddress;
            //items.Add(new SystemInfoElement(_languageService.GetString("SystemInfo_MaxProcossRam"), (wmi.GetLongValue("MaxProcessMemorySize")).ToReadableSize(1))); // KB

            return items;
        }

        public string ProcessorArchitectureString(ushort processorArchitecture)
        {
            string processorArchitect;

            switch (processorArchitecture)
            {
                case 0:
                    processorArchitect = "INTEL (32-bit)";
                    break;
                case 5:
                    processorArchitect = "ARM";
                    break;
                case 6:
                    processorArchitect = "IA64 (64-bit)";
                    break;
                case 9:
                    processorArchitect = "AMD64 (64-bit)";
                    break;
                case 12:
                    processorArchitect = "ARM64 (64-bit)";
                    break;
                case 0xFFFF:
                    processorArchitect = "UNKNOWN";
                    break;
                default:
                    processorArchitect = "UNKNOWN";
                    break;
            }

            return processorArchitect;
        }



    }
}
