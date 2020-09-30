/*==============================================================*/
/* DBMS name:      ORACLE Version 10gR2                         */
/* Created on:     2013/1/31 18:06:44                           */
/*==============================================================*/

CREATE SEQUENCE S_FDB_COLUMN_REGISTRY CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_FEATUREDATASET CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_ITEMS CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_ITEMTYPES CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_OBJECTCLASSES CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_REPLICATION_STEP CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDB_TABLE_REGISTRY CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDE_REP_CHECKOUTINFO CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDE_REP_FULLREPLICATIONTABLE CACHE 1000
/*#*/;

CREATE SEQUENCE S_FDE_SPATIAL_REFERENCES CACHE 1000
/*#*/;

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
CREATE TABLE FDB_FEATUREDATASET  (
   DATASETID            INTEGER                        NOT NULL,
   DATASETNAME          VARCHAR(128)                   NOT NULL,
   DATASETALIASNAME     VARCHAR(255),
   DATASETUUID          VARCHAR(40)                    NOT NULL,
   DATABASENAME         VARCHAR(32),
   SCHEMANAME           VARCHAR(32),
   DESCRIPTION          VARCHAR(1024),
   SRID                 INTEGER                        NOT NULL,
   TRID                 INTEGER                        NOT NULL,
   REGISTEROPTION       INTEGER                        DEFAULT 0 NOT NULL,
   EDITVERSION          INTEGER                        DEFAULT 0 NOT NULL,
   CREATETIME           TIMESTAMP                      NOT NULL,
   LASTUPDATETIME       TIMESTAMP                      NOT NULL,
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
   CLASSID              INTEGER                        NOT NULL,
   DATASETID            INTEGER                        NOT NULL,
   DATABASENAME         VARCHAR(32),
   SCHEMANAME           VARCHAR(32)                    NOT NULL,
   CLASSNAME            VARCHAR(128)                   NOT NULL,
   ALIASNAME            VARCHAR(255),
   CLSUUID              VARCHAR(40)                    NOT NULL,
   OIDCOLUMN            VARCHAR(32)                    NOT NULL,
   CLASSTYPE            INTEGER                        NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   CREATETIME           TIMESTAMP                      NOT NULL,
   LASTUPDATETIME       TIMESTAMP                      NOT NULL,
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
   COLUMNID             INTEGER                        NOT NULL,
   DOMAINID             INTEGER,
   CLASSID              INTEGER                        NOT NULL,
   FIELDNAME            VARCHAR(255)                   NOT NULL,
   ALIASNAME            VARCHAR(255),
   DEFAULTVALUESTRING   VARCHAR(255),
   DEFAULTVALUENUMBER   LARGEINT,
   ISNULLABLE           INTEGER                        NOT NULL,
   ISEDITABLE           INTEGER                        NOT NULL,
   ISREQUIRED           INTEGER,
   ISSYSTEMCOLUMN       INTEGER,
   ISRANDERINDEXFIELD   INTEGER                        DEFAULT 0 NOT NULL,
   FDE_TYPE             INTEGER                        NOT NULL,
   COLUMN_SIZE          INTEGER                        NOT NULL,
   DECIMAL_DIGITS       INTEGER                        NOT NULL,
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
   COLUMNID             INTEGER                        NOT NULL,
   F_TABLENAME          VARCHAR(64)                    NOT NULL,
   AVGPOINTCNT          INTEGER                        NOT NULL,
   GEOTYPE              INTEGER                        NOT NULL,
   HASID                INTEGER                        NOT NULL,
   HASM                 INTEGER                        NOT NULL,
   HASZ                 INTEGER                        NOT NULL,
   STORAGETYPE          INTEGER                        NOT NULL,
   MAXX                 NUMERIC(38,16)                 NOT NULL,
   MAXY                 NUMERIC(38,16)                 NOT NULL,
   MAXZ                 NUMERIC(38,16)                 NOT NULL,
   MAXM                 NUMERIC(38,16)                 NOT NULL,
   MINX                 NUMERIC(38,16)                 NOT NULL,
   MINY                 NUMERIC(38,16)                 NOT NULL,
   MINZ                 NUMERIC(38,16)                 NOT NULL,
   MINM                 NUMERIC(38,16)                 NOT NULL,
   CONSTRAINT PK_FDB_GEOCOLUMN PRIMARY KEY (COLUMNID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
CREATE TABLE FDB_GRIDINDEX  (
   COLUMNID             INTEGER                        NOT NULL,
   S_TABLENAME          VARCHAR(64)                    NOT NULL,
   INDEXNAME            VARCHAR(255)                   NOT NULL,
   CENTERX              NUMERIC(38,16)                 NOT NULL,
   CENTERY              NUMERIC(38,16)                 NOT NULL,
   L1                   NUMERIC(38,16)                 NOT NULL,
   L2                   NUMERIC(38,16)                 NOT NULL,
   L3                   NUMERIC(38,16)                 NOT NULL,
   RADIO                NUMERIC(38,16)                 NOT NULL,
   CONSTRAINT PK_FDB_GRIDINDEX PRIMARY KEY (COLUMNID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMS                                             */
/*==============================================================*/
CREATE TABLE FDB_ITEMS  (
   ID                   INTEGER                        NOT NULL,
   NAME                 VARCHAR(128)                   NOT NULL,
   FULLNAME             VARCHAR(255)                   NOT NULL,
   GUID                 VARCHAR(40)                    NOT NULL,
   TYPE                 VARCHAR(40)                    NOT NULL,
   BASETYPE             VARCHAR(40)                    NOT NULL,
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
   ID                   INTEGER                        NOT NULL,
   NAME                 VARCHAR(255)                   NOT NULL,
   GUID                 VARCHAR(40)                    NOT NULL,
   PARENTTYPEGUID       VARCHAR(40)                    NOT NULL,
   CONSTRAINT PK_FDB_ITEMTYPES PRIMARY KEY (ID),
   CONSTRAINT AK_IDENTIFIER_2_FDB_ITEMTYPES UNIQUE (NAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
CREATE TABLE FDB_RENDERINDEX  (
   COLUMNID             INTEGER                        NOT NULL,
   INDEXNAME            VARCHAR(255)                   NOT NULL,
   CENTERX              NUMERIC(38,16)                 NOT NULL,
   CENTERY              NUMERIC(38,16)                 NOT NULL,
   L1                   NUMERIC(38,16)                 NOT NULL,
   L2                   NUMERIC(38,16)                 NOT NULL,
   L3                   NUMERIC(38,16)                 NOT NULL,
   RADIO                NUMERIC(38,16)                 NOT NULL,
   FB_TABLENAME         VARCHAR(255),
   CONSTRAINT PK_FDB_RENDERINDEX PRIMARY KEY (COLUMNID),
   CONSTRAINT AK_FDB_GRIDINDEX_UC_N_FDB_REND UNIQUE (INDEXNAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_REPLICATION_STEP                                  */
/*==============================================================*/
CREATE TABLE FDB_REPLICATION_STEP  (
   ID                   INTEGER                        NOT NULL,
   DATASETID            INTEGER                        NOT NULL,
   OPTYPE               INTEGER                        NOT NULL,
   STEP                 INTEGER                        NOT NULL,
   SUBSTEP              INTEGER                        DEFAULT 0,
   DESCRIPTION          VARCHAR(1024),
   ISDONE               INTEGER                        NOT NULL,
   DATA                 BLOB,
   CONSTRAINT PK_FDB_REPLICATION_STEP PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
CREATE TABLE FDB_SERVERCONFIG  (
   PARAMETERNAME        VARCHAR(128)                  NOT NULL,
   PARAMETERVALUE       VARCHAR(255)                  NOT NULL,
   CONSTRAINT PK_FDB_SERVERCONFIG PRIMARY KEY (PARAMETERNAME)
)
/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
CREATE TABLE FDB_SERVERINFO  (
   DATABASENAME         VARCHAR(255),
   INSTANCENAME         VARCHAR(255),
   USERNAME             VARCHAR(255)                   NOT NULL,
   CREATETIME           TIMESTAMP                      NOT NULL,
   FDBVERSIONMAJOR      INTEGER                        NOT NULL,
   FDBVERSIONMINOR      INTEGER                        NOT NULL,
   FDBVERSIONBUGFIX     INTEGER                        NOT NULL,
   FDEPROVIDERNAME      VARCHAR(255)                   NOT NULL,
   FDEDESCRIPTION       VARCHAR(1024),
   "UID"                VARCHAR(64)                    NOT NULL
)
/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
CREATE TABLE FDB_TABLE_REGISTRY  (
   TABLEREGISTRATION_ID INTEGER                        NOT NULL,
   CLASSID              INTEGER,
   DATABASENAME         VARCHAR(32),
   SCHEMANAME           VARCHAR(32)                    NOT NULL,
   TABLENAME            VARCHAR(32)                    NOT NULL,
   TABLETYPE            INTEGER                        NOT NULL,
   ISSYSTEMTABLE        INTEGER                        NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   REGISTRATIONDATE     TIMESTAMP                      NOT NULL,
   LASTUPDATETIME       TIMESTAMP                      NOT NULL,
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
   TABLEREGISTRATION_ID INTEGER                        NOT NULL,
   SEQNAME              VARCHAR(128)                   NOT NULL,
   CONSTRAINT PK_FDB_TABLE_SEQ_LINK PRIMARY KEY (TABLEREGISTRATION_ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_PROCESS_INFORMATION                               */
/*==============================================================*/
CREATE TABLE FDE_PROCESS_INFORMATION  (
   FDEID                INTEGER                        NOT NULL,
   SPID                 INTEGER                        NOT NULL,
   PID                  INTEGER                        NOT NULL,
   STARTTIME            TIMESTAMP                      NOT NULL,
   OWNER                VARCHAR(266)                   NOT NULL,
   DIRECTCONNECT        CHAR(1)                        NOT NULL,
   CONSTRAINT PK_FDE_PROCESS_INFORMATION PRIMARY KEY (FDEID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
CREATE TABLE FDE_REP_CHECKOUTINFO  (
   ID                   INTEGER                        NOT NULL,
   DATASETID            INTEGER,
   CHECKOUTNAME         VARCHAR(40)                    NOT NULL,
   CHECKOUTDATETIME     TIMESTAMP                      NOT NULL,
   BCHECKOUT            INTEGER                        NOT NULL,
   BMASTER              INTEGER                        DEFAULT 0 NOT NULL,
   CONN                 VARCHAR(1024),
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
   ID                   INTEGER                        NOT NULL,
   REGISTERCLASSID      INTEGER                        NOT NULL,
   DATASETID            INTEGER                        NOT NULL,
   CONSTRAINT PK_FDE_REP_FULLREPLICATIONTABL PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_REP_TABLE_MODIFIED                                */
/*==============================================================*/
CREATE TABLE FDE_REP_TABLE_MODIFIED  (
   ID                   INTEGER                        NOT NULL,
   TABLEREGISTERID      INTEGER                        NOT NULL,
   FIELDID              INTEGER                        NOT NULL,
   OPTYPE               INTEGER                        NOT NULL,
   BKEEP                INTEGER                        NOT NULL,
   CONSTRAINT PK_FDE_REP_TABLE_MODIFIED PRIMARY KEY (ID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
CREATE TABLE FDE_SPATIAL_REFERENCES  (
   SRID                 INTEGER                        NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   AUTH_NAME            VARCHAR(255),
   AUTH_SRID            INTEGER,
   FALSEX               NUMERIC(38,16)                 NOT NULL,
   FALSEY               NUMERIC(38,16)                 NOT NULL,
   XYUNITS              NUMERIC(38,16)                 NOT NULL,
   FALSEZ               NUMERIC(38,16)                 NOT NULL,
   ZUNITS               NUMERIC(38,16)                 NOT NULL,
   FALSEM               NUMERIC(38,16)                 NOT NULL,
   MUNITS               NUMERIC(38,16)                 NOT NULL,
   XYCLUSTER_TOL        NUMERIC(38,16),
   ZCLUSTER_TOL         NUMERIC(38,16),
   MCLUSTER_TOL         NUMERIC(38,16),
   SRTEXT               VARCHAR(1024)                  NOT NULL,
   CONSTRAINT PK_FDE_SPATIAL_REFERENCES PRIMARY KEY (SRID)
)
/*#*/;

/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE_TABLE_LOCKS  (
   FDEID                INTEGER                        NOT NULL,
   LOCKTYPE             CHAR(1)                        NOT NULL,
   CLASSID              INTEGER                        NOT NULL,
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
   FDEID                INTEGER                        NOT NULL,
   LOCKTYPE             CHAR(1)         DEFAULT 'E'    NOT NULL,
   CLASSID              INTEGER                        NOT NULL,
   OBJECTID             INTEGER                        NOT NULL,
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
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_GEOCOLUMN
   ADD CONSTRAINT FK_FDB_GEOC_FDB_GEOCO_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
    --  ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_GRIDINDEX
   ADD CONSTRAINT FK_FDB_GRID_GEOCOLUMN_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_OBJECTCLASSES
   ADD CONSTRAINT FK_FDB_OBJE_FEATUREDA_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDB_FEATUREDATASET (DATASETID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_RENDERINDEX
   ADD CONSTRAINT FK_FDB_REND_REFERENCE_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDB_COLUMN_REGISTRY (COLUMNID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_TABLE_REGISTRY
   ADD CONSTRAINT FK_FDB_TABL_OBJECTATT_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDB_TABLE_SEQ_LINK
   ADD CONSTRAINT FK_FDB_TABL_REFERENCE_FDB_TABL FOREIGN KEY (TABLEREGISTRATION_ID)
      REFERENCES FDB_TABLE_REGISTRY (TABLEREGISTRATION_ID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_REP_CHECKOUTINFO
   ADD CONSTRAINT FK_FDE_REP__CHECKOUTD_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDB_FEATUREDATASET (DATASETID)
/*#*/;

ALTER TABLE FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE_PROCESS_INFORMATION (FDEID)
      -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_TABLETABL_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
    --  ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJ_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE_PROCESS_INFORMATION (FDEID)
     -- ON DELETE CASCADE
/*#*/;

ALTER TABLE FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJECT_LOCK_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDB_OBJECTCLASSES (CLASSID)
     -- ON DELETE CASCADE
/*#*/;

