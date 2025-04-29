-- IMAGE ITEMS

CREATE TABLE PersonalSite.Images (
    ID VARCHAR(32) PRIMARY KEY,
    Title VARCHAR(256) COLLATE utf8mb4_bin,
    Description VARCHAR(2048) COLLATE utf8mb4_bin,
    AlterationsDescription VARCHAR(1024) COLLATE utf8mb4_bin,
    NoAlterations TINYINT DEFAULT NULL,
    SourceWidth INT NOT NULL,
    SourceHeight INT NOT NULL,
    Brightness INT NOT NULL,
    CaptureDate DATETIME,
    IsUnlisted TINYINT DEFAULT 0 NOT NULL,
    SystemFileName VARCHAR(64) NOT NULL,
    OriginalFileName VARCHAR(64),
    ISO INT,
    ExposureTimeNumerator DECIMAL(6, 1),
    ExposureTimeDenominator INT,
    Aperature DECIMAL(6, 1),
    
    UploadDate DATETIME NOT NULL DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL
) CHARACTER SET = utf8mb4;

CREATE TABLE PersonalSite.ImageViews (
    ImageViewID INT PRIMARY KEY AUTO_INCREMENT, -- To make EF happy
    ImageID VARCHAR(32) NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImageViews_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.ImageColors (
    ImageID VARCHAR(32) NOT NULL,
    ColorID INT NOT NULL,

    CONSTRAINT FK_ImageColors_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImageColors_R_Colors FOREIGN KEY (ColorID)
        REFERENCES PersonalSite.Colors (ID)
        ON DELETE CASCADE
);

-- SUBJECTS

CREATE TABLE PersonalSite.Subjects (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    ParentID INT DEFAULT NULL,
    Name VARCHAR(128) COLLATE utf8mb4_bin,

    Timestamp DATETIME NOT NULL DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL,

    CONSTRAINT FK_Subjects_R_Parent
        FOREIGN KEY (ParentID) REFERENCES PersonalSite.Subjects (ID)
        ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE TABLE PersonalSite.ImageSubjects (
    ImageID VARCHAR(32) NOT NULL,
    SubjectID INT NOT NULL,
    QuantityBucket INT NOT NULL,
    IsPrimarySubject TINYINT NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImgSubj_R_Image
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImgSubj_R_Subjects
        FOREIGN KEY (SubjectID) REFERENCES PersonalSite.Subjects (ID)
        ON DELETE CASCADE
);

-- ATTRIBUTION

CREATE TABLE PersonalSite.Photographers (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(64) COLLATE utf8mb4_bin,
    LastName VARCHAR(128) COLLATE utf8mb4_bin,
    Timestamp DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET = utf8mb4;

CREATE TABLE PersonalSite.ImagePhotographers (
    ImageID VARCHAR(32) NOT NULL,
    PhotographerID INT NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImgPhotog_R_Img
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImgPhotog_R_Photog
        FOREIGN KEY (PhotographerID) REFERENCES PersonalSite.Photographers (ID)
        ON DELETE CASCADE
);

-- CURATION

CREATE TABLE PersonalSite.Collections (
    ID VARCHAR(32) PRIMARY KEY,
    Name VARCHAR(128) COLLATE utf8mb4_bin,
    Description VARCHAR(1024),
    IsUnlisted TINYINT DEFAULT 0 NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET = utf8mb4;

CREATE TABLE PersonalSite.CollectionImages (
    ImageID VARCHAR(32) NOT NULL,
    CollectionID VARCHAR(32) NOT NULL,
    
    CONSTRAINT FK_CollecImg_R_Img
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_CollecImg_R_Collec
        FOREIGN KEY (CollectionID) REFERENCES PersonalSite.Collections (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.ImageAttachments (
    FileID VARCHAR(32) NOT NULL,
    ImageID VARCHAR(32) NOT NULL,

    CONSTRAINT FK_ImageAtt_R_Files
        FOREIGN KEY (FileID) REFERENCES PersonalSite.Files(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImageAtt_R_Img
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images(ID)
        ON DELETE CASCADE
);