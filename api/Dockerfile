# ------------------- BUILD phase (SDK) ------------------- #
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /

# copy csproj and restore as distinct layers
COPY /. ./
RUN ./restore.sh

WORKDIR /api
RUN dotnet publish -c release -o /app --no-restore

# --------------------- RUNTIME phase --------------------- #
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.14-amd64 AS runtime

WORKDIR /app

COPY --from=build /app ./
WORKDIR /app

EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "api.dll"] 
