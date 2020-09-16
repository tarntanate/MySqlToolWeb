/* ---------------------------------------------------------------------- */
/* Add sequences                                                          */
/* ---------------------------------------------------------------------- */
CREATE SEQUENCE "public"."SEQ_AdStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdGroupStats" INCREMENT 1 START 1;
CREATE SEQUENCE "public"."SEQ_AdUnitStats" INCREMENT 1 START 1;
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
/* Scripts                                       	  */
/* ---------------------------------------------------------------------- */
/*
 
DELETE FROM public."AdGroupStats";
DELETE FROM public."AdUnitStats";
DELETE FROM public."AdStats";

SELECT * FROM public."AdGroupStats";
SELECT * FROM public."AdUnitStats";
SELECT * FROM public."AdStats";

*/









