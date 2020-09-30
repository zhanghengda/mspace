/*==============================================================*/
/* DBMS name:      PostgreSQL 8                                 */
/* Created on:     2014/1/26 9:45:36                            */
/*==============================================================*/

/*==============================================================*/
/* Table: FDB_COLUMN_REGISTRY                                   */
/*==============================================================*/
CREATE TABLE FDE.FDB_COLUMN_REGISTRY (
   COLUMNID             SERIAL               NOT NULL,
   DOMAINID             INT4                 NULL,
   CLASSID              INT4                 NOT NULL,
   FIELDNAME            VARCHAR(255)         NOT NULL,
   ALIASNAME            VARCHAR(255)         NULL,
   DEFAULTVALUESTRING   VARCHAR(255)         NULL,
   DEFAULTVALUENUMBER   NUMERIC(38,8)        NULL,
   ISNULLABLE           INT4                 NOT NULL,
   ISEDITABLE           INT4                 NOT NULL,
   ISREQUIRED           INT4                 NULL,
   ISSYSTEMCOLUMN       INT4                 NULL,
   ISRANDERINDEXFIELD   INT4                 NOT NULL DEFAULT 0,
   FDE_TYPE             INT4                 NOT NULL,
   COLUMN_SIZE          INT4                 NOT NULL,
   DECIMAL_DIGITS       INT4                 NOT NULL,
   CONSTRAINT PK_FDB_COLUMN_REGISTRY PRIMARY KEY (COLUMNID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_COLUMN_REGISTRY_PK                                */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_COLUMN_REGISTRY_PK ON FDE.FDB_COLUMN_REGISTRY (
COLUMNID
)/*#*/;

/*==============================================================*/
/* Index: TABLECOLUMNLINK_FK                                    */
/*==============================================================*/
CREATE  INDEX TABLECOLUMNLINK_FK ON FDE.FDB_COLUMN_REGISTRY (
CLASSID
)/*#*/;

/*==============================================================*/
/* Index: RELATIONSHIP_16_FK                                    */
/*==============================================================*/
CREATE  INDEX RELATIONSHIP_16_FK ON FDE.FDB_COLUMN_REGISTRY (
DOMAINID
)/*#*/;

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
CREATE TABLE FDE.FDB_FEATUREDATASET (
   DATASETID            SERIAL               NOT NULL,
   DATASETNAME          VARCHAR(128)         NOT NULL,
   DATASETALIASNAME     VARCHAR(255)         NULL,
   DATASETUUID          VARCHAR(40)          NOT NULL,
   DATABASENAME         VARCHAR(32)          NULL,
   SCHEMANAME           VARCHAR(32)          NULL,
   DESCRIPTION          VARCHAR(1024)        NULL,
   SRID                 INT4                 NOT NULL,
   TRID                 INT4                 NOT NULL,
   REGISTEROPTION       INT4                 NOT NULL DEFAULT 0,
   EDITVERSION          INT4                 NOT NULL DEFAULT 0,
   CREATETIME           TIMESTAMP            NOT NULL,
   LASTUPDATETIME       TIMESTAMP            NOT NULL,
   CONSTRAINT PK_FDB_FEATUREDATASET PRIMARY KEY (DATASETID)
)/*#*/;

CREATE UNIQUE INDEX FDB_FEATUREDATASET_UK_NAME ON FDE.FDB_FEATUREDATASET (
DATASETNAME ASC
)/*#*/;

/*==============================================================*/
/* Index: FDB_FEATUREDATASET_PK                                 */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_FEATUREDATASET_PK ON FDE.FDB_FEATUREDATASET (
DATASETID
)/*#*/;

/*==============================================================*/
/* Table: FDB_GEOCOLUMN                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_GEOCOLUMN (
   COLUMNID             INT4                 NOT NULL,
   F_TABLENAME          VARCHAR(32)          NOT NULL,
   AVGPOINTCNT          INT4                 NOT NULL,
   GEOTYPE              INT4                 NOT NULL,
   HASID                INT4                 NOT NULL,
   HASM                 INT4                 NOT NULL,
   HASZ                 INT4                 NOT NULL,
   STORAGETYPE          INT4                 NOT NULL,
   MAXX                 FLOAT8               NOT NULL,
   MAXY                 FLOAT8               NOT NULL,
   MAXZ                 FLOAT8               NOT NULL,
   MAXM                 FLOAT8               NOT NULL,
   MINX                 FLOAT8               NOT NULL,
   MINY                 FLOAT8               NOT NULL,
   MINZ                 FLOAT8               NOT NULL,
   MINM                 FLOAT8               NOT NULL,
   CONSTRAINT PK_FDB_GEOCOLUMN PRIMARY KEY (COLUMNID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_GEOCOLUMN_PK                                      */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_GEOCOLUMN_PK ON FDE.FDB_GEOCOLUMN (
COLUMNID
)/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_GRIDINDEX (
   COLUMNID             INT4                 NOT NULL,
   S_TABLENAME          VARCHAR(32)          NOT NULL,
   INDEXNAME            VARCHAR(255)         NOT NULL,
   CENTERX              FLOAT8               NOT NULL,
   CENTERY              FLOAT8               NOT NULL,
   L1                   FLOAT8               NOT NULL,
   L2                   FLOAT8               NOT NULL,
   L3                   FLOAT8               NOT NULL,
   RADIO                FLOAT8               NOT NULL,
   CONSTRAINT PK_FDB_GRIDINDEX PRIMARY KEY (COLUMNID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_GRIDINDEX_PK                                      */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_GRIDINDEX_PK ON FDE.FDB_GRIDINDEX (
COLUMNID
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMS                                             */
/*==============================================================*/
CREATE TABLE FDE.FDB_ITEMS (
   ID                   SERIAL               NOT NULL,
   NAME                 VARCHAR(128)         NOT NULL,
   FULLNAME             VARCHAR(255)         NOT NULL,
   GUID                 VARCHAR(40)          NOT NULL,
   TYPE                 VARCHAR(40)          NOT NULL,
   BASETYPE             VARCHAR(40)          NOT NULL,
   DEFAULTS             CHAR                 NULL,
   DEFINITION           BYTEA                NULL,
   DOCUMENTATION        CHAR                 NULL,
   CONSTRAINT PK_FDB_ITEMS PRIMARY KEY (ID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_ITEMS_PK                                          */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_ITEMS_PK ON FDE.FDB_ITEMS (
ID
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMTYPES                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_ITEMTYPES (
   ID                   SERIAL               NOT NULL,
   NAME                 VARCHAR(255)         NOT NULL,
   GUID                 VARCHAR(40)          NOT NULL,
   PARENTTYPEGUID       VARCHAR(40)          NOT NULL,
   CONSTRAINT PK_FDB_ITEMTYPES PRIMARY KEY (ID),
   CONSTRAINT AK_IDENTIFIER_2_FDB_ITEM UNIQUE (NAME)
)/*#*/;

/*==============================================================*/
/* Index: FDB_ITEMTYPES_PK                                      */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_ITEMTYPES_PK ON FDE.FDB_ITEMTYPES (
ID
)/*#*/;

/*==============================================================*/
/* Table: FDB_OBJECTCLASSES                                     */
/*==============================================================*/
CREATE TABLE FDE.FDB_OBJECTCLASSES (
   CLASSID              SERIAL               NOT NULL,
   DATASETID            INT4                 NOT NULL,
   DATABASENAME         VARCHAR(32)          NOT NULL,
   SCHEMANAME           VARCHAR(32)          NOT NULL,
   CLASSNAME            VARCHAR(128)         NOT NULL,
   ALIASNAME            VARCHAR(255)         NOT NULL,
   CLSUUID              VARCHAR(40)          NOT NULL,
   OIDCOLUMN            VARCHAR(32)          NOT NULL,
   CLASSTYPE            INT4                 NOT NULL,
   DESCRIPTION          VARCHAR(1024)        NULL,
   CREATETIME           TIMESTAMP            NOT NULL,
   LASTUPDATETIME       TIMESTAMP            NOT NULL,
   REGISTEROPTION       INT4                 NOT NULL DEFAULT 0,
   CONSTRAINT PK_FDB_OBJECTCLASSES PRIMARY KEY (CLASSID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_OBJECTCLASSES_PK                                  */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_OBJECTCLASSES_PK ON FDE.FDB_OBJECTCLASSES (
CLASSID
)/*#*/;

/*==============================================================*/
/* Index: FEATUREDATASETCLASSLINK_FK                            */
/*==============================================================*/
CREATE  INDEX FEATUREDATASETCLASSLINK_FK ON FDE.FDB_OBJECTCLASSES (
DATASETID
)/*#*/;

CREATE UNIQUE INDEX FDB_OBJECTCLASS_UK_NAME ON FDE.FDB_OBJECTCLASSES (
DATASETID ASC,
CLASSNAME ASC
)/*#*/;

/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
CREATE TABLE FDE.FDB_RENDERINDEX (
   COLUMNID             INT4                 NOT NULL,
   INDEXNAME            VARCHAR(255)         NOT NULL,
   CENTERX              FLOAT8               NOT NULL,
   CENTERY              FLOAT8               NOT NULL,
   L1                   FLOAT8               NOT NULL,
   L2                   FLOAT8               NOT NULL,
   L3                   FLOAT8               NOT NULL,
   RADIO                FLOAT8               NOT NULL,
   FB_TABLENAME         VARCHAR(255),
   CONSTRAINT PK_FDB_RENDERINDEX PRIMARY KEY (COLUMNID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_RENDERINDEX_PK                                    */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_RENDERINDEX_PK ON FDE.FDB_RENDERINDEX (
COLUMNID
)/*#*/;

/*==============================================================*/
/* Table: FDB_REPLICATION_STEP                                  */
/*==============================================================*/
CREATE TABLE FDE.FDB_REPLICATION_STEP (
   ID                   INT4                 NOT NULL,
   DATASETID            INT4                 NOT NULL,
   OPTYPE               INT4                 NOT NULL,
   STEP                 INT4                 NOT NULL,
   SUBSTEP              INT4                 NULL DEFAULT 0,
   DESCRIPTION          VARCHAR(1024)        NULL,
   ISDONE               INT4                 NOT NULL,
   DATA                 BYTEA                 NULL,
   CONSTRAINT PK_FDB_REPLICATION_STEP PRIMARY KEY (ID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_REPLICATION_STEP_PK                               */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_REPLICATION_STEP_PK ON FDE.FDB_REPLICATION_STEP (
ID
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
CREATE TABLE FDE.FDB_SERVERCONFIG (
   PARAMETERNAME        VARCHAR(128)         NOT NULL,
   PARAMETERVALUE       VARCHAR(255)         NOT NULL,
   CONSTRAINT PK_FDB_SERVERCONFIG PRIMARY KEY (PARAMETERNAME)
)/*#*/;

/*==============================================================*/
/* Index: FDB_SERVERCONFIG_PK                                   */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_SERVERCONFIG_PK ON FDE.FDB_SERVERCONFIG (
PARAMETERNAME
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
CREATE TABLE FDE.FDB_SERVERINFO (
   DATABASENAME         VARCHAR(255)         NULL,
   INSTANCENAME         VARCHAR(255)         NULL,
   USERNAME             VARCHAR(255)         NOT NULL,
   CREATETIME           TIMESTAMP            NOT NULL,
   FDBVERSIONMAJOR      INT4                 NOT NULL,
   FDBVERSIONMINOR      INT4                 NOT NULL,
   FDBVERSIONBUGFIX     INT4                 NOT NULL,
   FDEPROVIDERNAME      VARCHAR(255)         NOT NULL,
   FDEDESCRIPTION       VARCHAR(1024)        NULL,
   UID                  VARCHAR(64)          NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
CREATE TABLE FDE.FDB_TABLE_REGISTRY (
   TABLEREGISTRATION_ID SERIAL               NOT NULL,
   CLASSID              INT4                 NULL,
   DATABASENAME         VARCHAR(32)          NOT NULL,
   SCHEMANAME           VARCHAR(32)          NOT NULL,
   TABLENAME            VARCHAR(32)          NOT NULL,
   TABLETYPE            INT4                 NOT NULL,
   ISSYSTEMTABLE        INT4                 NOT NULL,
   DESCRIPTION          VARCHAR(1024)        NULL,
   REGISTRATIONDATE     TIMESTAMP            NOT NULL,
   LASTUPDATETIME       TIMESTAMP            NOT NULL,
   CONSTRAINT PK_FDB_TABLE_REGISTRY PRIMARY KEY (TABLEREGISTRATION_ID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_TABLE_REGISTRY_PK                                 */
/*==============================================================*/
CREATE UNIQUE INDEX FDB_TABLE_REGISTRY_PK ON FDE.FDB_TABLE_REGISTRY (
TABLEREGISTRATION_ID
)/*#*/;

/*==============================================================*/
/* Index: OBJECTATTACHLINK_FK                                   */
/*==============================================================*/
CREATE  INDEX OBJECTATTACHLINK_FK ON FDE.FDB_TABLE_REGISTRY (
CLASSID
)/*#*/;

/*==============================================================*/
/* Table: FDE_PROCESS_INFORMATION                               */
/*==============================================================*/
CREATE TABLE FDE.FDE_PROCESS_INFORMATION (
   FDEID                INT4                 NOT NULL,
   SPID                 INT4                 NOT NULL,
   PID                  INT4                 NOT NULL,
   STARTTIME            DATE                 NOT NULL,
   OWNER                VARCHAR(266)         NOT NULL,
   DIRECTCONNECT        CHAR(1)              NOT NULL,
   CONSTRAINT PK_FDE_PROCESS_INFORMATION PRIMARY KEY (FDEID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_PROCESS_INFORMATION_PK                            */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_PROCESS_INFORMATION_PK ON FDE.FDE_PROCESS_INFORMATION (
FDEID
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_CHECKOUTINFO (
   ID                   INT4                 NOT NULL,
   DATASETID            INT4                 NULL,
   CHECKOUTNAME         VARCHAR(40)          NOT NULL,
   CHECKOUTDATETIME     DATE                 NOT NULL,
   BCHECKOUT            INT4                 NOT NULL,
   BMASTER              INT4                 NOT NULL DEFAULT 0,
   CONN                 VARCHAR(1024)        NULL,
   CONSTRAINT PK_FDE_REP_CHECKOUTINFO PRIMARY KEY (ID),
   CONSTRAINT AK_CHECKOUTNAME_FDE_REP_ UNIQUE (CHECKOUTNAME)
)/*#*/;

/*==============================================================*/
/* Index: FDE_REP_CHECKOUTINFO_PK                               */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_REP_CHECKOUTINFO_PK ON FDE.FDE_REP_CHECKOUTINFO (
ID
)/*#*/;

/*==============================================================*/
/* Index: CHECKOUTDATASETLINK_FK                                */
/*==============================================================*/
CREATE  INDEX CHECKOUTDATASETLINK_FK ON FDE.FDE_REP_CHECKOUTINFO (
DATASETID
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_FULLREPLICATIONTABLE                          */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_FULLREPLICATIONTABLE (
   ID                   INT4                 NOT NULL,
   REGISTERCLASSID      INT4                 NOT NULL,
   DATASETID            INT4                 NOT NULL,
   CONSTRAINT PK_FDE_REP_FULLREPLICATIONTABL PRIMARY KEY (ID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_REP_FULLREPLICATIONTABLE_PK                       */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_REP_FULLREPLICATIONTABLE_PK ON 
FDE.FDE_REP_FULLREPLICATIONTABLE (ID)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_TABLE_MODIFIED                                */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_TABLE_MODIFIED (
   ID                   INT4                 NOT NULL,
   TABLEREGISTERID      INT4                 NOT NULL,
   FIELDID              INT4                 NOT NULL,
   OPTYPE               INT4                 NOT NULL,
   BKEEP                INT4                 NOT NULL,
   CONSTRAINT PK_FDE_REP_TABLE_MODIFIED PRIMARY KEY (ID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_REP_TABLE_MODIFIED_PK                             */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_REP_TABLE_MODIFIED_PK ON FDE.FDE_REP_TABLE_MODIFIED (
ID
)/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
CREATE TABLE FDE.FDE_SPATIAL_REFERENCES (
   SRID                 SERIAL               NOT NULL,
   DESCRIPTION          VARCHAR(1024)        NULL,
   AUTH_NAME            VARCHAR(255)         NULL,
   AUTH_SRID            INT4                 NULL,
   FALSEX               FLOAT8               NOT NULL,
   FALSEY               FLOAT8               NOT NULL,
   XYUNITS              FLOAT8               NOT NULL,
   FALSEZ               FLOAT8               NOT NULL,
   ZUNITS               FLOAT8               NOT NULL,
   FALSEM               FLOAT8               NOT NULL,
   MUNITS               FLOAT8               NOT NULL,
   XYCLUSTER_TOL        FLOAT8               NULL,
   ZCLUSTER_TOL         FLOAT8               NULL,
   MCLUSTER_TOL         FLOAT8               NULL,
   SRTEXT               VARCHAR(1024)        NOT NULL,
   CONSTRAINT PK_FDE_SPATIAL_REFERENCES PRIMARY KEY (SRID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_SPATIAL_REFERENCES_PK                             */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_SPATIAL_REFERENCES_PK ON FDE.FDE_SPATIAL_REFERENCES (
SRID
)/*#*/;

/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE.FDE_TABLE_LOCKS (
   FDEID                INT4                 NOT NULL,
   LOCKTYPE             CHAR(1)  DEFAULT 'E' NOT NULL,
   CLASSID              INT4                 NOT NULL,
   CONSTRAINT PK_FDE_TABLE_LOCKS PRIMARY KEY (FDEID, LOCKTYPE, CLASSID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_TABLE_LOCKS_PK                                    */
/*==============================================================*/
CREATE UNIQUE INDEX FDE_TABLE_LOCKS_PK ON FDE.FDE_TABLE_LOCKS (
FDEID,
LOCKTYPE,
CLASSID
)/*#*/;

/*==============================================================*/
/* Table: FDE_OBJECT_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE.FDE_OBJECT_LOCKS (
   FDEID                INT4                 NOT NULL,
   LOCKTYPE             CHAR(1)  DEFAULT 'E' NOT NULL,
   CLASSID              INT4                 NOT NULL,
   OBJECTID             INT4                 NOT NULL,
   CONSTRAINT PK_FDE_OBJECT_LOCKS PRIMARY KEY (FDEID, LOCKTYPE, CLASSID, OBJECTID)
)/*#*/;

/*==============================================================*/
/* Index: FDE_PROCESSINFO_TABLELOCKS_FK                         */
/*==============================================================*/
CREATE  INDEX FDE_PROCESSINFO_TABLELOCKS_FK ON FDE.FDE_TABLE_LOCKS (
FDEID
)/*#*/;

/*==============================================================*/
/* Index: TABLETABLELOCKLINK_FK                                 */
/*==============================================================*/
CREATE  INDEX TABLETABLELOCKLINK_FK ON FDE.FDE_TABLE_LOCKS (
CLASSID
)/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_RELATIONS_FDB_ITEM FOREIGN KEY (DOMAINID)
      REFERENCES FDE.FDB_ITEMS (ID)
/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_TABLECOLU_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDB_GEOCOLUMN
   ADD CONSTRAINT FK_FDB_GEOC_FDB_GEOCO_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDB_GRIDINDEX
   ADD CONSTRAINT FK_FDB_GRID_GEOCOLUMN_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDB_OBJECTCLASSES
   ADD CONSTRAINT FK_FDB_OBJE_FEATUREDA_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDE.FDB_FEATUREDATASET (DATASETID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDB_RENDERINDEX
   ADD CONSTRAINT FK_FDB_REND_COLUMN_RE_FDB_COLU FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDB_TABLE_REGISTRY
   ADD CONSTRAINT FK_FDB_TABL_OBJECTATT_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDE_REP_CHECKOUTINFO
   ADD CONSTRAINT FK_FDE_REP__CHECKOUTD_FDB_FEAT FOREIGN KEY (DATASETID)
      REFERENCES FDE.FDB_FEATUREDATASET (DATASETID) 
/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE.FDE_PROCESS_INFORMATION (FDEID)
      ON DELETE CASCADE 
/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_TABLETABL_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID)
      ON DELETE CASCADE 
/*#*/;

