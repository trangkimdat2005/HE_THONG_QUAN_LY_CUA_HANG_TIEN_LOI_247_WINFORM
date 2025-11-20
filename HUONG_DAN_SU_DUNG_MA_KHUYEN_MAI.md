# H??NG D?N S? D?NG TÍNH N?NG MÃ KHUY?N MÃI

## ?? T?ng quan

H? th?ng ?ã ???c c?p nh?t v?i các tính n?ng m?i ?? qu?n lý mã khuy?n mãi m?t cách linh ho?t và hi?u qu? h?n.

## ? Các tính n?ng m?i

### 1. **T?o mã khuy?n mãi ng?u nhiên**
- T? ??ng t?o mã code ng?u nhiên, duy nh?t
- Tùy ch?nh ti?n t? (prefix) cho mã
- T?o nhi?u mã cùng lúc (t?i ?a 100 mã/l?n)
- Ki?m tra trùng l?p t? ??ng

### 2. **Validation mã khuy?n mãi**
- B?t bu?c ph?i có ít nh?t 1 mã khuy?n mãi khi t?o/s?a ch??ng trình
- Ki?m tra mã trùng l?p trong database
- Ki?m tra mã trùng l?p trong danh sách hi?n t?i
- Validation giá tr? mã ph?i > 0

### 3. **Qu?n lý mã khuy?n mãi**
- Thêm mã th? công
- Xóa mã kh?i danh sách
- Hi?n th? thông tin ??y ??: Code, Giá tr?, S? l?n s? d?ng, Tr?ng thái

## ?? Cách s? d?ng

### A. Thêm m?i ch??ng trình khuy?n mãi

1. **M? form thêm m?i**
   - T? danh sách ch??ng trình khuy?n mãi, click nút "Thêm"
   - Form chi ti?t s? hi?n th?

2. **Nh?p thông tin ch??ng trình**
   - Mã ch??ng trình: T? ??ng sinh
   - Tên ch??ng trình: **(B?t bu?c)**
   - Lo?i: Ch?n t? dropdown
   - Ngày b?t ??u/k?t thúc: **(B?t bu?c, ngày k?t thúc > ngày b?t ??u)**
   - Mô t?: Tùy ch?n

3. **Thêm mã khuy?n mãi** (B?t bu?c ph?i có ít nh?t 1 mã)

   **Cách 1: T?o mã ng?u nhiên**
   - Click nút "?? T?o mã ng?u nhiên"
   - Nh?p thông tin:
     * S? l??ng mã: 1-100
     * Ti?n t? (tùy ch?n): VD: "SUMMER", "SALE"
     * Giá tr?: Giá tr? gi?m giá
     * Tr?ng thái: "Ho?t ??ng" ho?c "T?m d?ng"
   - Click "T?o mã"
   - H? th?ng s? t? ??ng t?o các mã duy nh?t

   **Cách 2: Thêm mã th? công**
   - Click nút "? Thêm mã"
   - Nh?p thông tin:
     * Mã code: VD: "NEWYEAR2025" **(B?t bu?c)**
     * Giá tr?: Giá tr? gi?m giá
     * Tr?ng thái: "Ho?t ??ng" ho?c "T?m d?ng"
   - Click "Thêm"

4. **Xóa mã khuy?n mãi**
   - Ch?n mã c?n xóa trong danh sách
   - Click nút "??? Xóa mã"
   - Xác nh?n xóa

5. **L?u ch??ng trình**
   - Click nút "L?u"
   - H? th?ng s? validate:
     * Tên ch??ng trình không ???c ?? tr?ng
     * Ngày k?t thúc ph?i sau ngày b?t ??u
     * Ph?i có ít nh?t 1 mã khuy?n mãi
     * Các mã không ???c trùng l?p
     * Giá tr? mã ph?i > 0

### B. Ch?nh s?a ch??ng trình khuy?n mãi

1. **M? form ch?nh s?a**
   - T? danh sách, ch?n ch??ng trình c?n s?a
   - Click nút "S?a"

2. **Ch?nh s?a thông tin**
   - Thay ??i các thông tin c?n thi?t
   - Thêm/xóa mã khuy?n mãi
   - **L?u ý**: S? l?n s? d?ng c?a mã c? s? ???c gi? nguyên

3. **L?u thay ??i**
   - Click "L?u"
   - H? th?ng s? validate t??ng t? nh? thêm m?i

## ?? C?u trúc d? li?u

### MaKhuyenMai (Promo Code)
```csharp
{
    id: string,              // Mã ??nh danh (MKM + s?)
    code: string,            // Mã khuy?n mãi (VD: "SUMMER-A1B2C3")
    giaTri: decimal,         // Giá tr? gi?m giá
    soLanSuDung: int,        // S? l?n ?ã s? d?ng
    trangThai: string,       // "Ho?t ??ng" / "T?m d?ng"
    chuongTrinhId: string    // Liên k?t ??n ch??ng trình
}
```

## ?? Các API m?i trong PromotionController

```csharp
// T?o mã ng?u nhiên
string GenerateRandomPromoCode(string prefix = "", int length = 6, bool includeNumbers = true)

// T?o nhi?u mã ng?u nhiên
List<string> GenerateMultiplePromoCodes(int count, string prefix = "", int length = 6, bool includeNumbers = true)

// T?o mã duy nh?t
string GenerateUniquePromoCode(string prefix = "", int length = 6, bool includeNumbers = true)

// Ki?m tra mã t?n t?i
bool IsPromoCodeExists(string code)

// T?o danh sách mã m?u
List<MaKhuyenMai> CreateSamplePromoCodes(int count, decimal defaultValue, string prefix = "")
```

## ?? L?u ý quan tr?ng

1. **Validation b?t bu?c**:
   - Ph?i có ít nh?t 1 mã khuy?n mãi khi t?o/s?a ch??ng trình
   - Mã không ???c trùng l?p (trong database và danh sách hi?n t?i)
   - Giá tr? mã ph?i l?n h?n 0

2. **??nh d?ng mã**:
   - Mã ng?u nhiên: 6 ký t? (A-Z, 0-9)
   - Có th? thêm ti?n t?: "SUMMER-A1B2C3"
   - Không phân bi?t ch? hoa/th??ng khi ki?m tra trùng

3. **Tr?ng thái mã**:
   - "Ho?t ??ng": Mã có th? s? d?ng
   - "T?m d?ng": Mã t?m th?i không s? d?ng ???c

4. **S? l?n s? d?ng**:
   - T? ??ng kh?i t?o = 0 khi t?o mã m?i
   - Gi? nguyên giá tr? khi c?p nh?t ch??ng trình

## ?? Ví d? s? d?ng

### Ví d? 1: T?o ch??ng trình gi?m giá mùa hè
```
Tên: Khuy?n mãi mùa hè 2025
Lo?i: Gi?m giá hóa ??n
Th?i gian: 01/06/2025 - 31/08/2025

Mã khuy?n mãi (T?o ng?u nhiên):
- S? l??ng: 10
- Ti?n t?: SUMMER
- Giá tr?: 50,000?
- Tr?ng thái: Ho?t ??ng

K?t qu?:
? SUMMER-A1B2C3 (50,000?)
? SUMMER-D4E5F6 (50,000?)
... (8 mã khác)
```

### Ví d? 2: T?o mã VIP th? công
```
Tên: Ch??ng trình VIP
Lo?i: Gi?m giá hóa ??n
Th?i gian: 01/01/2025 - 31/12/2025

Mã khuy?n mãi (Th? công):
- Mã code: VIP2025
- Giá tr?: 500,000?
- Tr?ng thái: Ho?t ??ng

K?t qu?:
? VIP2025 (500,000?)
```

## ?? X? lý l?i th??ng g?p

### L?i: "Ch??ng trình khuy?n mãi ph?i có ít nh?t 1 mã khuy?n mãi"
**Nguyên nhân**: Ch?a thêm mã khuy?n mãi vào ch??ng trình
**Gi?i pháp**: S? d?ng m?t trong hai cách t?o mã ? trên

### L?i: "Mã khuy?n mãi 'XXX' ?ã t?n t?i trong h? th?ng"
**Nguyên nhân**: Mã ?ã ???c s? d?ng trong ch??ng trình khác
**Gi?i pháp**: ??i mã khác ho?c s? d?ng tính n?ng t?o mã ng?u nhiên

### L?i: "Giá tr? khuy?n mãi ph?i l?n h?n 0"
**Nguyên nhân**: Nh?p giá tr? = 0
**Gi?i pháp**: Nh?p giá tr? > 0

### L?i: "Các mã sau b? trùng l?p: xxx, yyy"
**Nguyên nhân**: Có mã trùng l?p trong danh sách
**Gi?i pháp**: Xóa các mã trùng l?p

## ?? H? tr?

N?u g?p v?n ??, vui lòng liên h?:
- Email: support@example.com
- Hotline: 0123-456-789

---

**Phiên b?n**: 1.0
**Ngày c?p nh?t**: 20/11/2025
**Tác gi?**: Development Team
