#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FlexHealthApi/FlexHealthApi.csproj", "FlexHealthApi/"]
COPY ["FlexHealthDomain/FlexHealthDomain.csproj", "FlexHealthDomain/"]
COPY ["FlexHealthInfrastructure/FlexHealthInfrastructure.csproj", "FlexHealthInfrastructure/"]
RUN dotnet restore "FlexHealthApi/FlexHealthApi.csproj"
COPY . .
WORKDIR "/src/FlexHealthApi"
RUN dotnet build "FlexHealthApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlexHealthApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY FlexHealthApi/Resources /app/Resources
ENTRYPOINT ["dotnet", "FlexHealthApi.dll"]