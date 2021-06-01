FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Paylocity.Payroll.ApiService/Paylocity.Payroll.ApiService.csproj", "Paylocity.Payroll.ApiService/"]
RUN dotnet restore "Paylocity.Payroll.ApiService/Paylocity.Payroll.ApiService.csproj"
COPY . .
WORKDIR "/src/Paylocity.Payroll.ApiService"
RUN dotnet build "Paylocity.Payroll.ApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Paylocity.Payroll.ApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Paylocity.Payroll.ApiService.dll"]
