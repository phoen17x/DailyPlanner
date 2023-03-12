#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Identity/DailyPlanner.Identity/DailyPlanner.Identity.csproj", "Systems/Identity/DailyPlanner.Identity/"]
COPY ["Data/DailyPlanner.Context/DailyPlanner.Context.csproj", "Data/DailyPlanner.Context/"]
COPY ["Shared/DailyPlanner.Settings/DailyPlanner.Settings.csproj", "Shared/DailyPlanner.Settings/"]
COPY ["Data/DailyPlanner.Context.Entities/DailyPlanner.Context.Entities.csproj", "Data/DailyPlanner.Context.Entities/"]
COPY ["Shared/DailyPlanner.Common/DailyPlanner.Common.csproj", "Shared/DailyPlanner.Common/"]
RUN dotnet restore "Systems/Identity/DailyPlanner.Identity/DailyPlanner.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/DailyPlanner.Identity"
RUN dotnet build "DailyPlanner.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyPlanner.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyPlanner.Identity.dll"]