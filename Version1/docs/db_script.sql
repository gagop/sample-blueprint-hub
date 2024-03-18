-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-03-17 19:13:33.696

-- tables
-- Table: Appointment
CREATE TABLE Appointment (
    IdAppointment int  NOT NULL,
    Date date  NOT NULL,
    IdAppointmentStatus int  NOT NULL,
    IdCandidate int  NOT NULL,
    CONSTRAINT Appointment_pk PRIMARY KEY (IdAppointment)
);

-- Table: AppointmentStatus
CREATE TABLE AppointmentStatus (
    IdAppointmentStatus int  NOT NULL,
    Name varchar(100)  NOT NULL,
    CONSTRAINT AppointmentStatus_pk PRIMARY KEY (IdAppointmentStatus)
);

-- Table: DocumentType
CREATE TABLE DocumentType (
    IdDocumentType int  NOT NULL,
    Name varchar(100)  NOT NULL,
    CONSTRAINT DocumentType_pk PRIMARY KEY (IdDocumentType)
);

-- Table: Nationality
CREATE TABLE Nationality (
    IdNationality int  NOT NULL,
    Name varchar(100)  NOT NULL,
    CONSTRAINT Nationality_pk PRIMARY KEY (IdNationality)
);

-- Table: RequiredEnrollmentDocument
CREATE TABLE RequiredEnrollmentDocument (
    IdStudyProgramme int  NOT NULL,
    IdDocumentType int  NOT NULL,
    CONSTRAINT RequiredEnrollmentDocument_pk PRIMARY KEY (IdStudyProgramme,IdDocumentType)
);

-- Table: Status
CREATE TABLE Status (
    IdStatus int  NOT NULL,
    Name varchar(200)  NOT NULL,
    CONSTRAINT Status_pk PRIMARY KEY (IdStatus)
);

-- Table: Student
CREATE TABLE Student (
    IdCandidate int  NOT NULL,
    FirstName varchar(200)  NOT NULL,
    LastName varchar(200)  NOT NULL,
    PhoneNumber varchar(200)  NOT NULL,
    EmailAddress varchar(200)  NOT NULL,
    HomeAddress varchar(255)  NOT NULL,
    Gender int  NOT NULL,
    PeselNumber varchar(11)  NULL,
    PassportNumber varchar(9)  NULL,
    DateOfBirth date  NOT NULL,
    IdNationality int  NOT NULL,
    IdStudyProgramme int  NOT NULL,
    IdStatus int  NOT NULL,
    CONSTRAINT Student_pk PRIMARY KEY (IdCandidate)
);

-- Table: StudyLevel
CREATE TABLE StudyLevel (
    IdStudyLevel int  NOT NULL,
    Name varchar(100)  NOT NULL,
    CONSTRAINT StudyLevel_pk PRIMARY KEY (IdStudyLevel)
);

-- Table: StudyMode
CREATE TABLE StudyMode (
    IdStudyMode int  NOT NULL,
    Name varchar(100)  NOT NULL,
    CONSTRAINT StudyMode_pk PRIMARY KEY (IdStudyMode)
);

-- Table: StudyProgrammer
CREATE TABLE StudyProgrammer (
    IdStudyProgramme int  NOT NULL,
    Name varchar(100)  NOT NULL,
    IdStudyLevel int  NOT NULL,
    RecruitmentStart date  NOT NULL,
    RecruitmentEnd date  NOT NULL,
    IdStudyMode int  NOT NULL,
    CONSTRAINT StudyProgrammer_pk PRIMARY KEY (IdStudyProgramme)
);

-- foreign keys
-- Reference: Appointment_AppointmentStatus (table: Appointment)
ALTER TABLE Appointment ADD CONSTRAINT Appointment_AppointmentStatus
    FOREIGN KEY (IdAppointmentStatus)
    REFERENCES AppointmentStatus (IdAppointmentStatus)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Appointment_Candidate (table: Appointment)
ALTER TABLE Appointment ADD CONSTRAINT Appointment_Candidate
    FOREIGN KEY (IdCandidate)
    REFERENCES Student (IdCandidate)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Candidate_Nationality (table: Student)
ALTER TABLE Student ADD CONSTRAINT Candidate_Nationality
    FOREIGN KEY (IdNationality)
    REFERENCES Nationality (IdNationality)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Candidate_StudyProgrammer (table: Student)
ALTER TABLE Student ADD CONSTRAINT Candidate_StudyProgrammer
    FOREIGN KEY (IdStudyProgramme)
    REFERENCES StudyProgrammer (IdStudyProgramme)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: RequiredEnrollmentDocument_DocumentType (table: RequiredEnrollmentDocument)
ALTER TABLE RequiredEnrollmentDocument ADD CONSTRAINT RequiredEnrollmentDocument_DocumentType
    FOREIGN KEY (IdDocumentType)
    REFERENCES DocumentType (IdDocumentType)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: RequiredEnrollmentDocument_StudyProgrammer (table: RequiredEnrollmentDocument)
ALTER TABLE RequiredEnrollmentDocument ADD CONSTRAINT RequiredEnrollmentDocument_StudyProgrammer
    FOREIGN KEY (IdStudyProgramme)
    REFERENCES StudyProgrammer (IdStudyProgramme)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: Student_Status (table: Student)
ALTER TABLE Student ADD CONSTRAINT Student_Status
    FOREIGN KEY (IdStatus)
    REFERENCES Status (IdStatus)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: StudyProgrammer_StudyCycle (table: StudyProgrammer)
ALTER TABLE StudyProgrammer ADD CONSTRAINT StudyProgrammer_StudyCycle
    FOREIGN KEY (IdStudyLevel)
    REFERENCES StudyLevel (IdStudyLevel)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- Reference: StudyProgrammer_StudyMode (table: StudyProgrammer)
ALTER TABLE StudyProgrammer ADD CONSTRAINT StudyProgrammer_StudyMode
    FOREIGN KEY (IdStudyMode)
    REFERENCES StudyMode (IdStudyMode)  
    NOT DEFERRABLE 
    INITIALLY IMMEDIATE
;

-- End of file.

