#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/DailyPlanner.Api/DailyPlanner.Api.csproj", "Systems/Api/DailyPlanner.Api/"]
COPY ["Services/DailyPlanner.Services.Settings/DailyPlanner.Services.Settings.csproj", "Services/DailyPlanner.Services.Settings/"]
COPY ["Shared/DailyPlanner.Settings/DailyPlanner.Settings.csproj", "Shared/DailyPlanner.Settings/"]
COPY ["Shared/DailyPlanner.Common/DailyPlanner.Common.csproj", "Shared/DailyPlanner.Common/"]
RUN dotnet restore "Systems/Api/DailyPlanner.Api/DailyPlanner.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/DailyPlanner.Api"
RUN dotnet build "DailyPlanner.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyPlanner.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyPlanner.Api.dll"]