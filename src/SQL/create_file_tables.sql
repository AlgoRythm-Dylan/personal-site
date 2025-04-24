CREATE TABLE PersonalSite.Files (
    ID VARCHAR(32) PRIMARY KEY,
    Name VARCHAR(64) COLLATE utf8mb4_bin,
    Description VARCHAR(256) COLLATE utf8mb4_bin,
    SystemFileName VARCHAR(64) NOT NULL,
    OrginalFileName VARCHAR(64),

    Timestamp DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET = utf8mb4;