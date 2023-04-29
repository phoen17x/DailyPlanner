#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/Web/DailyPlanner.Web/DailyPlanner.Web.csproj", "Systems/Web/DailyPlanner.Web/DailyPlanner.Web.csproj"]
RUN dotnet restore "Systems/Web/DailyPlanner.Web/DailyPlanner.Web.csproj"
COPY . .
WORKDIR "/src/Systems/Web/DailyPlanner.Web"
RUN dotnet build "DailyPlanner.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyPlanner.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY Systems/Web/DailyPlanner.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80