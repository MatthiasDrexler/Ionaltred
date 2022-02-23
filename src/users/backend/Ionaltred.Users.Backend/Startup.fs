namespace Ionaltred.Users.Backend

open System
open System.Reflection
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Mvc.ApplicationParts
open Microsoft.AspNetCore.Routing
open Microsoft.Extensions.DependencyInjection

type Startup() =
    let ControllerAssemblyPart = AssemblyPart(Assembly.Load("Ionaltred.Users.Backend.Controller"))

    member this.ConfigureServices(services: IServiceCollection): unit =
        services.AddControllers()
            .PartManager.ApplicationParts.Add(ControllerAssemblyPart)
        |> ignore
//        services.AddCors(Action<CorsOptions> (fun corsOptions ->
//            corsOptions.AddPolicy("AllowAll", Action<CorsPolicyBuilder> (fun builder ->
//                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() |> ignore))))

    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment): unit =
        app
            .UseRouting()
//            .UseCors("AllowAll")
            .UseEndpoints(Action<IEndpointRouteBuilder> (fun endpoints -> endpoints.MapControllers() |> ignore))
            |> ignore
