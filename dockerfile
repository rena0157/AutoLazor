FROM microsoft/dotnet:2.1-sdk as build-env
WORKDIR /app

COPY ./src/ ./

RUN dotnet publish AutoLazer.Server/ -c Release -o ./out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app

COPY --from=build-env /app/AutoLazer.Server/out .

ENTRYPOINT [ "dotnet", "AutoLazor.Server.dll" ]