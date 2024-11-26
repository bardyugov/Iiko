FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o Published --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
COPY --from=build /source/Published ./

ENTRYPOINT ["dotnet", "Iiko.Core.dll"]