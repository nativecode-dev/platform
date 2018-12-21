FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src-tools/node-scheduler/node-scheduler.csproj", "src-tools/node-scheduler/"]
RUN dotnet restore "src-tools/node-scheduler/node-scheduler.csproj"
COPY . .
WORKDIR "/src/src-tools/node-scheduler"
RUN dotnet build "node-scheduler.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "node-scheduler.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "node-scheduler.dll"]