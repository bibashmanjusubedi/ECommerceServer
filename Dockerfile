# Build stage (Alpine)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln .
COPY *.csproj ./

# Restore dependencies
RUN dotnet restore

# Copy all source code to the working directory
COPY ./ ./

# Publish the app to /app/out folder
RUN dotnet publish -c Release -o /app/out

# Runtime stage (Alpine)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Set the URL the app listens to inside the container
ENV ASPNETCORE_URLS=http://+:80

# Copy published output from build stage
COPY --from=build /app/out ./

# Expose port 80
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "ecomServer.dll"]
