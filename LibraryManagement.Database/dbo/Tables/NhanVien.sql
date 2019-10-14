﻿CREATE TABLE [dbo].[NhanVien]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TenNV] NVARCHAR(30) NOT NULL, 
    [GioiTinh] NVARCHAR(10) NOT NULL, 
    [NgaySinh] DATE NOT NULL, 
    [CMND] NCHAR(12) NOT NULL, 
    [DiaChi] NVARCHAR(50) NULL, 
    [SDT] NCHAR(11) NULL, 
	[ViTri] NVARCHAR(30) NULL,
    [TrangThai] BIT NOT NULL DEFAULT 1, 
)
