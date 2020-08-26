/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */
CREATE SEQUENCE "public"."SEQ_AdStat" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroupStat" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnitStat" INCREMENT 1 START 1;
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdStat"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdStat" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdStat"') NOT NULL,
    "AdId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Impression" INTEGER DEFAULT 0 NOT NULL,
    "Click" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdStat" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdStat_1" ON "public"."AdStat" ("AdId", "Platform", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdGroupStat"                                       */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdGroupStat" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdGroupStat"') NOT NULL,
    "AdGroupId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdGroupStat" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdGroupStat_1" ON "public"."AdGroupStat" ("AdGroupId", "Platform", "CaculatedAt");
/* ---------------------------------------------------------------------- */
/* Add table "public"."AdUnitStat"                                        */
/* ---------------------------------------------------------------------- */
CREATE TABLE "public"."AdUnitStat" (
    "Id" INTEGER DEFAULT nextval('"SEQ_AdUnitStat"') NOT NULL,
    "AdUnitId" INTEGER NOT NULL,
    "Platform" CHARACTER VARYING(10) NOT NULL,
    "Request" INTEGER DEFAULT 0 NOT NULL,
    "Fill" INTEGER DEFAULT 0 NOT NULL,
    "CaculatedAt" TIMESTAMP,
    "CreatedAt" TIMESTAMP,
    "UpdatedAt" TIMESTAMP,
    CONSTRAINT "PK_AdUnitStat" PRIMARY KEY ("Id")
);
CREATE INDEX "IDX_AdUnitStat_1" ON "public"."AdUnitStat" ("AdUnitId", "Platform", "CaculatedAt");
