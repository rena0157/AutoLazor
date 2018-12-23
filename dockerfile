FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

COPY ./ ./

RUN dotnet publish -c Release -o /app/out

ENTRYPOINT [ "dotnet", "/out/AutoLazor.Server.dll" ]