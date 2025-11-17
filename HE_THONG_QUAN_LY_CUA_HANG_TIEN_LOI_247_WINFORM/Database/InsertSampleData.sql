-- =============================================
-- Script: Insert Sample Data for QuanLyCuaHangTienLoi
-- Database: QuanLyCuaHangTienLoi
-- Description: Thêm d? li?u m?u cho h? th?ng qu?n lý c?a hàng ti?n l?i
-- =============================================

USE QuanLyCuaHangTienLoi;
GO

-- =============================================
-- 1. THÊM D? LI?U NHÃN HI?U (NhanHieu)
-- =============================================
INSERT INTO core.NhanHieu (id, ten, isDelete) VALUES
(NEWID(), N'Coca Cola', 0),
(NEWID(), N'Pepsi', 0),
(NEWID(), N'TH True Milk', 0),
(NEWID(), N'Vinamilk', 0),
(NEWID(), N'Lavie', 0),
(NEWID(), N'Aquafina', 0),
(NEWID(), N'Acecook', 0),
(NEWID(), N'H?o H?o', 0),
(NEWID(), N'Oreo', 0),
(NEWID(), N'Kinh ?ô', 0),
(NEWID(), N'Nescafe', 0),
(NEWID(), N'Trung Nguyên', 0),
(NEWID(), N'Number 1', 0),
(NEWID(), N'Comfort', 0),
(NEWID(), N'OMO', 0);
GO

-- =============================================
-- 2. THÊM D? LI?U DANH M?C (DanhMuc)
-- =============================================
INSERT INTO core.DanhMuc (id, ten, isDelete) VALUES
(NEWID(), N'N??c gi?i khát', 0),
(NEWID(), N'S?a các lo?i', 0),
(NEWID(), N'Mì gói - Mì ly', 0),
(NEWID(), N'Bánh k?o', 0),
(NEWID(), N'?? u?ng có c?n', 0),
(NEWID(), N'Cà phê - Trà', 0),
(NEWID(), N'?? ?n v?t', 0),
(NEWID(), N'Hóa m? ph?m', 0),
(NEWID(), N'?? gia d?ng', 0),
(NEWID(), N'V?n phòng ph?m', 0),
(NEWID(), N'Th?c ph?m ?ông l?nh', 0),
(NEWID(), N'Rau c? qu?', 0);
GO

-- =============================================
-- 3. THÊM D? LI?U ??N V? ?O L??NG (DonViDoLuong)
-- =============================================
INSERT INTO core.DonViDoLuong (id, ten, loai, isDelete) VALUES
(NEWID(), N'Chai', N'??n v?', 0),
(NEWID(), N'Lon', N'??n v?', 0),
(NEWID(), N'H?p', N'??n v?', 0),
(NEWID(), N'Gói', N'??n v?', 0),
(NEWID(), N'Cái', N'??n v?', 0),
(NEWID(), N'Kg', N'Kh?i l??ng', 0),
(NEWID(), N'Gram', N'Kh?i l??ng', 0),
(NEWID(), N'Lít', N'Th? tích', 0),
(NEWID(), N'ML', N'Th? tích', 0),
(NEWID(), N'Thùng', N'??n v?', 0);
GO

-- =============================================
-- 4. THÊM D? LI?U S?N PH?M (SanPham)
-- =============================================
DECLARE @NhanHieuCoca UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Coca Cola');
DECLARE @NhanHieuPepsi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Pepsi');
DECLARE @NhanHieuLavie UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Lavie');
DECLARE @NhanHieuHaoHao UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'H?o H?o');
DECLARE @NhanHieuOreo UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Oreo');
DECLARE @NhanHieuVinamilk UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Vinamilk');
DECLARE @NhanHieuNescafe UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanHieu WHERE ten = N'Nescafe');

INSERT INTO core.SanPham (id, ten, nhanHieuId, moTa, isDelete) VALUES
(NEWID(), N'Coca Cola Nguyên b?n', @NhanHieuCoca, N'N??c ng?t có ga Coca Cola', 0),
(NEWID(), N'Pepsi Lon', @NhanHieuPepsi, N'N??c ng?t có ga Pepsi', 0),
(NEWID(), N'N??c su?i Lavie', @NhanHieuLavie, N'N??c khoáng thiên nhiên', 0),
(NEWID(), N'Mì H?o H?o Tôm Chua Cay', @NhanHieuHaoHao, N'Mì gói v? tôm chua cay', 0),
(NEWID(), N'Mì H?o H?o S??n', @NhanHieuHaoHao, N'Mì gói v? s??n heo', 0),
(NEWID(), N'Bánh Oreo Vani', @NhanHieuOreo, N'Bánh quy Oreo kem vani', 0),
(NEWID(), N'S?a t??i Vinamilk', @NhanHieuVinamilk, N'S?a t??i ti?t trùng', 0),
(NEWID(), N'Cà phê Nescafe 3in1', @NhanHieuNescafe, N'Cà phê hòa tan 3 trong 1', 0),
(NEWID(), N'Pepsi Zero', @NhanHieuPepsi, N'Pepsi không ???ng', 0),
(NEWID(), N'Coca Cola Light', @NhanHieuCoca, N'Coca Cola ít calo', 0);
GO

-- =============================================
-- 5. THÊM D? LI?U S?N PH?M - DANH M?C (SanPhamDanhMuc)
-- =============================================
DECLARE @DanhMucNuocGK UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DanhMuc WHERE ten = N'N??c gi?i khát');
DECLARE @DanhMucMi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DanhMuc WHERE ten = N'Mì gói - Mì ly');
DECLARE @DanhMucBanh UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DanhMuc WHERE ten = N'Bánh k?o');
DECLARE @DanhMucSua UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DanhMuc WHERE ten = N'S?a các lo?i');
DECLARE @DanhMucCaPhe UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DanhMuc WHERE ten = N'Cà phê - Trà');

DECLARE @SP_Coca UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Coca Cola Nguyên b?n');
DECLARE @SP_Pepsi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Pepsi Lon');
DECLARE @SP_Lavie UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'N??c su?i Lavie');
DECLARE @SP_MiHaoHao UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Mì H?o H?o Tôm Chua Cay');
DECLARE @SP_Oreo UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Bánh Oreo Vani');
DECLARE @SP_Vinamilk UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'S?a t??i Vinamilk');
DECLARE @SP_Nescafe UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Cà phê Nescafe 3in1');

INSERT INTO core.SanPhamDanhMuc (sanPhamId, danhMucId, id, isDelete) VALUES
(@SP_Coca, @DanhMucNuocGK, NEWID(), 0),
(@SP_Pepsi, @DanhMucNuocGK, NEWID(), 0),
(@SP_Lavie, @DanhMucNuocGK, NEWID(), 0),
(@SP_MiHaoHao, @DanhMucMi, NEWID(), 0),
(@SP_Oreo, @DanhMucBanh, NEWID(), 0),
(@SP_Vinamilk, @DanhMucSua, NEWID(), 0),
(@SP_Nescafe, @DanhMucCaPhe, NEWID(), 0);
GO

-- =============================================
-- 6. THÊM D? LI?U S?N PH?M - ??N V? (SanPhamDonVi)
-- =============================================
DECLARE @DonViChai UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DonViDoLuong WHERE ten = N'Chai');
DECLARE @DonViLon UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DonViDoLuong WHERE ten = N'Lon');
DECLARE @DonViGoi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DonViDoLuong WHERE ten = N'Gói');
DECLARE @DonViHop UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.DonViDoLuong WHERE ten = N'H?p');

DECLARE @SPCoca UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Coca Cola Nguyên b?n');
DECLARE @SPPepsi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Pepsi Lon');
DECLARE @SPLavie UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'N??c su?i Lavie');
DECLARE @SPMi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Mì H?o H?o Tôm Chua Cay');
DECLARE @SPOreo UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Bánh Oreo Vani');

INSERT INTO core.SanPhamDonVi (sanPhamId, donViId, id, heSoQuyDoi, giaBan, isDelete, trangThai) VALUES
(@SPCoca, @DonViChai, NEWID(), 1.0000, 10000, 0, N'?ang bán'),
(@SPPepsi, @DonViLon, NEWID(), 1.0000, 9000, 0, N'?ang bán'),
(@SPLavie, @DonViChai, NEWID(), 1.0000, 5000, 0, N'?ang bán'),
(@SPMi, @DonViGoi, NEWID(), 1.0000, 3500, 0, N'?ang bán'),
(@SPOreo, @DonViHop, NEWID(), 1.0000, 15000, 0, N'?ang bán');
GO

-- =============================================
-- 7. THÊM D? LI?U T?N KHO (TonKho)
-- =============================================
DECLARE @SPDV_Coca UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Coca Cola Nguyên b?n'));
DECLARE @SPDV_Pepsi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Pepsi Lon'));
DECLARE @SPDV_Lavie UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'N??c su?i Lavie'));
DECLARE @SPDV_Mi UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Mì H?o H?o Tôm Chua Cay'));
DECLARE @SPDV_Oreo UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Bánh Oreo Vani'));

INSERT INTO core.TonKho (id, sanPhamDonViId, soLuongTon, isDelete) VALUES
(NEWID(), @SPDV_Coca, 150, 0),
(NEWID(), @SPDV_Pepsi, 10, 0),  -- S? l??ng th?p ?? test c?nh báo
(NEWID(), @SPDV_Lavie, 200, 0),
(NEWID(), @SPDV_Mi, 8, 0),      -- S? l??ng th?p ?? test c?nh báo
(NEWID(), @SPDV_Oreo, 5, 0);    -- S? l??ng th?p ?? test c?nh báo
GO

-- =============================================
-- 8. THÊM D? LI?U KHÁCH HÀNG (KhachHang)
-- =============================================
-- T?o ?nh m?u tr??c
INSERT INTO core.HinhAnh (Id, TenAnh, Anh) VALUES
(NEWID(), N'avatar_default.jpg', 0x),
(NEWID(), N'avatar_male.jpg', 0x),
(NEWID(), N'avatar_female.jpg', 0x);
GO

DECLARE @AnhDefault UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM core.HinhAnh WHERE TenAnh = N'avatar_default.jpg');

INSERT INTO core.KhachHang (id, hoTen, soDienThoai, email, diaChi, ngayDangKy, trangThai, isDelete, gioiTinh, anhId) VALUES
(NEWID(), N'Nguy?n V?n An', '0901234567', 'nguyenvanan@email.com', N'123 ???ng ABC, Q.1, TP.HCM', GETDATE(), N'Ho?t ??ng', 0, 1, @AnhDefault),
(NEWID(), N'Tr?n Th? Bình', '0912345678', 'tranthib@email.com', N'456 ???ng DEF, Q.2, TP.HCM', GETDATE(), N'Ho?t ??ng', 0, 0, @AnhDefault),
(NEWID(), N'Lê V?n C??ng', '0923456789', 'levanc@email.com', N'789 ???ng GHI, Q.3, TP.HCM', GETDATE(), N'Ho?t ??ng', 0, 1, @AnhDefault),
(NEWID(), N'Ph?m Th? Dung', '0934567890', 'phamthid@email.com', N'321 ???ng JKL, Q.4, TP.HCM', GETDATE(), N'Ho?t ??ng', 0, 0, @AnhDefault),
(NEWID(), N'Hoàng V?n Em', '0945678901', 'hoangvane@email.com', N'654 ???ng MNO, Q.5, TP.HCM', GETDATE(), N'Ho?t ??ng', 0, 1, @AnhDefault);
GO

-- =============================================
-- 9. THÊM D? LI?U NHÂN VIÊN (NhanVien)
-- =============================================
DECLARE @AnhNV UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM core.HinhAnh WHERE TenAnh = N'avatar_male.jpg');

INSERT INTO core.NhanVien (id, hoTen, chucVu, luongCoBan, soDienThoai, email, diaChi, ngayVaoLam, trangThai, isDelete, gioiTinh, anhId) VALUES
(NEWID(), N'Nguy?n V?n A', N'Qu?n lý', 12000000, '0971234567', 'nvana@store.com', N'100 ???ng ABC', '2023-01-15', N'?ang làm', 0, 1, @AnhNV),
(NEWID(), N'Tr?n Th? B', N'Thu ngân', 8000000, '0982345678', 'ttb@store.com', N'200 ???ng DEF', '2023-03-20', N'?ang làm', 0, 0, @AnhNV),
(NEWID(), N'Lê V?n C', N'Nhân viên kho', 7500000, '0993456789', 'lvc@store.com', N'300 ???ng GHI', '2023-05-10', N'?ang làm', 0, 1, @AnhNV),
(NEWID(), N'Ph?m Th? D', N'Thu ngân', 8000000, '0904567890', 'ptd@store.com', N'400 ???ng JKL', '2023-06-01', N'?ang làm', 0, 0, @AnhNV);
GO

-- =============================================
-- 10. THÊM D? LI?U CA LÀM VI?C (CaLamViec)
-- =============================================
INSERT INTO core.CaLamViec (id, tenCa, thoiGianBatDau, thoiGianKetThuc, isDelete) VALUES
(NEWID(), N'Ca 1 (Sáng)', '06:00:00', '14:00:00', 0),
(NEWID(), N'Ca 2 (Chi?u)', '14:00:00', '22:00:00', 0),
(NEWID(), N'Ca 3 (?êm)', '22:00:00', '06:00:00', 0);
GO

-- =============================================
-- 11. THÊM D? LI?U PHÂN CÔNG CA LÀM VI?C (PhanCongCaLamViec)
-- =============================================
DECLARE @CaSang UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.CaLamViec WHERE tenCa = N'Ca 1 (Sáng)');
DECLARE @CaChieu UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.CaLamViec WHERE tenCa = N'Ca 2 (Chi?u)');
DECLARE @NV1 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanVien WHERE hoTen = N'Nguy?n V?n A');
DECLARE @NV2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanVien WHERE hoTen = N'Tr?n Th? B');

INSERT INTO core.PhanCongCaLamViec (nhanVienId, caLamViecId, ngay, id, isDelete) VALUES
(@NV1, @CaSang, CAST(GETDATE() AS DATE), NEWID(), 0),
(@NV2, @CaSang, CAST(GETDATE() AS DATE), NEWID(), 0),
(@NV1, @CaChieu, CAST(GETDATE() AS DATE), NEWID(), 0);
GO

-- =============================================
-- 12. THÊM D? LI?U HÓA ??N (HoaDon)
-- =============================================
DECLARE @KH1 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.KhachHang WHERE hoTen = N'Nguy?n V?n An');
DECLARE @KH2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.KhachHang WHERE hoTen = N'Tr?n Th? Bình');
DECLARE @NVien1 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanVien WHERE hoTen = N'Nguy?n V?n A');
DECLARE @NVien2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.NhanVien WHERE hoTen = N'Tr?n Th? B');

INSERT INTO core.HoaDon (id, khachHangId, nhanVienId, ngayLap, tongTien, trangThaiThanhToan, isDelete) VALUES
(NEWID(), @KH1, @NVien2, DATEADD(HOUR, -2, GETDATE()), 50000, N'?ã thanh toán', 0),
(NEWID(), @KH2, @NVien2, DATEADD(HOUR, -1, GETDATE()), 128000, N'?ã thanh toán', 0),
(NEWID(), @KH1, @NVien1, DATEADD(DAY, -1, GETDATE()), 75000, N'?ã thanh toán', 0),
(NEWID(), @KH2, @NVien2, DATEADD(DAY, -2, GETDATE()), 95000, N'?ã thanh toán', 0),
(NEWID(), @KH1, @NVien1, DATEADD(DAY, -3, GETDATE()), 150000, N'?ã thanh toán', 0),
(NEWID(), @KH2, @NVien2, DATEADD(DAY, -4, GETDATE()), 200000, N'?ã thanh toán', 0),
(NEWID(), @KH1, @NVien1, DATEADD(DAY, -5, GETDATE()), 85000, N'?ã thanh toán', 0),
(NEWID(), @KH2, @NVien2, DATEADD(DAY, -6, GETDATE()), 120000, N'?ã thanh toán', 0);
GO

-- =============================================
-- 13. THÊM D? LI?U CHI TI?T HÓA ??N (ChiTietHoaDon)
-- =============================================
DECLARE @HD1 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.HoaDon ORDER BY ngayLap DESC);
DECLARE @SPDV_CocaHD UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Coca Cola Nguyên b?n'));
DECLARE @SPDV_MiHD UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Mì H?o H?o Tôm Chua Cay'));
DECLARE @SPDV_OreoHD UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Bánh Oreo Vani'));

-- Chi ti?t cho hóa ??n g?n nh?t
INSERT INTO core.ChiTietHoaDon (hoaDonId, sanPhamDonViId, soLuong, donGia, giamGia, isDelete, tongTien) VALUES
(@HD1, @SPDV_CocaHD, 3, 10000, 0, 0, 30000),
(@HD1, @SPDV_MiHD, 5, 3500, 0, 0, 17500),
(@HD1, @SPDV_OreoHD, 1, 15000, 0, 0, 15000);
GO

-- Chi ti?t cho các hóa ??n khác (d? li?u cho bi?u ??)
DECLARE @AllHoaDons TABLE (id UNIQUEIDENTIFIER);
INSERT INTO @AllHoaDons SELECT id FROM core.HoaDon ORDER BY ngayLap DESC;

DECLARE @SPDVCoca2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Coca Cola Nguyên b?n'));
DECLARE @SPDVPepsi2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'Pepsi Lon'));
DECLARE @SPDVLavie2 UNIQUEIDENTIFIER = (SELECT TOP 1 id FROM core.SanPhamDonVi WHERE sanPhamId = (SELECT TOP 1 id FROM core.SanPham WHERE ten = N'N??c su?i Lavie'));

-- Thêm chi ti?t cho các hóa ??n khác ?? có d? li?u bi?u ??
DECLARE @HDTemp UNIQUEIDENTIFIER;
DECLARE hd_cursor CURSOR FOR SELECT id FROM @AllHoaDons;
OPEN hd_cursor;
FETCH NEXT FROM hd_cursor INTO @HDTemp;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Random thêm s?n ph?m cho m?i hóa ??n
    IF NOT EXISTS (SELECT 1 FROM core.ChiTietHoaDon WHERE hoaDonId = @HDTemp)
    BEGIN
        INSERT INTO core.ChiTietHoaDon (hoaDonId, sanPhamDonViId, soLuong, donGia, giamGia, isDelete, tongTien)
        VALUES 
        (@HDTemp, @SPDVCoca2, 2, 10000, 0, 0, 20000),
        (@HDTemp, @SPDVLavie2, 3, 5000, 0, 0, 15000);
    END
    
    FETCH NEXT FROM hd_cursor INTO @HDTemp;
END

CLOSE hd_cursor;
DEALLOCATE hd_cursor;
GO

-- =============================================
-- 14. THÊM D? LI?U NHÀ CUNG C?P (NhaCungCap)
-- =============================================
INSERT INTO core.NhaCungCap (id, ten, soDienThoai, email, diaChi, maSoThue, isDelete) VALUES
(NEWID(), N'Công ty TNHH Coca Cola Vi?t Nam', '0281234567', 'cocacola@vn.com', N'Khu CN Tân Bình, TP.HCM', '0123456789', 0),
(NEWID(), N'Công ty CP Acecook Vi?t Nam', '0282345678', 'acecook@vn.com', N'Bình D??ng', '0234567890', 0),
(NEWID(), N'Công ty Vinamilk', '0283456789', 'vinamilk@vn.com', N'Q.Bình Th?nh, TP.HCM', '0345678901', 0);
GO

PRINT '? ?ã thêm d? li?u m?u thành công!';
PRINT '? T?ng s? b?n ghi:';
PRINT '  - Nhãn hi?u: ' + CAST((SELECT COUNT(*) FROM core.NhanHieu) AS VARCHAR);
PRINT '  - Danh m?c: ' + CAST((SELECT COUNT(*) FROM core.DanhMuc) AS VARCHAR);
PRINT '  - S?n ph?m: ' + CAST((SELECT COUNT(*) FROM core.SanPham) AS VARCHAR);
PRINT '  - Khách hàng: ' + CAST((SELECT COUNT(*) FROM core.KhachHang) AS VARCHAR);
PRINT '  - Nhân viên: ' + CAST((SELECT COUNT(*) FROM core.NhanVien) AS VARCHAR);
PRINT '  - Hóa ??n: ' + CAST((SELECT COUNT(*) FROM core.HoaDon) AS VARCHAR);
PRINT '  - Chi ti?t hóa ??n: ' + CAST((SELECT COUNT(*) FROM core.ChiTietHoaDon) AS VARCHAR);
GO
