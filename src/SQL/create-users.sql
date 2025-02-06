-- User for the front end to view data in the database
CREATE USER 'PersonalSiteViewer'@'%' IDENTIFIED BY '<password>';
GRANT SELECT ON PersonalSite.* TO PersonalSiteViewer;
-- This user will need to update the ViewCount column on the Images table
GRANT UPDATE (ViewCount) ON PersonalSite.Images TO PersonalSiteViewer;

-- User for the management portal, which needs to be granted more
-- powerful permissions but should not have DDL permissions nor
-- the ability to do large damage such as TRUNCATE
CREATE USER 'PersonalSiteManager'@'%' IDENTIFIED BY '<password>';
GRANT INSERT, UPDATE, DELETE, SELECT ON PersonalSite.* TO PersonalSiteViewer;

-- A third user could be defined here that has admin privileges
-- for things like DDL, but I have no need for this as I can just
-- log on to my database as root.