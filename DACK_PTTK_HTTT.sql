create database QLTC
go
use QLTC

CREATE TABLE KHACHHANG
(
	MAKH CHAR(8) PRIMARY KEY,
	HOTEN NVARCHAR(50),
	DIACHI NVARCHAR(200),
	NGAYSINH DATE, 
	SDT CHAR(10)
);

CREATE TABLE VACCINE 
(
	MAVC CHAR(8) PRIMARY KEY, 
	TENVACCINE VARCHAR(50), 
	XUATXU VARCHAR(30), 
	SLT INT, 
	GIA DECIMAL(19,4)
);

CREATE TABLE HOSOTIEMCHUNG 
(	
	MAHS CHAR(8) PRIMARY KEY,
	NGAYLAP DATE, 
	MAKH CHAR(8)
);

CREATE TABLE CT_HSTC 
(
	MAHS CHAR(8) NOT NULL, 
	MAVC CHAR(8) NOT NULL, 
	NGAYTIEM DATE
);

ALTER TABLE CT_HSTC 
ADD CONSTRAINT PK_CT_HSTC PRIMARY KEY (MAHS, MAVC);

CREATE TABLE DSDATVACCINE 
(
	MADS CHAR(8) PRIMARY KEY, 
	NGAYLAP DATE, 
	MANV CHAR(8),
	duyet bit, 
	lydo nvarchar(200)
);

CREATE TABLE CT_DSDAT 
(
	MADS CHAR(8) NOT NULL, 
	MAVC CHAR(8) NOT NULL, 
	SOLUONG INT
);

ALTER TABLE CT_DSDAT 
ADD CONSTRAINT PK_CT_DSDAT PRIMARY KEY (MADS, MAVC);

create table GOIVC
(
	magoi char(8) primary key,
	tongtien decimal(19,4)
);

create table ct_goivc 
(
	magoi char(8) not null, 
	mavc char(8) not null
);

alter table ct_goivc
add constraint pk_goi primary key(magoi, mavc);

alter table ct_goivc
add constraint fk_vc_goi foreign key (mavc) references vaccine (mavc)
alter table ct_goivc
add constraint fk_goi_ct foreign key (magoi) references goivc (magoi)


CREATE TABLE PHIEUDKTIEM 
(
	MAPDKTIEM CHAR(8) PRIMARY KEY, 
	MAKH CHAR(8), 
	DIACHI NVARCHAR(200), 
	GIOITINH BIT, 
	NGAYTIEM DATE, 
	HOTEN NVARCHAR(50), 
	SDT CHAR(10), 
	HOTEN_NGH NVARCHAR(50), 
	SDT_NGH CHAR(10), 
	MQH NVARCHAR(20)
);

CREATE TABLE DONMUAVC 
(
	MADON CHAR(8) PRIMARY KEY, 
	MAKH CHAR(8), 
	NGAYDATMUA DATE, 
	TONGTIEN DECIMAL(19,4)
);

CREATE TABLE HOADON 
(
	MAHD CHAR(10) PRIMARY KEY, 
	HINHTHUC VARCHAR(10), 
	SOTIEN DECIMAL(19, 4),
	madon CHAR(8), 
	dott int,
	tinhtrang nvarchar(20)
);

CREATE TABLE NHANVIEN 
(
	MANV CHAR(8) PRIMARY KEY, 
	HOTEN NVARCHAR(50), 
	NGAYSINH DATE, 
	BANGCAP NVARCHAR(30), 
	SDT CHAR(10), 
	TTLAMVIEC NVARCHAR(10), 
	VITRI VARCHAR(10)
);



CREATE TABLE CT_DMVC
(
	MADON CHAR(8) NOT NULL, 
	MAVC CHAR(8) NOT NULL, 
	SOLUONG INT, 
	GIA DECIMAL (19,4)
);

ALTER TABLE CT_DMVC
ADD CONSTRAINT PK_CT_DMVC PRIMARY KEY (MADON, MAVC)

CREATE TABLE THEKHACHHANG 
(
	MATHE CHAR(8) PRIMARY KEY, 
	MAKH CHAR(8), 
	NGAYLAP DATE
);

CREATE TABLE TAIKHOAN 
(
	USRNAME CHAR(8) PRIMARY KEY, 
	PASSWRD VARCHAR(50),
	TK_TYPE INT
);

CREATE TABLE LICHLAMVIEC 
(
	MANV CHAR(8) PRIMARY KEY, 
	NGAYAPDUNG DATE, 
	THOIGIANAPDUNG INT, 
	MON INT, 
	TUE INT,
	WED INT, 
	THU INT, 
	FRI INT, 
	SAT INT,
	SUN INT
);

CREATE TABLE LICHRANH
(
	MANV CHAR(8) PRIMARY KEY, 
	MON INT, 
	TUE INT,
	WED INT, 
	THU INT, 
	FRI INT, 
	SAT INT,
	SUN INT
);

ALTER TABLE LICHLAMVIEC 
ADD CONSTRAINT FK_NV_LLV FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV)

ALTER TABLE LICHRANH 
ADD CONSTRAINT FK_NV_LR FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV)

ALTER TABLE HOSOTIEMCHUNG 
ADD CONSTRAINT FK_KH_HSTC FOREIGN KEY(MAKH) REFERENCES KHACHHANG (MAKH)

ALTER TABLE CT_HSTC 
ADD CONSTRAINT FK_VC_CT_HSTC FOREIGN KEY (MAVC) REFERENCES VACCINE (MAVC)

ALTER TABLE CT_HSTC
ADD CONSTRAINT FK_HS_CT_HSTC FOREIGN KEY (MAHS) REFERENCES HOSOTIEMCHUNG (MAHS)

ALTER TABLE CT_DSDAT
ADD CONSTRAINT FK_DS_CT_DSDAT FOREIGN KEY (MADS) REFERENCES DSDATVACCINE (MADS)

ALTER TABLE CT_DSDAT
ADD CONSTRAINT FK_VC_CT_DSDAT FOREIGN KEY (MAVC) REFERENCES VACCINE (MAVC)

ALTER TABLE PHIEUDKTIEM 
ADD CONSTRAINT FK_KH_PDKT FOREIGN KEY(MAKH) REFERENCES KHACHHANG (MAKH)

ALTER TABLE THEKHACHHANG 
ADD CONSTRAINT FK_KH_THE FOREIGN KEY(MAKH) REFERENCES KHACHHANG (MAKH)

ALTER TABLE DONMUAVC
ADD CONSTRAINT FK_KH_DON FOREIGN KEY(MAKH) REFERENCES KHACHHANG (MAKH)

ALTER TABLE HOADON 
ADD CONSTRAINT FK_DONMUA_HOADON FOREIGN KEY (MADON) REFERENCES DONMUAVC (MADON)

ALTER TABLE CT_DMVC
ADD CONSTRAINT FK_DON_CT_DMVC FOREIGN KEY (MADON) REFERENCES DONMUAVC (MADON)

ALTER TABLE CT_DMVC
ADD CONSTRAINT FK_VC_CT_DMVC FOREIGN KEY (MAVC) REFERENCES VACCINE (MAVC)

ALTER TABLE DSDATVACCINE 
ADD CONSTRAINT FK_NV_DSDAT FOREIGN KEY (MANV) REFERENCES NHANVIEN (MANV)

ALTER TABLE TAIKHOAN 
ADD CONSTRAINT FK_NV_TK FOREIGN KEY (USRNAME) REFERENCES NHANVIEN (MANV)

ALTER TABLE TAIKHOAN 
ADD CONSTRAINT FK_KH_TK FOREIGN KEY (USRNAME) REFERENCES KHACHHANG (MAKH)

BULK INSERT KHACHHANG
FROM '\\Mac\AllFiles\Users\Study\temp\khachhang.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT vaccine
FROM '\\Mac\AllFiles\Users\Study\temp\vc.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT thekhachhang
FROM '\\Mac\AllFiles\Users\Study\temp\thekh.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT hosotiemchung
FROM '\\Mac\AllFiles\Users\Study\temp\hs.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT ct_hstc
FROM '\\Mac\AllFiles\Users\Study\temp\cths.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT nhanvien
FROM '\\Mac\AllFiles\Users\Study\temp\nv.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT lichlamviec
FROM '\\Mac\AllFiles\Users\Study\temp\lichlamviec.csv'
WITH
(
    FIRSTROW = 1, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT lichranh
FROM '\\Mac\AllFiles\Users\Study\temp\lichranh.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT phieudktiem
FROM '\\Mac\AllFiles\Users\Study\temp\pdkt.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT hoadon
FROM '\\Mac\AllFiles\Users\Study\temp\hoadon.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT donmuavc
FROM '\\Mac\AllFiles\Users\Study\temp\donmua.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT ct_dmvc
FROM '\\Mac\AllFiles\Users\Study\temp\ctdm.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT dsdatvaccine
FROM '\\Mac\AllFiles\Users\Study\temp\nhaphang.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)


BULK INSERT ct_dsdat
FROM '\\Mac\AllFiles\Users\Study\temp\ctnh.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT taikhoan
FROM '\\Mac\AllFiles\Users\Study\temp\taikhoan.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

UPDATE donmuavc
SET TONGTIEN = (SELECT SUM(ct.Gia)
				FROM CT_DMVC ct
				WHERE DONMUAVC.MADON = CT.MADON
				GROUP BY ct.MADON)

BULK INSERT goivc
FROM '\\Mac\AllFiles\Users\Study\temp\goi.csv'
WITH
(
    FIRSTROW = 1, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

BULK INSERT ct_goivc
FROM '\\Mac\AllFiles\Users\Study\temp\ctgoi.csv'
WITH
(
    FIRSTROW = 2, -- as 1st one is header
    FIELDTERMINATOR = ',',  --CSV field delimiter
    ROWTERMINATOR = '0x0a',   --Use to shift the control to next row
    TABLOCK
)

UPDATE goivc
SET TONGTIEN = (SELECT SUM(vc.GIA)
				FROM ct_goivc ct join vaccine vc on ct.mavc = vc.MAVC
				WHERE goivc.magoi = CT.magoi
				GROUP BY ct.magoi)

update hoadon 
set sotien = (select dm.TONGTIEN/(select count(mahd) from hoadon hd where hd.madon = dm.MADON group by(madon))
			from donmuavc dm 
			where dm.madon = hoadon.madon)


