#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mcare21_Api/Mcare21_Api.csproj", "Mcare21_Api/"]
COPY ["Mcare21/Mcare21.csproj", "Mcare21/"]
COPY ["Mcare21_Api_Contracts/Mcare21_Api_Contracts.csproj", "Mcare21_Api_Contracts/"]
RUN dotnet restore "Mcare21_Api/Mcare21_Api.csproj"
COPY . .
WORKDIR "/src/Mcare21_Api"
RUN dotnet build "Mcare21_Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mcare21_Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mcare21_Api.dll"]