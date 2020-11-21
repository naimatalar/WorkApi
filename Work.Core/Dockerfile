#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.



FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore 

COPY . .
RUN dotnet publish -c debug -o pub

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1


WORKDIR /app
COPY --from=build /app/pub .
ENTRYPOINT ["dotnet", "Work.Core.dll"]