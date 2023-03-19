#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Workers/DailyPlanner.Worker/DailyPlanner.Worker.csproj", "Systems/Workers/DailyPlanner.Worker/"]
COPY ["Data/DailyPlanner.Context/DailyPlanner.Context.csproj", "Data/DailyPlanner.Context/"]
COPY ["Shared/DailyPlanner.Settings/DailyPlanner.Settings.csproj", "Shared/DailyPlanner.Settings/"]
COPY ["Data/DailyPlanner.Context.Entities/DailyPlanner.Context.Entities.csproj", "Data/DailyPlanner.Context.Entities/"]
COPY ["Services/DailyPlanner.Services.EmailSender/DailyPlanner.Services.EmailSender.csproj", "Services/DailyPlanner.Services.EmailSender/"]
COPY ["Services/DailyPlanner.Services.RabbitMq/DailyPlanner.Services.RabbitMq.csproj", "Services/DailyPlanner.Services.RabbitMq/"]
COPY ["Services/DailyPlanner.Services.Settings/DailyPlanner.Services.Settings.csproj", "Services/DailyPlanner.Services.Settings/"]
COPY ["Shared/DailyPlanner.Common/DailyPlanner.Common.csproj", "Shared/DailyPlanner.Common/"]
RUN dotnet restore "Systems/Workers/DailyPlanner.Worker/DailyPlanner.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Workers/DailyPlanner.Worker"
RUN dotnet build "DailyPlanner.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyPlanner.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyPlanner.Worker.dll"]