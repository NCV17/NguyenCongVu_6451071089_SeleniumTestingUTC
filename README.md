# SeleniumTestingUTC

## 1. Project description
Dự án kiểm thử tự động cho website Văn phòng điện tử UTC (https://vanphongdientu.utc.edu.vn/Login).

## 2. Technology stack
- C# & .NET 9.0
- NUnit
- Selenium WebDriver
- Page Object Model (POM)
- Allure Report

## 3. Project structure
- `SeleniumTestingUTC.Tests/Pages`: Chứa các Page Object
- `SeleniumTestingUTC.Tests/Tests`: Chứa các Test script
- `SeleniumTestingUTC.Tests/Utilities`: Chứa các tiện ích, ví dụ WebDriverFactory
- `Docs/`: Chứa các tài liệu yêu cầu, kịch bản kiểm thử và báo cáo

## 4. Cách restore
```bash
dotnet restore
```

## 5. Cách build
```bash
dotnet build
```

## 6. Cách chạy test
```bash
dotnet test
```

## 7. Cách tạo Allure Report
Allure sẽ sinh kết quả vào thư mục `allure-results` tại thư mục gốc của project. Để xem báo cáo, chạy lệnh (cần cài đặt Java và Allure CLI):
```bash
allure serve allure-results
```

## 8. Git workflow
Mỗi test case tương ứng với đúng 1 commit.

## 9. Danh sách 12 test case
- TC01 - Đăng nhập thành công
- TC02 - Sai Username
- TC03 - Sai Password
- TC04 - Bỏ trống Username và Password
- TC05 - Remember Me
- TC06 - Đăng nhập bằng e-mail UTC
- TC07 - Lấy lại mật khẩu
- TC08 - Response Time (Performance - JMeter)
- TC09 - Load Testing (Performance - JMeter)
- TC10 - Stress Testing (Performance - JMeter)
- TC11 - SQL Injection - Username
- TC12 - SQL Injection - Username + Password
