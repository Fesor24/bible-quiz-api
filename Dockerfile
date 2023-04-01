#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BibleQuiz.API/BibleQuiz.API.csproj", "BibleQuiz.API/"]
COPY ["BibleQuiz.Core/BibleQuiz.Core.csproj", "BibleQuiz.Core/"]
RUN dotnet restore "BibleQuiz.API/BibleQuiz.API.csproj"
COPY . .
WORKDIR "/src/BibleQuiz.API"
RUN dotnet build "BibleQuiz.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BibleQuiz.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BibleQuiz.API.dll"]