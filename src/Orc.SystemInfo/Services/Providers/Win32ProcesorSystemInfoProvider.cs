namespace Orc.SystemInfo
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Catel.Logging;
    using Catel.Services;

    public class Win32ProcesorSystemInfoProvider : ISystemInfoProvider
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ILanguageService _languageService;

        public Win32ProcesorSystemInfoProvider(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        public IEnumerable<SystemInfoElement> GetSystemInfoElements()
        {
            throw new NotImplementedException();
        }
    }
}
