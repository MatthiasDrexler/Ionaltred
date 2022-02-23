namespace Ionaltred.Users.Backend.Domain

type RegisterUserService() =
    interface IRegisterUserService with
        member this.Hello () : string = "Hello World"
