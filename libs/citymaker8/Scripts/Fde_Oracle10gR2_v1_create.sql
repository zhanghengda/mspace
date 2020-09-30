/*==============================================================*/
/* DBMS name:      ORACLE Version 10gR2                         */
/* Created on:     2013/1/31 18:06:44                           */
/*==============================================================*/

CREATE SEQUENCE S_FDB_COLUMN_REGISTRY
/*#*/;

CREATE SEQUENCE S_FDB_FEATUREDATASET
/*#*/;

CREATE SEQUENCE S_FDB_ITEMS
/*#*/;

CREATE SEQUENCE S_FDB_ITEMTYPES
/*#*/;

CREATE SEQUENCE S_FDB_OBJECTCLASSES
/*#*/;

CREATE SEQUENCE S_FDB_REPLICATION_STEP
/*#*/;

CREATE SEQUENCE S_FDB_TABLE_REGISTRY
/*#*/;

CREATE SEQUENCE S_FDE_REP_CHECKOUTINFO
/*#*/;

CREATE SEQUENCE S_FDE_REP_FULLREPLICATIONTABLE
/*#*/;

CREATE SEQUENCE S_FDE_SPATIAL_REFERENCES
/*#*/;

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
CREATE TABLE FDB_FEATUREDATASET  (
   DATASETID            INTEGER                         NOT NULL,
   DATASETNAME          VARCHAR2(128)                   NOT NULL,
   DATASETALIASNAME     VARCHAR2(255),
   DATASETUUID          VARCHAR2(40)                    NOT NULL,
   DATABASENAME         VARCHAR2(32),
   SCHEMANAME           VARCHAR2(32),
   DESCRIPTION          VARCHAR2(1024),
   SRID                 INTEGER                         NOT NULL,
   TRID                 INTEGER                         NOT NULL,
   REGISTEROPTION       INTEGER                        DEFAULT 0 NOT NULL,
   EDITVERSION          INTEGER                        DEFAULT 0 NOT NULL,
   CREATETIME           DATE                            NOT NULL,
   LASTUPDATETIME       DATE                            NOT NULL,
   CONSTRAINT PK_FDB_FEATUREDATASET PRIMARY KEY (DATASETID)
)
/*#*/;

/*==============================================================*/
/* Index: FDB_FEATUREDATASET_UK_NAME                            */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_FEATUREDATASET_UK_NAME ON FDB_FEATUREDATASET (
   DATASETNAME ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDB_OBJECTCLASSES                                     */
/*==============================================================*/
CREATE TABLE FDB_OBJECTCLASSES  (
   CLASSID              INTEGER                         NOT NULL,
   DATASETID            INTEGER                         NOT NULL,
   DATABASENAME         VARCHAR2(32),
   SCHEMANAME           VARCHAR2(32)                    NOT NULL,
   CLASSNAME            VARCHAR2(128)                   NOT NULL,
   ALIASNAME            VARCHAR2(255),
   CLSUUID              VARCHAR2(40)                    NOT NULL,
   OIDCOLUMN            VARCHAR2(32)                    NOT NULL,
   CLASSTYPE            INTEGER                         NOT NULL,
   DESCRIPTION          VARCHAR2(1024),
   CREATETIME           DATE                            NOT NULL,
   LASTUPDATETIME       DATE                            NOT NULL,
   REGISTEROPTION       INTEGER                        DEFAULT 0 NOT NULL,
   CONSTRAINT PK_FDB_OBJECTCLASSES PRIMARY KEY (CLASSID)
)
/*#*/;

/*==============================================================*/
/* Index: FEATUREDATASETCLASSLINK_FK                            */
/*==============================================================*/
CREATE INDEX FEATUREDATASETCLASSLINK_FK ON FDB_OBJECTCLASSES (
   DATASETID ASC
)
/*#*/;

/*==============================================================*/
/* Index: FDB_FDT_NAME_UK                                       */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_FDT_NAME_UK ON FDB_OBJECTCLASSES (
   DATASETID ASC,
   CLASSNAME ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDB_COLUMN_REGISTRY                                   */
/*==============================================================*/
CREATE TABLE FDB_COLUMN_REGISTRY  (
   COLUMNID             INTEGER                         NOT NULL,
   DOMAINID             INTEGER,
   CLASSID              INTEGER                         NOT NULL,
   FIELDNAME            VARCHAR2(255)                   NOT NULL,
   ALIASNAME            VARCHAR2(255),
   DEFAULTVALUESTRING   VARCHAR2(255),
   DEFAULTVALUENUMBER   NUMBER(38,8),
   ISNULLABLE           INTEGER                         NOT NULL,
   ISEDITABLE           INTEGER                         NOT NULL,
   ISREQUIRED           INTEGER,
   ISSYSTEMCOLUMN       INTEGER,
   ISRANDERINDEXFIELD   INTEGER                        DEFAULT 0 NOT NULL,
   FDE_TYPE             INTEGER                         NOT NULL,
   COLUMN_SIZE          INTEGER                         NOT NULL,
   DECIMAL_DIGITS       INTEGER                         NOT NULL,
   CONSTRAINT PK_FDB_COLUMN_REGISTRY PRIMARY KEY (COLUMNID)
)
/*#*/;

/*==============================================================*/
/* Index: TABLECOLUMNLINK_FK                                    */
/*==============================================================*/
CREATE INDEX TABLECOLUMNLINK_FK ON FDB_COLUMN_REGISTRY (
   CLASSID ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDB_GEOCOLUMN                                         */
/*==============================================================*/
CREATE TABLE FDB_GEOCOLUMN  (
   COLUMNID             INTEGER                         NOT NULL,
   F_TABLENAME          VARCHAR2(64)                    NOT NULL,
   AVGPOINTCNT          INTEGER                         NOT NULL,
   GEOTYPE              INTEGER                         NOT NULL,
   HASID                INTEGER                         NOT NULL,
   HASM                 INTEGER                         NOT NULL,
   HASZ                 INTEGER                         NOT NULL,
   STORAGETYPE          INTEGER                         NOT NULL,
   MAXX                 NUMBER(38,16)                   NOT NULL,
   MAXY                 NUMBER(38,16)                   NOT NULL,
   MAXZ                 NUMBER(38,16)                   NOT NULL,
   MAXM                 NUMBER(38,16)                   NOT NULL,
   MINX                 NUMBER(38,16)                   NOT NULL,
   MINY                 NUMBER(38,16)                   NOT NULL,
   MINZ                 NUMBER(38,16)                   NOT NULL,
   MINM                 NUMBER(38,16)                   NOT NULL,
   CONSTRAINT PK_FDB_GEOCOLUMN PRIMARY KEY (COLUMNID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
CREATE TABLE FDB_GRIDINDEX  (
   COLUMNID             INTEGER                         NOT NULL,
   S_TABLENAME          VARCHAR2(64)                    NOT NULL,
   INDEXNAME            VARCHAR2(255)                   NOT NULL,
   CENTERX              NUMBER(38,16)                   NOT NULL,
   CENTERY              NUMBER(38,16)                   NOT NULL,
   L1                   NUMBER(38,16)                   NOT NULL,
   L2                   NUMBER(38,16)                   NOT NULL,
   L3                   NUMBER(38,16)                   NOT NULL,
   RADIO                NUMBER(38,16)                   NOT NULL,
   CONSTRAINT PK_FDB_GRIDINDEX PRIMARY KEY (COLUMNID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMS                                             */
/*==============================================================*/
CREATE TABLE FDB_ITEMS  (
   ID                   INTEGER                         NOT NULL,
   NAME                 VARCHAR2(128)                   NOT NULL,
   FULLNAME             VARCHAR2(255)                   NOT NULL,
   GUID                 VARCHAR2(40)                    NOT NULL,
   TYPE                 VARCHAR2(40)                    NOT NULL,
   BASETYPE             VARCHAR2(40)                    NOT NULL,
   DEFAULTS             BLOB,
   DEFINITION           BLOB,
   DOCUMENTATION        BLOB,
   CONSTRAINT PK_FDB_ITEMS PRIMARY KEY (ID),
   CONSTRAINT AK_IDENTIFIER_2_FDB_ITEMS UNIQUE (FULLNAME,BASETYPE)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMTYPES                                         */
/*==============================================================*/
CREATE TABLE FDB_ITEMTYPES  (
   ID                   INTEGER                         NOT NULL,
   NAME                 VARCHAR2(255)                   NOT NULL,
   GUID                 VARCHAR2(40)                    NOT NULL,
   PARENTTYPEGUID       VARCHAR2(40)                    NOT NULL,
   CONSTRAINT PK_FDB_ITEMTYPES PRIMARY KEY (ID),
   CONSTRAINT AK_IDENTIFIER_2_FDB_ITEMTYPES UNIQUE (NAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
CREATE TABLE FDB_RENDERINDEX  (
   COLUMNID             INTEGER                         NOT NULL,
   INDEXNAME            VARCHAR2(255)                   NOT NULL,
   CENTERX              NUMBER(38,16)                   NOT NULL,
   CENTERY              NUMBER(38,16)                   NOT NULL,
   L1                   NUMBER(38,16)                   NOT NULL,
   L2                   NUMBER(38,16)                   NOT NULL,
   L3                   NUMBER(38,16)                   NOT NULL,
   RADIO                NUMBER(38,16)                   NOT NULL,
   FB_TABLENAME         VARCHAR2(255),
   CONSTRAINT PK_FDB_RENDERINDEX PRIMARY KEY (COLUMNID),
   CONSTRAINT AK_FDB_GRIDINDEX_UC_N_FDB_REND UNIQUE (INDEXNAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_REPLICATION_STEP                                  */
/*==============================================================*/
CREATE TABLE FDB_REPLICATION_STEP  (
   ID                   INTEGER                         NOT NULL,
   DATASETID            INTEGER                         NOT NULL,
   OPTYPE               INTEGER                         NOT NULL,
   STEP                 INTEGER                         NOT NULL,
   SUBSTEP              INTEGER                        DEFAULT 0,
   DESCRIPTION          VARCHAR2(1024),
   ISDONE               INTEGER                         NOT NULL,
   DATA                 BLOB,
   CONSTRAINT PK_FDB_REPLICATION_STEP PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
CREATE TABLE FDB_SERVERCONFIG  (
   PARAMETERNAME        VARCHAR2(128)                   NOT NULL,
   PARAMETERVALUE       VARCHAR2(255)                   NOT NULL,
   CONSTRAINT PK_FDB_SERVERCONFIG PRIMARY KEY (PARAMETERNAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
CREATE TABLE FDB_SERVERINFO  (
   DATABASENAME         VARCHAR2(255),
   INSTANCENAME         VARCHAR2(255),
   USERNAME             VARCHAR2(255)                   NOT NULL,
   CREATETIME           TIMESTAMP                       NOT NULL,
   FDBVERSIONMAJOR      INTEGER                         NOT NULL,
   FDBVERSIONMINOR      INTEGER                         NOT NULL,
   FDBVERSIONBUGFIX     INTEGER                         NOT NULL,
   FDEPROVIDERNAME      VARCHAR2(255)                   NOT NULL,
   FDEDESCRIPTION       VARCHAR2(1024),
   "UID"                VARCHAR2(64)                    NOT NULL
)
/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
CREATE TABLE FDB_TABLE_REGISTRY  (
   TABLEREGISTRATION_ID INTEGER                         NOT NULL,
   CLASSID              INTEGER,
   DATABASENAME         VARCHAR2(32),
   SCHEMANAME           VARCHAR2(32)                    NOT NULL,
   TABLENAME            VARCHAR2(32)                    NOT NULL,
   TABLETYPE            INTEGER                         NOT NULL,
   ISSYSTEMTABLE        INTEGER                         NOT NULL,
   DESCRIPTION          VARCHAR2(1024),
   REGISTRATIONDATE     DATE                            NOT NULL,
   LASTUPDATETIME       DATE                            NOT NULL,
   CONSTRAINT PK_FDB_TABLE_REGISTRY PRIMARY KEY (TABLEREGISTRATION_ID)
)
/*#*/;

/*==============================================================*/
/* Index: OBJECTATTACHLINK_FK                                   */
/*==============================================================*/
CREATE INDEX OBJECTATTACHLINK_FK ON FDB_TABLE_REGISTRY (
   CLASSID ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_SEQ_LINK                                    */
/*==============================================================*/
CREATE TABLE FDB_TABLE_SEQ_LINK  (
   TABLEREGISTRATION_ID INTEGER                         NOT NULL,
   SEQNAME              VARCHAR2(128)                   NOT NULL,
   CONSTRAINT PK_FDB_TABLE_SEQ_LINK PRIMARY KEY (TABLEREGISTRATION_ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_PROCESS_INFORMATION                               */
/*==============================================================*/
CREATE TABLE FDE_PROCESS_INFORMATION  (
   FDEID                INTEGER                         NOT NULL,
   SPID                 INTEGER                         NOT NULL,
   PID                  INTEGER                         NOT NULL,
   STARTTIME            DATE                            NOT NULL,
   OWNER                VARCHAR2(266)                   NOT NULL,
   DIRECTCONNECT        CHAR(1)                         NOT NULL,
   CONSTRAINT PK_FDE_PROCESS_INFORMATION PRIMARY KEY (FDEID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
CREATE TABLE FDE_REP_CHECKOUTINFO  (
   ID                   INTEGER                         NOT NULL,
   DATASETID            INTEGER,
   CHECKOUTNAME         VARCHAR2(40)                    NOT NULL,
   CHECKOUTDATETIME     DATE                            NOT NULL,
   BCHECKOUT            INTEGER                         NOT NULL,
   BMASTER              INTEGER                        DEFAULT 0 NOT NULL,
   CONN                 VARCHAR2(1024),
   CONSTRAINT PK_FDE_REP_CHECKOUTINFO PRIMARY KEY (ID),
   CONSTRAINT AK_CHECKOUTNAME_FDE_REP_ UNIQUE (CHECKOUTNAME)
)
/*#*/;

/*==============================================================*/
/* Index: CHECKOUTDATASETLINK_FK                                */
/*==============================================================*/
CREATE INDEX CHECKOUTDATASETLINK_FK ON FDE_REP_CHECKOUTINFO (
   DATASETID ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDE_REP_FULLREPLICATIONTABLE                          */
/*==============================================================*/
CREATE TABLE FDE_REP_FULLREPLICATIONTABLE  (
   ID                   INTEGER                         NOT NULL,
   REGISTERCLASSID      INTEGER                         NOT NULL,
   DATASETID            INTEGER                         NOT NULL,
   CONSTRAINT PK_FDE_REP_FULLREPLICATIONTABL PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_REP_TABLE_MODIFIED                                */
/*==============================================================*/
CREATE TABLE FDE_REP_TABLE_MODIFIED  (
   ID                   INTEGER                         NOT NULL,
   TABLEREGISTERID      INTEGER                         NOT NULL,
   FIELDID              INTEGER                         NOT NULL,
   OPTYPE               INTEGER                         NOT NULL,
   BKEEP                INTEGER                         NOT NULL,
   CONSTRAINT PK_FDE_REP_TABLE_MODIFIED PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
CREATE TABLE FDE_SPATIAL_REFERENCES  (
   SRID                 INTEGER                         NOT NULL,
   DESCRIPTION          VARCHAR2(1024),
   AUTH_NAME            VARCHAR2(255),
   AUTH_SRID            INTEGER,
   FALSEX               NUMBER(38,16)                   NOT NULL,
   FALSEY               NUMBER(38,16)                   NOT NULL,
   XYUNITS              NUMBER(38,16)                   NOT NULL,
   FALSEZ               NUMBER(38,16)                   NOT NULL,
   ZUNITS               NUMBER(38,16)                   NOT NULL,
   FALSEM               NUMBER(38,16)                   NOT NULL,
   MUNITS               NUMBER(38,16)                   NOT NULL,
   XYCLUSTER_TOL        NUMBER(38,16),
   ZCLUSTER_TOL         NUMBER(38,16),
   MCLUSTER_TOL         NUMBER(38,16),
   SRTEXT               VARCHAR2(1024)                  NOT NULL,
   CONSTRAINT PK_FDE_SPATIAL_REFERENCES PRIMARY KEY (SRID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE_TABLE_LOCKS  (
   FDEID                INTEGER                         NOT NULL,
   LOCKTYPE             CHAR(1)                         NOT NULL,
   CLASSID              INTEGER                         NOT NULL,
   CONSTRAINT PK_FDE_TABLE_LOCKS PRIMARY KEY (FDEID, LOCKTYPE, CLASSID)
)
/*#*/;

/*==============================================================*/
/* Index: FDE_PROCESSINFO_TABLELOCKS_FK                         */
/*==============================================================*/
CREATE INDEX FDE_PROCESSINFO_TABLELOCKS_FK ON FDE_TABLE_LOCKS (
   FDEID ASC
)
/*#*/;

/*==============================================================*/
/* Index: TABLETABLELOCKLINK_FK                                 */
/*==============================================================*/
CREATE INDEX TABLETABLELOCKLINK_FK ON FDE_TABLE_LOCKS (
   CLASSID ASC
)
/*#*/;

/*==============================================================*/
/* Table: FDE_OBJECT_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE_OBJECT_LOCKS  (
   FDEID                INTEGER                         NOT NULL,
   LOCKTYPE             CHAR(1)          DEFAULT 'E'    NOT NULL,
   CLASSID              INTEGER                         NOT NULL,
   OBJECTID             INTEGER                         NOT NULL,
   CONSTRAINT PK_FDE_OBJECT_LOCKS PRIMARY KEY (FDEID, LOCKTYPE, CLASSID,OBJECTID),
   CONSTRAINT AK_FDE_OBJECT_LOCKS_UC UNIQUE (CLASSID,OBJECTID)
)
/*#*/;

ALTER TABLE FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_RELATIONS_FDB_ITEM FOREIGN KEY (DOMAINID)
      REFERENCES FDB_ITEMS (ID)
/*#*/;

ALTER TABLE FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_TABLECOLU_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_GEOCOLUMN
   ADD CONSTRAINT FK_FDB_GEOC_FDB_GEOCO_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_GRIDINDEX
   ADD CONSTRAINT FK_FDB_GRID_GEOCOLUMN_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_OBJECTCLASSES
   ADD CONSTRAINT FK_FDB_OBJE_FEATUREDA_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDB_FEATUREDATASET (DATASETID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_RENDERINDEX
   ADD CONSTRAINT FK_FDB_REND_REFERENCE_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_TABLE_REGISTRY
   ADD CONSTRAINT FK_FDB_TABL_OBJECTATT_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_TABLE_SEQ_LINK
   ADD CONSTRAINT FK_FDB_TABL_REFERENCE_FDB_TABL FOREIGN KEY (TABLEREGISTRATION_ID)
      REFERENCES FDB_TABLE_REGISTRY (TABLEREGISTRATION_ID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_REP_CHECKOUTINFO
   ADD CONSTRAINT FK_FDE_REP__CHECKOUTD_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDB_FEATUREDATASET (DATASETID)
/*#*/;

ALTER TABLE FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE_PROCESS_INFORMATION (FDEID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_TABLETABL_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJ_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE_PROCESS_INFORMATION (FDEID)
      ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJECT_LOCK_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE
/*#*/;

CREATE TRIGGER TIB_FDB_COLUMN_REGISTRY BEFORE INSERT
ON FDB_COLUMN_REGISTRY FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "COLUMNID" USES SEQUENCE S_FDB_COLUMN_REGISTRY
    SELECT S_FDB_COLUMN_REGISTRY.NEXTVAL INTO :NEW.COLUMNID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_FEATUREDATASET BEFORE INSERT
ON FDB_FEATUREDATASET FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "DATASETID" USES SEQUENCE S_FDB_FEATUREDATASET
    SELECT S_FDB_FEATUREDATASET.NEXTVAL INTO :NEW.DATASETID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_ITEMS BEFORE INSERT
ON FDB_ITEMS FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "ID" USES SEQUENCE S_FDB_ITEMS
    SELECT S_FDB_ITEMS.NEXTVAL INTO :NEW.ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_ITEMTYPES BEFORE INSERT
ON FDB_ITEMTYPES FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "ID" USES SEQUENCE S_FDB_ITEMTYPES
    SELECT S_FDB_ITEMTYPES.NEXTVAL INTO :NEW.ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_OBJECTCLASSES BEFORE INSERT
ON FDB_OBJECTCLASSES FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "CLASSID" USES SEQUENCE S_FDB_OBJECTCLASSES
    SELECT S_FDB_OBJECTCLASSES.NEXTVAL INTO :NEW.CLASSID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_REPLICATION_STEP BEFORE INSERT
ON FDB_REPLICATION_STEP FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "ID" USES SEQUENCE S_FDB_REPLICATION_STEP
    SELECT S_FDB_REPLICATION_STEP.NEXTVAL INTO :NEW.ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDB_TABLE_REGISTRY BEFORE INSERT
ON FDB_TABLE_REGISTRY FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "TABLEREGISTRATION_ID" USES SEQUENCE S_FDB_TABLE_REGISTRY
    SELECT S_FDB_TABLE_REGISTRY.NEXTVAL INTO :NEW.TABLEREGISTRATION_ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDE_REP_CHECKOUTINFO BEFORE INSERT
ON FDE_REP_CHECKOUTINFO FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "ID" USES SEQUENCE S_FDE_REP_CHECKOUTINFO
    SELECT S_FDE_REP_CHECKOUTINFO.NEXTVAL INTO :NEW.ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDE_REP_FULLREPLICATIONTAB BEFORE INSERT
ON FDE_REP_FULLREPLICATIONTABLE FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "ID" USES SEQUENCE S_FDE_REP_FULLREPLICATIONTABLE
    SELECT S_FDE_REP_FULLREPLICATIONTABLE.NEXTVAL INTO :NEW.ID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;


CREATE TRIGGER TIB_FDE_SPATIAL_REFERENCES BEFORE INSERT
ON FDE_SPATIAL_REFERENCES FOR EACH ROW
DECLARE
    INTEGRITY_ERROR  EXCEPTION;
    ERRNO            INTEGER;
    ERRMSG           CHAR(200);
    DUMMY            INTEGER;
    FOUND            BOOLEAN;

BEGIN
    --  COLUMN "SRID" USES SEQUENCE S_FDE_SPATIAL_REFERENCES
    SELECT S_FDE_SPATIAL_REFERENCES.NEXTVAL INTO :NEW.SRID FROM DUAL;

--  ERRORS HANDLING
EXCEPTION
    WHEN INTEGRITY_ERROR THEN
       RAISE_APPLICATION_ERROR(ERRNO, ERRMSG);
END;
/*#*/;

