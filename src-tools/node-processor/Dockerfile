# -----------------------------------------------------------------------------
# STAGE: Runtime
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS RUNTIME
WORKDIR /app
RUN set -ex \
    && apk add --update --no-cache libc6-compat \
    ;

# -----------------------------------------------------------------------------
# STAGE: Build
# -----------------------------------------------------------------------------
FROM microsoft/dotnet:2.1-sdk-alpine AS BUILD
# Build Arguments
ENV CAKE_VERSION "0.30.0"
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
COPY src-tools/ /build/src-tools
COPY src-tests/ /build/src-tests
RUN set -ex \
    && apk add --update --no-cache nodejs nodejs-npm \
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
RUN apk update && apk add libc6-compat && chmod +x /app/runtimes -R && ls -lahR /app
ENTRYPOINT ["dotnet", "node-processor.dll"]
