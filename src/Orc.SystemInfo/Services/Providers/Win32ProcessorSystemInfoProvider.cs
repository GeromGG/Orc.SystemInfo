namespace Orc.SystemInfo
{
    using System.Collections.Generic;
    using Catel.Logging;
    using Catel.Services;
    using Orc.SystemInfo.Win32;
    using static Orc.SystemInfo.Win32.Kernel32;

    public class Win32ProcessorSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public Win32ProcessorSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            var items = new List<SystemInfoElement>();
            return items;
        }
    }
}
