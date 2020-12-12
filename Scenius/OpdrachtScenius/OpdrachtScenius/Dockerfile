FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["OpdrachtScenius/OpdrachtScenius.csproj", "OpdrachtScenius/"]
RUN dotnet restore "OpdrachtScenius/OpdrachtScenius.csproj"
COPY . .
WORKDIR "/src/OpdrachtScenius"
RUN dotnet build "OpdrachtScenius.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpdrachtScenius.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpdrachtScenius.dll"]