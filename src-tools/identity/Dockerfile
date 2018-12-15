FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src-tools/identity/identity.csproj", "src-tools/identity/"]
RUN dotnet restore "src-tools/identity/identity.csproj"
COPY . .
WORKDIR "/src/src-tools/identity"
RUN dotnet build "identity.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "identity.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "identity.dll"]