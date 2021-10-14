# sdk image download
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
# creating working directory 
WORKDIR /app

# copying all .csproj files
COPY *.csproj ./
# do restore
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# create runtime image and copy the what we build before
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "PlatformService.dll" ]