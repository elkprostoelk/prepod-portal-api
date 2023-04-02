#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PrepodPortal.WebAPI/PrepodPortal.WebAPI.csproj", "PrepodPortal.WebAPI/"]
COPY ["PrepodPortal.Common/PrepodPortal.Common.csproj", "PrepodPortal.Common/"]
COPY ["PrepodPortal.Core/PrepodPortal.Core.csproj", "PrepodPortal.Core/"]
COPY ["PrepodPortal.DataAccess/PrepodPortal.DataAccess.csproj", "PrepodPortal.DataAccess/"]
RUN dotnet restore "PrepodPortal.WebAPI/PrepodPortal.WebAPI.csproj"
COPY . .
WORKDIR "/src/PrepodPortal.WebAPI"
RUN dotnet build "PrepodPortal.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrepodPortal.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrepodPortal.WebAPI.dll"]