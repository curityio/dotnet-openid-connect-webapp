# Secure a .NET Web App with OpenID Connect

[![Quality](https://img.shields.io/badge/quality-demo-red)](https://curity.io/resources/code-examples/status/)
[![Availability](https://img.shields.io/badge/availability-source-blue)](https://curity.io/resources/code-examples/status/)

A demo web application in .NET, secured using the OpenID Connect protocol.

## Configure the Application

The API uses an `appSettings.json` file to configure its OpenID Connect details:

```json
{
  "OpenIDConnect" : {
    "ClientId": "dotnet-client",
    "ClientSecret": "U2U9EnSKx31fUnvgGR3coOUszko5MiuCSI2Z_4ogjIiO5-UbBzIBWU6JQQaljEis",
    "Issuer": "http://login.example.com:8443/oauth/v2/oauth-anonymous",
    "Scope": "openid profile",
    "CallbackPath": "/callback",
    "PostLogoutRedirectUri": "http://www.example.com:5000",
    "TokenEndpoint": "http://login.example.com:8443/oauth/v2/oauth-token"
  }
}
```

## Configure the Curity Identity Server

Before running the app you need to configure an OpenID provider like a local Docker instance of the Curity Identity Server:

- [Run a local Docker instance](https://curity.io/resources/learn/run-curity-docker/).
- [Configure a client](https://curity.io/resources/learn/configure-client/).

The configuration uses local example domains for the web application and the Curity Identity Server.\
To use them, add the following entries to your local computer's hosts file:

```text
127.0.0.1 www.example.com login.example.com
```

## Run the Example App

Ensure that an up to date [.NET SDK](https://dotnet.microsoft.com/en-us/download) is installed, then run the example:

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

- See the [.NET Website Tutorial](https://curity.io/resources/learn/dotnet-openid-connect-website) for further details on the app's code and configuration.
- Please visit [curity.io](https://curity.io/) for more information about the Curity Identity Server.
