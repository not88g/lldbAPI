# 1. Берём официальный .NET SDK образ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# 2. Устанавливаем рабочую директорию
WORKDIR /app

# 3. Копируем csproj и восстанавливаем зависимости
COPY lldbAPI.csproj ./
RUN dotnet restore

# 4. Копируем весь проект
COPY . ./

# 5. Сборка релиза
RUN dotnet publish lldbAPI.csproj -c Release -o /app/publish

# 6. Создаём runtime образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 7. Открываем порт
EXPOSE 5000

# 8. Команда запуска
ENTRYPOINT ["dotnet", "lldbAPI.dll"]
