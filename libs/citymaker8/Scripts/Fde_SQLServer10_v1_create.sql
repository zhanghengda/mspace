/*==============================================================*/
/* DBMS name:      SQLServer10                                    */
/* Created on:     2012/9/18 16:36:11                           */
/*==============================================================*/

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
CREATE TABLE FDE.FDB_FEATUREDATASET
(
   DATASETID            INT NOT NULL PRIMARY KEY,
   DATASETNAME          NVARCHAR(128) NOT NULL,
   DATASETALIASNAME     NVARCHAR(255),
   DATASETUUID          uniqueidentifier NOT NULL,
   DATABASENAME         NVARCHAR(32),
   SCHEMANAME           NVARCHAR(32),
   DESCRIPTION          NVARCHAR(1024),
   SRID                 INT NOT NULL,
   TRID                 INT NOT NULL,
   REGISTEROPTION       INT NOT NULL DEFAULT 0,
   EDITVERSION          INT NOT NULL DEFAULT 0,
   CREATETIME           DATETIME2(7) NOT NULL,
   LASTUPDATETIME       DATETIME2(7) NOT NULL,
   CONSTRAINT AK_UK_DTNAME_DBNAME UNIQUE(DATASETNAME)
)/*#*/;

/*==============================================================*/
/* Table: FDB_OBJECTCLASSES                                     */
/*==============================================================*/
CREATE TABLE FDE.FDB_OBJECTCLASSES
(
   CLASSID              INT NOT NULL PRIMARY KEY,
   DATASETID            INT NOT NULL,
   DATABASENAME         NVARCHAR(32) NOT NULL,
   SCHEMANAME           NVARCHAR(32) NOT NULL,
   CLASSNAME            NVARCHAR(128) NOT NULL,
   ALIASNAME            NVARCHAR(255) NOT NULL,
   CLSUUID              uniqueidentifier NOT NULL,
   OIDCOLUMN            NVARCHAR(32) NOT NULL,
   CLASSTYPE            INT NOT NULL,
   DESCRIPTION          NVARCHAR(1024),
   CREATETIME           DATETIME2(7) NOT NULL,
   LASTUPDATETIME       DATETIME2(7) NOT NULL,
   REGISTEROPTION       INT NOT NULL DEFAULT 0
)/*#*/;

/*==============================================================*/
/* Index: AK_UK_TNAME_DBNAME                                 */
/*==============================================================*/
CREATE NONCLUSTERED INDEX AK_UK_TNAME_DBNAME ON FDE.FDB_OBJECTCLASSES
(
   DATABASENAME, CLASSNAME
)/*#*/;

/*==============================================================*/
/* Table: FDB_COLUMN_REGISTRY                                   */
/*==============================================================*/
CREATE TABLE FDE.FDB_COLUMN_REGISTRY
(
   COLUMNID             INT NOT NULL PRIMARY KEY,
   DOMAINID             INT,
   CLASSID              INT NOT NULL,
   FIELDNAME            NVARCHAR(255) NOT NULL,
   ALIASNAME            NVARCHAR(255),
   DEFAULTVALUESTRING   NVARCHAR(4000),
   DEFAULTVALUENUMBER   BIGINT,
   ISNULLABLE           INT NOT NULL,
   ISEDITABLE           INT NOT NULL,
   ISREQUIRED           INT,
   ISSYSTEMCOLUMN       INT,
   ISRANDERINDEXFIELD   INT NOT NULL DEFAULT 0,
   FDE_TYPE             INT NOT NULL,
   COLUMN_SIZE          INT NOT NULL,
   DECIMAL_DIGITS       INT NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_GEOCOLUMN                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_GEOCOLUMN
(
   COLUMNID             INT NOT NULL PRIMARY KEY,
   F_TABLENAME          NVARCHAR(64) NOT NULL,
   AVGPOINTCNT          INT NOT NULL,
   GEOTYPE              INT NOT NULL,
   HASID                INT NOT NULL,
   HASM                 INT NOT NULL,
   HASZ                 INT NOT NULL,
   STORAGETYPE          INT NOT NULL,
   MAXX                 FLOAT NOT NULL,
   MAXY                 FLOAT NOT NULL,
   MAXZ                 FLOAT NOT NULL,
   MAXM                 FLOAT NOT NULL,
   MINX                 FLOAT NOT NULL,
   MINY                 FLOAT NOT NULL,
   MINZ                 FLOAT NOT NULL,
   MINM                 FLOAT NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_GRIDINDEX
(
   COLUMNID             INT NOT NULL PRIMARY KEY,
   S_TABLENAME          NVARCHAR(64) NOT NULL,
   INDEXNAME            NVARCHAR(255) NOT NULL,
   CENTERX              FLOAT NOT NULL,
   CENTERY              FLOAT NOT NULL,
   L1                   FLOAT NOT NULL,
   L2                   FLOAT NOT NULL,
   L3                   FLOAT NOT NULL,
   RADIO                FLOAT NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: AK_UC_FDB_GRIDINDEX_NAME                              */
/*==============================================================*/
CREATE NONCLUSTERED INDEX AK_UC_FDB_GRIDINDEX_NAME ON FDE.FDB_GRIDINDEX
(
   INDEXNAME
)/*#*/;

/*==============================================================*/
/* Index: AK_UC_FDB_GRIDINDEX_STNAME                            */
/*==============================================================*/
CREATE NONCLUSTERED INDEX AK_UC_FDB_GRIDINDEX_STNAME ON FDE.FDB_GRIDINDEX
(
   S_TABLENAME
)/*#*/;


/*==============================================================*/
/* Table: FDB_ITEMS                                             */
/*==============================================================*/
CREATE TABLE FDE.FDB_ITEMS
(
   ID                   INT NOT NULL PRIMARY KEY,
   NAME                 NVARCHAR(128) NOT NULL,
   FULLNAME             NVARCHAR(255) NOT NULL,
   GUID                 uniqueidentifier NOT NULL,
   TYPE                 uniqueidentifier NOT NULL,
   BASETYPE             uniqueidentifier NOT NULL,
   DEFAULTS             VARBINARY(max),
   DEFINITION           VARBINARY(max),
   DOCUMENTATION        VARBINARY(max),
   CONSTRAINT AK_FDB_ITEMS UNIQUE(FULLNAME,BASETYPE)
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMTYPES                                         */
/*==============================================================*/
CREATE TABLE FDE.FDB_ITEMTYPES
(
   ID                   INT NOT NULL PRIMARY KEY ,
   NAME                 NVARCHAR(255) NOT NULL,
   GUID                 uniqueidentifier NOT NULL,
   PARENTTYPEGUID       uniqueidentifier NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: AK_IDENTIFIER_2                            */
/*==============================================================*/
CREATE NONCLUSTERED INDEX AK_IDENTIFIER_2 ON FDE.FDB_ITEMTYPES
(
   NAME
)/*#*/;
/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
CREATE TABLE FDE.FDB_RENDERINDEX
(
   COLUMNID             INT NOT NULL PRIMARY KEY ,
   INDEXNAME            NVARCHAR(255) NOT NULL,
   CENTERX              FLOAT NOT NULL,
   CENTERY              FLOAT NOT NULL,
   L1                   FLOAT NOT NULL,
   L2                   FLOAT NOT NULL,
   L3                   FLOAT NOT NULL,
   RADIO                FLOAT NOT NULL,
   FB_TABLENAME			NVARCHAR(255) NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: AK_IDENTIFIER_2                            */
/*==============================================================*/
CREATE NONCLUSTERED INDEX AK_FDB_RENDERINDEX_UC_NAME ON FDE.FDB_RENDERINDEX
(
   INDEXNAME
)/*#*/;

/*==============================================================*/
/* Table: FDB_REPLICATION_STEP                                  */
/*==============================================================*/
CREATE TABLE FDE.FDB_REPLICATION_STEP
(
   ID                   INT NOT NULL PRIMARY KEY,
   DATASETID            INT NOT NULL,
   OPTYPE               INT NOT NULL,
   STEP                 INT NOT NULL,
   SUBSTEP              INT DEFAULT 0,
   DESCRIPTION          NVARCHAR(1024),
   ISDONE               INT NOT NULL,
   DATA                 NVARCHAR(max)
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
CREATE TABLE FDE.FDB_SERVERCONFIG
(
   PARAMETERNAME        NVARCHAR(128) NOT NULL PRIMARY KEY,
   PARAMETERVALUE       NVARCHAR(255) NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
CREATE TABLE FDE.FDB_SERVERINFO
(
   DATABASENAME         NVARCHAR(255),
   INSTANCENAME         NVARCHAR(255),
   USERNAME             NVARCHAR(255) NOT NULL,
   CREATETIME           DATETIME2(7) NOT NULL,
   FDBVERSIONMAJOR      INT NOT NULL,
   FDBVERSIONMINOR      INT NOT NULL,
   FDBVERSIONBUGFIX     INT NOT NULL,
   FDEPROVIDERNAME      NVARCHAR(255) NOT NULL,
   FDEDESCRIPTION       NVARCHAR(1024),
   UID                  uniqueidentifier NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
CREATE TABLE FDE.FDB_TABLE_REGISTRY
(
   TABLEREGISTRATION_ID INT NOT NULL PRIMARY KEY ,
   CLASSID              INT,
   DATABASENAME         NVARCHAR(32) NOT NULL,
   SCHEMANAME           NVARCHAR(32) NOT NULL,
   TABLENAME            NVARCHAR(32) NOT NULL,
   TABLETYPE            INT NOT NULL,
   ISSYSTEMTABLE        INT NOT NULL,
   DESCRIPTION          NVARCHAR(1024),
   REGISTRATIONDATE     DATETIME2(7) NOT NULL,
   LASTUPDATETIME       DATETIME2(7) NOT NULL,
   CONSTRAINT AK_UK_TABLE_REGISTRY UNIQUE(DATABASENAME, SCHEMANAME,TABLENAME)
)/*#*/;


/*==============================================================*/
/* Table: FDE_PROCESS_INFORMATION                               */
/*==============================================================*/
CREATE TABLE FDE.FDE_PROCESS_INFORMATION
(
   FDEID                INT IDENTITY(1,1) NOT NULL PRIMARY KEY ,
   SPID                 INT NOT NULL,
   PID                  INT NOT NULL,
   STARTTIME            DATETIME2(7) NOT NULL,
   OWNER                NVARCHAR(32) NOT NULL,
   HOST                 NVARCHAR(128) NOT NULL,
   DIRECTCONNECT        NCHAR(1) NOT NULL,
   TMP_TABLENAME        NVARCHAR(128) NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_CHECKOUTINFO
(
   ID                   INT NOT NULL PRIMARY KEY,
   DATASETID            INT,
   CHECKOUTNAME         NVARCHAR(40) NOT NULL,
   CHECKOUTDATETIME     DATETIME2(7) NOT NULL,
   BCHECKOUT            INT NOT NULL,
   BMASTER              INT NOT NULL DEFAULT 0,
   CONN                 NVARCHAR(1024),
   CONSTRAINT AK_CHECKOUTNAME UNIQUE(CHECKOUTNAME)
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_FULLREPLICATIONTABLE                          */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_FULLREPLICATIONTABLE
(
   ID                   INT NOT NULL PRIMARY KEY,
   REGISTERCLASSID      INT NOT NULL,
   DATASETID            INT NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_TABLE_MODIFIED                                */
/*==============================================================*/
CREATE TABLE FDE.FDE_REP_TABLE_MODIFIED
(
   ID                   INT NOT NULL PRIMARY KEY,
   TABLEREGISTERID      INT NOT NULL,
   FIELDID              INT NOT NULL,
   OPTYPE               INT NOT NULL,
   BKEEP                INT NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
CREATE TABLE FDE.FDE_SPATIAL_REFERENCES
(
   SRID                 INT NOT NULL PRIMARY KEY ,
   DESCRIPTION          NVARCHAR(1024),
   AUTH_NAME            NVARCHAR(255),
   AUTH_SRID            INT,
   FALSEX               FLOAT NOT NULL,
   FALSEY               FLOAT NOT NULL,
   XYUNITS              FLOAT NOT NULL,
   FALSEZ               FLOAT NOT NULL,
   ZUNITS               FLOAT NOT NULL,
   FALSEM               FLOAT NOT NULL,
   MUNITS               FLOAT NOT NULL,
   XYCLUSTER_TOL        FLOAT,
   ZCLUSTER_TOL         FLOAT,
   MCLUSTER_TOL         FLOAT,
   SRTEXT               NVARCHAR(1024) NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE.FDE_TABLE_LOCKS
(
   FDEID                INT NOT NULL,
   LOCKTYPE             NCHAR(1) NOT NULL,
   CLASSID              INT NOT NULL,
   PRIMARY KEY (FDEID, LOCKTYPE, CLASSID)
)/*#*/;

/*==============================================================*/
/* Table: FDE_SYSTEM_IDS                                       */
/*==============================================================*/
CREATE TABLE FDE.FDE_SYSTEM_IDS
(
   TYPE                 INT NOT NULL PRIMARY KEY,
   STARTID              INT NOT NULL DEFAULT 1,
   DESCRIPTION          NVARCHAR(128) NOT NULL
)/*#*/;


/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
CREATE TABLE FDE.FDE_OBJECT_LOCKS
(
   FDEID                INT NOT NULL,
   LOCKTYPE             NCHAR(1) NOT NULL DEFAULT 'E',
   CLASSID              INT NOT NULL,
   OBJECTID				INT NOT NULL,
   PRIMARY KEY (FDEID, LOCKTYPE, CLASSID,OBJECTID),
   CONSTRAINT AK_FDE_OBJECT_LOCKS UNIQUE(CLASSID,OBJECTID)
)/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY ADD CONSTRAINT FK_RELATIONSHIP_16 FOREIGN KEY (DOMAINID)
      REFERENCES FDE.FDB_ITEMS (ID) ON DELETE NO ACTION ON UPDATE NO ACTION/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY ADD CONSTRAINT FK_TABLECOLUMNLINK FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID)/*#*/;

ALTER TABLE FDE.FDB_GEOCOLUMN ADD CONSTRAINT FK_FDB_GEOCOLUMN FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID) ON DELETE CASCADE/*#*/;

ALTER TABLE FDE.FDB_GRIDINDEX ADD CONSTRAINT FK_GEOCOLUMNGRIDINDEXLINK FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID) ON DELETE CASCADE/*#*/;

ALTER TABLE FDE.FDB_OBJECTCLASSES ADD CONSTRAINT FK_FEATUREDATASETCLASSLINK FOREIGN KEY (DATASETID)
      REFERENCES FDE.FDB_FEATUREDATASET (DATASETID)/*#*/;

ALTER TABLE FDE.FDB_RENDERINDEX ADD CONSTRAINT FK_COLUMN_RENDERINDEX_PK_LINK FOREIGN KEY (COLUMNID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (COLUMNID) ON DELETE CASCADE ON UPDATE CASCADE/*#*/;

ALTER TABLE FDE.FDB_TABLE_REGISTRY ADD CONSTRAINT FK_OBJECTATTACHLINK FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID) ON DELETE CASCADE/*#*/;

ALTER TABLE FDE.FDE_REP_CHECKOUTINFO ADD CONSTRAINT FK_CHECKOUTDATASETLINK FOREIGN KEY (DATASETID)
      REFERENCES FDE.FDB_FEATUREDATASET (DATASETID) ON DELETE CASCADE/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS ADD CONSTRAINT FK_FDE_PROCESSINFO_TABLELOCKS FOREIGN KEY (FDEID)
      REFERENCES FDE.FDE_PROCESS_INFORMATION (FDEID)/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS ADD CONSTRAINT FK_TABLETABLELOCKLINK FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID) ON DELETE CASCADE/*#*/;

ALTER TABLE FDE.FDE_OBJECT_LOCKS ADD CONSTRAINT FK_OBJECTLOCKLINK FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (CLASSID) ON DELETE CASCADE/*#*/;
