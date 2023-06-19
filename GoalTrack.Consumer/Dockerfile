FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoalTrack.Consumer/GoalTrack.Consumer.csproj", "GoalTrack.Consumer/"]
RUN dotnet restore "GoalTrack.Consumer/GoalTrack.Consumer.csproj"
COPY . .
WORKDIR "/src/GoalTrack.Consumer"
RUN dotnet build "GoalTrack.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoalTrack.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoalTrack.Consumer.dll"]
