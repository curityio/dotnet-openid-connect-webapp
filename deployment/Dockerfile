FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /usr/webapp
COPY bin/Release/net6.0/linux-x64/publish/*  /usr/webapp/

RUN adduser --disabled-password --home /home/webuser --gecos '' webuser
USER webuser
CMD ["dotnet", "OidcClientDemoApplication.dll"]