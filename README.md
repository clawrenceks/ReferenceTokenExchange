# ReferenceTokenExchange
An ASP.NET Core Middleware providing reference token exchange with Identity Server 4.

---
### Use Case

ReferenceTokenExchange was designed for use in a Microservices Gateway. It supports a scenario whereby clients of the Microservice gateway provide a reference token issued by Identity Server 4 for Authorization, but all downstream Microservices require a JWT for authorization, removing the need for downstream services to validate reference tokens against Identity Server.

The above is desirable as it removes the need for an access token to be issued to clients of the Microservices gateway, instead allowing reference tokens to be used, which can be revoked more easily than access tokens.  More information on the benefits of reference tokens in Identity Server can be found [here](https://leastprivilege.com/2015/11/25/reference-tokens-and-introspection/).

When configured the ReferenceTokenExchange middleware will take the users inbound Reference Token, exchange it for a JWT with Identity Server and add the JWT as a HTTP Header on the inbound request so that this can easily be added as an Authorization header for any downstream requests that the Microservices gateway may need to make. Inbuilt caching of JWT's is also supported to reduce round trips to Identity Server for each request.  This process allows downstream Microservices to use a JWT for Authorization, without the JWT being exposed to public clients.

### Installing & Configuring

ReferenceTokenExchange is distributed as a [NuGet package](https://www.nuget.org/packages/Clawrenceks.Middleware.ReferenceTokenExchange/) and steps for installing and configuring can be seen on the [GitHub Wiki](https://github.com/clawrenceks/ReferenceTokenExchange/wiki).

### Platform

ReferenceTokenExchange is built against ASP.NET Core 1.1

### 3rd Party Dependencies

ReferenceTokenExchange currently uses the excellent IdentityModel client from the guys at Identity Server, the client is available on GitHub [here](https://github.com/IdentityModel/IdentityModel).
