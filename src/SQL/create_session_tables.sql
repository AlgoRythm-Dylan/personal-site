CREATE TABLE PersonalSite.RefreshTokens (
    Token VARCHAR(64) PRIMARY KEY,
    AccountID INT NOT NULL,
    Expiry DATETIME NOT NULL,

    CONSTRAINT FK_RefreshTokens_R_Accounts FOREIGN KEY (AccountID)
        REFERENCES PersonalSite.Accounts(ID)
        ON DELETE CASCADE
);