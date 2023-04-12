#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductService/ProductService.csproj", "ProductService/"]
COPY ["ProductService.Data.Common/ProductService.Data.Common.csproj", "ProductService.Data.Common/"]
COPY ["ProductService.Data/ProductService.Data.csproj", "ProductService.Data/"]
COPY ["ProductService.Data.Models/ProductService.Data.Models.csproj", "ProductService.Data.Models/"]
COPY ["ProductService.DTOModels/ProductService.DTOModels.csproj", "ProductService.DTOModels/"]
COPY ["ProductService.Services.Mapping/ProductService.Services.Mapping.csproj", "ProductService.Services.Mapping/"]
COPY ["ProductService.Services.Data/ProductService.Services.Data.csproj", "ProductService.Services.Data/"]
RUN dotnet restore "ProductService/ProductService.csproj"
COPY . .
WORKDIR "/src/ProductService"
RUN dotnet build "ProductService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]