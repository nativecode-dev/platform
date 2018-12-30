# -----------------------------------------------------------------------------
# STAGE: Runtime
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime AS RUNTIME
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app

# -----------------------------------------------------------------------------
# STAGE: Artifacts
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:sdk AS ARTIFACTS
# Build Arguments
ENV CAKE_VERSION "0.30.0"
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /build
# Copy Files
COPY .gitattributes /build
COPY .gitignore /build
COPY build.cake /build
COPY build.ps1 /build
COPY platform.sln /build
# Copy Folders
COPY templates/ /build/templates
COPY src/ /build/src
COPY src-libs/ /build/src-libs
COPY src-tests/ /build/src-tests
RUN set -ex \
    && curl --silent --location https://deb.nodesource.com/setup_8.x | bash - \
    && apt-get install nodejs -y \
    && mkdir tools \
    && cp templates/tools-csproj.template tools/tools.csproj \
    && dotnet add tools/tools.csproj package Cake.CoreCLR -v $CAKE_VERSION --package-directory tools/Cake.CoreCLR.$CAKE_VERSION \
    && dotnet tools/Cake.CoreCLR.$CAKE_VERSION/cake.coreclr/$CAKE_VERSION/Cake.dll build.cake -target="Default" -configuration="Release" \
    ;

# -----------------------------------------------------------------------------
# STAGE: Identity
# -----------------------------------------------------------------------------
FROM RUNTIME AS IDENTITY
WORKDIR /app
COPY --from=ARTIFACTS /build/.artifacts/published/identity /app
ENTRYPOINT ["dotnet", "identity.dll"]

# -----------------------------------------------------------------------------
# STAGE: Node
# -----------------------------------------------------------------------------
FROM RUNTIME AS NODE
WORKDIR /app
COPY --from=ARTIFACTS /build/.artifacts/published/node /app
ENTRYPOINT ["dotnet", "node.dll"]

# -----------------------------------------------------------------------------
# STAGE: Delegate
# -----------------------------------------------------------------------------
FROM RUNTIME AS DELEGATE
WORKDIR /app
COPY --from=ARTIFACTS /build/.artifacts/published/node-delegate /app
ENTRYPOINT ["dotnet", "node-delegate.dll"]

# -----------------------------------------------------------------------------
# STAGE: Processor
# -----------------------------------------------------------------------------
FROM RUNTIME AS PROCESSOR
WORKDIR /app
COPY --from=ARTIFACTS /build/.artifacts/published/node-processor /app
ENTRYPOINT ["dotnet", "node-processor.dll"]

# -----------------------------------------------------------------------------
# STAGE: Watcher
# -----------------------------------------------------------------------------
FROM RUNTIME AS WATCHER
WORKDIR /app
COPY --from=ARTIFACTS /build/.artifacts/published/node-watcher /app
ENTRYPOINT ["dotnet", "node-watcher.dll"]