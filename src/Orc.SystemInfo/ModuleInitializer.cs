﻿using Catel.IoC;
using Catel.Services;
using Orc.SystemInfo;
using Orc.SystemInfo.Win32;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static partial class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IDotNetFrameworkService, DotNetFrameworkService>();
        serviceLocator.RegisterType<IDbProvidersService, DbProvidersService>();
        serviceLocator.RegisterType<ISystemInfoService, SystemInfoService>();
        serviceLocator.RegisterType<ISystemIdentificationService, SystemIdentificationService>();
        serviceLocator.RegisterType<IWindowsManagementInformationService, WindowsManagementInformationService>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.SystemInfo", "Orc.SystemInfo.Properties", "Resources"));
    }
}
