FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoalTrack.Publisher/GoalTrack.Publisher.csproj", "GoalTrack.Publisher/"]
RUN dotnet restore "GoalTrack.Publisher/GoalTrack.Publisher.csproj"
COPY . .
WORKDIR "/src/GoalTrack.Publisher"
RUN dotnet build "GoalTrack.Publisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoalTrack.Publisher.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoalTrack.Publisher.dll"]
