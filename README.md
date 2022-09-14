# dotnet-openid-connect-webapp
A simple web application in .NET that integrates with the Curity Identity Server using the OpenID Connect protocol.

Add a client with the code-flow capability in the Curity Identity Server. Add the following redirect URI: `https://localhost:5001/signin-oidc`. Make sure to add the scopes `openid` and `profile`.

Update the `ClientId`, `ClientSecret` and `Issuer` in the `appsettings.json` (or `appsettings.Development.json` if you are developing). Optionally, adapt the scope.

Navigate to https://localhost:5001/protected and log in at the Curity Identity Server. The application receives an ID token that it uses to present user data on the screen, and an access token that it could use in upstream requests to some backend API.