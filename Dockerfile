FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
EXPOSE 7031
EXPOSE 5021
WORKDIR /src
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
COPY . .
RUN dotnet restore
CMD dotnet run --urls=http://0.0.0.0:7000