# -----------------------------------------------------------------------------
# STAGE: Runtime
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime AS runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app

# -----------------------------------------------------------------------------
# STAGE: Build
# -----------------------------------------------------------------------------
FROM nativecode/platform-artifacts as build

# -----------------------------------------------------------------------------
# STAGE: Final
# -----------------------------------------------------------------------------
FROM runtime
WORKDIR /app
COPY --from=build /build/.artifacts/published/identity /app
ENTRYPOINT ["dotnet", "identity.dll"]
