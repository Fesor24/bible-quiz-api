FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY BibleQuiz.API/BibleQuiz.API.csproj src/BibleQuiz.API/BibleQuiz.API.csproj
COPY BibleQuiz.Core/BibleQuiz.Core.csproj src/BibleQuiz.Core/BibleQuiz.Core.csproj

WORKDIR /app/src/BibleQuiz.API
RUN dotnet restore BibleQuiz.API.csproj

WORKDIR /app/src
COPY . .

WORKDIR /app/src/BibleQuiz.API

RUN dotnet publish -c Release -o /app/out BibleQuiz.API.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BibleQuiz.API.dll"]