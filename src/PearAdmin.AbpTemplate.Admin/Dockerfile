#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["module/PearAdmin.AbpTemplate.Storage.Minio/PearAdmin.AbpTemplate.Storage.Minio.csproj", "module/PearAdmin.AbpTemplate.Storage.Minio/"]
COPY ["module/PearAdmin.AbpTemplate.Storage/PearAdmin.AbpTemplate.Storage.csproj", "module/PearAdmin.AbpTemplate.Storage/"]
COPY ["module/PearAdmin.AbpTemplate.NLog/PearAdmin.AbpTemplate.NLog.csproj", "module/PearAdmin.AbpTemplate.NLog/"]
COPY ["src/PearAdmin.AbpTemplate.Admin/PearAdmin.AbpTemplate.Admin.csproj", "src/PearAdmin.AbpTemplate.Admin/"]
COPY ["src/PearAdmin.AbpTemplate.Gateway/PearAdmin.AbpTemplate.Gateway.csproj", "src/PearAdmin.AbpTemplate.Gateway/"]
COPY ["src/PearAdmin.AbpTemplate.Application/PearAdmin.AbpTemplate.Application.csproj", "src/PearAdmin.AbpTemplate.Application/"]
COPY ["src/PearAdmin.AbpTemplate.Core/PearAdmin.AbpTemplate.Core.csproj", "src/PearAdmin.AbpTemplate.Core/"]
COPY ["src/PearAdmin.AbpTemplate.EntityFrameworkCore/PearAdmin.AbpTemplate.EntityFrameworkCore.csproj", "src/PearAdmin.AbpTemplate.EntityFrameworkCore/"]
COPY ["tool/PearAdmin.AbpTemplate.Shared/PearAdmin.AbpTemplate.Shared.csproj", "tool/PearAdmin.AbpTemplate.Shared/"]
RUN dotnet restore "src/PearAdmin.AbpTemplate.Admin/PearAdmin.AbpTemplate.Admin.csproj"
COPY . .
WORKDIR "/src/src/PearAdmin.AbpTemplate.Admin"
RUN dotnet build "PearAdmin.AbpTemplate.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PearAdmin.AbpTemplate.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PearAdmin.AbpTemplate.Admin.dll"]