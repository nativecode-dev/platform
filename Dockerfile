# -----------------------------------------------------------------------------
# STAGE: Identity
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime as identity
RUN ls -lah /
COPY .artifacts/identity /app
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
ENTRYPOINT ["dotnet", "identity.dll"]

# -----------------------------------------------------------------------------
# STAGE: Node
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime as node
COPY .artifacts/published/node /app
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
ENTRYPOINT ["dotnet", "node.dll"]

# -----------------------------------------------------------------------------
# STAGE: Delegate
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime as delegate
COPY .artifacts/published/node-delegate /app
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
ENTRYPOINT ["dotnet", "node-delegate.dll"]

# -----------------------------------------------------------------------------
# STAGE: Processor
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime as processor
COPY .artifacts/published/node-processor /app
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
ENTRYPOINT ["dotnet", "node-processor.dll"]

# -----------------------------------------------------------------------------
# STAGE: Watcher
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime as watcher
COPY .artifacts/published/node-watcher /app
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app
ENTRYPOINT ["dotnet", "node-watcher.dll"]
