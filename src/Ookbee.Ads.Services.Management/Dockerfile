#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /
COPY . .

FROM build AS publish
WORKDIR "/src/Ookbee.Ads.Services.Management"
RUN dotnet publish "Ookbee.Ads.Services.Management.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY /src/Ookbee.Ads.Services.Management/appsettings.json .
COPY /src/Ookbee.Ads.Services.Management/appsettings.Local.json .
ENTRYPOINT ["dotnet", "Ookbee.Ads.Services.Management.dll"]