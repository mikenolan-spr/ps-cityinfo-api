# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers to leverage Docker cache
COPY ["CityInfo.API.csproj", "."]
RUN dotnet restore "./CityInfo.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/."
RUN dotnet build "./CityInfo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./CityInfo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]

# To build this Dockerfile, navigate to the 'CityInfo.API' directory and run:
# docker build -t cityinfo-api .
#
# To run the container:
# docker run -d -p 8080:8080 --name my-cityinfo-app cityinfo-api