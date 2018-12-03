FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["node/node.csproj", "node/"]
RUN dotnet restore "node/node.csproj"
COPY . .
WORKDIR "/src/node"
RUN dotnet build "node.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "node.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "node.dll"]