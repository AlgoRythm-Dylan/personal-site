CREATE TABLE PersonalSite.PaletteColors (
	ID INT PRIMARY KEY AUTO_INCREMENT,
	Red TINYINT UNSIGNED NOT NULL,
	Green TINYINT UNSIGNED NOT NULL,
	Blue TINYINT UNSIGNED NOT NULL,
    
    CONSTRAINT C_PaletteColorsUnique UNIQUE (Red, Green, Blue)
);

CREATE INDEX IDX_PaletteColorsSpacial ON PersonalSite.PaletteColors (Red, Green, Blue);

CREATE TABLE PersonalSite.Images (
	ID INT PRIMARY KEY AUTO_INCREMENT,
    Title VARCHAR(256) CHARACTER SET utf8mb4,
    Description VARCHAR(2048) CHARACTER SET utf8mb4,
    ViewCount INT NOT NULL DEFAULT 0,
    Width INT NOT NULL,
    Height INT NOT NULL,
    Brightness INT NOT NULL,
    CaptureDate DATETIME,
    FileName VARCHAR(64) NOT NULL,
    IsHDR INT NOT NULL DEFAULT 0,
    ISO INT,
    ExposureTimeNumerator INT,
    ExposureTimeDenominator INT,
    Aperature DECIMAL(4, 4),
    
    UploadDate DATETIME NOT NULL DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.ImageViews (
    ImageID INT NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImageViews_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.ImageColors (
    ImageID INT NOT NULL,
    PaletteColorID INT NOT NULL,

    CONSTRAINT FK_ImageColors_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImageColors_R_Colors FOREIGN KEY (PaletteColorID)
        REFERENCES PersonalSite.PaletteColors (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.ImageSizes (
    ImageID INT NOT NULL,
    Width INT NOT NULL,
    Height INT NOT NULL,

    CONSTRAINT FK_ImageSizes_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.Subjects (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    ParentID INT DEFAULT NULL,
    Name VARCHAR(128) CHARACTER SET utf8mb4,

    CreatedDate DATETIME NOT NULL DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL,

    CONSTRAINT FK_Subjects_R_Parent
        FOREIGN KEY (ParentID) REFERENCES PersonalSite.Subjects (ID)
        ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.ImageSubjects (
    ImageID INT NOT NULL,
    SubjectID INT NOT NULL,
    QuantityBucket INT NOT NULL,
    IsPrimarySubject TINYINT NOT NULL,
    DateAdded DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImgSubj_R_Image
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImgSubj_R_Subjects
        FOREIGN KEY (SubjectID) REFERENCES PersonalSite.Subjects (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.Photographers (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(64) CHARACTER SET utf8mb4,
    LastName VARCHAR(128) CHARACTER SET utf8mb4
    DateAdded DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.ImagePhotographers (
    ImageID INT NOT NULL,
    PhotographerID INT NOT NULL,
    DateAdded DATETIME NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_ImgPhotog_R_Img
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImgPhotog_R_Photog
        FOREIGN KEY (PhotographerID) REFERENCES PersonalSite.Photographers (ID)
        ON DELETE CASCADE
);


CREATE TABLE PersonalSite.Collections (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(64) CHARACTER SET utf8mb4,
    DateAdded DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.CollectionImages (
    ImageID INT NOT NULL,
    CollectionID INT NOT NULL,
    
    CONSTRAINT FK_CollecImg_R_Img
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images (ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_CollecImg_R_Collec
        FOREIGN KEY (CollectionID) REFERENCES PersonalSite.Collections (ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.PageViews (
    PageName VARCHAR(64) CHARACTER SET utf8mb4,
    Timestamp DATETIME NOT NULL DEFAULT NOW()
);

CREATE INDEX IDX_PageViewName ON PersonalSite.PageViews (PageName);