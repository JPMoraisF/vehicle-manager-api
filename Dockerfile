# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
RUN dotnet dev-certs https --trust

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/

# Expose the port your application will run on
EXPOSE 80
EXPOSE 443
# Start the application
ENTRYPOINT ["dotnet", "VehicleManager.dll"]