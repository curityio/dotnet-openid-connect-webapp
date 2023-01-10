# DotNet Core and OpenID Connect - Web App Example

A simple web application in .NET that integrates with the Curity Identity Server using the OpenID Connect protocol.

## Configure the Curity Identity Server

- Follow the tutorial on how to [Configure a Client](https://curity.io/resources/learn/configure-client/). 
- Add a client with the code-flow capability in the Curity Identity Server. 
- Add the following redirect URI: `https://www.example.com:5000/signin-oidc`. 
- Make sure to add the scopes `openid` and `profile`. 
- Then, update the `ClientId`, `ClientSecret` and `Issuer` in the `appsettings.json` file. 
- Optionally, adapt the scope.

## Endpoints and Domain Names

This example assumes that the Curity Identity Server and the web app are deployed on different subdomains and expose the following endpoints:

| Name     | URL                                                     | Description |
|--------- | ------------------------------------------------------- | ----------- |
| Admin UI | https://login.example.com:6749/admin                    | Web interface for configuring the Curity Identity Server |
| Issuer   | https://login.example.com:8443/oauth/v2/oauth-anonymous | Endpoint at the Curity Identity Server that serves the OpenID Connect metadata. .Net reads the OpenID Connect metadata to retrieve the settings for communicating with the server, e.g., the endpoints for calls or verification keys.  |
| Web App  | https://www.example.com:5000                            | Entry point for the example web app |

The endpoints may differ depending on your infrastructure. If you have deployed the Curity Identity Server and the web app locally (or in a local Docker container), simply add the following lines in the `/etc/hosts` file to resolve the domains to localhost:

```
127.0.0.1 www.example.com login.example.com
```

## Run the Example App

Ensure that [.Net 7.0](https://dotnet.microsoft.com/en-us/download) is installed, then run the example:

```bash
dotnet build
dotnet run
```
Navigate to https://www.example.com:5000/. You will be presented with an unauthenticated view. Click on `Login` to start the OpenID Connect flow. Log in at the Curity Identity Server. The application receives an ID token that it uses to present user data on the screen, and tokens that could be used in upstream requests to some backend API, to access data on behalf of the user.

## Run a Deployed App

To run the app in a [Docker](https://docs.docker.com/engine/install/) container, execute the deployment script:

```bash
./deployment/run.sh
```

## Further Information

- See the [Website Tutorial](https://curity.io/resources/learn/dotnet-openid-connect-website) for further details on this example's security flow.
- Please visit [curity.io](https://curity.io/) for more information about the Curity Identity Server.