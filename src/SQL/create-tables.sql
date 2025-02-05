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
    Width INT,
    Height INT,
    Brightness INT,
    CaptureDate DATETIME DEFAULT NOW(),
    
    UploadDate DATETIME DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.ImageColors (
    ImageID INT NOT NULL,
    PaletteColorID INT NOT NULL,

    CONSTRAINT FK_ImageColors_R_Images
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImageColors_R_Colors FOREIGN KEY (PaletteColorID)
        REFERENCES PersonalSite.PaletteColors(ID)
        ON DELETE CASCADE
);

CREATE TABLE PersonalSite.Subjects (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    ParentID INT DEFAULT NULL,
    Name VARCHAR(128) CHARACTER SET utf8mb4,

    CreatedDate DATETIME DEFAULT NOW(),
    DeletedDate DATETIME DEFAULT NULL,

    CONSTRAINT FK_Subjects_R_Parent
        FOREIGN KEY (ParentID) REFERENCES PersonalSite.Subjects(ID)
        ON DELETE CASCADE
) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.ImageSubjects (
    ImageID INT NOT NULL,
    SubjectID INT NOT NULL,
    QuantityBucket INT NOT NULL,
    IsPrimarySubject TINYINT NOT NULL,

    CONSTRAINT FK_ImgSubj_R_Image
        FOREIGN KEY (ImageID) REFERENCES PersonalSite.Images(ID)
        ON DELETE CASCADE,
    CONSTRAINT FK_ImgSubj_R_Subjects
        FOREIGN KEY (SubjectID) REFERENCES PersonalSite.Subjects(ID)
        ON DELETE CASCADE
);