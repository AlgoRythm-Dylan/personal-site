CREATE TABLE PersonalSite.LoginAudit (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    WasSuccess TINYINT NOT NULL,
    Username VARCHAR(256) CHARACTER SET utf8mb4,
    AccountID INT DEFAULT NULL,
    IpAddress VARCHAR(64),
    Timestamp DATE NOT NULL,

    CONSTRAINT FK_LoginAudit_R_Accounts
        FOREIGN KEY (AccountID) REFERENCES PersonalSite.Accounts(ID)

) CHARACTER SET utf8mb4;

CREATE TABLE PersonalSite.PageViews (
    PageName VARCHAR(64) CHARACTER SET utf8mb4,
    Timestamp DATETIME NOT NULL DEFAULT NOW()
);

CREATE INDEX IDX_PageViewName ON PersonalSite.PageViews (PageName);