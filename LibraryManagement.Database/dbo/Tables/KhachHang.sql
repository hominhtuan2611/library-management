﻿CREATE TABLE [dbo].[KhachHang]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TenKH] NVARCHAR(30) NOT NULL, 
    [GioiTinh] NVARCHAR(10) NOT NULL, 
    [NgaySinh] DATE NOT NULL, 
    [CMND] NCHAR(12) NOT NULL, 
    [DiaChi] NVARCHAR(50) NULL, 
    [SDT] NCHAR(11) NULL, 
    [SoLanVIPham] INT NULL DEFAULT 0,
	[NgayDangKy] DATE NULL , 
    [TrangThai] BIT NOT NULL DEFAULT 1
)
