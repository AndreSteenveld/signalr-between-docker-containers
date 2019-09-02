FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AndreSteenveld.SignalRInDocker.Hub/AndreSteenveld.SignalRInDocker.Hub.csproj", "AndreSteenveld.SignalRInDocker.Hub/"]
COPY ["AndreSteenveld.SignalRInDocker.Emitter/AndreSteenveld.SignalRInDocker.Emitter.csproj", "AndreSteenveld.SignalRInDocker.Emitter/"]
RUN dotnet restore "AndreSteenveld.SignalRInDocker.Hub/AndreSteenveld.SignalRInDocker.Hub.csproj"
COPY . .
WORKDIR "/src/AndreSteenveld.SignalRInDocker.Hub"
RUN dotnet build "AndreSteenveld.SignalRInDocker.Hub.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AndreSteenveld.SignalRInDocker.Hub.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AndreSteenveld.SignalRInDocker.Hub.dll"]