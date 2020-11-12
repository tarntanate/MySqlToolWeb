/* ---------------------------------------------------------------------- */
/* Drop table                                                      */
/* ---------------------------------------------------------------------- */
DROP TABLE IF EXISTS public."Ad" CASCADE;
DROP TABLE IF EXISTS public."AdAsset" CASCADE;
DROP TABLE IF EXISTS public."AdGroup" CASCADE;
DROP TABLE IF EXISTS public."AdGroupStats" CASCADE;
DROP TABLE IF EXISTS public."AdGroupType" CASCADE;
DROP TABLE IF EXISTS public."AdNetwork" CASCADE;
DROP TABLE IF EXISTS public."AdStats" CASCADE;
DROP TABLE IF EXISTS public."AdUnit" CASCADE;
DROP TABLE IF EXISTS public."AdUnitStats" CASCADE;
DROP TABLE IF EXISTS public."Advertiser" CASCADE;
DROP TABLE IF EXISTS public."Campaign" CASCADE;
DROP TABLE IF EXISTS public."Publisher" CASCADE;
DROP TABLE IF EXISTS public."User" CASCADE;
DROP TABLE IF EXISTS public."UserPermission" CASCADE;
DROP TABLE IF EXISTS public."UserRole" CASCADE;
/* ---------------------------------------------------------------------- */
/* Delete sequences                                                       */
/* ---------------------------------------------------------------------- */
DROP SEQUENCE IF EXISTS public."SEQ_Ad";
DROP SEQUENCE IF EXISTS public."SEQ_AdAsset";
DROP SEQUENCE IF EXISTS public."SEQ_AdGroup";
DROP SEQUENCE IF EXISTS public."SEQ_AdGroupStats";
DROP SEQUENCE IF EXISTS public."SEQ_AdGroupType";
DROP SEQUENCE IF EXISTS public."SEQ_AdNetwork";
DROP SEQUENCE IF EXISTS public."SEQ_AdStats";
DROP SEQUENCE IF EXISTS public."SEQ_AdUnit";
DROP SEQUENCE IF EXISTS public."SEQ_AdUnitStats";
DROP SEQUENCE IF EXISTS public."SEQ_Advertiser";
DROP SEQUENCE IF EXISTS public."SEQ_Campaign";
DROP SEQUENCE IF EXISTS public."SEQ_Publisher";
DROP SEQUENCE IF EXISTS public."SEQ_UserPermission";
DROP SEQUENCE IF EXISTS public."SEQ_UserRole";
