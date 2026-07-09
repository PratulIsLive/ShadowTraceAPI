
# Project Building Stage

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore ShadowTraceAPI.csproj

RUN dotnet publish ShadowTraceAPI.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# Runtime Stage

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

EXPOSE 10000

ENTRYPOINT ["dotnet", "ShadowTraceAPI.dll"]