FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /usr/webapp
COPY bin/Release/net8.0/linux-x64/publish/*  /usr/webapp/

RUN groupadd --gid 10000 webuser \
  && useradd --uid 10001 --gid webuser --shell /bin/bash --create-home webuser
USER 10001

ENV ASPNETCORE_URLS=http://+:5000
CMD ["dotnet", "OidcClientDemoApplication.dll"]