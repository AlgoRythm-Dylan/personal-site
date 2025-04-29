CREATE TABLE PersonalSite.LoginAudit (
    ID INT PRIMARY KEY AUTO_INCREMENT,
    WasSuccess TINYINT NOT NULL,
    Username VARCHAR(256) COLLATE utf8mb4_bin,
    AccountID INT DEFAULT NULL,
    IpAddress VARCHAR(64),
    Timestamp DATE NOT NULL,

    CONSTRAINT FK_LoginAudit_R_Accounts
        FOREIGN KEY (AccountID) REFERENCES PersonalSite.Accounts(ID)

) CHARACTER SET = utf8mb4;

CREATE TABLE PersonalSite.PageViews (
    PageViewID INT PRIMARY KEY AUTO_INCREMENT, -- To make EF happy
    PageName VARCHAR(64) COLLATE utf8mb4_bin,
    Timestamp DATETIME NOT NULL DEFAULT NOW()
) CHARACTER SET = utf8mb4;

CREATE INDEX IDX_PageViewName ON PersonalSite.PageViews (PageName);