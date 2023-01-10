#!/bin/bash

cd "$(dirname "${BASH_SOURCE[0]}")"
cd ..

dotnet publish OidcClientDemoApplication.csproj -c Release -r linux-x64 --no-self-contained
if [ $? -ne 0 ]; then
  exit 1
fi

docker build -f deployment/Dockerfile -t dotnetdemoapp:v1 .
if [ $? -ne 0 ]; then
  exit
fi

docker run -it -p 5000:5000 dotnetdemoapp:v1
