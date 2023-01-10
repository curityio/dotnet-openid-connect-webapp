FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /usr/webapp
COPY bin/Release/net7.0/linux-x64/publish/*  /usr/webapp/

RUN adduser --disabled-password --home /home/webuser --gecos '' webuser
USER webuser

ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "OidcClientDemoApplication.dll"]