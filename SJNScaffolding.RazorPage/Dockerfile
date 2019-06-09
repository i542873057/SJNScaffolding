#本地安装Docker for Windows后，可使用Docker方式运行
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
#WORKDIR /src
#COPY ["SJNScaffolding.RazorPage/SJNScaffolding.RazorPage.csproj", "SJNScaffolding.RazorPage/"]
#COPY ["SJNScaffolding/SJNScaffolding.csproj", "SJNScaffolding/"]
#RUN dotnet restore "SJNScaffolding.RazorPage/SJNScaffolding.RazorPage.csproj"
#COPY . .
#WORKDIR "/src/SJNScaffolding.RazorPage"
#RUN dotnet build "SJNScaffolding.RazorPage.csproj" -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish "SJNScaffolding.RazorPage.csproj" -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "SJNScaffolding.RazorPage.dll"]

# 发布时，使用此Dockerfile配置
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
COPY . /app
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "SJNScaffolding.RazorPage.dll"]