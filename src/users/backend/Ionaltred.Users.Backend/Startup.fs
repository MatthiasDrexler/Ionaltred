namespace Ionaltred.Users.Backend

open System
open System.Reflection
open Ionaltred.Users.Backend.Domain
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.AspNetCore.Routing
open Microsoft.Extensions.DependencyInjection

type Startup() =
    let EndpointsAssemblyPart = AssemblyPart(Assembly.Load("Ionaltred.Users.Backend.Controller"))

    member this.ConfigureServices(services: IServiceCollection): unit =
        services.AddScoped<IRegisterUserService, RegisterUserService>() |> ignore

        services.AddControllers()
            .PartManager.ApplicationParts.Add(EndpointsAssemblyPart)
        services.AddCors(fun corsOptions ->
            corsOptions.AddPolicy("AllowAll", fun builder ->
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() |> ignore))
        |> ignore

    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment): unit =
        app
            .UseRouting()
            .UseCors("AllowAll")
            .UseEndpoints(Action<IEndpointRouteBuilder> (fun endpoints -> endpoints.MapControllers() |> ignore))
            |> ignore
