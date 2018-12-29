# -----------------------------------------------------------------------------
# STAGE: Runtime
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:aspnetcore-runtime AS RUNTIME
ENV DEBIAN_FRONTEND=noninteractive
WORKDIR /app

# -----------------------------------------------------------------------------
# STAGE: Build
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:sdk AS BUILD
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
# STAGE: Final
# -----------------------------------------------------------------------------
FROM RUNTIME
WORKDIR /app
COPY --from=BUILD /build/.artifacts/published/node-processor /app
ENTRYPOINT ["dotnet", "node-delegate.dll"]
