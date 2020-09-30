/*==============================================================*/
/* DBMS name:      SQLite 3                                     */
/* Created on:     2011-12-29 16:30:01                          */
/*==============================================================*/

DROP TABLE IF EXISTS FDB_GRIDINDEX/*#*/;

DROP TABLE IF EXISTS FDB_GEOCOLUMN/*#*/;

DROP TABLE IF EXISTS FDB_COLUMN_REGISTRY/*#*/;

DROP TABLE IF EXISTS FDB_ITEMS/*#*/;

DROP TABLE IF EXISTS FDB_ITEMTYPES/*#*/;

DROP TABLE IF EXISTS FDB_TABLE_REGISTRY/*#*/;

DROP TABLE IF EXISTS FDE_TABLE_LOCKS/*#*/;

DROP TABLE IF EXISTS FDB_OBJECTCLASSES/*#*/;

DROP TABLE IF EXISTS FDB_FEATUREDATASET/*#*/;

DROP TABLE IF EXISTS FDB_RENDERINDEX/*#*/;

DROP TABLE IF EXISTS FDB_REPLICATION_STEP/*#*/;

DROP TABLE IF EXISTS FDB_SERVERCONFIG/*#*/;

DROP TABLE IF EXISTS FDB_SERVERINFO/*#*/;

DROP TABLE IF EXISTS FDE_CONNECTIONINFO/*#*/;

DROP TABLE IF EXISTS FDE_PROCESS_INFORMATION/*#*/;

DROP TABLE IF EXISTS FDE_REP_CHECKOUTINFO/*#*/;

DROP TABLE IF EXISTS FDE_REP_FULLREPLICATIONTABLE/*#*/;

DROP TABLE IF EXISTS FDE_REP_TABLE_MODIFIED/*#*/;

DROP TABLE IF EXISTS FDE_SPATIAL_REFERENCES/*#*/;



/*==============================================================*/
/* Table: FDB_ITEMS                                   */
/*==============================================================*/
CREATE TABLE FDB_ITEMS
(
   ID					INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
   NAME					VARCHAR(128) NOT NULL,
   FULLNAME				VARCHAR(255) NOT NULL,
   GUID					VARCHAR(40) NOT NULL,
   TYPE					VARCHAR(40) NOT NULL,
   BASETYPE				VARCHAR(40) NOT NULL,
   DEFAULTS				BLOB,
   DEFINITION           BLOB,
   DOCUMENTATION        BLOB
)/*#*/;

/*==============================================================*/
/* Index: UK_UK_ITEMS_NAME_TYPE                                 */
/*==============================================================*/
CREATE UNIQUE INDEX UK_UK_ITEMS_NAME_TYPE ON FDB_ITEMS
(
   FULLNAME, BASETYPE
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMTYPES                                   */
/*==============================================================*/
CREATE TABLE FDB_ITEMTYPES
(
   ID					INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
   NAME					VARCHAR(255) NOT NULL,
   GUID					VARCHAR(40) NOT NULL,
   PARENTTYPEGUID		VARCHAR(40) NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: UK_ITEMTYPES                                 */
/*==============================================================*/
CREATE UNIQUE INDEX UK_ITEMTYPES ON FDB_ITEMTYPES
(
   NAME
)/*#*/;

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
CREATE TABLE FDB_FEATUREDATASET
(
   DATASETID            INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
   DATASETNAME          VARCHAR(128) NOT NULL,
   DATASETALIASNAME     VARCHAR(255),
   DATASETUUID          VARCHAR(40) NOT NULL,
   DATABASENAME         VARCHAR(32) NOT NULL,
   SCHEMANAME           VARCHAR(32) NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   SRID                 INTEGER NOT NULL,
   TRID                 INTEGER NOT NULL,
   REGISTEROPTION       INTEGER NOT NULL DEFAULT 0,
   EDITVERSION          INTEGER NOT NULL DEFAULT 0,
   CREATETIME           TIMESTAMP NOT NULL,
   LASTUPDATETIME       TIMESTAMP NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: UK_DTNAME_DBNAME                                         */
/*==============================================================*/
CREATE UNIQUE INDEX UK_DTNAME_DBNAME ON FDB_FEATUREDATASET
(
   DATASETNAME, DATABASENAME
)/*#*/;


/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
CREATE TABLE FDB_SERVERCONFIG
(
   PARAMETERNAME        VARCHAR(128) NOT NULL,
   PARAMETERVALUE       VARCHAR(255) NOT NULL,
   PRIMARY KEY (PARAMETERNAME)
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
CREATE TABLE FDB_SERVERINFO
(
   DATABASENAME         VARCHAR(255),
   INSTANCENAME         VARCHAR(255),
   USERNAME             VARCHAR(255) NOT NULL,
   CREATETIME           TIMESTAMP NOT NULL,
   FDBVERSIONMAJOR      INTEGER NOT NULL,
   FDBVERSIONMINOR      INTEGER NOT NULL,
   FDBVERSIONBUGFIX     INTEGER NOT NULL,
   FDEPROVIDERNAME      VARCHAR(255) NOT NULL,
   FDEDESCRIPTION       VARCHAR(1024),
   UID                  VARCHAR(64) NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
CREATE TABLE FDB_TABLE_REGISTRY
(
   TABLEREGISTRATION_ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
   CLASSID              INTEGER,
   DATABASENAME         VARCHAR(32) NOT NULL,
   SCHEMANAME           VARCHAR(32) NOT NULL,
   TABLENAME            VARCHAR(32) NOT NULL,
   TABLETYPE            INTEGER NOT NULL,
   ISSYSTEMTABLE        INTEGER NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   REGISTRATIONDATE     TIMESTAMP NOT NULL,
   LASTUPDATETIME       TIMESTAMP NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
CREATE TABLE FDE_SPATIAL_REFERENCES
(
   SRID                 INTEGER NOT NULL  PRIMARY KEY AUTOINCREMENT,
   DESCRIPTION          VARCHAR(1024),
   AUTH_NAME            VARCHAR(255),
   AUTH_SRID            INTEGER,
   FALSEX               DOUBLE NOT NULL,
   FALSEY               DOUBLE NOT NULL,
   XYUNITS              DOUBLE NOT NULL,
   FALSEZ               DOUBLE NOT NULL,
   ZUNITS               DOUBLE NOT NULL,
   FALSEM               DOUBLE NOT NULL,
   MUNITS               DOUBLE NOT NULL,
   XYCLUSTER_TOL        DOUBLE,
   ZCLUSTER_TOL         DOUBLE,
   MCLUSTER_TOL         DOUBLE,
   SRTEXT               VARCHAR(1024) NOT NULL
)/*#*/;


/*==============================================================*/
/* Table: FDB_OBJECTCLASSES                                     */
/*==============================================================*/
CREATE TABLE FDB_OBJECTCLASSES
(
   CLASSID              INTEGER NOT NULL  PRIMARY KEY AUTOINCREMENT,
   DATASETID            INTEGER NOT NULL,
   DATABASENAME         VARCHAR(32) NOT NULL,
   SCHEMANAME           VARCHAR(32) NOT NULL,
   CLASSNAME            VARCHAR(128) NOT NULL,
   ALIASNAME            VARCHAR(255) NOT NULL,
   CLSUUID              VARCHAR(40) NOT NULL,
   OIDCOLUMN            VARCHAR(32) NOT NULL,
   CLASSTYPE            INTEGER NOT NULL,
   DESCRIPTION          VARCHAR(1024),
   CREATETIME           TIMESTAMP NOT NULL,
   LASTUPDATETIME       TIMESTAMP NOT NULL,
   REGISTEROPTION       INTEGER NOT NULL DEFAULT 0
)/*#*/;

/*==============================================================*/
/* Index: AK_UK_TNAME_DBNAME                                    */
/*==============================================================*/
CREATE  INDEX AK_UK_TNAME_DBNAME ON FDB_OBJECTCLASSES
(
   DATABASENAME, CLASSNAME
)/*#*/;

/*==============================================================*/
/* Table: FDB_COLUMN_REGISTRY                                   */
/*==============================================================*/
CREATE TABLE FDB_COLUMN_REGISTRY
(
   COLUMNID             INTEGER NOT NULL  PRIMARY KEY AUTOINCREMENT,
   DOMAINID             INTEGER,
   CLASSID              INTEGER NOT NULL,
   FIELDNAME            VARCHAR(255) NOT NULL,
   ALIASNAME            VARCHAR(255),
   DEFAULTVALUESTRING   VARCHAR(255),
   DEFAULTVALUENUMBER   NUMERIC(38,8),
   ISNULLABLE           INTEGER NOT NULL,
   ISEDITABLE           INTEGER NOT NULL,
   ISREQUIRED           INTEGER,
   ISSYSTEMCOLUMN       INTEGER,
   ISRANDERINDEXFIELD   INTEGER NOT NULL DEFAULT 0,
   FDE_TYPE             INTEGER NOT NULL,
   COLUMN_SIZE          INTEGER NOT NULL,
   DECIMAL_DIGITS       INTEGER NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_GEOCOLUMN                                         */
/*==============================================================*/
CREATE TABLE FDB_GEOCOLUMN
(
   COLUMNID             INTEGER NOT NULL  PRIMARY KEY,
   F_TABLENAME          VARCHAR(64) NOT NULL,
   AVGPOINTCNT          INTEGER NOT NULL,
   GEOTYPE              INTEGER NOT NULL,
   HASID                INTEGER NOT NULL,
   HASM                 INTEGER NOT NULL,
   HASZ                 INTEGER NOT NULL,
   STORAGETYPE          INTEGER NOT NULL,
   MAXX                 DOUBLE NOT NULL,
   MAXY                 DOUBLE NOT NULL,
   MAXZ                 DOUBLE NOT NULL,
   MAXM                 DOUBLE NOT NULL,
   MINX                 DOUBLE NOT NULL,
   MINY                 DOUBLE NOT NULL,
   MINZ                 DOUBLE NOT NULL,
   MINM                 DOUBLE NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
CREATE TABLE FDB_GRIDINDEX
(
   COLUMNID             INTEGER NOT NULL PRIMARY KEY,
   S_TABLENAME          VARCHAR(64) NOT NULL,
   INDEXNAME            VARCHAR(255) NOT NULL,
   CENTERX              DOUBLE NOT NULL,
   CENTERY              DOUBLE NOT NULL,
   L1                   DOUBLE NOT NULL,
   L2                   DOUBLE NOT NULL,
   L3                   DOUBLE NOT NULL,
   RADIO                DOUBLE NOT NULL
)/*#*/;

/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
CREATE TABLE FDB_RENDERINDEX
(
   COLUMNID             INTEGER NOT NULL PRIMARY KEY,
   INDEXNAME            VARCHAR(255) NOT NULL,
   CENTERX              DOUBLE NOT NULL,
   CENTERY              DOUBLE NOT NULL,
   L1                   DOUBLE NOT NULL,
   L2                   DOUBLE NOT NULL,
   L3                   DOUBLE NOT NULL,
   RADIO                DOUBLE NOT NULL,
   FB_TABLENAME			VARCHAR(255) NOT NULL
)/*#*/;

/*==============================================================*/
/* Index: AK_UC_FDB_GRIDINDEX_NAME                              */
/*==============================================================*/
CREATE  UNIQUE INDEX AK_UC_FDB_GRIDINDEX_NAME ON FDB_GRIDINDEX
(
   INDEXNAME
)/*#*/;

/*==============================================================*/
/* Index: AK_UC_FDB_GRIDINDEX_STNAME                            */
/*==============================================================*/
CREATE   INDEX AK_UC_FDB_GRIDINDEX_STNAME ON FDB_GRIDINDEX
(
   S_TABLENAME
)/*#*/;



/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
CREATE TABLE FDE_REP_CHECKOUTINFO
(
   ID                   INTEGER NOT NULL  PRIMARY KEY AUTOINCREMENT,
   DATASETID            INTEGER,
   CHECKOUTNAME         VARCHAR(40) NOT NULL,
   CHECKOUTDATETIME     TIMESTAMP NOT NULL,
   BCHECKOUT            INTEGER NOT NULL,
   BMASTER              INTEGER NOT NULL DEFAULT 0,
   CONN                 VARCHAR(1024),
   UNIQUE   (CHECKOUTNAME)
)/*#*/;
