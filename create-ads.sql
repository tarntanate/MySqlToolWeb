/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */
CREATE SEQUENCE "public"."SEQ_Ad" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdAsset" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroup" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroupStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroupType" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdNetwork" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnit" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnitStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Advertiser" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Campaign" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_Publisher" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_UserRole" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_UserPermission" INCREMENT 1 START 1;
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
    "Quota" INTEGER DEFAULT 0 NOT NULL,
    "StartAt" TIMESTAMP WITH TIME ZONE NOT NULL,
    "EndAt" TIMESTAMP WITH TIME ZONE NOT NULL,
    "CountdownSecond" SMALLINT,
    "ForegroundColor" CHARACTER VARYING(7),
    "BackgroundColor" CHARACTER VARYING(7),
    "Analytics" CHARACTER VARYING(255) ARRAY,
    "Platforms" CHARACTER VARYING(10) ARRAY NOT NULL,
    "AppLink" CHARACTER VARYING(255),
    "WebLink" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_Ad" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_Ad_1" CHECK ("Status" = ANY ((ARRAY ['Wait', 'Approve', 'Preview', 'Publish', 'Unpublish', 'Ended'])::CHARACTER VARYING [])),
    CONSTRAINT "TCC_Ad_2" CHECK ("Platforms" <@ ARRAY ['Android', 'iOS', 'Web']::CHARACTER VARYING [])
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
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdAsset" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_AdAsset_1" CHECK ("AssetType" = ANY (ARRAY ['Audio','Image','Video']::CHARACTER VARYING [])),
    CONSTRAINT "TCC_AdAsset_2" CHECK ("Position" = ANY (ARRAY ['Center','Left','Right','Top','TopLeft','TopRight','Bottom','BottomLeft','BottomRight']::CHARACTER VARYING []))
);
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdStats"                                        	  */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdStats"') NOT NULL,
    "AdId" INTEGER NOT NULL,
    "Quota" INTEGER DEFAULT 0 NOT NULL,
    "Impression" INTEGER DEFAULT 0 NOT NULL,
    "Click" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP WITH TIME ZONE,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdStats_1" ON "public"."AdStats" ("AdId", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroup"                                           */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroup" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroup"') NOT NULL,
    "AdGroupTypeId" INTEGER NOT NULL,
    "PublisherId" INTEGER NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500),
    "Enabled" BOOLEAN DEFAULT TRUE NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdGroup" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroup_1" ON "public"."AdGroup" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdGroup_2" ON "public"."AdGroup" ("DeletedAt", "AdGroupTypeId", "PublisherId");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroupStats"                                      */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroupStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroupStats"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP WITH TIME ZONE,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdGroupStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroupStats_1" ON "public"."AdGroupStats" ("AdGroupId", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroupType"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroupType" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroupType"') NOT NULL,
    "Name" CHARACTER VARYING(40) NOT NULL,
    "Description" CHARACTER VARYING(500) NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdGroupType" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroupType_1" ON "public"."AdGroupType" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdGroupType_2" ON "public"."AdGroupType" ("DeletedAt", "Name");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdNetwork"                                     */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdNetwork" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdNetwork"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "AdNetworkUnitId" CHARACTER VARYING(50) NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdNetwork" PRIMARY KEY ("Id"),
    CONSTRAINT "TCC_AdNetwork_1" CHECK ("Platform" = ANY (ARRAY ['Android', 'iOS', 'Web']::CHARACTER VARYING []))
);
CREATE INDEX "IDX_AdNetwork_1" ON "public"."AdNetwork" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdNetwork_2" ON "public"."AdNetwork" ("DeletedAt", "AdUnitId", "Platform");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnit"                                            */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnit" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnit"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "AdNetwork" CHARACTER VARYING(10) NOT NULL,
    "SortSeq" INTEGER,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdUnit" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnit_1" ON "public"."AdUnit" ("DeletedAt", "Id");
CREATE INDEX "IDX_AdUnit_2" ON "public"."AdUnit" ("DeletedAt", "AdGroupId", "AdNetwork");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnitStats"                                       */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnitStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnitStats"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "Fill" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP WITH TIME ZONE,
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_AdUnitStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnitStats_1" ON "public"."AdUnitStats" ("AdUnitId", "CaculatedAt");
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
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
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
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_Campaign" PRIMARY KEY ("Id")
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
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_Publisher" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_Publisher_1" ON "public"."Publisher" ("DeletedAt", "Id");
CREATE INDEX "IDX_Publisher_2" ON "public"."Publisher" ("DeletedAt", "Name");
/* ---------------------------------------------------------------------- */
/* Add table "public"."User"                                              */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."User" (
    "Id" INTEGER NOT NULL,
    "RoleId" INTEGER NOT NULL,
    "UserName" CHARACTER VARYING(40) NOT NULL,
    "DisplayName" CHARACTER VARYING(40),
    "AvatarUrl" CHARACTER VARYING(255),
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
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
    "CreatedAt" TIMESTAMP WITH TIME ZONE,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE,
    "DeletedAt" TIMESTAMP WITH TIME ZONE,
    CONSTRAINT "PK_UserRole" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_UserRole_1" ON "public"."UserRole" ("DeletedAt", "Id");
CREATE INDEX "IDX_UserRole_2" ON "public"."UserRole" ("DeletedAt", "Name");
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
/* Add foreign key constraints                                            */
/* ---------------------------------------------------------------------- */
ALTER TABLE "public"."Ad"
ADD CONSTRAINT "FK_Ad_Campaign" FOREIGN KEY ("CampaignId") REFERENCES "public"."Campaign" ("Id");

ALTER TABLE "public"."Ad"
ADD CONSTRAINT "FK_Ad_AdUnit" FOREIGN KEY ("AdUnitId") REFERENCES "public"."AdUnit" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdAsset"
ADD CONSTRAINT "FK_AdAsset_Ad" FOREIGN KEY ("AdId") REFERENCES "public"."Ad" ("Id");

ALTER TABLE "public"."AdStats"
ADD CONSTRAINT "FK_AdStats_Ad" FOREIGN KEY ("AdId") REFERENCES "public"."Ad" ("Id");

ALTER TABLE "public"."AdGroup"
ADD CONSTRAINT "FK_AdGroup_Publisher" FOREIGN KEY ("PublisherId") REFERENCES "public"."Publisher" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdGroup"
ADD CONSTRAINT "FK_AdGroup_AdGroupType" FOREIGN KEY ("AdGroupTypeId") REFERENCES "public"."AdGroupType" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdGroupStats"
ADD CONSTRAINT "FK_AdGroupStats_AdGroup" FOREIGN KEY ("AdGroupId") REFERENCES "public"."AdGroup" ("Id");

ALTER TABLE "public"."AdUnit"
ADD CONSTRAINT "FK_AdUnit_AdGroup" FOREIGN KEY ("AdGroupId") REFERENCES "public"."AdGroup" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."AdUnitStats"
ADD CONSTRAINT "FK_AdUnitStats_AdUnit" FOREIGN KEY ("AdUnitId") REFERENCES "public"."AdUnit" ("Id");

ALTER TABLE "public"."AdNetwork"
ADD CONSTRAINT "FK_AdNetwork_AdUnit" FOREIGN KEY ("AdUnitId") REFERENCES "public"."AdUnit" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."Campaign"
ADD CONSTRAINT "FK_Campaign_Advertiser" FOREIGN KEY ("AdvertiserId") REFERENCES "public"."Advertiser" ("Id");

ALTER TABLE "public"."User"
ADD CONSTRAINT "FK_User_UserRole" FOREIGN KEY ("RoleId") REFERENCES "public"."UserRole" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE "public"."UserPermission"
ADD CONSTRAINT "FK_UserPermission_UserRole" FOREIGN KEY ("RoleId") REFERENCES "public"."UserRole" ("Id")
ON DELETE CASCADE ON UPDATE CASCADE;
/* ---------------------------------------------------------------------- */
/* Add data                                                               */
/* ---------------------------------------------------------------------- */	
INSERT INTO public."Publisher" ("Name", "Description", "ImagePath", "CreatedAt", "UpdatedAt", "DeletedAt")
VALUES 		('Joylada', 'Joylada แอปฯ อ่านนิยายแชทรูปแบบใหม่ น้องสาวของเว็บอ่านนิยายธัญวลัย ภายใต้บริษัท Ookbee U ที่เกิดจากการร่วมทุนกันระหว่าง Ookbee และ Tencent', null, '2020-08-13 18:05:54.834332+07', null, null),
			('Tunwalai', 'ธัญวลัย', null, '2020-08-13 18:06:10.463594+07', null, null);
	
INSERT INTO public."AdGroupType" ("Name","Description","CreatedAt","UpdatedAt","DeletedAt")
VALUES		('tab','tabchat','2020-08-13 18:06:30.033549+07',NULL,NULL),
	 		('mRec','medium rectangle banner','2020-08-13 18:06:39.192178+07',NULL,NULL);
		
INSERT INTO public."AdGroup" ("AdGroupTypeId","PublisherId","Name","Description","Enabled","CreatedAt","UpdatedAt","DeletedAt")
VALUES 		(1,1,'Tab Chat',NULL,true,'2020-08-13 18:07:51.383189+07',NULL,NULL),
	 		(2,1,'Bottom Novel',NULL,true,'2020-08-31 15:34:07.978959+07',NULL,NULL),
	 		(2,1,'Recommend',NULL,true,'2020-09-17 11:10:18.095465+07',NULL,NULL),
	 		(2,1,'Bottom Chat',NULL,true,'2020-08-31 15:33:51.37531+07',NULL,NULL),
	 		(2,1,'Top Bubble Ad (Chat)',NULL,true,'2020-08-17 12:12:13.104025+07',NULL,NULL),
	 		(2,1,'Chapter List',NULL,true,'2020-08-31 15:33:14.314188+07',NULL,NULL);
	 	
INSERT INTO public."AdUnit" ("AdGroupId","AdNetwork","SortSeq","CreatedAt","UpdatedAt","DeletedAt")
VALUES 		(1,'ookbee',1,'2020-08-26 14:31:39.071804+07',NULL,NULL),
	 		(2,'admob',2,'2020-09-17 15:13:08.802629+07',NULL,NULL),
	 		(2,'ookbee',1,'2020-08-27 07:40:10.155673+07',NULL,NULL),
	 		(4,'admob',2,'2020-09-17 15:17:44.699423+07',NULL,NULL),
	 		(4,'ookbee',1,'2020-09-17 15:16:25.477228+07',NULL,NULL),
	 		(5,'admob',2,'2020-09-17 15:19:20.275636+07',NULL,NULL),
	 		(5,'ookbee',1,'2020-09-17 15:18:55.358688+07',NULL,NULL),
	 		(6,'admob',2,'2020-09-17 15:22:44.309285+07',NULL,NULL),
	 		(6,'ookbee',1,'2020-10-26 16:59:56.865267+07',NULL,NULL);

INSERT INTO public."AdNetwork" ("AdUnitId","AdNetworkUnitId","Platform","CreatedAt","UpdatedAt","DeletedAt")
VALUES 		(1,'ca-app-pub-8034539772302467/5199748887','Android','2020-08-26 14:31:39+07',NULL,NULL),
	 		(1,'ca-app-pub-8034539772302467/8927327516','iOS','2020-09-21 15:17:02.929298+07',NULL,NULL),
	 		(2,'ca-app-pub-8034539772302467/9585803061','iOS','2020-09-17 15:15:18+07',NULL,NULL),
	 		(2,'ca-app-pub-8034539772302467/1071257915','Android','2020-09-17 15:15:18+07',NULL,NULL),
	 		(3,'ca-app-pub-8034539772302467/4837567941','Android','2020-09-17 15:17:44+07',NULL,NULL),
	 		(3,'ca-app-pub-8034539772302467/4221795741','iOS','2020-09-17 15:17:44+07',NULL,NULL),
	 		(4,'ca-app-pub-8034539772302467/9665694116','iOS','2020-09-17 15:19:20+07',NULL,NULL),
	 		(4,'ca-app-pub-8034539772302467/9825389602','Android','2020-09-17 15:19:20+07',NULL,NULL),
	 		(5,'ca-app-pub-8034539772302467/6286380981','Android','2020-09-17 15:22:44+07',NULL,NULL),
	 		(5,'ca-app-pub-8034539772302467/8959240810','iOS','2020-09-17 15:22:44+07',NULL,NULL);

INSERT INTO public."UserRole" ("Name","Description","CreatedAt","UpdatedAt","DeletedAt") 
VALUES 		('Administrator','Admin can Create, Update, Delete Ads','2020-09-01 12:54:32.736022+07','2020-10-01 17:48:57.842622+07',NULL);
       
INSERT INTO public."UserPermission" ("RoleId","ExtensionName","IsCreate","IsRead","IsUpdate","IsDelete")
VALUES 		(1,'Preview',false,false,false,false);

	
	
	