﻿create database QLPG1
go
use QLPG1
go
create table Roles
(
	id_Role int primary key identity not null,
	Roles nvarchar(500)
)
go
create table Account
(
	id int primary key identity not null,
	id_Role int,
	Username varchar(500),
	TenNV nvarchar(500),
    SDT char(100),
	Email varchar(500),
	Pass varchar(200) ,
	foreign key(id_Role) references dbo.Roles(id_Role)
)
go
create table GoiTap
(
	id_GT int primary key identity not null,
	TenGoiTap nvarchar(700),
    ThoiGianHieuLuc datetime,
    ChiPhi money
)
go
create table ThanhVien
(
	id_TV int primary key identity not null,
	TenTV nvarchar(500),
	SDT varchar(250),
	Email varchar(500),
	TenGT nvarchar(700), 
    NgayTao datetime   --ngày đăng ký tập thử
)
go
--DELETE FROM ThanhVien
create table HoiVien
(
	id_HV int primary key identity not null,
    id_TV int,
	HinhAnh nvarchar(255), 
	NgaySinh datetime,
	CCCD char(100),
	TinhTrang bit,
	NgayGiaNhap datetime,  --ngày đk hội viên
	foreign key(id_TV) references dbo.ThanhVien(id_TV)
)
go
CREATE TABLE BuoiTap
(
    id_BT int primary key identity not null,
    id_HV int,
    NgayThamGia datetime, --ngày hội viên đến tập 
    DaDiemDanh bit,
    foreign key(id_HV) references dbo.HoiVien(id_HV)
)
go
create table ChiTietDK_GoiTap
(
             id_CTDKGoiTap int primary key identity not null,
             id_GT int,
             id_HV int,
			 NgayBatDau datetime,
             NgayKetThuc datetime,
             ThanhTien money ,
             foreign key(id_GT) references dbo.GoiTap(id_GT),
             foreign key(id_HV) references dbo.HoiVien(id_HV)  
)
go

--tổng doanh thu theo tháng và năm
SELECT
    MONTH(NgayBatDau) AS Thang,
    YEAR(NgayBatDau) AS Nam,
    SUM(ThanhTien) AS TongDoanhThu
FROM
    ChiTietDK_GoiTap
WHERE
    YEAR(NgayBatDau) = 2023
GROUP BY
    MONTH(NgayBatDau),
    YEAR(NgayBatDau)
ORDER BY
    Nam, Thang;

--tổng doanh thu
SELECT COALESCE(SUM(ThanhTien), 0) AS DoanhThunam2023
FROM ChiTietDK_GoiTap
WHERE YEAR(NgayBatDau) = 2023;

--số lượng đk gói tập và tổng doanh thu
SELECT
    COUNT(id_CTDKGoiTap) AS TongSoLuong,
    COALESCE(SUM(ThanhTien), 0) AS TongDoanhThu
FROM ChiTietDK_GoiTap;

-- Tính tỷ số chuyển đổi giữa ThanhVien và HoiVien
SELECT 
    (SELECT COUNT(*) FROM ThanhVien) as SoLuong_ThanhVien,
    (SELECT COUNT(*) FROM HoiVien) as SoLuong_HoiVien,
    CASE
        WHEN (SELECT COUNT(*) FROM ThanhVien) > 0 THEN
            CONVERT(decimal(18, 2), (SELECT COUNT(*) FROM HoiVien) * 1.0 / (SELECT COUNT(*) FROM ThanhVien))
        ELSE
            NULL
    END as TySo_ChuyenDoi;

--tổng số lượng hết hạn trong đăng ký gói tập
SELECT COUNT(DISTINCT id_HV) AS NumberOfExpiredMembers
FROM ChiTietDK_GoiTap
WHERE id_HV IS NOT NULL
  AND NgayKetThuc IS NOT NULL
  AND NgayKetThuc < GETDATE(); -- Điều kiện ngày kết thúc nhỏ hơn ngày hiện tại
  
--hiển thị đầy đủ hội viên đã hết hạn bn gói tập
SELECT id_HV, COUNT(*) AS HVhethengoitap
FROM ChiTietDK_GoiTap
WHERE id_HV IS NOT NULL
    AND NgayKetThuc IS NOT NULL
    AND NgayKetThuc < GETDATE() -- Điều kiện ngày kết thúc nhỏ hơn ngày hiện tại
GROUP BY id_HV;

--tổng số hội viên đk gói tập không còn hoạt động
SELECT COUNT(DISTINCT hv.id_HV) AS NumberOfExpiredMembers
FROM HoiVien hv
JOIN ChiTietDK_GoiTap ct ON hv.id_HV = ct.id_HV
WHERE ct.NgayKetThuc IS NOT NULL
      AND ct.NgayKetThuc < GETDATE()
      AND hv.TinhTrang = 0;

select * from HoiVien

--tổng số hội viên đk gói tập còn hoạt động
SELECT COUNT(DISTINCT hv.id_HV) AS NumberOfExpiredMembers
FROM HoiVien hv
JOIN ChiTietDK_GoiTap ct ON hv.id_HV = ct.id_HV
WHERE ct.NgayKetThuc IS NOT NULL
      AND ct.NgayBatDau < GETDATE()
      AND hv.TinhTrang = 1;