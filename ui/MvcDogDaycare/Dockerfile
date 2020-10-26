FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
WORKDIR /src
COPY  . /src
RUN dotnet publish -c Release -f netcoreapp3.1

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=builder /src/bin/Release/netcoreapp3.1/* /app/
COPY entrypoint.sh /app/
ENTRYPOINT [ "dotnet", "MvcDogDaycare.dll" ]
