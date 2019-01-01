FROM microsoft/dotnet:aspnetcore-runtime AS runtime
ENV DEBIAN_FRONTEND=noninteractive
COPY .artifacts/published/identity /app
WORKDIR /app
ENTRYPOINT ["dotnet", "identity.dll"]
