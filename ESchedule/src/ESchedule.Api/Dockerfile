FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["Directory.Packages.props", "Directory.Packages.props"]

COPY ["src/ESchedule.Api/*.csproj", "ESchedule.Api/"]
COPY ["src/ESchedule.Api.Models/*.csproj", "ESchedule.Api.Models/"]
COPY ["src/ESchedule.Business/*.csproj", "ESchedule.Business/"]
COPY ["src/ESchedule.DataAccess/*.csproj", "ESchedule.DataAccess/"]
COPY ["src/ESchedule.Domain/*.csproj", "ESchedule.Domain/"]
COPY ["src/ESchedule.Startup/*.csproj", "ESchedule.Startup/"]
COPY ["src/PowerInfrastructure/*.csproj", "PowerInfrastructure/"]

RUN dotnet restore ESchedule.Api/ESchedule.Api.csproj

COPY src .

RUN dotnet publish ESchedule.Api/ESchedule.Api.csproj -c Release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR .

COPY --from=build /out .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ESchedule.Api.dll"]
