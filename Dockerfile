#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Work.Api/Work.Api.csproj", "Work.Api/"]
COPY ["Work.Infrastructure/Work.Infrastructure.csproj", "Work.Infrastructure/"]
COPY ["Work.Core/Work.Core.csproj", "Work.Core/"]


RUN dotnet restore "Work.Api/Work.Api.csproj"
COPY . .
WORKDIR "/src/Work.Api"
RUN dotnet build "Work.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Work.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Work.Api.dll"]