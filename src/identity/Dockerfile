FROM microsoft/dotnet:aspnetcore-runtime AS runtime
ENV DEBIAN_FRONTEND=noninteractive
RUN ls -lah /
COPY /drone/.artifacts/published/identity /app
WORKDIR /app
ENTRYPOINT ["dotnet", "identity.dll"]
