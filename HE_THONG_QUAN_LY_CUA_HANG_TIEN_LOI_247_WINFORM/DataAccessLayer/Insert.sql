USE [QuanLyCuaHangTienLoi]

GO



-- 1. core.HinhAnh

INSERT INTO core.HinhAnh (Id, TenAnh, Anh) VALUES

('Anh001', N'Avatar_Admin', 0x41444D494E),

('Anh002', N'Avatar_NhanVien1', 0x4E56303031),

('Anh003', N'Avatar_KhachHang1', 0x4B48303031),

('Anh004', N'TemNhan_Duyet', 0x544E303031),

('Anh005', N'TemNhan_HetHan', 0x544E303032),

('Anh006', N'Avatar_NhanVien2', 0x4E56303032);



---

-- 2. core.Role

INSERT INTO core.Role (id, code, ten, moTa, trangThai, isDelete) VALUES

('R_ADM', 'ADMIN', N'Quản trị viên hệ thống', N'Toàn quyền quản lý.', 'Active', 0),

('R_NV', 'NV_BANHANG', N'Nhân viên bán hàng', N'Thực hiện giao dịch bán hàng, nhập hàng.', 'Active', 0),

('R_KH', 'KHACH_HANG', N'Khách hàng', N'Thực hiện mua sắm, quản lý giỏ hàng.', 'Active', 0),

('R_KHO', 'NV_KHO', N'Nhân viên kho', N'Quản lý nhập xuất, kiểm kê kho.', 'Active', 0),

('R_SHIP', 'SHIPPER', N'Người giao hàng', N'Vận chuyển đơn hàng.', 'Active', 0);



---

-- 3. core.Permission

INSERT INTO core.Permission (id, code, ten, moTa, isDelete) VALUES

('P_QLSP', 'QL_SANPHAM', N'Quản lý sản phẩm', N'Thêm, sửa, xóa sản phẩm.', 0),

('P_QLHD', 'QL_HOADON', N'Quản lý hóa đơn', N'Tạo, xem, hủy hóa đơn.', 0),

('P_QLKH', 'QL_KHACHHANG', N'Quản lý khách hàng', N'Thêm, sửa thông tin khách hàng.', 0),

('P_QLNV', 'QL_NHANVIEN', N'Quản lý nhân viên', N'Quản lý thông tin, phân công nhân viên.', 0),

('P_QLTK', 'QL_TAIKHOAN', N'Quản lý tài khoản', N'Quản lý tài khoản người dùng và phân quyền.', 0),

('P_VIEWBC', 'VIEW_BAOCAO', N'Xem báo cáo', N'Xem các báo cáo doanh thu, tồn kho.', 0);



---

-- 4. core.RolePermission

INSERT INTO core.RolePermission (roleId, permissionId, isDelete) VALUES

('R_ADM', 'P_QLSP', 0),

('R_ADM', 'P_QLHD', 0),

('R_ADM', 'P_QLKH', 0),

('R_ADM', 'P_QLNV', 0),

('R_ADM', 'P_QLTK', 0),

('R_ADM', 'P_VIEWBC', 0),

('R_NV', 'P_QLHD', 0),

('R_NV', 'P_QLKH', 0),

('R_KHO', 'P_QLSP', 0),

('R_KHO', 'P_VIEWBC', 0),

('R_KH', 'P_QLHD', 0);



---

-- 5. core.TaiKhoan (Mật khẩu: '123456')

INSERT INTO core.TaiKhoan (id, tenDangNhap, matKhauHash, email, trangThai, isDelete) VALUES

('TK_NV4', 'nhanvien4', '123456', 'nv4@cstore.com', 'Active', 0),

('TK_NV3', 'nhanvien3', '1234567', 'nv3@cstore.com', 'Active', 0),

('TK_ADM', 'admin', '123567', 'admin@cstore.com', 'Active', 0),

('TK_NV1', 'nhanvien1', '12345678', 'nv1@cstore.com', 'Active', 0),

('TK_NV2', 'nhanvien2', '12345676', 'nv2@cstore.com', 'Active', 0),

('TK_KH1', 'khachhang1', '12345652', 'kh1@gmail.com', 'Active', 0),

('TK_KH2', 'khachhang2', '12345632', 'kh2@gmail.com', 'Active', 0),

('TK_SHIP1', 'shipper1', '12345622', 'shipper1@giao.com', 'Active', 0),

('TK_KH3', 'khachhang3', '12345611', 'kh3@gmail.com', 'Active', 0), -- Bổ sung

('TK_KH4', 'khachhang4', '12345644', 'kh4@gmail.com', 'Active', 0), -- Bổ sung

('TK_KH5', 'khachhang5', '12345677', 'kh5@gmail.com', 'Active', 0); -- Bổ sung



---

-- 6. core.UserRole

INSERT INTO core.UserRole (taiKhoanId, roleId, hieuLucTu, hieuLucDen, isDelete) VALUES

('TK_ADM', 'R_ADM', '2025-01-01', NULL, 0),

('TK_NV1', 'R_NV', '2025-02-15', NULL, 0),

('TK_NV2', 'R_KHO', '2025-03-01', NULL, 0),

('TK_KH1', 'R_KH', '2025-04-10', NULL, 0),

('TK_KH2', 'R_KH', '2025-05-20', NULL, 0),

('TK_SHIP1', 'R_SHIP', '2025-06-05', NULL, 0),

('TK_KH3', 'R_KH', '2025-03-05', NULL, 0),

('TK_KH4', 'R_KH', '2025-04-15', NULL, 0),

('TK_KH5', 'R_KH', '2025-05-25', NULL, 0);



---

-- 7. core.NhanVien

INSERT INTO core.NhanVien (id, hoTen, chucVu, luongCoBan, soDienThoai, email, diaChi, ngayVaoLam, trangThai, isDelete, gioiTinh, anhId) VALUES

('NV001', N'Trần Văn An', N'Quản lý', 15000000.00, '0901111222', 'an@cstore.com', N'20 Đường ABC, Q.1, TP.HCM', '2024-01-01', 'Active', 0, 1, 'Anh002'),

('NV002', N'Lê Thị Bình', N'Nhân viên bán hàng', 8000000.00, '0902222333', 'binh@cstore.com', N'55 Nguyễn Du, Q.3, TP.HCM', '2024-03-01', 'Active', 0, 0, 'Anh006'),

('NV003', N'Phạm Minh Cường', N'Nhân viên kho', 7500000.00, '0903333444', 'cuong@cstore.com', N'100 Lê Lợi, Q.Gò Vấp, TP.HCM', '2024-05-15', 'Active', 0, 1, 'Anh002'),

('NV004', N'Đỗ Thị Diệu', N'Kế toán', 10000000.00, '0904444555', 'dieu@cstore.com', N'77 Hoàng Văn Thụ, Q.Phú Nhuận, TP.HCM', '2024-06-20', 'Active', 0, 0, 'Anh006'),

('NV005', N'Hoàng Văn Phát', N'Nhân viên bán hàng', 8000000.00, '0905555666', 'phat@cstore.com', N'123 CMT8, Q.10, TP.HCM', '2024-08-10', 'Active', 0, 1, 'Anh002');



---

-- 8. core.TaiKhoanNhanVien

INSERT INTO core.TaiKhoanNhanVien (nhanVienId, taiKhoanId, isDelete) VALUES

('NV001', 'TK_ADM', 0),

('NV002', 'TK_NV1', 0),

('NV003', 'TK_NV2', 0),

('NV004', 'TK_NV4', 0),

('NV005', 'TK_NV3', 0);



---

-- 9. core.KhachHang

INSERT INTO core.KhachHang (id, hoTen, soDienThoai, email, diaChi, ngayDangKy, trangThai, isDelete, gioiTinh, anhId) VALUES

('KH001', N'Nguyễn Văn Thắng', '0912345678', 'thang@gmail.com', N'1A Lê Văn Sỹ, Q.Tân Bình', '2024-01-10', 'Active', 0, 1, 'Anh003'),

('KH002', N'Trần Thị Mai', '0987654321', 'mai@gmail.com', N'22 Cộng Hòa, Q.Tân Bình', '2024-02-20', 'Active', 0, 0, 'Anh003'),

('KH003', N'Lê Văn Hùng', '0908888999', 'hung@gmail.com', N'10 Trường Chinh, Q.Tân Phú', '2024-03-05', 'Active', 0, 1, 'Anh003'),

('KH004', N'Phạm Thị Thanh', '0976123456', 'thanh@gmail.com', N'45 Hai Bà Trưng, Q.1', '2024-04-15', 'Active', 0, 0, 'Anh003'),

('KH005', N'Đỗ Minh Quân', '0933555777', 'quan@gmail.com', N'66 Phan Đăng Lưu, Q.Bình Thạnh', '2024-05-25', 'Active', 0, 1, 'Anh003');



---

-- 10. core.TaiKhoanKhachHang

INSERT INTO core.TaiKhoanKhachHang (khachHangId, taiKhoanid, isDelete) VALUES

('KH001', 'TK_KH1', 0),

('KH002', 'TK_KH2', 0),

('KH003', 'TK_KH3', 0),

('KH004', 'TK_KH4', 0),

('KH005', 'TK_KH5', 0);



---

-- 11. core.TheThanhVien

INSERT INTO core.TheThanhVien (id, khachHangId, hang, diemTichLuy, ngayCap, isDelete) VALUES

('TTV001', 'KH001', N'Vàng', 1500, '2024-01-10', 0),

('TTV002', 'KH002', N'Bạc', 500, '2024-02-20', 0),

('TTV003', 'KH003', N'Đồng', 100, '2024-03-05', 0),

('TTV004', 'KH004', N'Đồng', 50, '2024-04-15', 0),

('TTV005', 'KH005', N'Bạc', 750, '2024-05-25', 0);



---

-- 12. core.DanhMuc

INSERT INTO core.DanhMuc (id, ten, isDelete) VALUES

('DM001', N'Nước giải khát', 0),

('DM002', N'Thực phẩm đóng gói', 0),

('DM003', N'Hóa mỹ phẩm', 0),

('DM004', N'Bánh kẹo', 0),

('DM005', N'Đồ gia dụng nhỏ', 0);



---

-- 13. core.NhanHieu

INSERT INTO core.NhanHieu (id, ten, isDelete) VALUES

('NH001', N'Coca-Cola', 0),

('NH002', N'Omachi', 0),

('NH003', N'Unilever', 0),

('NH004', N'Orion', 0),

('NH005', N'Milo', 0);



---

-- 14. core.SanPham

INSERT INTO core.SanPham (id, ten, nhanHieuId, moTa, isDelete) VALUES

('SP001', N'Coca-Cola lon', 'NH001', N'Nước giải khát có ga', 0),

('SP002', N'Mì Omachi Xốt tôm chua cay', 'NH002', N'Mì ăn liền cao cấp', 0),

('SP003', N'Dầu gội Clear Men', 'NH003', N'Dầu gội sạch gàu cho nam', 0),

('SP004', N'Bánh Chocopie hộp 12 cái', 'NH004', N'Bánh xốp phủ socola', 0),

('SP005', N'Sữa Milo hộp giấy 180ml', 'NH005', N'Sữa lúa mạch uống liền', 0);



---

-- 15. core.SanPhamDanhMuc

INSERT INTO core.SanPhamDanhMuc (sanPhamId, danhMucId, id, isDelete) VALUES

('SP001', 'DM001', 'SDM001', 0),

('SP002', 'DM002', 'SDM002', 0),

('SP003', 'DM003', 'SDM003', 0),

('SP004', 'DM004', 'SDM004', 0),

('SP005', 'DM001', 'SDM005', 0);



---

-- 16. core.DonViDoLuong

INSERT INTO core.DonViDoLuong (id, ten, kyHieu, isDelete) VALUES

('DV001', N'Lon', N'lon', 0),

('DV002', N'Thùng (24 lon)', N'thùng', 0),

('DV003', N'Gói', N'gói', 0),

('DV004', N'Thùng (30 gói)', N'thùng', 0),

('DV005', N'Chai (650g)', N'chai', 0),

('DV006', N'Hộp (12 cái)', N'hộp', 0),

('DV007', N'Hộp giấy (180ml)', N'hộp', 0),

('DV008', N'Thùng (48 hộp)', N'thùng', 0);



---

-- 17. core.SanPhamDonVi

INSERT INTO core.SanPhamDonVi (sanPhamId, donViId, id, heSoQuyDoi, giaBan,trangThai, isDelete) VALUES

('SP001', 'DV001', 'SPDV001', 1.0000, 10000.00,'Available', 0),

('SP001', 'DV002', 'SPDV002', 24.0000, 230000.00,'Available', 0),

('SP002', 'DV003', 'SPDV003', 1.0000, 15000.00,'Available', 0),

('SP003', 'DV005', 'SPDV004', 1.0000, 85000.00, 'Available',0),

('SP004', 'DV006', 'SPDV005', 1.0000, 48000.00,'Available', 0),

('SP005', 'DV007', 'SPDV006', 1.0000, 7000.00, 'Available',0),

('SP005', 'DV008', 'SPDV007', 48.0000, 320000.00, 'Available',0);



---

-- 18. core.LichSuGiaBan

INSERT INTO core.LichSuGiaBan (id, sanPhamId, donViId, giaBan, ngayBatDau, ngayKetThuc, isDelete) VALUES

('LSGB001', 'SP001', 'DV001', 10000.00, '2025-01-01', '9999-12-31', 0),

('LSGB002', 'SP002', 'DV003', 15000.00, '2025-01-01', '9999-12-31', 0),

('LSGB003', 'SP003', 'DV005', 85000.00, '2025-01-01', '9999-12-31', 0),

('LSGB004', 'SP004', 'DV006', 48000.00, '2025-01-01', '9999-12-31', 0),

('LSGB005', 'SP005', 'DV007', 7000.00, '2025-01-01', '9999-12-31', 0);



---

-- 19. core.ViTri

INSERT INTO core.ViTri (id, maViTri, loaiViTri, moTa, isDelete) VALUES

('VT001', 'KHO-A1', N'Kệ', N'Kệ khu vực A, Tầng 1', 0),

('VT002', 'KHO-A2', N'Kệ', N'Kệ khu vực A, Tầng 2', 0),

('VT003', 'KHO-B1', N'Kệ', N'Kệ khu vực B, Tầng 1', 0),

('VT004', 'QUAY-1', N'Quầy', N'Quầy thanh toán số 1', 0),

('VT005', 'QUAY-2', N'Quầy', N'Quầy thanh toán số 2', 0);



---

-- 20. core.SanPhamViTri

INSERT INTO core.SanPhamViTri (sanPhamDonViId, viTriId, soLuong, isDelete) VALUES

('SPDV001', 'VT004', 50, 0),

('SPDV002', 'VT001', 10, 0),

('SPDV003', 'VT005', 100, 0),

('SPDV004', 'VT003', 20, 0),

('SPDV005', 'VT004', 30, 0);



---

-- 21. core.TonKho

INSERT INTO core.TonKho (id, sanPhamDonViId, soLuongTon, isDelete) VALUES

('TKHO001', 'SPDV001', 50, 0),

('TKHO002', 'SPDV002', 10, 0),

('TKHO003', 'SPDV003', 100, 0),

('TKHO004', 'SPDV004', 20, 0),

('TKHO005', 'SPDV005', 30, 0);



---

-- 22. core.NhaCungCap

INSERT INTO core.NhaCungCap (id, ten, soDienThoai, email, diaChi, maSoThue, isDelete) VALUES

('NCC001', N'Công ty CP Nước Giải Khát', '0281234567', 'contact@nuocgiai.com', N'789 Điện Biên Phủ, Q.Bình Thạnh', '0301111222', 0),

('NCC002', N'Tập đoàn Thực phẩm Á Châu', '0289876543', 'info@tpachau.com', N'123 Xa Lộ Hà Nội, TP.Thủ Đức', '0302222333', 0),

('NCC003', N'Công ty TNHH Hóa Mỹ Phẩm VN', '0245678901', 'sales@hoamyphamvn.com', N'10 Kim Mã, Hà Nội', '0103333444', 0),

('NCC004', N'Đại lý Bánh kẹo Miền Nam', '0919191919', 'daily@banhkeo.vn', N'200 Phạm Văn Đồng, Q.Gò Vấp', '0304444555', 0),

('NCC005', N'Công ty Sữa Lúa Mạch', '0907777888', 'contact@sua.com', N'50 Đinh Tiên Hoàng, Q.1', '0305555666', 0);



---

-- 23. core.PhieuNhap

INSERT INTO core.PhieuNhap (id, nhaCungCapId, ngayNhap, tongTien, isDelete, nhanVienId) VALUES

('PN001', 'NCC001', '2025-10-01', 2200000.00, 0, 'NV003'),

('PN002', 'NCC002', '2025-10-05', 1300000.00, 0, 'NV003'),

('PN003', 'NCC003', '2025-10-10', 1600000.00, 0, 'NV003'),

('PN004', 'NCC004', '2025-10-15', 1800000.00, 0, 'NV003'),

('PN005', 'NCC005', '2025-10-20', 2500000.00, 0, 'NV003');



---

-- 24. core.ChiTietPhieuNhap

INSERT INTO core.ChiTietPhieuNhap (phieuNhapId, sanPhamDonViId, soLuong, donGia, isDelete, tongTien) VALUES

('PN001', 'SPDV002', 10, 220000.00, 0, 2200000.00),

('PN002', 'SPDV003', 100, 13000.00, 0, 1300000.00),

('PN003', 'SPDV004', 20, 80000.00, 0, 1600000.00),

('PN004', 'SPDV005', 30, 60000.00, 0, 1800000.00),

('PN005', 'SPDV007', 8, 312500.00, 0, 2500000.00);



---

-- 25. core.LichSuGiaoDich

INSERT INTO core.LichSuGiaoDich (id, nhaCungCapId, ngayGD, tongTien, isDelete) VALUES

('LSGD001', 'NCC001', '2025-10-02', 2200000.00, 0),

('LSGD002', 'NCC002', '2025-10-06', 1300000.00, 0),

('LSGD003', 'NCC003', '2025-10-11', 1600000.00, 0),

('LSGD004', 'NCC004', '2025-10-16', 1800000.00, 0),

('LSGD005', 'NCC005', '2025-10-21', 2500000.00, 0);



---

-- 26. core.ChiTietGiaoDichNCC

INSERT INTO core.ChiTietGiaoDichNCC (giaoDichId, sanPhamDonViId, soLuong, donGia, thanhTien, isDelete) VALUES

('LSGD001', 'SPDV002', 10, 220000.00, 2200000.00, 0),

('LSGD002', 'SPDV003', 100, 13000.00, 1300000.00, 0),

('LSGD003', 'SPDV004', 20, 80000.00, 1600000.00, 0),

('LSGD004', 'SPDV005', 30, 60000.00, 1800000.00, 0),

('LSGD005', 'SPDV007', 8, 312500.00, 2500000.00, 0);



---

-- 27. core.HoaDon (Tổng tiền sẽ được cập nhật sau khi tính KM)

INSERT INTO core.HoaDon (id, ngayLap, tongTien, nhanVienId, khachHangId, trangThai, isDelete) VALUES

('HD001', '2025-11-10 09:30:00', 55000.00, 'NV002', 'KH001', 'Paid', 0),

('HD002', '2025-11-10 11:00:00', 105000.00, 'NV005', 'KH002', 'Paid', 0),

('HD003', '2025-11-11 14:45:00', 100000.00, 'NV002', 'KH003', 'Paid', 0),

('HD004', '2025-11-11 16:30:00', 7000.00, 'NV005', 'KH004', 'Paid', 0),

('HD005', '2025-11-12 08:15:00', 230000.00, 'NV002', 'KH005', 'Paid', 0);



---

-- 28. core.ChiTietHoaDon

-- HD001: 2 Lon Coca, 1 Gói Mì, 3 Hộp Milo. Tổng: 56,000 - 1,000 Giảm = 55,000

INSERT INTO core.ChiTietHoaDon (hoaDonId, sanPhamDonViId, soLuong, donGia, giamGia, isDelete, tongTien) VALUES

('HD001', 'SPDV001', 2, 10000.00, 0.00, 0, 20000.00),

('HD001', 'SPDV003', 1, 15000.00, 0.00, 0, 15000.00),

('HD001', 'SPDV006', 3, 7000.00, 1000.00, 0, 20000.00),



-- HD002: 1 Chai Dầu gội, 2 Lon Coca. Tổng: 105,000 - 500 Giảm = 104,500

('HD002', 'SPDV004', 1, 85000.00, 0.00, 0, 85000.00),

('HD002', 'SPDV001', 2, 10000.00, 500.00, 0, 19500.00),



-- HD003: 2 Hộp Chocopie, 4 Lon Coca. Tổng: 96,000 + 40,000 = 136,000

('HD003', 'SPDV005', 2, 48000.00, 0.00, 0, 96000.00),

('HD003', 'SPDV001', 4, 10000.00, 0.00, 0, 40000.00),



-- HD004: 1 Hộp Milo. Tổng: 7,000

('HD004', 'SPDV006', 1, 7000.00, 0.00, 0, 7000.00),



-- HD005: 1 Thùng Coca (24 lon). Tổng: 230,000

('HD005', 'SPDV002', 1, 230000.00, 0.00, 0, 230000.00);




-- 29. core.LichSuMuaHang

INSERT INTO core.LichSuMuaHang (khachHangId, hoaDonId, tongTien, ngayMua, isDelete) VALUES

('KH001', 'HD001', 55000.00, '2025-11-10', 0),

('KH002', 'HD002', 104500.00, '2025-11-10', 0),

('KH003', 'HD003', 136000.00, '2025-11-11', 0),

('KH004', 'HD004', 7000.00, '2025-11-11', 0),

('KH005', 'HD005', 230000.00, '2025-11-12', 0);



---

-- 30. core.KenhThanhToan

INSERT INTO core.KenhThanhToan (id, tenKenh, loaiKenh, phiGiaoDich, trangThai, ngayTao, ngayCapNhat, isDelete) VALUES

('KTT001', N'Tiền mặt (Cash)', 'Offline', 0.00, 'Active', '2024-01-01', '2024-01-01', 0),

('KTT002', N'Chuyển khoản (Bank Transfer)', 'Online', 0.00, 'Active', '2024-01-01', '2024-01-01', 0),

('KTT003', N'Thẻ Tín dụng (Credit Card)', 'Online', 0.02, 'Active', '2024-01-01', '2024-01-01', 0),

('KTT004', N'Momo Wallet', 'Online', 0.01, 'Active', '2024-03-01', '2024-03-01', 0),

('KTT005', N'VNPay QR', 'Online', 0.005, 'Active', '2024-04-15', '2024-04-15', 0);



---

-- 31. core.GiaoDichThanhToan

INSERT INTO core.GiaoDichThanhToan (id, hoaDonId, soTien, ngayGD, kenhThanhToanId, moTa, isDelete) VALUES

('GDTT001', 'HD001', 55000.00, '2025-11-10', 'KTT001', N'Thanh toán tiền mặt', 0),

('GDTT002', 'HD002', 104500.00, '2025-11-10', 'KTT004', N'Thanh toán qua Momo', 0),

('GDTT003', 'HD003', 136000.00, '2025-11-11', 'KTT001', N'Thanh toán tiền mặt', 0),

('GDTT004', 'HD004', 7000.00, '2025-11-11', 'KTT001', N'Thanh toán tiền mặt', 0),

('GDTT005', 'HD005', 230000.00, '2025-11-12', 'KTT002', N'Chuyển khoản ngân hàng', 0);



---

-- 32. core.ChuongTrinhKhuyenMai

INSERT INTO core.ChuongTrinhKhuyenMai (id, ten, loai, ngayBatDau, ngayKetThuc, moTa, isDelete) VALUES

('CTKM001', N'Khuyến mãi Black Friday', 'Discount', '2025-11-20', '2025-11-27', N'Giảm giá lớn cho nhiều mặt hàng', 0),

('CTKM002', N'Tặng quà cho hóa đơn 100K', 'Gift', '2025-12-01', '2025-12-31', N'Tặng 1 lon Coca cho hóa đơn trên 100,000 VND', 0),

('CTKM003', N'Giảm 10% ngành hàng Sữa', 'Discount', '2025-10-01', '2025-10-31', N'Giảm 10% cho tất cả các sản phẩm sữa.', 0),

('CTKM004', N'MUA 2 TẶNG 1: Nước suối', 'BOGO', '2025-11-01', '2025-11-30', N'Mua 2 sản phẩm nước suối cùng loại tặng 1.', 0),

('CTKM005', N'Ưu đãi Khách hàng Vàng', 'Loyalty', '2025-01-01', '2025-12-31', N'Giảm thêm 5% cho KH hạng Vàng.', 0);



---

-- 33. core.MaKhuyenMai

INSERT INTO core.MaKhuyenMai (id, chuongTrinhId, code, giaTri, soLanSuDung, trangThai, isDelete) VALUES

('MKM001', 'CTKM001', 'BF2025', 0.15, 1000, 'Active', 0),

('MKM002', 'CTKM002', 'FREECOCA', 10000.00, 500, 'Active', 0),

('MKM003', 'CTKM003', 'SUA10', 0.10, 200, 'Active', 0),

('MKM004', 'CTKM004', 'BUY2GET1', 0.00, 500, 'Active', 0),

('MKM005', 'CTKM005', 'GOLD5', 0.05, 9999, 'Active', 0);



---

-- 34. core.DieuKienApDung

INSERT INTO core.DieuKienApDung (id, chuongTrinhId, dieuKien, giaTriToiThieu, giamTheo, giaTriToiDa, isDelete) VALUES

('DK001', 'CTKM001', N'Áp dụng cho mọi đơn hàng', 50000.00, 'PhanTram', 500000.00, 0),

('DK002', 'CTKM002', N'Áp dụng cho mọi đơn hàng', 100000.00, 'GiaTri', 100000.00, 0),

('DK003', 'CTKM003', N'Áp dụng cho Danh mục Sữa', 0.00, 'PhanTram', 20000.00, 0),

('DK004', 'CTKM004', N'Chỉ áp dụng cho Danh mục Nước giải khát, cần 2 sản phẩm', 0.00, 'KhongGiam', 10000.00, 0),

('DK005', 'CTKM005', N'Áp dụng cho mọi đơn hàng của KH Vàng', 1.00, 'PhanTram', 10000.00, 0);


-- 35. core.DieuKienApDungDanhMuc

INSERT INTO core.DieuKienApDungDanhMuc (id, dieuKienId, danhMucId, isDelete) VALUES

('DKDM001', 'DK003', 'DM001', 0),

('DKDM002', 'DK004', 'DM001', 0),

('DKDM003', 'DK003', 'DM004', 0),

('DKDM004', 'DK001', 'DM002', 0),

('DKDM005', 'DK002', 'DM003', 0);



---

-- 36. core.ChiTietHoaDonKhuyenMai

-- HD003: Giảm 5% Chocopie (96000 * 0.05 = 4800)

INSERT INTO core.ChiTietHoaDonKhuyenMai (hoaDonId, sanPhamDonViId, maKhuyenMaiId, giaTriApDung, id, isDelete) VALUES

('HD003', 'SPDV005', 'MKM001', 4800.00, 'CTHDKM001', 0),

('HD001', 'SPDV001', 'MKM002', 20000.00, 'CTHDKM002', 0),

('HD002', 'SPDV004', 'MKM003', 8500.00, 'CTHDKM003', 0),

('HD004', 'SPDV006', 'MKM005', 350.00, 'CTHDKM004', 0),

('HD005', 'SPDV002', 'MKM001', 23000.00, 'CTHDKM005', 0);



-- 37. core.Shipper

INSERT INTO core.Shipper (id, hoTen, soDienThoai, isDelete) VALUES

('SHIP001', N'Nguyễn Văn Giao', '0910101010', 0),

('SHIP002', N'Trần Thị Hàng', '0920202020', 0),

('SHIP003', N'Lê Minh Vận', '0930303030', 0),

('SHIP004', N'Phạm Giao Nhanh', '0940404040', 0),

('SHIP005', N'Đỗ Tài Xế', '0950505050', 0);



---

-- 38. core.PhiVanChuyen

INSERT INTO core.PhiVanChuyen (id, soTien, phuongThucTinh, isDelete) VALUES

('PVC001', 20000.00, N'Cố định', 0),

('PVC002', 30000.00, N'Cố định', 0),

('PVC003', 15000.00, N'Cố định', 0),

('PVC004', 0.00, N'Miễn phí', 0),

('PVC005', 45000.00, N'Cố định', 0);



---

-- 39. core.DonHangOnline

INSERT INTO core.DonHangOnline (id, hoaDonId, khachHangId, kenhDat, ngayDat, tongTien, isDelete) VALUES

('DHO001', 'HD002', 'KH002', 'Website', '2025-11-10', 104500.00, 0),

('DHO002', 'HD005', 'KH005', 'App', '2025-11-12', 230000.00, 0),

('DHO003', 'HD001', 'KH001', 'App', '2025-11-10', 55000.00, 0),

('DHO004', 'HD003', 'KH003', 'Website', '2025-11-11', 131200.00, 0),

('DHO005', 'HD004', 'KH004', 'App', '2025-11-11', 7000.00, 0);



---

-- 40. core.ChiTietDonOnline

INSERT INTO core.ChiTietDonOnline (donHangId, sanPhamDonViId, soLuong, donGia, isDelete) VALUES

('DHO001', 'SPDV004', 1, 85000.00, 0),

('DHO001', 'SPDV001', 2, 10000.00, 0),

('DHO002', 'SPDV002', 1, 230000.00, 0),

('DHO003', 'SPDV001', 2, 10000.00, 0),

('DHO003', 'SPDV003', 1, 15000.00, 0),

('DHO003', 'SPDV006', 3, 7000.00, 0),

('DHO004', 'SPDV005', 2, 48000.00, 0),

('DHO004', 'SPDV001', 4, 10000.00, 0);



---

-- 41. core.DonGiaoHang

INSERT INTO core.DonGiaoHang (id, hoaDonId, shipperId, phiVanChuyenId, ngayGiao, trangThaiHienTai, isDelete) VALUES

('DGH001', 'HD002', 'SHIP001', 'PVC001', '2025-11-10', N'Đã giao', 0),

('DGH002', 'HD005', 'SHIP002', 'PVC003', '2025-11-12', N'Đang giao', 0),

('DGH003', 'HD001', 'SHIP003', 'PVC004', '2025-11-10', N'Đã giao', 0),

('DGH004', 'HD003', 'SHIP004', 'PVC002', '2025-11-11', N'Đang chờ lấy hàng', 0),

('DGH005', 'HD004', 'SHIP005', 'PVC001', '2025-11-11', N'Đã giao', 0);



---

-- 42. core.TrangThaiGiaoHang

INSERT INTO core.TrangThaiGiaoHang (id, donGiaoHangId, trangThai, ngayCapNhat, ghiChu, isDelete) VALUES

('TTGH001', 'DGH001', N'Đã tiếp nhận', '2025-11-10 11:05:00', N'Chờ lấy hàng', 0),

('TTGH002', 'DGH001', N'Đang giao', '2025-11-10 13:00:00', N'Đang trên đường đến KH', 0),

('TTGH003', 'DGH001', N'Đã giao', '2025-11-10 14:30:00', N'Giao hàng thành công', 0),

('TTGH004', 'DGH002', N'Đã tiếp nhận', '2025-11-12 08:20:00', N'Chờ lấy hàng', 0),

('TTGH005', 'DGH002', N'Đang giao', '2025-11-12 10:00:00', N'Đang trên đường đến KH', 0);



---

-- 43. core.TrangThaiXuLy

INSERT INTO core.TrangThaiXuLy (id, donHangId, trangThai, ngayCapNhat, isDelete) VALUES

('TTXL001', 'DHO001', N'Chờ xác nhận', '2025-11-10 11:00:00', 0),

('TTXL002', 'DHO001', N'Đã xử lý', '2025-11-10 11:15:00', 0),

('TTXL003', 'DHO002', N'Chờ xác nhận', '2025-11-12 08:15:00', 0),

('TTXL004', 'DHO002', N'Đã xử lý', '2025-11-12 08:30:00', 0),

('TTXL005', 'DHO003', N'Đã xử lý', '2025-11-10 09:35:00', 0);



---



-- 44. core.CaLamViec

INSERT INTO core.CaLamViec (id, tenCa, thoiGianBatDau, thoiGianKetThuc, isDelete) VALUES

('CA001', N'Ca Sáng', '07:00:00', '12:00:00', 0),

('CA002', N'Ca Chiều', '12:00:00', '17:00:00', 0),

('CA003', N'Ca Tối', '17:00:00', '22:00:00', 0),

('CA004', N'Hành Chính', '08:00:00', '17:00:00', 0),

('CA005', N'Ca Gãy', '10:00:00', '15:00:00', 0);



---

-- 45. core.PhanCongCaLamViec

INSERT INTO core.PhanCongCaLamViec (nhanVienId, caLamViecId, ngay, id, isDelete) VALUES

('NV002', 'CA001', '2025-11-12', 'PC001', 0),

('NV005', 'CA002', '2025-11-12', 'PC002', 0),

('NV002', 'CA003', '2025-11-13', 'PC003', 0),

('NV001', 'CA004', '2025-11-12', 'PC004', 0),

('NV003', 'CA005', '2025-11-12', 'PC005', 0);



---

-- 46. core.ChamCong

INSERT INTO core.ChamCong (id, nhanVienId, ngay, gioVao, gioRa, ghiChu, isDelete) VALUES

('CC001', 'NV002', '2025-11-12', '2025-11-12 06:55:00', '2025-11-12 12:05:00', N'Đúng giờ', 0),

('CC002', 'NV005', '2025-11-12', '2025-11-12 11:58:00', '2025-11-12 17:02:00', N'Đúng giờ', 0),

('CC003', 'NV001', '2025-11-12', '2025-11-12 07:50:00', '2025-11-12 17:10:00', N'Hành chính', 0),

('CC004', 'NV003', '2025-11-12', '2025-11-12 09:55:00', '2025-11-12 15:00:00', N'Ca gãy', 0),

('CC005', 'NV002', '2025-11-13', '2025-11-13 16:50:00', '2025-11-13 22:05:00', N'Ca tối', 0);



---

-- 47. core.BaoCao

INSERT INTO core.BaoCao (id, loaiBaoCao, ngayLap, isDelete, tuNgay, denNgay) VALUES

('BC001', N'DoanhThu', '2025-11-12', 0, '2025-11-01', '2025-11-11'),

('BC002', N'BanChay', '2025-11-12', 0, '2025-11-01', '2025-11-11'),

('BC003', N'TonKho', '2025-11-12', 0, '2025-11-01', '2025-11-11'),

('BC004', N'DoanhThu', '2025-11-01', 0, '2025-10-01', '2025-10-31'),

('BC005', N'BanChay', '2025-11-01', 0, '2025-10-01', '2025-10-31');



---

-- 48. core.BaoCaoDoanhThu

INSERT INTO core.BaoCaoDoanhThu (id, baoCaoId, tongDoanhThu, kyBaoCao, isDelete) VALUES

('BCDT001', 'BC001', 532700.00, N'1/11/2025 - 11/11/2025', 0),

('BCDT002', 'BC004', 15000000.00, N'1/10/2025 - 31/10/2025', 0),

('BCDT003', 'BC004', 12000000.00, N'1/10/2025 - 31/10/2025', 0),

('BCDT004', 'BC004', 18000000.00, N'1/10/2025 - 31/10/2025', 0),

('BCDT005', 'BC004', 14500000.00, N'1/10/2025 - 31/10/2025', 0);



---

-- 49. core.BaoCaoBanChay

INSERT INTO core.BaoCaoBanChay (baoCaoId, sanPhamId, soLuongBan, id, isDelete) VALUES

('BC002', 'SP001', 6, 'BCBC001', 0),

('BC002', 'SP003', 1, 'BCBC002', 0),

('BC002', 'SP005', 4, 'BCBC003', 0),

('BC002', 'SP002', 1, 'BCBC004', 0),

('BC002', 'SP004', 2, 'BCBC005', 0);



---

-- 50. core.BaoCaoTonKho

INSERT INTO core.BaoCaoTonKho (id, baoCaoId, isDelete, sanPhamDonViId, tonDauKy, nhapTrongKy, xuatTrongKy, tonCuoiKy) VALUES

('BCTK001', 'BC003', 0, 'SPDV001', 100, 0, 8, 92),

('BCTK002', 'BC003', 0, 'SPDV002', 5, 10, 1, 14),

('BCTK003', 'BC003', 0, 'SPDV003', 150, 100, 1, 249),

('BCTK004', 'BC003', 0, 'SPDV004', 30, 20, 1, 49),

('BCTK005', 'BC003', 0, 'SPDV005', 40, 30, 2, 68);



---

-- 51. core.ChinhSachHoanTra

INSERT INTO core.ChinhSachHoanTra (id, tenChinhSach, thoiHan, dieuKien, apDungToanBo, apDungTuNgay, apDungDenNgay, isDelete) VALUES

('CSHT001', N'Hoàn trả 7 ngày', 7, N'Sản phẩm còn nguyên tem mác, hóa đơn còn nguyên vẹn.', 1, '2025-01-01', '2026-01-01', 0),

('CSHT002', N'Hoàn trả Hóa mỹ phẩm 3 ngày', 3, N'Chỉ áp dụng cho Hóa mỹ phẩm, không được mở nắp.', 0, '2025-10-01', '2026-01-01', 0),

('CSHT003', N'Hoàn trả thực phẩm tươi sống 1 ngày', 1, N'Chỉ áp dụng cho thực phẩm tươi, còn nguyên bao bì.', 0, '2025-01-01', '2026-01-01', 0),

('CSHT004', N'Hoàn tiền trong 30 ngày', 30, N'Lỗi do nhà sản xuất.', 1, '2025-01-01', '2026-01-01', 0),

('CSHT005', N'Hoàn trả hàng điện tử 7 ngày', 7, N'Hàng còn nguyên hộp, chưa kích hoạt bảo hành.', 0, '2025-01-01', '2026-01-01', 0);



---

-- 52. core.ChinhSachHoanTra_DanhMuc

INSERT INTO core.ChinhSachHoanTra_DanhMuc (chinhSachId, danhMucId, id, isDelete) VALUES

('CSHT002', 'DM003', 'CSHTDM001', 0),

('CSHT003', 'DM002', 'CSHTDM002', 0),

('CSHT005', 'DM005', 'CSHTDM003', 0),

('CSHT001', 'DM001', 'CSHTDM004', 0),

('CSHT002', 'DM004', 'CSHTDM005', 0);



---

-- 53. core.PhieuDoiTra

INSERT INTO core.PhieuDoiTra (id, hoaDonId, sanPhamDonViId, ngayDoiTra, lyDo, soTienHoan, chinhSachId, isDelete) VALUES

('PDT001', 'HD001', 'SPDV001', '2025-11-12', N'Khách hàng mua nhầm số lượng', 10000.00, 'CSHT001', 0),

('PDT002', 'HD002', 'SPDV004', '2025-11-12', N'Sản phẩm bị lỗi đóng gói', 85000.00, 'CSHT002', 0),

('PDT003', 'HD003', 'SPDV005', '2025-11-11', N'Mua dư số lượng', 48000.00, 'CSHT001', 0),

('PDT004', 'HD005', 'SPDV002', '2025-11-11', N'Thùng bị rách do vận chuyển', 230000.00, 'CSHT001', 0),

('PDT005', 'HD004', 'SPDV006', '2025-11-11', N'Hết hạn sử dụng (lỗi kho)', 7000.00, 'CSHT003', 0);



---

-- 54. core.NhatKyHoatDong

INSERT INTO core.NhatKyHoatDong (id, taiKhoanId, thoiGian, hanhDong, isDelete) VALUES

('NK001', 'TK_ADM', '2025-11-12 10:00:00', N'Đăng nhập thành công', 0),

('NK002', 'TK_NV1', '2025-11-12 09:30:00', N'Tạo hóa đơn HD001', 0),

('NK003', 'TK_KH1', '2025-11-12 11:00:00', N'Xem lịch sử mua hàng', 0),

('NK004', 'TK_NV2', '2025-11-12 15:00:00', N'Cập nhật tồn kho SPDV003', 0),

('NK005', 'TK_SHIP1', '2025-11-12 13:00:00', N'Cập nhật trạng thái giao hàng DGH001', 0);



---

-- 55. core.MaDinhDanhSanPham

INSERT INTO core.MaDinhDanhSanPham (id, sanPhamDonViId, loaiMa, maCode, duongDan, isDelete, anhId) VALUES

('MDD001', 'SPDV001', 'Barcode', '8934567890123', '/barcode/SPDV001.png', 0, 'Anh004'),

('MDD002', 'SPDV003', 'QR Code', 'QR_SPDV003', '/qrcode/SPDV003.png', 0, 'Anh004'),

('MDD003', 'SPDV004', 'Barcode', '8934567890456', '/barcode/SPDV004.png', 0, 'Anh004'),

('MDD004', 'SPDV005', 'QR Code', 'QR_SPDV005', '/qrcode/SPDV005.png', 0, 'Anh004'),

('MDD005', 'SPDV006', 'Barcode', '8934567890789', '/barcode/SPDV006.png', 0, 'Anh004');



---

-- 56. core.TemNhan

INSERT INTO core.TemNhan (id, maDinhDanhId, noiDungTem, ngayIn, isDelete, anhId) VALUES

('TN001', 'MDD001', N'Sản phẩm: Coca-Cola lon. Giá: 10,000 VND', '2025-10-01', 0, 'Anh004'),

('TN002', 'MDD003', N'Sản phẩm: Dầu gội Clear Men. HSD: 2026-12-31', '2025-10-10', 0, 'Anh005'),

('TN003', 'MDD004', N'Sản phẩm: Bánh Chocopie. Giá: 48,000 VND', '2025-10-15', 0, 'Anh004'),

('TN004', 'MDD002', N'Sản phẩm: Mì Omachi. HSD: 2026-06-01', '2025-10-05', 0, 'Anh004'),

('TN005', 'MDD005', N'Sản phẩm: Milo hộp giấy. Giá: 7,000 VND', '2025-10-20', 0, 'Anh004');



---

-- 57. core.PhieuXuat

INSERT INTO core.PhieuXuat (id, khachHangId, ngayXuat, tongTien, isDelete, nhanVienId) VALUES

('PX001', 'KH003', '2025-11-12', 131200.00, 0, 'NV002'),

('PX002', 'KH001', '2025-11-10', 55000.00, 0, 'NV005'),

('PX003', 'KH002', '2025-11-10', 104500.00, 0, 'NV002'),

('PX004', 'KH004', '2025-11-11', 7000.00, 0, 'NV005'),

('PX005', 'KH005', '2025-11-12', 230000.00, 0, 'NV002');



---

-- 58. core.ChiTietPhieuXuat

INSERT INTO core.ChiTietPhieuXuat (phieuXuatId, sanPhamDonViId, soLuong, donGia, isDelete, tongTien) VALUES

('PX001', 'SPDV005', 2, 48000.00, 0, 96000.00),

('PX001', 'SPDV001', 4, 10000.00, 0, 40000.00),

('PX002', 'SPDV001', 2, 10000.00, 0, 20000.00),

('PX002', 'SPDV003', 1, 15000.00, 0, 15000.00),

('PX002', 'SPDV006', 3, 7000.00, 0, 21000.00),

('PX003', 'SPDV004', 1, 85000.00, 0, 85000.00),

('PX003', 'SPDV001', 2, 10000.00, 0, 20000.00);



---

-- 59. core.GioHang

INSERT INTO core.GioHang (taiKhoanId, sanPhamDonViId, soLuong, isDelete, ngayTao, ngayCapNhat) VALUES

('TK_KH1', 'SPDV006', 5, 0, '2025-11-12 21:00:00', '2025-11-12 22:00:00'),

('TK_KH1', 'SPDV003', 2, 0, '2025-11-12 21:30:00', '2025-11-12 22:00:00'),

('TK_KH2', 'SPDV005', 1, 0, '2025-11-12 21:00:00', '2025-11-12 22:00:00'),

('TK_KH3', 'SPDV001', 10, 0, '2025-11-12 21:00:00', '2025-11-12 22:00:00'),

('TK_KH4', 'SPDV004', 1, 0, '2025-11-12 21:00:00', '2025-11-12 22:00:00');



---

-- 60. core.KiemKe

INSERT INTO core.KiemKe (id, ngayKiemKe, ketQua, nhanVienId, isDelete, sanPhamDonViID) VALUES

('KK001', '2025-11-05', N'Đúng số lượng, không chênh lệch', 'NV003', 0, 'SPDV001'),

('KK002', '2025-11-05', N'Phát hiện thiếu 2 gói mì', 'NV003', 0, 'SPDV003'),

('KK003', '2025-11-06', N'Tồn đúng, dán lại tem nhãn', 'NV003', 0, 'SPDV004'),

('KK004', '2025-11-07', N'Hết hàng SPDV006, đề nghị nhập thêm', 'NV003', 0, 'SPDV006'),

('KK005', '2025-11-08', N'Kiểm tra ngẫu nhiên Quầy 1: Đủ', 'NV002', 0, NULL);



---

-- 61. core.Anh_SanPhamDonVi (Liên kết Ảnh với SPDV)

INSERT INTO core.Anh_SanPhamDonVi (sanPhamDonViId, anhId, isDelete) VALUES

('SPDV001', 'Anh004', 0),

('SPDV003', 'Anh004', 0),

('SPDV004', 'Anh005', 0),

('SPDV005', 'Anh004', 0),

('SPDV006', 'Anh004', 0),

('SPDV007', 'Anh004', 0);



---

-- 62. core.DieuKienApDungSanPham (Điều kiện áp dụng theo Sản phẩm)

INSERT INTO core.DieuKienApDungSanPham (id, dieuKienId, sanPhamId, isDelete) VALUES

('DKSP001', 'DK001', 'SP001', 0),

('DKSP002', 'DK001', 'SP004', 0),

('DKSP003', 'DK002', 'SP003', 0),

('DKSP004', 'DK003', 'SP005', 0),

('DKSP005', 'DK005', 'SP001', 0);



-- 63. core.DieuKienApDungToanBo (Điều kiện áp dụng toàn bộ cửa hàng)

INSERT INTO core.DieuKienApDungToanBo (id, dieuKienId, ghiChu, isDelete) VALUES

('DKTB001', 'DK001', N'Áp dụng toàn hệ thống, không loại trừ.', 0),

('DKTB002', 'DK002', N'Chương trình tặng quà áp dụng cho mọi hóa đơn đạt 100K.', 0),

('DKTB003', 'DK005', N'Ưu đãi Vàng áp dụng trên mọi sản phẩm.', 0),

('DKTB004', 'DK001', N'Được phép sử dụng kèm theo một số KM khác.', 0),

('DKTB005', 'DK002', N'Không áp dụng cho hóa đơn đã sử dụng KM khác.', 0);



