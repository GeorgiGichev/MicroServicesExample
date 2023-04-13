#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VendorService/VendorService.csproj", "VendorService/"]
COPY ["VendorService.Client/VendorService.Client.csproj", "VendorService.Client/"]
COPY ["VendorService.Services.DTOModels/VendorService.Services.DTOModels.csproj", "VendorService.Services.DTOModels/"]
COPY ["VendorService.Data.Models/VendorService.Data.Models.csproj", "VendorService.Data.Models/"]
COPY ["VendorService.Data.Common/VendorService.Data.Common.csproj", "VendorService.Data.Common/"]
COPY ["VendorService.Services.Mapping/VendorService.Services.Mapping.csproj", "VendorService.Services.Mapping/"]
COPY ["VendorService.Data/VendorService.Data.csproj", "VendorService.Data/"]
COPY ["VendorService.Services.Data/VendorService.Services.Data.csproj", "VendorService.Services.Data/"]
RUN dotnet restore "VendorService/VendorService.csproj"
COPY . .
WORKDIR "/src/VendorService"
RUN dotnet build "VendorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VendorService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VendorService.dll"]