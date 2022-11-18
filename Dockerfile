FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CountryVATCalculator/CountryVATCalculator.csproj", "CountryVATCalculator/"]
RUN dotnet restore "CountryVATCalculator/CountryVATCalculator.csproj"
COPY . .
WORKDIR "/src/CountryVATCalculator"
RUN dotnet build "CountryVATCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountryVATCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountryVATCalculator.dll"]