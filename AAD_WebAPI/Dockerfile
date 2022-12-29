#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QuotesASPNETCoreWebAPI/QuotesASPNETCoreWebAPI.csproj", "QuotesASPNETCoreWebAPI/"]
RUN dotnet restore "QuotesASPNETCoreWebAPI/QuotesASPNETCoreWebAPI.csproj"
COPY . .
WORKDIR "/src/QuotesASPNETCoreWebAPI"
RUN dotnet build "QuotesASPNETCoreWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuotesASPNETCoreWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuotesASPNETCoreWebAPI.dll"]