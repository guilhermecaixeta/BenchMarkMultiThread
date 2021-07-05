#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
# WORKDIR /app

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["BenchMarkMultiThread/BenchMarkMultiThread.csproj", "BenchMarkMultiThread/"]
# RUN dotnet restore "BenchMarkMultiThread/BenchMarkMultiThread.csproj"
# COPY . .
# WORKDIR "/src/BenchMarkMultiThread"
# RUN dotnet build "BenchMarkMultiThread.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "BenchMarkMultiThread.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "BenchMarkMultiThread.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BenchMarkMultiThread/BenchMarkMultiThread.csproj", "BenchMarkMultiThread/"]
RUN dotnet restore "BenchMarkMultiThread/BenchMarkMultiThread.csproj"
COPY . .
# WORKDIR "/src/BenchMarkMultiThread"
RUN dotnet build "/src/BenchMarkMultiThread/BenchMarkMultiThread.csproj" -c Release -o /src/BenchMarkMultiThread/app/build

FROM build AS publish
RUN dotnet publish "/src/BenchMarkMultiThread/BenchMarkMultiThread.csproj" -c Release -o /src/BenchMarkMultiThread/app/publish

FROM base AS final
WORKDIR /src/BenchMarkMultiThread/app/publish
COPY --from=publish /src /src
ENTRYPOINT ["dotnet", "BenchMarkMultiThread.dll"]