namespace Ionaltred.Users.Backend.Controller

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("/api/user")>]
type RegisterUserController() =

    [<HttpGet>]
    member this.Hello (): string =
        "Hello world"
