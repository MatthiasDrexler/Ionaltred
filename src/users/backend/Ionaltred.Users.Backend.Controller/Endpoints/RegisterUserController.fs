namespace Ionaltred.Users.Backend.Controller

open Ionaltred.Users.Backend.Domain
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("/api/user")>]
type RegisterUserController(registerUserService: IRegisterUserService) =

    [<HttpGet>]
    member this.Hello() : string = registerUserService.Hello()
