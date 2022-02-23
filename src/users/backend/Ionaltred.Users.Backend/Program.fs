namespace Ionaltred.Users.Backend

open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting

module Program =
    let SuccessCode = 0
    let EnvironmentVariablePrefix = "Ionaltred_"
    let EnvironmentEnvironmentVariable = $"{EnvironmentVariablePrefix}_Environment"

    let BuildHostBuilderConfiguration (config: IConfigurationBuilder) : unit =
        config.AddEnvironmentVariables(EnvironmentVariablePrefix)
        |> ignore

    let ConfigureAppConfiguration (config: IConfigurationBuilder, commandLineArguments: string []) : unit =
        config
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{EnvironmentEnvironmentVariable}.json", true)
            .AddEnvironmentVariables(EnvironmentVariablePrefix)
            .AddCommandLine(commandLineArguments)
        |> ignore

    let ConfigureWebHost (builder: IWebHostBuilder) : unit =
        builder
            .UseUrls("http://localhost:29000")
            .CaptureStartupErrors(true)
            .UseStartup<Startup>()
        |> ignore

    [<EntryPoint>]
    let Main args =
        let host =
            Host
                .CreateDefaultBuilder()
                .ConfigureHostConfiguration(fun config -> BuildHostBuilderConfiguration(config))
                .ConfigureAppConfiguration(fun config -> ConfigureAppConfiguration(config, args))
                .ConfigureWebHostDefaults(fun builder -> ConfigureWebHost(builder))
                .Build()

        host.Run()

        SuccessCode
