-- =========================================================
-- DATABASE CLEANUP & RESET
-- =========================================================
DELETE FROM [dbo].[AppointmentSchedule];
DELETE FROM [dbo].[StayParticipation];
DELETE FROM [dbo].[Stay];
DELETE FROM [dbo].[Patient];
DELETE FROM [dbo].[Therapist];
DELETE FROM [dbo].[TreatmentCatalog];
DELETE FROM [dbo].[Specialization];
DELETE FROM [dbo].[RehabRoom];

DBCC CHECKIDENT ('[dbo].[AppointmentSchedule]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[StayParticipation]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Stay]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Patient]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Therapist]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[TreatmentCatalog]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[Specialization]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[RehabRoom]', RESEED, 0);

-- Helper variable: Today's date at 00:00:00
DECLARE @Today DATETIME = CAST(GETDATE() AS DATE);

-- 1. REHABILITATION ROOMS (English Types)
INSERT INTO [dbo].[RehabRoom] (RoomNumber, Type, CreatedAt, IsActive) VALUES 
('101A', 'Kinesitherapy Room', GETDATE(), 1),
('102B', 'Massage Parlor', GETDATE(), 1),
('201C', 'Hydrotherapy Pool', GETDATE(), 1),
('202D', 'Physiotherapy Office', GETDATE(), 1);

-- 2. SPECIALIZATIONS (English)
INSERT INTO [dbo].[Specialization] (Name, CreatedAt, IsActive) VALUES 
('Kinesitherapy', GETDATE(), 1),
('Manual Therapy', GETDATE(), 1),
('Hydrotherapy', GETDATE(), 1),
('Physical Therapy', GETDATE(), 1);

-- 3. TREATMENT CATALOG (English Names)
INSERT INTO [dbo].[TreatmentCatalog] (Name, DurationMinutes, Price, CreatedAt, IsActive) VALUES 
('Individual Exercises', 45, 120.00, GETDATE(), 1),
('Classic Spine Massage', 30, 100.00, GETDATE(), 1),
('Whirlpool Bath', 20, 60.00, GETDATE(), 1),
('Point Laser Therapy', 15, 40.00, GETDATE(), 1);

-- 4. THERAPISTS (Polish Names, English Logic)
INSERT INTO [dbo].[Therapist] (FirstName, LastName, LicenseNumber, SpecializationId, CreatedAt, IsActive) VALUES 
('Marek', 'Kowalski', 'PWZ-112233', 1, GETDATE(), 1),
('Anna', 'Nowak', 'PWZ-445566', 2, GETDATE(), 1),
('Piotr', 'Zieliński', 'PWZ-778899', 3, GETDATE(), 1),
('Katarzyna', 'Wiśniewska', 'PWZ-101112', 4, GETDATE(), 1);

-- 5. PATIENTS (Polish Names)
INSERT INTO [dbo].[Patient] (FirstName, LastName, PESEL, PhoneNumber, CreatedAt, IsActive) VALUES 
('Jan', 'Kowalski', '80010112345', '500-111-222', GETDATE(), 1),
('Maria', 'Wójcik', '75020223456', '500-222-333', GETDATE(), 1),
('Adam', 'Kamiński', '90030334567', '500-333-444', GETDATE(), 1),
('Agnieszka', 'Lewandowska', '85040445678', '500-444-555', GETDATE(), 1),
('Michał', 'Dąbrowski', '82050556789', '500-555-666', GETDATE(), 1);

-- 6. REHABILITATION STAYS (English Names, Dynamic Dates)
INSERT INTO [dbo].[Stay] (Name, StartDate, EndDate, CreatedAt, IsActive) VALUES 
('Spring Session I', DATEADD(DAY, -5, @Today), DATEADD(DAY, 9, @Today), GETDATE(), 1),
('Spring Session II', DATEADD(DAY, 10, @Today), DATEADD(DAY, 24, @Today), GETDATE(), 1);

-- 7. STAY PARTICIPATION
INSERT INTO [dbo].[StayParticipation] (StayId, PatientId, CreatedAt, IsActive) VALUES 
(1, 1, GETDATE(), 1), (1, 2, GETDATE(), 1), (1, 3, GETDATE(), 1), (1, 4, GETDATE(), 1), (1, 5, GETDATE(), 1);

-- 8. APPOINTMENT SCHEDULE (English Enum Statuses from your code)
-- Using: Scheduled, InProgress, Completed, Cancelled
INSERT INTO [dbo].[AppointmentSchedule] (PatientId, TreatmentId, TherapistId, RoomId, StartDateTime, Status, StayParticipationId, CreatedAt, IsActive) VALUES 
-- 08:00 AM - Completed
(1, 2, 2, 2, DATEADD(HOUR, 8, @Today), 'Completed', 1, GETDATE(), 1), 
-- 09:30 AM - Completed
(2, 2, 2, 2, DATEADD(MINUTE, 570, @Today), 'Completed', 2, GETDATE(), 1), 
-- 11:00 AM - InProgress (Right now)
(3, 1, 1, 1, DATEADD(HOUR, 11, @Today), 'InProgress', 3, GETDATE(), 1), 
-- 01:00 PM - Scheduled
(4, 1, 1, 1, DATEADD(HOUR, 13, @Today), 'Scheduled', 4, GETDATE(), 1), 
-- 03:00 PM - Scheduled
(5, 3, 3, 3, DATEADD(HOUR, 15, @Today), 'Scheduled', 5, GETDATE(), 1),
-- 04:30 PM - Cancelled
(1, 4, 4, 4, DATEADD(MINUTE, 990, @Today), 'Cancelled', 1, GETDATE(), 1);

PRINT 'Database successfully populated with English records and Polish names.';