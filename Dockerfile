# -----------------------------------------------------------------------------
# STAGE: Identity
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
COPY /.artifacts/published/identity /app
ENTRYPOINT ["dotnet", "identity.dll"]

# -----------------------------------------------------------------------------
# STAGE: Node
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
COPY /.artifacts/published/node /app
ENTRYPOINT ["dotnet", "node.dll"]

# -----------------------------------------------------------------------------
# STAGE: Delegate
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
COPY /.artifacts/published/node-delegate /app
ENTRYPOINT ["dotnet", "node-delegate.dll"]

# -----------------------------------------------------------------------------
# STAGE: Processor
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
COPY /.artifacts/published/node-processor /app
ENTRYPOINT ["dotnet", "node-processor.dll"]

# -----------------------------------------------------------------------------
# STAGE: Watcher
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
COPY /.artifacts/published/node-watcher /app
ENTRYPOINT ["dotnet", "node-watcher.dll"]
