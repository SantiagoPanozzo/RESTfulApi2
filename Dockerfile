FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
RUN dotnet dev-certs https
RUN dotnet dev-certs https --trust
RUN dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM 
EXPOSE 80
EXPOSE 443
EXPOSE 7031

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TodoApi.csproj", "./"]
RUN dotnet restore "TodoApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build

FROM build AS publish

