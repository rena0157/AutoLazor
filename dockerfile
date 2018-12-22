FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

COPY /src/AutoLazer.Server/*.csproj ./

RUN dotnet restore

COPY ./src/ ./

RUN dotnet publish -c Release -o out

ENTRYPOINT [ "dotnet", "out/AutoLazor.Server.dll" ]