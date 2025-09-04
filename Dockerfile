# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем все файлы и билдим релиз
COPY . ./
RUN dotnet publish -c Release -o out

# Используем runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Указываем порт, который слушает Render
EXPOSE 10000

# Запуск приложения
ENTRYPOINT ["dotnet", "lldbAPI.dll"]
