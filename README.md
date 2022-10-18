# DotNet Core and OpenID Connect - Web App Example
A simple web application in .NET that integrates with the Curity Identity Server using the OpenID Connect protocol.

## Configure the Curity Identity Server

Follow the tutorial on how to [Configure a Client](https://curity.io/reso
urces/learn/configure-client/). 
Add a client with the code-flow capability in the Curity Identity Server. 
Add the following redirect URI: `https://www.example.com:5000/signin-oidc`. 
Make sure to add the scopes `openid` and `profile`. 
Then, update the `ClientId`, `ClientSecret` and `Issuer` in the `appsettings.json` (or `appsettings.Development.json` if you are developing) accordingly. 
Optionally, adapt the scope.

## Endpoints and Domain Names

This example assumes that the Curity Identity Server and the web app are deployed on different subdomains and expose the following endpoints:

| Name     | URL                                                     | Description |
|--------- | ------------------------------------------------------- | ----------- |
| Admin UI | https://login.example.com:6749/admin                    | Web interface for configuring the Curity Identity Server |
| Issuer   | https://login.example.com:8443/oauth/v2/oauth-anonymous | Endpoint at the Curity Identity Server that serves the OpenID Connect metadata. .Net reads the OpenID Connect metadata to retrieve the settings for communicating with the server, e.g., the endpoints for calls or verification keys.  |
| Web App  | https://www.example.com:5001                            | Entry point for the example web app |

The endpoints may differ depending on your infrastructure. If you have deployed the Curity Identity Server and the web app locally (or in a local Docker container), simply add the following lines in the `/etc/hosts` file to resolve the domains to localhost:

```
127.0.0.1 www.example.com login.example.com
```

## Run the Example app

The example includes a script that runs the app in a Docker container. 
In order to run the script, you need to have the following tools installed:

* [.Net](https://dotnet.microsoft.com/en-us/download) 6.0
* [Docker](https://docs.docker.com/engine/install/)

With the pre-requisites in place, you can simply run the following command to deploy the web app in a Docker container:

```bash
./deployment/run.sh
```

Navigate to https://www.example.com:5001/. You will be presented with an unauthenticated view. Click on `Login` to start the OpenID Connect flow. Log in at the Curity Identity Server. The application receives an ID token that it uses to present user data on the screen, and an access token that it could use in upstream requests to some backend API to access data on behalf of the user.

## Build the Example app

If you are more interested in development, use an IDE of your choice or build and run the app with the following command:

```bash
dotnet run --configuration Release OidcClientDemoApplication.csproj
```