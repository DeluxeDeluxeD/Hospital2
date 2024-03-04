CREATE TABLE Patients (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Surname VARCHAR(50) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    Patronymic VARCHAR(50) NULL,
    PassportNumber FLOAT NOT NULL,
    PassportSeries INT NOT NULL,
    PlaceWorks NVARCHAR(50) NULL,
    Birthdate DATE NULL,
    Gender NVARCHAR(1) NOT NULL,
    Address NVARCHAR(100) NOT NULL,
    Telephone NVARCHAR(50) NOT  NULL,
    Email NVARCHAR(50) NOT  NULL,
    MedicalCardNumber INT NOT NULL,
    ReleaseDate DATE NULL,
    LastVisitDate DATE NULL,
    NextVisitDate DATE NULL,
    InsurancePolicy NVARCHAR(20) NULL,
    InsuranceExpirationDate DATE NULL,
    Diagnosis NVARCHAR(50) NULL,
    HistoryDisease NVARCHAR(255) NULL
);

CREATE TABLE HistoryHospitalizations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdPatient INT NOT NULL,
    DateHospitalization DATE NULL,
    ReleaseDate DATE NULL,
    ReasonHospitalization VARCHAR(100) NULL,
    DescriptionState VARCHAR(255) NULL,
    FOREIGN KEY (IdPatient) REFERENCES Patients(Id)
);

CREATE TABLE Doctors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Surname VARCHAR(50) NOT NULL,
    Name VARCHAR(50) NOT NULL,  
    Patronymic VARCHAR(50) NULL,
    Specialization NVARCHAR(50) NOT NULL,
    Telephone NVARCHAR(50) NULL,
    Login NVARCHAR(50) NOT  NULL,
    Password NVARCHAR(50) NOT  NULL,
);
CREATE TABLE MedicalDiagnosticProcedure (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdPatient INT NOT NULL,
    IdDoctor INT NOT NULL,
    DateMeeting DATE NULL,
    EventType VARCHAR(50) NOT NULL,
    EventName VARCHAR(100) NOT NULL,
    Results NVARCHAR(100) NULL,
    Recommendations VARCHAR(255) NULL,
    Price INT NULL,
    Payment_type VARCHAR(50) NULL,
    FOREIGN KEY (IdPatient) REFERENCES Patients(Id),
    FOREIGN KEY (IdDoctor) REFERENCES Doctors(Id)
);

CREATE TABLE ReceptionSchedule (
    Id INT PRIMARY KEY IDENTITY(1,1),
    IdDoctor INT NOT NULL,
    WeekDay VARCHAR(20) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    FOREIGN KEY (IdDoctor) REFERENCES Doctors(Id)
);
