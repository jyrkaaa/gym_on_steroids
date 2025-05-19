#base layer
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#create catalog /app and cd into it
WORKDIR /app
#Copys solution from host catalog to containers active catalog
COPY *.sln .
# Copy all the projects.csproj filed to active containers Webapp catalog (also make it)
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.Contracts/*.csproj ./App.BLL.Contracts/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.DAL/*.csproj ./App.DAL/
COPY App.DAL.Contracts/*.csproj ./App.DAL.Contracts/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Domain.EF/*.csproj ./App.Domain.EF/
COPY App.Resources/*.csproj ./App.Resources/
COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.BLL.Contracts/*.csproj ./Base.BLL.Contracts/
COPY Base.Contracts/*.csproj ./Base.Contracts/
COPY Base.DAL.Contracts/*.csproj ./Base.DAL.Contracts/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Helpers/*.csproj ./Base.Helpers/
COPY Base.Resources/*.csproj ./Base.Resources/
COPY WebApp/*.csproj ./WebApp/

#install all nuget packets
RUN dotnet restore

COPY App.BLL/. ./App.BLL/
COPY App.BLL.Contracts/. ./App.BLL.Contracts/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.DAL/. ./App.DAL/
COPY App.DAL.Contracts/. ./App.DAL.Contracts/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DTO/. ./App.DTO/
COPY App.Domain.EF/. ./App.Domain.EF/
COPY App.Resources/. ./App.Resources/
COPY Base.BLL/. ./Base.BLL/
COPY Base.BLL.Contracts/. ./Base.BLL.Contracts/
COPY Base.Contracts/. ./Base.Contracts/
COPY Base.DAL.Contracts/. ./Base.DAL.Contracts/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Helpers/. ./Base.Helpers/
COPY Base.Resources/. ./Base.Resources/
COPY WebApp/. ./WebApp/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ConnectionStrings:SqlServerConnection="Host=host.docer.internal;Port=5432;Database=gymDb;Username=postgres;Password=postgres"
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]