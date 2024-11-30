CREATE TABLE Client
(	
	Id INT IDENTITY(1,1) PRIMARY KEY,           -- Bemor uchun unikal ID
    AdmissionDate DATETIME,                      -- Qabul kuni
    PatientName NVARCHAR(100),                   -- Bemor
    DateOfBirth DATE,                            -- Tug'ilgan yil
    PhoneNumber NVARCHAR(15),                    -- Telefon raqami
    DoctorConsultant NVARCHAR(100),              -- Shifokor maslahatchisi
    ServiceName NVARCHAR(100),                   -- Xizmat nomi
    DiscountPercentage DECIMAL(5,2),             -- Chegirma miqdori
    VisitType NVARCHAR(50),                      -- Tashrif turi
    AdmissionDetails NVARCHAR(500),              -- Qabul haqida ma'lumot
    AllDiscountAmount DECIMAL(10,2)				 
)
