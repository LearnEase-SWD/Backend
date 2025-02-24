# Sử dụng .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy dự án vào container và restore dependencies
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Tạo runtime container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Chạy ứng dụng (Đổi tên đúng với project của bạn)
CMD ["dotnet", "LearnEase.API.dll"]
