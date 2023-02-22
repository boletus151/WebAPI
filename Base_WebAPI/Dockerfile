#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Base_WebAPI/Base_WebAPI.csproj", "Base_WebAPI/"]
RUN dotnet restore "Base_WebAPI/Base_WebAPI.csproj"
COPY . .
WORKDIR "/src/Base_WebAPI"
RUN dotnet build "Base_WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Base_WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Base_WebAPI.dll"]