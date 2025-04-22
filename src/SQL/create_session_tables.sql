CREATE TABLE PersonalSite.RefreshTokens (
    Token PRIMARY KEY VARCHAR(64) NOT NULL,
    AccountID INT NOT NULL,
    Expiry DATETIME NOT NULL,

    CONSTRAINT FK_RefreshTokens_R_Accounts FOREIGN KEY (AccountID)
        REFERENCES PersonalSite.Accounts(ID)
        ON DELETE CASCADE
);