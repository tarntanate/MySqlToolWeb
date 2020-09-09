/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */
CREATE SEQUENCE "public"."SEQ_ActivityLog" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Advertiser" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Ad" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdAsset" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroup" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnit" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnitType" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Campaign" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Publisher" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_UserPermission" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_UserRole" INCREMENT 1 START 1;
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroup"                                           */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroup" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroup"') NOT NULL,
    "AdUnitTypeId" INTEGER NOT NULL,
    "PublisherId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdGroup" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroup_1" ON "public"."AdGroup" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdGroup_2" ON "public"."AdGroup" ("DeletedAt", "AdUnitTypeId", "PublisherId");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnitType"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnitType" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnitType"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NULL,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnitType" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnitType_1" ON "public"."AdUnitType" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdUnitType_2" ON "public"."AdUnitType" ("DeletedAt", "Name");
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
CREATE INDEX "IDX_Advertiser_1" ON "public"."Advertiser" ("DeletedAt", "Id");
CREATE INDEX "IDX_Advertiser_2" ON "public"."Advertiser" ("DeletedAt", "Name");
/* ---------------------------------------------------------------------- */
/* Add table "public"."Campaign"                                          */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."Campaign" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Campaign"') NOT NULL,
    "AdvertiserId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40),
    "Description" CHARACTER VARYING(500),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Campaign" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Campaign_1" CHECK (
        "PricingModel" = ANY (
            (ARRAY ['IMP', 'CPC', 'CPM'])::CHARACTER VARYING []
        )
    )
);
CREATE INDEX "IDX_Campaign_1" ON "public"."Campaign" ("DeletedAt", "AdvertiserId");
/* ---------------------------------------------------------------------- */
/* Add table "public"."Publisher"                                         */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."Publisher" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Publisher"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NULL,
    "ImagePath" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Publisher" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_Publisher_1" ON "public"."Publisher" ("DeletedAt", "Id");
CREATE INDEX "IDX_Publisher_2" ON "public"."Publisher" ("DeletedAt", "Name");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnit"                                            */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnit" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnit"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "AdNetwork" CHARACTER VARYING(10) NOT NULL,
    "AdNetworkUnitId" CHARACTER VARYING(50) NULL,
    "SortSeq" INTEGER,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnit" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnit_1" ON "public"."AdUnit" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdUnit_2" ON "public"."AdUnit" ("DeletedAt", "AdGroupId", "AdNetwork");
/* ---------------------------------------------------------------------- */
/* Add table "public"."User"                                              */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."User" (
    "Id" INTEGER NOT NULL,
    "UserName" CHARACTER VARYING(40) NOT NULL,
    "DisplayName" CHARACTER VARYING(40),
    "AvatarUrl" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_User_1" ON "public"."Publisher" ("DeletedAt", "Id");
/* ---------------------------------------------------------------------- */
/* Add table "public"."UserRole"                                          */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."UserRole" (
    "Id" INTEGER DEFAULT nextval('"SEQ_UserRole"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NULL,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_UserRole" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_UserRole_1" ON "public"."UserRole" ("DeletedAt", "Id");
CREATE INDEX "IDX_UserRole_2" ON "public"."UserRole" ("DeletedAt", "Name");
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
    "ObjectData" JSONB NOT NULL,
    "CreatedAt" TIMESTAMP,
    CONSTRAINT "PK_ActivityLog" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Activity_1" CHECK (
        "Activity" = ANY (
            (ARRAY ['Create', 'Read', 'Update', 'Delete'])::CHARACTER VARYING []
        )
    )
);
CREATE INDEX "IDX_ActivityLog_1" ON "public"."ActivityLog" ("ObjectType", "ObjectId");
CREATE INDEX "IDX_ActivityLog_2" ON "public"."ActivityLog" ("ObjectType", "UserId");
/* ---------------------------------------------------------------------- */
/* Add table "public"."UserPermission"                                    */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."UserPermission" (
    "Id" INTEGER DEFAULT nextval('"SEQ_UserPermission"') NOT NULL,
    "RoleId" INTEGER NOT NULL,
    "ExtensionName" CHARACTER VARYING(40) NOT NULL,
    "IsCreate" BOOLEAN DEFAULT FALSE NOT NULL,
    "IsRead" BOOLEAN DEFAULT FALSE NOT NULL,
    "IsUpdate" BOOLEAN DEFAULT FALSE NOT NULL,
    "IsDelete" BOOLEAN DEFAULT FALSE NOT NULL,
    CONSTRAINT "PK_UserPermission" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_UserPermission_1" ON "public"."UserPermission" ("RoleId", "Id");
/* ---------------------------------------------------------------------- */
/* Add table "public"."Ad"                                                */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."Ad" (
    "Id" INTEGER DEFAULT nextval('"SEQ_Ad"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "CampaignId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NULL,
    "Status" CHARACTER VARYING(10),
    "Quota" INTEGER,
    "StartAt" TIMESTAMP,
    "EndAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "CountdownSecond" SMALLINT,
    "ForegroundColor" CHARACTER VARYING(7),
    "BackgroundColor" CHARACTER VARYING(7),
    "Analytics" CHARACTER VARYING(255) ARRAY,
    "Platforms" CHARACTER VARYING(10) ARRAY NOT NULL,
    "AppLink" CHARACTER VARYING(255),
    "WebLink" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    "DeletedAt" TIMESTAMP,
    CONSTRAINT "PK_Ad" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Ad_1" CHECK (
        "Status" = ANY (
            (
                ARRAY ['Wait', 'Approve', 'Preview', 'Publish', 'Unpublish']
            )::CHARACTER VARYING []
        )
    ),
    CONSTRAINT "TCC_Ad_2" CHECK (
        "Platforms" <@ ARRAY ['Android', 'iOS', 'Web']::CHARACTER VARYING []
    )
);
CREATE INDEX "IDX_Ad_1" ON "public"."Ad" ("DeletedAt", "Id");
CREATE INDEX "IDX_Ad_2" ON "public"."Ad" ("DeletedAt", "AdUnitId", "CampaignId", "Status");
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
    CONSTRAINT "TCC_AdAsset_1" CHECK (
        "AssetType" = ANY (
            ARRAY ['Audio','Image','Video']::CHARACTER VARYING []
        )
    ),
    CONSTRAINT "TCC_AdAsset_2" CHECK (
        "Position" = ANY (
            ARRAY ['Center','Left','Right','Top','TopLeft','TopRight','Bottom','BottomLeft','BottomRight']::CHARACTER VARYING []
        )
    )
);
CREATE INDEX "IDX_AdAsset_1" ON "public"."AdAsset" ("DeletedAt", "AdId", "Position");
/* ---------------------------------------------------------------------- */
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */
ALTER TABLE "public"."ActivityLog"
ADD CONSTRAINT "FK_ActivityLog_User" FOREIGN KEY ("UserId") REFERENCES "public"."User" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Ad"
ADD CONSTRAINT "FK_Ad_Campaign" FOREIGN KEY ("CampaignId") REFERENCES "public"."Campaign" ("Id");
ALTER TABLE "public"."Ad"
ADD CONSTRAINT "FK_Ad_AdUnit" FOREIGN KEY ("AdUnitId") REFERENCES "public"."AdUnit" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."AdAsset"
ADD CONSTRAINT "FK_AdAsset_Ad" FOREIGN KEY ("AdId") REFERENCES "public"."Ad" ("Id");
ALTER TABLE "public"."AdGroup"
ADD CONSTRAINT "FK_AdGroup_Publisher" FOREIGN KEY ("PublisherId") REFERENCES "public"."Publisher" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."AdGroup"
ADD CONSTRAINT "FK_AdGroup_AdUnitType" FOREIGN KEY ("AdUnitTypeId") REFERENCES "public"."AdUnitType" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."AdUnit"
ADD CONSTRAINT "FK_AdUnit_AdGroup" FOREIGN KEY ("AdGroupId") REFERENCES "public"."AdGroup" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Campaign"
ADD CONSTRAINT "FK_Campaign_Advertiser" FOREIGN KEY ("AdvertiserId") REFERENCES "public"."Advertiser" ("Id");
ALTER TABLE "public"."UserPermission"
ADD CONSTRAINT "FK_UserPermission_UserRole" FOREIGN KEY ("Id") REFERENCES "public"."UserRole" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."UserRoleMapping"
ADD CONSTRAINT "FK_UserRoleMapping_User" FOREIGN KEY ("UserId") REFERENCES "public"."User" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."UserRoleMapping"
ADD CONSTRAINT "FK_UserRoleMapping_UserRole" FOREIGN KEY ("RoleId") REFERENCES "public"."UserRole" ("Id") ON DELETE CASCADE ON UPDATE CASCADE;
/* ---------------------------------------------------------------------- */
/* Add data                                                               */
/* ---------------------------------------------------------------------- */
INSERT INTO public."User" (
        "Id",
        "UserName",
        "DisplayName",
        "AvatarUrl",
        "CreatedAt",
        "UpdatedAt",
        "DeletedAt"
    )
VALUES (
        6383511,
        'nat@ookbee.com',
        'แมวเซง',
        'https://albertpotjes.files.wordpress.com/2014/07/10492317_649860621770493_5460005525881717072_n.jpg                                                                                                                                                            ',
        '2020-08-13 10:34:31.740',
        NULL,
        NULL
    );