#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM ccr.ccs.tencentyun.com/dotnet-core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
RUN echo 'Asia/Shanghai' >/etc/timezone

FROM ccr.ccs.tencentyun.com/dotnet-core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Partner.Surround.Admin/Partner.Surround.Admin.csproj", "src/Partner.Surround.Admin/"]
COPY ["src/Partner.Surround.EntityFrameworkCore/Partner.Surround.EntityFrameworkCore.csproj", "src/Partner.Surround.EntityFrameworkCore/"]
COPY ["src/Partner.Surround.Gateway/Partner.Surround.Gateway.csproj", "src/Partner.Surround.Gateway/"]
COPY ["src/Partner.Surround.Core/Partner.Surround.Core.csproj", "src/Partner.Surround.Core/"]
COPY ["src/Partner.Surround.Application/Partner.Surround.Application.csproj", "src/Partner.Surround.Application/"]
RUN dotnet restore "src/Partner.Surround.Admin/Partner.Surround.Admin.csproj"
COPY . .
WORKDIR "/src/src/Partner.Surround.Admin"
RUN dotnet build "Partner.Surround.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Partner.Surround.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Partner.Surround.Admin.dll"]
