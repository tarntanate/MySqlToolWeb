/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */
CREATE SEQUENCE "public"."SEQ_AdStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroupStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnitStats" INCREMENT 1 START 1;
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroupStats"                                       */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroupStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroupStats"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdGroupStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroupStats_1" ON "public"."AdGroupStats" ("AdGroupId", "Platform", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnitStats"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnitStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnitStats"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "Fill" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnitStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnitStats_1" ON "public"."AdUnitStats" ("AdUnitId", "Platform", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdStats"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdAssetStats" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdStats"') NOT NULL,
    "AdId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Impression" INTEGER DEFAULT 0 NOT NULL,
    "Click" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdStats" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdStats_1" ON "public"."AdStats" ("AdId", "Platform", "CaculatedAt");