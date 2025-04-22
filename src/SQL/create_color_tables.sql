CREATE TABLE PersonalSite.Colors (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    Red TINYINT UNSIGNED NOT NULL,
    Green TINYINT UNSIGNED NOT NULL,
    Blue TINYINT UNSIGNED NOT NULL,

    Timestamp DATETIME NOT NULL DEFAULT NOW(),
    
    CONSTRAINT C_ColorsUnique UNIQUE (Red, Green, Blue)
);

CREATE INDEX IDX_PaletteColorsSpacial ON PersonalSite.Colors (Red, Green, Blue);