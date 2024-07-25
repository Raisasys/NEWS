FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApiWrite.csproj", "."]
RUN dotnet restore "./ApiWrite.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiWrite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiWrite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiWrite.dll"]