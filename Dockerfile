FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY . .

RUN dotnet tool install --global dotnet-ef
RUN dotnet restore
ENV PATH="$PATH:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT=Production
RUN dotnet ef database update --project Iiko.Infrastructure --startup-project Iiko.Core

RUN dotnet publish -c Release -o Published --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

WORKDIR /app
COPY --from=build /source/Published ./

ENTRYPOINT ["dotnet", "Iiko.Core.dll"]