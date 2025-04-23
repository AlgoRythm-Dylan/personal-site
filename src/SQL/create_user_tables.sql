CREATE TABLE PersonalSite.Accounts (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    DisplayName VARCHAR(64) COLLATE utf8mb4_bin,
    Username VARCHAR(64) NOT NULL COLLATE utf8mb4_bin,
    PasswordHash VARCHAR(64) NOT NULL
) CHARACTER SET = utf8mb4;