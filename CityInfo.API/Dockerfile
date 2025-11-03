# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

COPY ./creating-the-api-and-returning-resources-slides.pdf .

FROM base AS final
WORKDIR /app
COPY ./app/publish .
ENTRYPOINT ["dotnet", "CityInfo.API.dll"]

# To build this Dockerfile, navigate to the 'CityInfo.API' directory and run:
# docker build -t cityinfo-api .
#
# To run the container:
# docker run -d -p 8080:8080 --name my-cityinfo-app cityinfo-api