# Customer Management Module - MVC Architecture

## T?ng quan
Module qu?n lý khách hàng ???c xây d?ng theo mô hình MVC (Model-View-Controller) ?? ??m b?o tính module hóa, d? b?o trì và m? r?ng.

## C?u trúc MVC

### 1. Models (Th? m?c: Models/)
Các entity class ??i di?n cho d? li?u trong c? s? d? li?u:

- **KhachHang.cs**: Entity chính cho thông tin khách hàng
  - Thu?c tính: id, hoTen, soDienThoai, email, diaChi, ngayDangKy, trangThai, gioiTinh, anhId, isDelete
  - Navigation Properties: DonHangOnlines, HoaDons, LichSuMuaHangs, PhieuXuats, TaiKhoanKhachHangs, TheThanhViens

- **TheThanhVien.cs**: Entity cho th? thành viên
  - Thu?c tính: id, khachHangId, hang (??ng/B?c/Vàng), diemTichLuy, ngayCap, isDelete

- **LichSuMuaHang.cs**: Entity cho l?ch s? mua hàng
  - Thu?c tính: khachHangId, hoaDonId, tongTien, ngayMua, isDelete

### 2. DataAccessLayer (Th? m?c: DataAccessLayer/Repositories/)
Repository pattern ?? t??ng tác v?i c? s? d? li?u:

**CustomerRepository.cs** - Các ph??ng th?c CRUD:
- `GetAllCustomers()`: L?y t?t c? khách hàng
- `GetCustomerById(string id)`: L?y khách hàng theo ID
- `GetCustomerByPhone(string phone)`: L?y khách hàng theo s? ?i?n tho?i
- `AddCustomer(KhachHang customer)`: Thêm khách hàng m?i
- `UpdateCustomer(KhachHang customer)`: C?p nh?t thông tin khách hàng
- `DeleteCustomer(string id)`: Xóa khách hàng (soft delete)
- `SearchCustomers(string keyword)`: Tìm ki?m khách hàng
- `GetVIPCustomers()`: L?y danh sách khách hàng VIP
- `GetCustomerTotalPurchase(string customerId)`: Tính t?ng ti?n mua hàng
- `GetCustomerPurchaseCount(string customerId)`: ??m s? l?n mua hàng
- `GetPurchaseHistory(string customerId)`: L?y l?ch s? mua hàng
- `GetMemberCard(string customerId)`: L?y th? thành viên
- `SaveMemberCard(TheThanhVien memberCard)`: L?u/c?p nh?t th? thành viên

### 3. BusinessLayer (Th? m?c: BusinessLayer/Services/)
X? lý business logic và validation:

**CustomerService.cs** - Logic nghi?p v?:
- Validation d? li?u khách hàng (h? tên, s? ?i?n tho?i, email, ??a ch?)
- Ki?m tra s? ?i?n tho?i trùng l?p
- Tính toán ?i?m tích l?y: 1,000 VND = 1 ?i?m
- Xác ??nh h?ng th? d?a trên ?i?m:
  - ??ng: < 500 ?i?m
  - B?c: 500 - 999 ?i?m
  - Vàng: ? 1,000 ?i?m
- X? lý các quy t?c nghi?p v? khác

**CustomerDetailDto** - Data Transfer Object:
- Ch?a thông tin t?ng h?p c?a khách hàng
- Customer, PurchaseHistory, MemberCard, TotalPurchase, PurchaseCount

### 4. Controllers (Th? m?c: Controllers/)
?i?u ph?i gi?a View và BusinessLayer:

**CustomerController.cs** - Controller chính:
- Nh?n request t? View
- G?i Service ?? x? lý logic
- X? lý exception và tr? v? k?t qu? cho View
- Các ph??ng th?c t??ng ?ng v?i các action trên View

### 5. Views (Th? m?c: Views/Forms/Customers/)
Giao di?n ng??i dùng:

**frmCustomers.cs** - Form qu?n lý khách hàng chính:
- Thêm, s?a, xóa khách hàng
- Tìm ki?m khách hàng
- Xem chi ti?t khách hàng
- Xem khách hàng VIP

**frmCustomerList.cs** - Form danh sách khách hàng:
- Hi?n th? danh sách khách hàng
- L?c theo tr?ng thái, h?ng th?
- Th?ng kê khách hàng
- S?p x?p danh sách
- Xu?t báo cáo

**frmPurchaseHistory.cs** - Form l?ch s? mua hàng:
- Xem l?ch s? mua hàng c?a khách hàng
- L?c theo kho?ng th?i gian
- Xem chi ti?t hóa ??n
- Tính t?ng ti?n và s? l?n mua

**frmMemberCards.cs** - Form qu?n lý th? thành viên:
- Xem danh sách th? thành viên
- C?p nh?t thông tin th?
- C?ng ?i?m tích l?y
- Nâng/h? h?ng th?
- Xem l?ch s? tích ?i?m

## Lu?ng ho?t ??ng (Flow)

### Thêm khách hàng m?i:
```
View (frmCustomers) 
  ? Controller (CustomerController.AddCustomer)
    ? Service (CustomerService.AddCustomer)
      - Validate d? li?u
      - Ki?m tra s? ?i?n tho?i trùng
      ? Repository (CustomerRepository.AddCustomer)
        - Sinh ID t? ??ng
        - L?u vào database
      ? Tr? v? k?t qu?
    ? Tr? v? (success, message)
  ? Hi?n th? thông báo
```

### Tìm ki?m khách hàng:
```
View (frmCustomers)
  ? Controller (CustomerController.SearchCustomers)
    ? Service (CustomerService.SearchCustomers)
      ? Repository (CustomerRepository.SearchCustomers)
        - Query database theo keyword
      ? List<KhachHang>
    ? List<KhachHang>
  ? Hi?n th? k?t qu? trên DataGridView
```

### C?p nh?t th? thành viên:
```
View (frmMemberCards)
  ? Controller (CustomerController.UpdateMemberCard)
    ? Service (CustomerService.UpdateMemberCard)
      - Validate khách hàng t?n t?i
      ? Repository (CustomerRepository.SaveMemberCard)
        - Ki?m tra th? ?ã t?n t?i
        - Insert ho?c Update
      ? K?t qu?
    ? (success, message)
  ? Hi?n th? thông báo
```

## ?u ?i?m c?a ki?n trúc MVC

1. **Separation of Concerns**: Tách bi?t logic x? lý, d? li?u và giao di?n
2. **Maintainability**: D? b?o trì và s?a l?i
3. **Testability**: D? dàng vi?t unit test cho t?ng layer
4. **Reusability**: Có th? tái s? d?ng Service và Repository cho nhi?u View khác nhau
5. **Scalability**: D? m? r?ng thêm ch?c n?ng m?i

## Quy t?c coding

1. **Repository**: Ch? ch?a code truy v?n database, không có business logic
2. **Service**: Ch?a business logic, validation, x? lý d? li?u
3. **Controller**: ?i?u ph?i gi?a View và Service, x? lý exception
4. **View**: Ch? x? lý hi?n th? và nh?n input t? user, g?i Controller

## M? r?ng trong t??ng lai

- Thêm caching cho d? li?u khách hàng th??ng xuyên truy v?n
- Implement async/await cho các thao tác database
- Thêm logging cho các thao tác quan tr?ng
- Thêm Unit of Work pattern ?? qu?n lý transaction
- Implement repository interface ?? d? mock khi testing
- Thêm AutoMapper ?? map gi?a Entity và DTO

## Ghi chú

- T?t c? các thao tác xóa ??u là soft delete (set isDelete = true)
- ID ???c sinh t? ??ng theo format: KH001, KH002, ...
- Th? thành viên ID: TTV001, TTV002, ...
- ?i?m tích l?y ???c tính t? ??ng khi có giao d?ch mua hàng m?i
