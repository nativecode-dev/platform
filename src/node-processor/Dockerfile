FROM platform as artifacts

FROM microsoft/dotnet:aspnetcore-runtime AS runtime
ENV DEBIAN_FRONTEND=noninteractive
COPY --from=artifacts /build/.artifacts/published/node-processor /app
WORKDIR /app
ENTRYPOINT ["dotnet", "node-processor.dll"]
