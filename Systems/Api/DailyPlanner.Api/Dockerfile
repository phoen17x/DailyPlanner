#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Api/DailyPlanner.Api/DailyPlanner.Api.csproj", "Systems/Api/DailyPlanner.Api/"]
COPY ["Data/DailyPlanner.Context/DailyPlanner.Context.csproj", "Data/DailyPlanner.Context/"]
COPY ["Shared/DailyPlanner.Settings/DailyPlanner.Settings.csproj", "Shared/DailyPlanner.Settings/"]
COPY ["Data/DailyPlanner.Context.Entities/DailyPlanner.Context.Entities.csproj", "Data/DailyPlanner.Context.Entities/"]
COPY ["Data/Migrations/DailyPlanner.Context.MigrationsPostgreSQL/DailyPlanner.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/DailyPlanner.Context.MigrationsPostgreSQL/"]
COPY ["Services/DailyPlanner.Services.Notebooks/DailyPlanner.Services.Notebooks.csproj", "Services/DailyPlanner.Services.Notebooks/"]
COPY ["Shared/DailyPlanner.Common/DailyPlanner.Common.csproj", "Shared/DailyPlanner.Common/"]
COPY ["Services/DailyPlanner.Services.Settings/DailyPlanner.Services.Settings.csproj", "Services/DailyPlanner.Services.Settings/"]
COPY ["Services/DailyPlanner.Services.TodoTasks/DailyPlanner.Services.TodoTasks.csproj", "Services/DailyPlanner.Services.TodoTasks/"]
COPY ["Services/DailyPlanner.Services.UserAccount/DailyPlanner.Services.UserAccount.csproj", "Services/DailyPlanner.Services.UserAccount/"]
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