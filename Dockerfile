FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o core --no-restore
WORKDIR /source/app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runner
WORKDIR app
COPY --from=build * ./

ENTRYPOINT ["dotnet", "app/Aiko.Core.dll"]