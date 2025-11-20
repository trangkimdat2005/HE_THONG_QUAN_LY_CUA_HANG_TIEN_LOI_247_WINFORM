-- ================================================
-- SCRIPT TEST TOÀN B? frmProducts
-- ================================================

-- 1. KI?M TRA D? LI?U CÓ S?N
-- ================================================

PRINT '=== 1. KI?M TRA D? LI?U ===';

-- Ki?m tra Nhãn hi?u
SELECT COUNT(*) AS 'S? nhãn hi?u' FROM core.NhanHieu WHERE isDelete = 0;
SELECT TOP 5 id, ten FROM core.NhanHieu WHERE isDelete = 0;

-- Ki?m tra Danh m?c
SELECT COUNT(*) AS 'S? danh m?c' FROM core.DanhMuc WHERE isDelete = 0;
SELECT TOP 5 id, ten FROM core.DanhMuc WHERE isDelete = 0;

-- Ki?m tra Hàng hóa
SELECT COUNT(*) AS 'S? hàng hóa' FROM core.SanPham WHERE isDelete = 0;
SELECT TOP 5 id, ten, nhanHieuId FROM core.SanPham WHERE isDelete = 0;

-- Ki?m tra ??n v?
SELECT COUNT(*) AS 'S? ??n v?' FROM core.DonViDoLuong WHERE isDelete = 0;
SELECT * FROM core.DonViDoLuong WHERE isDelete = 0;

-- Ki?m tra SanPhamDonVi
SELECT COUNT(*) AS 'S? s?n ph?m ??n v?' FROM core.SanPhamDonVi WHERE isDelete = 0;
SELECT TOP 5 * FROM core.SanPhamDonVi WHERE isDelete = 0;

-- ================================================
-- 2. QUERY GI?NG NH? ProductService.FilterProducts()
-- ================================================

PRINT '';
PRINT '=== 2. TEST QUERY FILTERPRODUCTS ===';

SELECT 
    spd.id AS 'Id',
    spd.sanPhamId AS 'SanPhamId',
    spd.donViId AS 'DonViId',
    sp.id AS 'MaSP',
    sp.ten AS 'TenSanPham',
    ISNULL(nh.ten, 'Ch?a có') AS 'NhanHieu',
    ISNULL((
        SELECT TOP 1 dm.ten 
        FROM core.SanPhamDanhMuc spdm 
        INNER JOIN core.DanhMuc dm ON spdm.danhMucId = dm.id 
        WHERE spdm.sanPhamId = sp.id AND spdm.isDelete = 0 AND dm.isDelete = 0
    ), 'Ch?a phân lo?i') AS 'DanhMuc',
    dv.ten AS 'DonVi',
    spd.heSoQuyDoi AS 'HeSoQuyDoi',
    spd.giaBan AS 'GiaBan',
    spd.trangThai AS 'TrangThai',
    sp.moTa AS 'MoTa'
FROM core.SanPhamDonVi spd
INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
LEFT JOIN core.NhanHieu nh ON sp.nhanHieuId = nh.id
INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
WHERE spd.isDelete = 0 AND sp.isDelete = 0
ORDER BY sp.id, dv.ten;

-- ================================================
-- 3. THÊM D? LI?U M?U (N?U CH?A CÓ)
-- ================================================

PRINT '';
PRINT '=== 3. THÊM D? LI?U M?U ===';

-- N?u ch?a có SanPhamDonVi, thêm d? li?u m?u
IF NOT EXISTS (SELECT 1 FROM core.SanPhamDonVi WHERE isDelete = 0)
BEGIN
    PRINT '?ang thêm d? li?u m?u...';
    
    -- L?y 1 s?n ph?m và 1 ??n v? ?? t?o m?u
    DECLARE @spId VARCHAR(50), @dvId VARCHAR(50);
    
    SELECT TOP 1 @spId = id FROM core.SanPham WHERE isDelete = 0;
    SELECT TOP 1 @dvId = id FROM core.DonViDoLuong WHERE isDelete = 0;
    
    IF @spId IS NOT NULL AND @dvId IS NOT NULL
    BEGIN
        -- T?o ID t? ??ng
        DECLARE @newId VARCHAR(50) = 'SPDV' + RIGHT('000000' + CAST(1 AS VARCHAR), 6);
        
        INSERT INTO core.SanPhamDonVi (sanPhamId, donViId, id, heSoQuyDoi, giaBan, isDelete, trangThai)
        VALUES (@spId, @dvId, @newId, 1, 10000, 0, 'Available');
        
        PRINT '?ã thêm 1 b?n ghi m?u: ' + @newId;
    END
    ELSE
    BEGIN
        PRINT 'Không th? thêm! C?n có d? li?u trong SanPham và DonViDoLuong tr??c.';
    END
END
ELSE
BEGIN
    PRINT '?ã có d? li?u SanPhamDonVi.';
END

-- ================================================
-- 4. TEST CÁC CASE TÌM KI?M
-- ================================================

PRINT '';
PRINT '=== 4. TEST TÌM KI?M ===';

-- 4.1. Tìm theo t? khóa (gi?ng nh? txtSearch)
PRINT 'Test 1: Tìm theo t? khóa';
DECLARE @keyword NVARCHAR(100) = '%coca%';  -- Thay b?ng t? khóa b?n mu?n test

SELECT 
    spd.id, sp.ten, nh.ten AS 'NhanHieu', dv.ten AS 'DonVi', spd.giaBan
FROM core.SanPhamDonVi spd
INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
LEFT JOIN core.NhanHieu nh ON sp.nhanHieuId = nh.id
INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
WHERE spd.isDelete = 0 AND sp.isDelete = 0
    AND (
        LOWER(sp.id) LIKE LOWER(@keyword) OR 
        LOWER(sp.ten) LIKE LOWER(@keyword) OR
        LOWER(spd.id) LIKE LOWER(@keyword)
    );

-- 4.2. L?c theo Nhãn hi?u
PRINT '';
PRINT 'Test 2: L?c theo Nhãn hi?u';
DECLARE @brandId VARCHAR(50);
SELECT TOP 1 @brandId = id FROM core.NhanHieu WHERE isDelete = 0;

IF @brandId IS NOT NULL
BEGIN
    PRINT 'L?c theo Nhãn hi?u: ' + @brandId;
    
    SELECT 
        spd.id, sp.ten, nh.ten AS 'NhanHieu', dv.ten AS 'DonVi', spd.giaBan
    FROM core.SanPhamDonVi spd
    INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
    INNER JOIN core.NhanHieu nh ON sp.nhanHieuId = nh.id
    INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
    WHERE spd.isDelete = 0 AND sp.isDelete = 0
        AND sp.nhanHieuId = @brandId;
END

-- 4.3. L?c theo Danh m?c
PRINT '';
PRINT 'Test 3: L?c theo Danh m?c';
DECLARE @categoryId VARCHAR(50);
SELECT TOP 1 @categoryId = id FROM core.DanhMuc WHERE isDelete = 0;

IF @categoryId IS NOT NULL
BEGIN
    PRINT 'L?c theo Danh m?c: ' + @categoryId;
    
    SELECT 
        spd.id, sp.ten, dv.ten AS 'DonVi', spd.giaBan
    FROM core.SanPhamDonVi spd
    INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
    INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
    WHERE spd.isDelete = 0 AND sp.isDelete = 0
        AND EXISTS (
            SELECT 1 FROM core.SanPhamDanhMuc spdm
            WHERE spdm.sanPhamId = sp.id 
                AND spdm.danhMucId = @categoryId 
                AND spdm.isDelete = 0
        );
END

-- ================================================
-- 5. TEST THÊM M?I
-- ================================================

PRINT '';
PRINT '=== 5. TEST THÊM M?I ===';

-- Ki?m tra có th? thêm ???c không
DECLARE @testSpId VARCHAR(50), @testDvId VARCHAR(50);

SELECT TOP 1 @testSpId = id FROM core.SanPham WHERE isDelete = 0;
SELECT TOP 1 @testDvId = id FROM core.DonViDoLuong WHERE isDelete = 0 
    AND id NOT IN (SELECT donViId FROM core.SanPhamDonVi WHERE sanPhamId = @testSpId AND isDelete = 0);

IF @testSpId IS NOT NULL AND @testDvId IS NOT NULL
BEGIN
    PRINT 'Có th? thêm: S?n ph?m ' + @testSpId + ' + ??n v? ' + @testDvId;
END
ELSE
BEGIN
    PRINT 'Không th? thêm! T?t c? s?n ph?m ?ã có ??y ?? ??n v?.';
END

-- ================================================
-- 6. TEST KI?M TRA TRÙNG
-- ================================================

PRINT '';
PRINT '=== 6. TEST KI?M TRA TRÙNG ===';

-- Ki?m tra xem có b?n ghi nào trùng không
SELECT 
    sp.ten AS 'S?n ph?m', 
    dv.ten AS '??n v?', 
    COUNT(*) AS 'S? l?n trùng'
FROM core.SanPhamDonVi spd
INNER JOIN core.SanPham sp ON spd.sanPhamId = sp.id
INNER JOIN core.DonViDoLuong dv ON spd.donViId = dv.id
WHERE spd.isDelete = 0
GROUP BY sp.ten, dv.ten, spd.sanPhamId, spd.donViId
HAVING COUNT(*) > 1;

-- ================================================
-- 7. T?NG K?T
-- ================================================

PRINT '';
PRINT '=== 7. T?NG K?T ===';

PRINT 'Nhãn hi?u: ' + CAST((SELECT COUNT(*) FROM core.NhanHieu WHERE isDelete = 0) AS VARCHAR);
PRINT 'Danh m?c: ' + CAST((SELECT COUNT(*) FROM core.DanhMuc WHERE isDelete = 0) AS VARCHAR);
PRINT 'Hàng hóa: ' + CAST((SELECT COUNT(*) FROM core.SanPham WHERE isDelete = 0) AS VARCHAR);
PRINT '??n v?: ' + CAST((SELECT COUNT(*) FROM core.DonViDoLuong WHERE isDelete = 0) AS VARCHAR);
PRINT 'S?n ph?m ??n v?: ' + CAST((SELECT COUNT(*) FROM core.SanPhamDonVi WHERE isDelete = 0) AS VARCHAR);

PRINT '';
PRINT '=== HOÀN T?T ===';

-- ================================================
-- 8. SCRIPT T?O D? LI?U M?U ??Y ?? (N?U C?N)
-- ================================================

/*
-- Ch?y script này n?u c?n t?o thêm nhi?u d? li?u m?u

-- T?o nhi?u SanPhamDonVi t? các s?n ph?m và ??n v? có s?n
INSERT INTO core.SanPhamDonVi (sanPhamId, donViId, id, heSoQuyDoi, giaBan, isDelete, trangThai)
SELECT 
    sp.id,
    dv.id,
    'SPDV' + RIGHT('000000' + CAST(ROW_NUMBER() OVER (ORDER BY sp.id, dv.id) + 
        (SELECT ISNULL(MAX(CAST(SUBSTRING(id, 5, 6) AS INT)), 0) FROM core.SanPhamDonVi) AS VARCHAR), 6),
    1,
    10000 + (ABS(CHECKSUM(NEWID())) % 40000), -- Giá ng?u nhiên t? 10,000 ??n 50,000
    0,
    'Available'
FROM core.SanPham sp
CROSS JOIN (SELECT TOP 2 * FROM core.DonViDoLuong WHERE isDelete = 0) dv
WHERE sp.isDelete = 0
    AND NOT EXISTS (
        SELECT 1 FROM core.SanPhamDonVi spd2 
        WHERE spd2.sanPhamId = sp.id AND spd2.donViId = dv.id AND spd2.isDelete = 0
    );

PRINT '?ã t?o d? li?u m?u cho SanPhamDonVi';
*/
