# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy the project file and restore dependencies
COPY ["PassGen.csproj", "./"]
RUN dotnet restore "PassGen.csproj"
# Copy all files and build the app in Release configuration
COPY . .
RUN dotnet build "PassGen.csproj" -c Release -o /app/build

# Stage 2: Publish the app
FROM build AS publish
RUN dotnet publish "PassGen.csproj" -c Release -o /app/publish

# Stage 3: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PassGen.dll"]
