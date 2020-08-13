/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */

CREATE SEQUENCE "public"."SEQ_ActivityLog" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_Advertiser" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_Ad" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_AdAsset" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_AdUnit" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_AdUnitType" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_Campaign" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_CampaignCost" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_CampaignImpression" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_Publisher" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_AdGroup" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_AdGroupItem" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_UserPermission" INCREMENT 1 MINVALUE 0 START 0;

CREATE SEQUENCE "public"."SEQ_UserRole" INCREMENT 1 MINVALUE 0 START 0;

/* ---------------------------------------------------------------------- */
/* Add tables                                                             */
/* ---------------------------------------------------------------------- */

/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroup"                                           */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."AdGroup" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroup"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdGroup" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_AdGroup_1" ON "public"."AdGroup" ("DeletedAt","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroupItem"                                       */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."AdGroupItem" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroupItem"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "AdUnitKey" CHARACTER VARYING(40) NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "SortSeq" INTEGER,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdGroupItem" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_AdGroupItem_1" ON "public"."AdGroupItem" ("DeletedAt","AdGroupId","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnitType"                                        */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."AdUnitType" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnitType"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnitType" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_AdUnitType_1" ON "public"."AdUnitType" ("DeletedAt","Id");

CREATE INDEX "IDX_AdUnitType_2" ON "public"."AdUnitType" ("DeletedAt","Name");

/* ---------------------------------------------------------------------- */
/* Add table "public"."Advertiser"                                        */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."Advertiser" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Advertiser"') NOT NULL,
    "Name" CHARACTER VARYING(40),
    "Description" CHARACTER VARYING(500),
    "ImagePath" CHARACTER VARYING(255),
    "Contact" CHARACTER VARYING(5000),
    "Email" CHARACTER VARYING(250),
    "PhoneNumber" CHARACTER VARYING(10),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Advertiser" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_Advertiser_1" ON "public"."Advertiser" ("DeletedAt","Id");

CREATE INDEX "IDX_Advertiser_2" ON "public"."Advertiser" ("DeletedAt","Name");

/* ---------------------------------------------------------------------- */
/* Add table "public"."Campaign"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."Campaign" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Campaign"') NOT NULL,
    "AdvertiserId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40),
    "Description" CHARACTER VARYING(500),
    "PricingModel" CHARACTER VARYING(40),
    "StartDate" TIMESTAMP NOT NULL,
    "EndDate" TIMESTAMP NOT NULL,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Campaign" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Campaign_1" CHECK ("PricingModel" = ANY ((ARRAY['IMP', 'CPC', 'CPM'])::CHARACTER VARYING[]))
);

CREATE INDEX "IDX_Campaign_1" ON "public"."Campaign" ("DeletedAt", "AdvertiserId");

/* ---------------------------------------------------------------------- */
/* Add table "public"."CampaignCost"                                      */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."CampaignCost" (
    "Id" INTEGER DEFAULT nextval('"SEQ_CampaignCost"') NOT NULL,
    "CampaignId" INTEGER NOT NULL,
    "Budget" MONEY,
    "CostPerUnit" MONEY,
    CONSTRAINT "PK_CampaignCost" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_CampaignCost_1" ON "public"."CampaignCost" ("CampaignId","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."CampaignImpression"                                */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."CampaignImpression" (
    "Id" INTEGER DEFAULT nextval('"SEQ_CampaignImpression"') NOT NULL,
    "CampaignId" INTEGER NOT NULL,
    "Quota" INTEGER,
    CONSTRAINT "PK_CampaignImpression" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_CampaignImpression_1" ON "public"."CampaignImpression" ("CampaignId","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."Publisher"                                         */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."Publisher" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Publisher"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "ImagePath" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Publisher" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_Publisher_1" ON "public"."Publisher" ("DeletedAt","Id");

CREATE INDEX "IDX_Publisher_2" ON "public"."Publisher" ("DeletedAt","Name");

/* ---------------------------------------------------------------------- */
/* Add table "public"."User"                                              */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."User" (
    "Id" INTEGER NOT NULL,
    "User" CHARACTER VARYING(40) NOT NULL,
    "DisplayName" CHARACTER(40),
    "AvatarUrl" CHARACTER(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_User_1" ON "public"."Publisher" ("DeletedAt","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."UserRole"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."UserRole" (
    "Id" INTEGER DEFAULT nextval('"SEQ_UserRole"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_UserRole" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_UserRole_1" ON "public"."UserRole" ("DeletedAt","Id");

CREATE INDEX "IDX_UserRole_2" ON "public"."UserRole" ("DeletedAt","Name");

/* ---------------------------------------------------------------------- */
/* Add table "public"."UserRoleMapping"                                   */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."UserRoleMapping" (
    "UserId" INTEGER NOT NULL,
    "RoleId" INTEGER NOT NULL,
    CONSTRAINT "PK_UserRoleMapping" PRIMARY KEY ("UserId", "RoleId")
);

/* ---------------------------------------------------------------------- */
/* Add table "public"."ActivityLog"                                       */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."ActivityLog" (
    "Id" INTEGER DEFAULT nextval('"SEQ_ActivityLog"') NOT NULL,
    "UserId" INTEGER NOT NULL,
    "Activity" CHARACTER VARYING(10) NOT NULL,
    "ObjectType" CHARACTER VARYING(40),
    "ObjectId" CHARACTER VARYING(40) NOT NULL,
    "ObjectData" CHARACTER,
    "CreatedAt" TIMESTAMP,
    CONSTRAINT "PK_ActivityLog" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Activity_1" CHECK ("Activity" = ANY ((ARRAY['Create', 'Read', 'Update', 'Delete'])::CHARACTER VARYING[]))
);

CREATE INDEX "IDX_ActivityLog_1" ON "public"."ActivityLog" ("ObjectType","ObjectId");

CREATE INDEX "IDX_ActivityLog_2" ON "public"."ActivityLog" ("ObjectType","UserId");

/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnit"                                            */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."AdUnit" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnit"') NOT NULL,
    "AdUnitTypeId" INTEGER NOT NULL,
    "PublisherId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "AdNetworks" CHARACTER VARYING(10) ARRAY,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnit" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_AdUnit_1" CHECK ("AdNetworks" <@ ARRAY['AdMob', 'Appodeal']::CHARACTER VARYING[])
);

CREATE INDEX "IDX_AdUnit_1" ON "public"."AdUnit" ("DeletedAt","Id");

CREATE INDEX "IDX_AdUnit_2" ON "public"."AdUnit" ("DeletedAt","AdUnitTypeId","PublisherId","Name");

/* ---------------------------------------------------------------------- */
/* Add table "public"."UserPermission"                                    */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."UserPermission" (
    "Id" INTEGER DEFAULT nextval('"SEQ_UserPermission"') NOT NULL,
    "RoleId" INTEGER NOT NULL,
    "ExtensionName" CHARACTER VARYING(40) NOT NULL,
    "IsCreate" BOOLEAN DEFAULT false NOT NULL,
    "IsRead" BOOLEAN DEFAULT false NOT NULL,
    "IsUpdate" BOOLEAN DEFAULT false NOT NULL,
    "IsDelete" BOOLEAN DEFAULT false NOT NULL,
    CONSTRAINT "PK_UserPermission" PRIMARY KEY ("Id")
);

CREATE INDEX "IDX_UserPermission_1" ON "public"."UserPermission" ("RoleId","Id");

/* ---------------------------------------------------------------------- */
/* Add table "public"."Ad"                                                */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."Ad" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Ad"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "CampaignId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NOT NULL,
    "Status" CHARACTER VARYING(10),
    "CountdownSecond" SMALLINT,
    "ForegroundColor" CHARACTER VARYING(7),
    "BackgroundColor" CHARACTER VARYING(7),
    "Analytics" CHARACTER VARYING(255) ARRAY,
    "Platforms" CHARACTER VARYING(10) ARRAY,
    "AppLink" CHARACTER VARYING(255),
    "WebLink" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Ad" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Ad_1" CHECK ("Status" = ANY ((ARRAY['Wait', 'Approve', 'Preview', 'Publish', 'Unpublish'])::CHARACTER VARYING[])),
    CONSTRAINT "TCC_Ad_2" CHECK ("Platforms" <@ ARRAY['Android', 'iOS', 'Web']::CHARACTER VARYING[])
);

CREATE INDEX "IDX_Ad_1" ON "public"."Ad" ("DeletedAt","Id");

CREATE INDEX "IDX_Ad_2" ON "public"."Ad" ("DeletedAt","AdUnitId","CampaignId","Status");

/* ---------------------------------------------------------------------- */
/* Add table "public"."AdAsset"                                           */
/* ---------------------------------------------------------------------- */

CREATE TABLE "public"."AdAsset" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdAsset"') NOT NULL,
    "AdId" INTEGER NOT NULL,
    "AssetPath" CHARACTER VARYING(255),
    "AssetType" CHARACTER VARYING(10),
    "Position" CHARACTER VARYING(15),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdAsset" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_AdAsset_1" CHECK ("AssetType" = ANY (ARRAY['Audio','Image','Video']::CHARACTER VARYING[])),
    CONSTRAINT "TCC_AdAsset_2" CHECK ("Position" = ANY (ARRAY['Center','Left','Right','Top','TopLeft','TopRight','Bottom','BottomLeft','BottomRight']::CHARACTER VARYING[]))
);

CREATE INDEX "IDX_AdAsset_1" ON "public"."AdAsset" ("DeletedAt","AdId","Position");

/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */

ALTER TABLE "public"."ActivityLog" ADD CONSTRAINT "FK_ActivityLog_User"
    FOREIGN KEY ("UserId") REFERENCES "public"."User" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."Ad" ADD CONSTRAINT "FK_Campaign_Ad"
    FOREIGN KEY ("CampaignId") REFERENCES "public"."Campaign" ("Id");

ALTER TABLE "public"."Ad" ADD CONSTRAINT "FK_AdUnit_Ad" 
    FOREIGN KEY ("AdUnitId") REFERENCES "public"."AdUnit" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdAsset" ADD CONSTRAINT "FK_Ad_AdAsset" 
    FOREIGN KEY ("AdId") REFERENCES "public"."Ad" ("Id");

ALTER TABLE "public"."AdGroupItem" ADD CONSTRAINT "FK_AdGroupItem_AdGroup" 
    FOREIGN KEY ("AdGroupId") REFERENCES "public"."AdGroup" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdUnit" ADD CONSTRAINT "FK_Publisher_AdUnit" 
    FOREIGN KEY ("PublisherId") REFERENCES "public"."Publisher" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdUnit" ADD CONSTRAINT "FK_AdUnitType_AdUnit"
    FOREIGN KEY ("AdUnitTypeId") REFERENCES "public"."AdUnitType" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."Campaign" ADD CONSTRAINT "FK_Advertiser_Campaign" 
    FOREIGN KEY ("AdvertiserId") REFERENCES "public"."Advertiser" ("Id");

ALTER TABLE "public"."CampaignCost" ADD CONSTRAINT "FK_Campaign_CampaignCost"
    FOREIGN KEY ("CampaignId") REFERENCES "public"."Campaign" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."CampaignImpression" ADD CONSTRAINT "FK_Campaign_CampaignImpression" 
    FOREIGN KEY ("CampaignId") REFERENCES "public"."Campaign" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."UserPermission" ADD CONSTRAINT "FK_UserRole_UserPermission"
    FOREIGN KEY ("Id") REFERENCES "public"."UserRole" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."UserRoleMapping" ADD CONSTRAINT "FK_UserRoleMapping_User" 
    FOREIGN KEY ("UserId") REFERENCES "public"."User" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."UserRoleMapping" ADD CONSTRAINT "FK_UserRoleMapping_UserRole" 
    FOREIGN KEY ("RoleId") REFERENCES "public"."UserRole" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
