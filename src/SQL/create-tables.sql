CREATE TABLE PersonalSite.PaletteColors (
	ID INT PRIMARY KEY AUTO_INCREMENT,
	Red TINYINT UNSIGNED NOT NULL,
	Green TINYINT UNSIGNED NOT NULL,
	Blue TINYINT UNSIGNED NOT NULL,
    
    CONSTRAINT PaletteColorsUnique UNIQUE (Red, Green, Blue)
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
    DeleteDate DATETIME DEFAULT NULL
) CHARACTER SET utf8mb4;