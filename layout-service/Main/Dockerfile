FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY dockerize-dot-net-core/dockerize-dot-net-core.csproj dockerize-dot-net-core/
RUN dotnet restore dockerize-dot-net-core/dockerize-dot-net-core.csproj
COPY . .
WORKDIR /src/dockerize-dot-net-core
RUN dotnet build dockerize-dot-net-core.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish dockerize-dot-net-core.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS http://+:83
EXPOSE 83
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dockerize-dot-net-core.dll"]
