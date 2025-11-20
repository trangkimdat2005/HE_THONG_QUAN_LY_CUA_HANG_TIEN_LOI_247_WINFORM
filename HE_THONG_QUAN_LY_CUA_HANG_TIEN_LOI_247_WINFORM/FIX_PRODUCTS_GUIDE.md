# ?? KI?M TRA VÀ S?A TOÀN B? frmProducts

## ?? CÁC V?N ?? PHÁT HI?N:

### 1. **DataPropertyName không kh?p:**

**Designer.cs:**
```csharp
colId.DataPropertyName = "id";              // ? Sai
colProductName.DataPropertyName = "ten";    // ? Sai
colDescription.DataPropertyName = "moTa";   // ? Sai
donVi - KHÔNG CÓ DataPropertyName          // ? Sai
giaBan - KHÔNG CÓ DataPropertyName         // ? Sai
trangThai - KHÔNG CÓ DataPropertyName      // ? Sai
```

**ProductDetailDto:**
```csharp
public string Id { get; set; }              // ? ?úng (vi?t hoa)
public string TenSanPham { get; set; }      // ? ?úng
public string MoTa { get; set; }            // ? ?úng
public string DonVi { get; set; }           // ? ?úng
public decimal GiaBan { get; set; }         // ? ?úng
public string TrangThai { get; set; }       // ? ?úng
```

### 2. **Thi?u mapping trong ProductService:**

?ang map:
- ? Id
- ? TenSanPham
- ? NhanHieu
- ? DanhMuc
- ? DonVi
- ? GiaBan
- ? TrangThai
- ? MoTa

---

## ?? GI?I PHÁP:

### **B??c 1: S?a Designer (th? công trong Visual Studio)**

M? `frmProducts.Designer.cs` và s?a:

```csharp
// S?a:
colId.DataPropertyName = "Id";              // Vi?t hoa I
colProductName.DataPropertyName = "TenSanPham";  // Không ph?i "ten"
colDescription.DataPropertyName = "MoTa";   // Vi?t hoa M

// Thêm:
donVi.DataPropertyName = "DonVi";
giaBan.DataPropertyName = "GiaBan";
trangThai.DataPropertyName = "TrangThai";
```

### **B??c 2: Test d? li?u**

Ch?y query này ?? ki?m tra:

```sql
-- 1. Ki?m tra SanPham
SELECT TOP 5 * FROM core.SanPham WHERE isDelete = 0;

-- 2. Ki?m tra DonViDoLuong
SELECT * FROM core.DonViDoLuong WHERE isDelete = 0;

-- 3. Ki?m tra SanPhamDonVi
SELECT TOP 5 * FROM core.SanPhamDonVi WHERE isDelete = 0;

-- 4. Test FilterProducts (gi?ng code C#)
SELECT 
    spd.id AS 'Id',
    sp.id AS 'MaSP',
    sp.ten AS 'TenSanPham',
    nh.ten AS 'NhanHieu',
    (SELECT TOP 1 dm.ten 
     FROM core.SanPhamDanhMuc spdm 
     INNER JOIN core.DanhMuc dm ON spdm.danhMucId = dm.id 
     WHERE spdm.sanPhamId = sp.id AND spdm.isDelete = 0 AND dm.isDelete = 0
    ) AS 'DanhMuc',
    dv.ten AS 'DonVi',
    spd.heSoQuyDoi AS 'HeSoQuyDoi',
    spd.giaBan AS 'GiaBan',
    spd.trangThai AS 'TrangThai',
    sp.moTa AS 'MoTa'
FROM core.SanPhamDonVi spd
INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
INNER JOIN core.NhanHieu nh ON sp.nhanHieuId = nh.id
INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
WHERE spd.isDelete = 0 AND sp.isDelete = 0;
```

### **B??c 3: N?u không có d? li?u, ch?y script m?u:**

```sql
-- Thêm d? li?u m?u cho SanPhamDonVi
INSERT INTO core.SanPhamDonVi (sanPhamId, donViId, id, heSoQuyDoi, giaBan, isDelete, trangThai)
SELECT TOP 1
    sp.id,
    dv.id,
    'SPDV' + RIGHT('000000' + CAST(ROW_NUMBER() OVER (ORDER BY sp.id) AS VARCHAR), 6),
    1,
    10000,
    0,
    'Available'
FROM core.SanPham sp
CROSS JOIN core.DonViDoLuong dv
WHERE sp.isDelete = 0 AND dv.isDelete = 0
    AND NOT EXISTS (
        SELECT 1 FROM core.SanPhamDonVi spd2 
        WHERE spd2.sanPhamId = sp.id AND spd2.donViId = dv.id
    );
```

---

## ?? CHECKLIST S?A L?I:

### **Trong Designer.cs:**
- [ ] S?a `colId.DataPropertyName = "Id"`
- [ ] S?a `colProductName.DataPropertyName = "TenSanPham"`
- [ ] Thêm `donVi.DataPropertyName = "DonVi"`
- [ ] Thêm `giaBan.DataPropertyName = "GiaBan"`
- [ ] Thêm `trangThai.DataPropertyName = "TrangThai"`
- [ ] S?a `colDescription.DataPropertyName = "MoTa"` (n?u có)

### **Trong Database:**
- [ ] Có d? li?u trong `core.SanPham`
- [ ] Có d? li?u trong `core.DonViDoLuong`
- [ ] Có d? li?u trong `core.NhanHieu`
- [ ] Có d? li?u trong `core.DanhMuc`
- [ ] Có d? li?u trong `core.SanPhamDonVi`

### **Test:**
- [ ] Build thành công
- [ ] Form m? ???c
- [ ] DataGridView hi?n th? d? li?u
- [ ] ComboBoxes load ?úng
- [ ] Thêm m?i thành công
- [ ] S?a thành công
- [ ] Xóa thành công

---

## ?? CÁC B??C TH?C HI?N:

### **1. S?a Designer.cs (QUAN TR?NG):**

M? file `frmProducts.Designer.cs` trong Visual Studio:

1. Tìm dòng `colId.DataPropertyName = "id";`
2. S?a thành `colId.DataPropertyName = "Id";`

3. Tìm dòng `colProductName.DataPropertyName = "ten";`
4. S?a thành `colProductName.DataPropertyName = "TenSanPham";`

5. Tìm dòng khai báo c?t `donVi`:
```csharp
// donVi
// 
this.donVi.HeaderText = "??n v?";
this.donVi.MinimumWidth = 6;
this.donVi.Name = "donVi";
this.donVi.ReadOnly = true;
```

6. Thêm dòng này:
```csharp
this.donVi.DataPropertyName = "DonVi";
```

7. Làm t??ng t? v?i `giaBan` và `trangThai`:
```csharp
this.giaBan.DataPropertyName = "GiaBan";
this.trangThai.DataPropertyName = "TrangThai";
```

### **2. Rebuild:**
```
Ctrl + Shift + B
```

### **3. Run & Test:**
```
F5
```

---

## ?? K?T QU? MONG MU?N:

Sau khi s?a, DataGridView s? hi?n th?:

| Mã SP | Tên s?n ph?m | Nhãn hi?u | Danh m?c | ??n v? | Giá bán | Mô t? | Tr?ng thái |
|-------|--------------|-----------|----------|--------|---------|-------|------------|
| SPDV000001 | Coca-Cola | Coca-Cola | N??c gi?i khát | Lon | 10,000 | ... | Available |
| SPDV000002 | Pepsi | Pepsi | N??c gi?i khát | Lon | 9,500 | ... | Available |

---

**N?u v?n g?p l?i, hãy:**
1. Ch?y query ki?m tra d? li?u
2. Xem Output Window ? Debug
3. G?i screenshot l?i cho tôi

**File này s? h??ng d?n chi ti?t t?ng b??c!** ??
