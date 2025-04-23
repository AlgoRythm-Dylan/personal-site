GRANT SELECT ON PersonalSite.* TO PersonalSiteViewer;
GRANT INSERT ON PersonalSite.ImageViews TO PersonalSiteViewer;
GRANT INSERT ON PersonalSite.PageViews TO PersonalSiteViewer;

GRANT INSERT, UPDATE, DELETE, SELECT ON PersonalSite.* TO PersonalSiteManager;