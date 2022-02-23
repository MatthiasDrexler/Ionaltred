namespace Ionaltred.Users.Backend

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Serilog

module Program =
    let SuccessCode = 0
    let EnvironmentVariablePrefix = "Ionaltred_"
    let EnvironmentEnvironmentVariable = $"{EnvironmentVariablePrefix}_Environment"

    let UseEnvironmentVariablesForHostBuilder (config: IConfigurationBuilder) : unit =
        config.AddEnvironmentVariables(EnvironmentVariablePrefix)
        |> ignore

    let UseAppsettingsAndEnvironmentVariablesForAppConfiguration (config: IConfigurationBuilder, commandLineArguments: string []) : unit =
        config
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{EnvironmentEnvironmentVariable}.json", true)
            .AddEnvironmentVariables(EnvironmentVariablePrefix)
            .AddCommandLine(commandLineArguments)
        |> ignore

    let UseStartupToServeAtUrl (builder: IWebHostBuilder) : unit =
        builder
            .UseUrls("http://localhost:29000")
            .CaptureStartupErrors(true)
            .UseStartup<Startup>()
        |> ignore

    [<EntryPoint>]
    let Main args =
        Log.Logger <- LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Log/Log.txt")
            .CreateLogger()

        let host =
            Host
                .CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureHostConfiguration(UseEnvironmentVariablesForHostBuilder)
                .ConfigureAppConfiguration(fun config -> UseAppsettingsAndEnvironmentVariablesForAppConfiguration(config, args))
                .ConfigureWebHostDefaults(UseStartupToServeAtUrl)
                .Build()

        host.Run()

        SuccessCode
