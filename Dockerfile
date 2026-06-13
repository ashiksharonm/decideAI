FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project files first for layer caching
COPY ["DecideAI.sln", "./"]
COPY ["DecideAI.API/DecideAI.API.csproj", "DecideAI.API/"]
COPY ["DecideAI.Core/DecideAI.Core.csproj", "DecideAI.Core/"]
COPY ["DecideAI.Tests/DecideAI.Tests.csproj", "DecideAI.Tests/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY . .

# Build and publish the API
WORKDIR "/src/DecideAI.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 (Render expects web services to listen on a port, usually 80 or the PORT env var)
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "DecideAI.API.dll"]
