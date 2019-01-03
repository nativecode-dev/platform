FROM microsoft/dotnet:aspnetcore-runtime AS runtime
ENV DEBIAN_FRONTEND=noninteractive
RUN sert -ex \
    && ls -lah / \
    && ls -lah /var/ \
    ;
COPY /drone/.artifacts/published/identity /app
WORKDIR /app
ENTRYPOINT ["dotnet", "identity.dll"]
