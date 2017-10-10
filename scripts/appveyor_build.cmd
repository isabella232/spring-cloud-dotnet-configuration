:: @ECHO OFF

:: Build packages
cd src\Pivotal.Extensions.Configuration.ConfigServer
dotnet restore --configfile ..\..\nuget.config
IF NOT "%APPVEYOR_REPO_TAG_NAME%"=="" (
    IF NOT "%STEELTOE_VERSION_SUFFIX%"=="" (
        dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX%
		nuget add "bin\%BUILD_TYPE%\Pivotal.Extensions.Configuration.ConfigServer.%STEELTOE_VERSION%-%STEELTOE_VERSION_SUFFIX%.nupkg" -Source "%USERPROFILE%\localfeed"
    ) ELSE (
        dotnet pack --configuration %BUILD_TYPE%
nuget add "bin\%BUILD_TYPE%\Pivotal.Extensions.Configuration.ConfigServer.%STEELTOE_VERSION%.nupkg" -Source "%USERPROFILE%\localfeed"
    )    
)
IF "%APPVEYOR_REPO_TAG_NAME%"=="" (dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX% --include-symbols --include-source)
IF "%APPVEYOR_REPO_TAG_NAME%"=="" (nuget add bin\%BUILD_TYPE%\Pivotal.Extensions.Configuration.ConfigServer.%STEELTOE_VERSION%-%STEELTOE_VERSION_SUFFIX%.nupkg -Source %USERPROFILE%\localfeed)
cd ..\..

cd src\Pivotal.Extensions.Configuration.ConfigServerCore
dotnet restore --configfile ..\..\nuget.config
IF NOT "%APPVEYOR_REPO_TAG_NAME%"=="" (
    IF NOT "%STEELTOE_VERSION_SUFFIX%"=="" (
        dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX%
    ) ELSE (
        dotnet pack --configuration %BUILD_TYPE%
    )    
)
IF "%APPVEYOR_REPO_TAG_NAME%"=="" (dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX% --include-symbols --include-source)
cd ..\..

cd src\Pivotal.Extensions.Configuration.ConfigServerAutofac
dotnet restore --configfile ..\..\nuget.config
IF NOT "%APPVEYOR_REPO_TAG_NAME%"=="" (
    IF NOT "%STEELTOE_VERSION_SUFFIX%"=="" (
        dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX%
    ) ELSE (
        dotnet pack --configuration %BUILD_TYPE%
    )    
)
IF "%APPVEYOR_REPO_TAG_NAME%"=="" (dotnet pack --configuration %BUILD_TYPE% --version-suffix %STEELTOE_VERSION_SUFFIX% --include-symbols --include-source)
cd ..\..