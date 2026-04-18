CREATE DATABASE VinhKhanhApp;
GO

USE VinhKhanhApp;
GO

CREATE TABLE QUAYHANG (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenQuayHang NVARCHAR(255) NOT NULL,
    MonChinh NVARCHAR(255) NOT NULL,
    DiaChi NVARCHAR(255),
    DanhGia FLOAT CHECK (DanhGia BETWEEN 0 AND 5),
    MoTa NVARCHAR(MAX)
);
GO

INSERT INTO QUAYHANG (TenQuayHang, MonChinh, DiaChi, DanhGia, MoTa) VALUES

(N'Ốc Oanh', N'Ốc - Hải sản', N'534 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.6,
N'Quán ốc nổi tiếng lâu năm tại Vĩnh Khánh, luôn đông khách vào buổi tối. Các món ốc được chế biến đậm đà, đặc biệt là ốc len xào dừa và sò điệp nướng mỡ hành. Không gian vỉa hè nhộn nhịp, đúng chất ăn vặt Sài Gòn.'),

(N'Ốc Thảo', N'Ốc - Hải sản', N'346 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.5,
N'Quán ốc quen thuộc với menu đa dạng và giá cả hợp lý. Các món được nêm nếm vừa miệng, phục vụ nhanh. Phù hợp đi nhóm bạn buổi tối.'),

(N'Ốc Tô', N'Ốc - Hải sản', N'224 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.4,
N'Quán ốc bình dân nhưng chất lượng ổn định. Các món xào me, xào bơ tỏi rất được ưa chuộng. Không gian đơn giản, thoải mái.'),

(N'Ốc Nho', N'Ốc - Hải sản', N'307 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.3,
N'Quán nhỏ nhưng đông khách, nổi bật với các món ốc tươi và cách chế biến đậm vị. Phục vụ nhanh, giá hợp lý.'),

(N'Ốc Ốc', N'Ốc - Hải sản', N'40 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.2,
N'Quán ốc vỉa hè đơn giản nhưng lúc nào cũng nhộn nhịp. Thực đơn đa dạng, phù hợp ăn chơi buổi tối.'),

(N'Bánh tráng nướng Dì Đinh', N'Bánh tráng nướng', N'387 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.5,
N'Bánh tráng nướng giòn, topping đầy đủ như trứng, xúc xích, phô mai. Món ăn vặt quen thuộc, giá mềm.'),

(N'Phá lấu bò Cô Thảo', N'Phá lấu', N'243 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.6,
N'Phá lấu nước cốt dừa béo nhẹ, lòng bò mềm, không bị hôi. Ăn kèm bánh mì rất hợp, đặc biệt vào buổi chiều tối.'),

(N'Bò bía - Gỏi cuốn', N'Ăn vặt', N'295 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.3,
N'Quầy nhỏ bán bò bía và gỏi cuốn tươi, giá rẻ. Phù hợp ăn nhẹ khi dạo phố.'),

(N'Chè Thái Lan', N'Chè - Tráng miệng', N'200 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.4,
N'Các món chè và tráng miệng đa dạng, vị ngọt thanh, mát lạnh. Phù hợp giải nhiệt sau khi ăn ốc.'),

(N'Trà sữa Vĩnh Khánh', N'Trà sữa', N'150 Vĩnh Khánh, Phường 8, Quận 4, TP.HCM', 4.2,
N'Quán trà sữa bình dân với nhiều loại topping. Giá rẻ, phù hợp học sinh sinh viên, thường đông vào buổi tối.');