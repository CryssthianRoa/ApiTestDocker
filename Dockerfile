#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Use the official .NET 7 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy csproj and restore dependencies
COPY ./*.sln ./
COPY ApiTestDocker/*.csproj ./ApiTestDocker/
COPY LibraryTest/*.csproj ./LibraryTest/
RUN dotnet restore "LibraryTest/LibraryTest.csproj"
RUN dotnet restore "ApiTestDocker/ApiTestDocker.csproj"

# Copy everything else and build the API project
COPY ApiTestDocker/. ./ApiTestDocker/
COPY LibraryTest/. ./LibraryTest/
RUN dotnet publish ./ApiTestDocker -c Release -o /app

# Use the .NET 7 runtime image as the final base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the built API from the build image
COPY --from=build /app .

# Expose port 80 for the Web API traffic
EXPOSE 80

# Run the API when the container is run
ENTRYPOINT ["dotnet", "ApiTestDocker.dll"]
