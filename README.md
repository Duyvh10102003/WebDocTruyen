# 📚 WebDocTruyen - Novel Reading Website

Web đọc tiểu thuyết được xây dựng bằng ASP.NET MVC, hỗ trợ đọc truyện và quản lý nội dung với hệ thống xác thực và phân quyền người dùng.

---

## 🚀 Tính năng chính

- 📖 Xem danh sách và đọc truyện  
- 🔍 Lọc truyện theo tác giả, thể loại  
- 🔐 Đăng ký / đăng nhập người dùng  
- 🔑 Phân quyền:
  - **Admin**: Thêm / sửa / xóa truyện  
  - **User**: Xem và đọc truyện  
- ⚡ Load dữ liệu động bằng AJAX  

---

## 🛠️ Công nghệ sử dụng

- ASP.NET MVC (.NET)  
- Entity Framework (Code First + Migrations)  
- SQL Server  
- Razor View + JavaScript (AJAX)  

---

## 📁 Cấu trúc project

```text
📁 WebDocTruyen
│──📁 Areas
│ └── 📁 Admin # Khu vực quản trị
│── 📁 Controllers
│── 📁 Models
│── 📁 Repositories
│── 📁 ViewModel
│── 📁 Views
│── 📁 wwwroot
│── 📁 Program.cs
│── 📁 appsettings.json
```

---

## 🧠 Kiến trúc & xử lý

- Áp dụng mô hình **MVC (Model - View - Controller)**  
- Sử dụng **Repository Pattern** để tách logic dữ liệu  
- Dùng **ViewModel** để truyền dữ liệu sang View  
- Sử dụng **Areas (Admin)** để tách riêng phần quản trị  
- Tích hợp **Authentication & Authorization**  
- Kết hợp **AJAX + Partial View** để cải thiện trải nghiệm người dùng  

---

## 📌 Điểm nổi bật

- 🔐 Có **xác thực người dùng (Login/Register)**  
- 🔑 **Phân quyền rõ ràng (Admin / User)**  
- 🧩 Tách riêng khu vực Admin (Areas)  
- ⚡ Sử dụng AJAX giúp load dữ liệu nhanh hơn  
- 🧱 Code có cấu trúc rõ ràng, dễ mở rộng  

---
