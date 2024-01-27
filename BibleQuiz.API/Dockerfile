FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY BibleQuiz.API/BibleQuiz.API.csproj BibleQuiz.API/BibleQuiz.API.csproj
COPY BibleQuiz.Core/BibleQuiz.Core.csproj BibleQuiz.Core/BibleQuiz.Core.csproj

WORKDIR /app/BibleQuiz.API
RUN dotnet restore BibleQuiz.API.csproj

WORKDIR /app
COPY . .

WORKDIR /app/BibleQuiz.API

RUN dotnet publish -c Release -o /app/out BibleQuiz.API.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "BibleQuiz.API.dll"]