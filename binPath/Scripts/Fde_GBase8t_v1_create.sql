/*==============================================================
/* DBMS name:      INFORMIX SQL 11.x
/* Created on:     2016/3/11 16:04:24
/*==============================================================


/*==============================================================
/* Table: FDB_COLUMN_REGISTRY
/*==============================================================
create table FDB_COLUMN_REGISTRY  (
  COLUMNID             SERIAL                          not null,
  DOMAINID             INTEGER,
  CLASSID              INTEGER                         not null,
  FIELDNAME            VARCHAR(255)                    not null,
  ALIASNAME            VARCHAR(255),
  DEFAULTVALUESTRING   LVARCHAR,
  DEFAULTVALUENUMBER   NUMERIC(32,8),
  ISNULLABLE           INTEGER                         not null,
  ISEDITABLE           INTEGER                         not null,
  ISREQUIRED           INTEGER,
  ISSYSTEMCOLUMN       INTEGER,
  ISRANDERINDEXFIELD   INTEGER                        default 0 not null,
  FDE_TYPE             INTEGER                         not null,
  COLUMN_SIZE          INTEGER                         not null,
  DECIMAL_DIGITS       INTEGER                         not null,
primary key (COLUMNID)
      constraint PK_FDB_COL_REG
)/*#*/;

/*==============================================================
/* Table: FDB_FEATUREDATASET
/*==============================================================
create table FDB_FEATUREDATASET  (
  DATASETID            SERIAL                          not null,
  DATASETNAME          VARCHAR(128)                    not null,
  DATASETALIASNAME     LVARCHAR,
  DATASETUUID          VARCHAR(40)                     not null,
  DATABASENAME         VARCHAR(32),
  SCHEMANAME           VARCHAR(32),
  DESCRIPTION          LVARCHAR,
  SRID                 INTEGER                         not null,
  TRID                 INTEGER                         not null,
  REGISTEROPTION       INTEGER                        default 0 not null,
  EDITVERSION          INTEGER                        default 0 not null,
  CREATETIME           DATETIME YEAR TO SECOND         not null,
  LASTUPDATETIME       DATETIME YEAR TO SECOND         not null,
primary key (DATASETID)
      constraint PK_FDB_FDT
)/*#*/;

/*==============================================================
/* Index: UK_FDB_FDT
/*==============================================================
create unique  index UK_FDB_FDT on FDB_FEATUREDATASET (
        DATASETNAME          ASC
)/*#*/;

/*==============================================================
/* Table: FDB_GEOCOLUMN
/*==============================================================
create table FDB_GEOCOLUMN  (
  COLUMNID             SERIAL                          not null,
  F_TABLENAME          VARCHAR(64)                     not null,
  AVGPOINTCNT          INTEGER                         not null,
  GEOTYPE              INTEGER                         not null,
  HASID                INTEGER                         not null,
  HASM                 INTEGER                         not null,
  HASZ                 INTEGER                         not null,
  STORAGETYPE          INTEGER                         not null,
  MAXX                 FLOAT                           not null,
  MAXY                 FLOAT                           not null,
  MAXZ                 FLOAT                           not null,
  MAXM                 FLOAT                           not null,
  MINX                 FLOAT                           not null,
  MINY                 FLOAT                           not null,
  MINZ                 FLOAT                           not null,
  MINM                 FLOAT                           not null,
primary key (COLUMNID)
      constraint PK_FDB_GEOCOLUMN
)/*#*/;

/*==============================================================
/* Table: FDB_GRIDINDEX
/*==============================================================
create table FDB_GRIDINDEX  (
  COLUMNID             INTEGER                         not null,
  S_TABLENAME          VARCHAR(64)                     not null,
  INDEXNAME            VARCHAR(255)                    not null,
  CENTERX              FLOAT                           not null,
  CENTERY              FLOAT                           not null,
  L1                   FLOAT                           not null,
  L2                   FLOAT                           not null,
  L3                   FLOAT                           not null,
  RADIO                FLOAT                           not null,
primary key (COLUMNID)
      constraint PK_FDB_GRIDINDEX
)/*#*/;

/*==============================================================
/* Table: FDB_ITEMS
/*==============================================================
create table FDB_ITEMS  (
  ID                   SERIAL                          not null,
  NAME                 VARCHAR(128)                    not null,
  FULLNAME             VARCHAR(255)                    not null,
  GUID                 VARCHAR(40)                     not null,
  TYPE                 VARCHAR(40)                     not null,
  BASETYPE             VARCHAR(40)                     not null,
  DEFAULTS             BYTE,
  DEFINITION           BYTE,
  DOCUMENTATION        BYTE,
primary key (ID)
      constraint PK_FDB_ITEMS,
unique (FULLNAME, BASETYPE)
      constraint AK_KEY_2_FDB_ITEMS
)/*#*/;

/*==============================================================
/* Table: FDB_ITEMTYPES
/*==============================================================
create table FDB_ITEMTYPES  (
  ID                   INTEGER                         not null,
  NAME                 VARCHAR(255)                    not null,
  GUID                 VARCHAR(40)                     not null,
  PARENTTYPEGUID       VARCHAR(40)                     not null,
primary key (ID)
      constraint PK_FDB_ITEMTYPES,
unique (NAME)
      constraint AK_KEY_2_ITEMTYPES
)/*#*/;

/*==============================================================
/* Table: FDB_OBJECTCLASSES
/*==============================================================
create table FDB_OBJECTCLASSES  (
  CLASSID              SERIAL                          not null,
  DATASETID            INTEGER                         not null,
  DATABASENAME         VARCHAR(32)                     not null,
  SCHEMANAME           VARCHAR(32)                     not null,
  CLASSNAME            VARCHAR(128)                    not null,
  ALIASNAME            VARCHAR(255)                    not null,
  CLSUUID              VARCHAR(40)                     not null,
  OIDCOLUMN            VARCHAR(32)                     not null,
  CLASSTYPE            INTEGER                         not null,
  DESCRIPTION          LVARCHAR,
  CREATETIME           DATETIME YEAR TO SECOND         not null,
  LASTUPDATETIME       DATETIME YEAR TO SECOND         not null,
  REGISTEROPTION       INTEGER                        default 0 not null,
primary key (CLASSID)
      constraint PK_FDB_OBJECTCLASS
)/*#*/;

/*==============================================================
/* Index: UK_FDB_OBJECTCLASS
/*==============================================================
create unique  index UK_FDB_OBJECTCLASS on FDB_OBJECTCLASSES (
        DATASETID            DESC,
        CLASSNAME            ASC
)/*#*/;

/*==============================================================
/* Table: FDB_RENDERINDEX
/*==============================================================
create table FDB_RENDERINDEX  (
  COLUMNID             INTEGER                         not null,
  INDEXNAME            VARCHAR(255)                    not null,
  CENTERX              FLOAT                           not null,
  CENTERY              FLOAT                           not null,
  L1                   FLOAT                           not null,
  L2                   FLOAT                           not null,
  L3                   FLOAT                           not null,
  RADIO                FLOAT                           not null,
  FB_TABLENAME         LVARCHAR                        not null,
primary key (COLUMNID)
      constraint PK_FDB_RENDERINDEX,
unique (INDEXNAME)
      constraint AK_KEY_2_FDB_RI
)/*#*/;

/*==============================================================
/* Table: FDB_REPLICATION_STEP
/*==============================================================
create table FDB_REPLICATION_STEP  (
  ID                   INTEGER                         not null,
  DATASETID            INTEGER                         not null,
  OPTYPE               INTEGER                         not null,
  STEP                 INTEGER                         not null,
  SUBSTEP              INTEGER                        default 0,
  DESCRIPTION          LVARCHAR,
  ISDONE               INTEGER                         not null,
  DATA                 LVARCHAR,
primary key (ID)
      constraint PK_FDB_REP_STEP
)/*#*/;

/*==============================================================
/* Table: FDB_SERVERCONFIG
/*==============================================================
create table FDB_SERVERCONFIG  (
  PARAMETERNAME        VARCHAR(128)                    not null,
  PARAMETERVALUE       VARCHAR(255)                    not null,
primary key (PARAMETERNAME)
      constraint PK_FDB_SERVERCONF
)/*#*/;

/*==============================================================
/* Table: FDB_SERVERINFO
/*==============================================================
create table FDB_SERVERINFO  (
  DATABASENAME         VARCHAR(255),
  INSTANCENAME         VARCHAR(255),
  USERNAME             VARCHAR(255)                    not null,
  CREATETIME           DATETIME YEAR TO SECOND         not null,
  FDBVERSIONMAJOR      INTEGER                         not null,
  FDBVERSIONMINOR      INTEGER                         not null,
  FDBVERSIONBUGFIX     INTEGER                         not null,
  FDEPROVIDERNAME      VARCHAR(255),
  FDEDESCRIPTION       LVARCHAR                        not null,
  UID                  VARCHAR(40)                     not null
)/*#*/;

/*==============================================================
/* Table: FDB_TABLE_REGISTRY
/*==============================================================
create table FDB_TABLE_REGISTRY  (
  TABLEREGISTRATION_ID SERIAL                          not null,
  CLASSID              INTEGER,
  DATABASENAME         VARCHAR(32)                     not null,
  SCHEMANAME           VARCHAR(32)                     not null,
  TABLENAME            VARCHAR(32)                     not null,
  TABLETYPE            INTEGER                         not null,
  ISSYSTEMTABLE        INTEGER                         not null,
  DESCRIPTION          LVARCHAR,
  REGISTRATIONDATE     DATETIME YEAR TO SECOND         not null,
  LASTUPDATETIME       DATETIME YEAR TO SECOND         not null,
primary key (TABLEREGISTRATION_ID)
      constraint PK_FDB_TABLE_REG,
unique (DATABASENAME, SCHEMANAME, TABLENAME)
      constraint AK_KEY_2_TAB_REG
)/*#*/;

/*==============================================================
/* Table: FDE_OBJECT_LOCKS
/*==============================================================
create table FDE_OBJECT_LOCKS  (
  FDEID                INTEGER                         not null,
  LOCKTYPE             VARCHAR(1)                     default "E" not null,
  CLASSID              INTEGER                         not null,
  OBJECTID             INTEGER                         not null,
primary key (FDEID, LOCKTYPE, CLASSID, OBJECTID)
      constraint PK_FDE_OBJECT_LOCK,
unique (CLASSID, OBJECTID)
      constraint AK_KEY_2_OBJ_LOCK
)/*#*/;

/*==============================================================
/* Table: FDE_PROCESS_INFORMATION
/*==============================================================
create table FDE_PROCESS_INFORMATION  (
  FDEID                INTEGER                         not null,
  SPID                 INTEGER                         not null,
  PID                  INTEGER                         not null,
  STARTTIME            DATETIME YEAR TO SECOND         not null,
  OWNER                VARCHAR(32)                     not null,
  HOST                 VARCHAR(128)                    not null,
  DIRECTCONNECT        VARCHAR(1)                      not null,
  TMP_TABLENAME        VARCHAR(128)                    not null,
primary key (FDEID)
      constraint PK_FDE_PROC_INFO
)/*#*/;

/*==============================================================
/* Table: FDE_REP_CHECKOUTINFO
/*==============================================================
create table FDE_REP_CHECKOUTINFO  (
  ID                   INTEGER                         not null,
  DATASETID            INTEGER,
  CHECKOUTNAME         VARCHAR(40)                     not null,
  CHECKOUTDATETIME     DATETIME YEAR TO SECOND         not null,
  BCHECKOUT            INTEGER                         not null,
  BMASTER              INTEGER                        default 0 not null,
  CONN                 LVARCHAR,
primary key (ID)
      constraint PK_FDE_REP_CHECK,
unique (CHECKOUTNAME)
      constraint AK_KEY_2_FDE_REP
)/*#*/;

/*==============================================================
/* Table: FDE_REP_FULLREPLICATIONTABLE
/*==============================================================
create table FDE_REP_FULLREPLICATIONTABLE (
  ID                   INTEGER                         not null,
  REGISTERCLASSID      INTEGER                         not null,
  DATASETID            INTEGER                         not null,
primary key (ID)
      constraint PK_FDE_REP_FULLREP
)/*#*/;

/*==============================================================
/* Table: FDE_REP_TABLE_MODIFIED
/*==============================================================
create table FDE_REP_TABLE_MODIFIED  (
  ID                   INTEGER                         not null,
  TABLEREGISTERID      INTEGER                         not null,
  FIELDID              INTEGER                         not null,
  OPTYPE               INTEGER                         not null,
  BKEEP                INTEGER                         not null,
primary key (ID)
      constraint PK_REP_TABLE_MODI
)/*#*/;

/*==============================================================
/* Table: FDE_SPATIAL_REFERENCES
/*==============================================================
create table FDE_SPATIAL_REFERENCES (
  SRID                 SERIAL                          not null,
  DESCRIPTION          LVARCHAR,
  AUTH_NAME            VARCHAR(255),
  AUTH_SRID            INTEGER,
  FALSEX               FLOAT                           not null,
  FALSEY               FLOAT                           not null,
  XYUNITS              FLOAT                           not null,
  FALSEZ               FLOAT                           not null,
  ZUNITS               FLOAT                           not null,
  FALSEM               FLOAT                           not null,
  MUNITS               FLOAT                           not null,
  XYCLUSTER_TOL        FLOAT,
  ZCLUSTER_TOL         FLOAT,
  MCLUSTER_TOL         FLOAT,
  SRTEXT               LVARCHAR,
primary key (SRID)
      constraint PK_FDE_SPATIAL_REF
)/*#*/;

/*==============================================================
/* Table: FDE_TABLE_LOCKS
/*==============================================================
create table FDE_TABLE_LOCKS  (
  FDEID                INTEGER                         not null,
  LOCKTYPE             VARCHAR(1)                      not null,
  CLASSID              INTEGER                         not null,
primary key (FDEID, LOCKTYPE, CLASSID)
      constraint PK_FDE_TABLE_LOCKS
)/*#*/;

alter table FDB_COLUMN_REGISTRY
   add constraint foreign key (DOMAINID)
      references FDB_ITEMS (ID) 
      constraint FK_COLREG_ITEMS/*#*/;

alter table FDB_COLUMN_REGISTRY
   add constraint foreign key (CLASSID)
      references FDB_OBJECTCLASSES (CLASSID) 
      constraint FK_COLREG_OC on delete cascade/*#*/;

alter table FDB_GEOCOLUMN
   add constraint foreign key (COLUMNID)
      references FDB_COLUMN_REGISTRY (COLUMNID) 
      constraint FK_GEOCOL_COLREG on delete cascade/*#*/;

alter table FDB_GRIDINDEX
   add constraint foreign key (COLUMNID)
      references FDB_COLUMN_REGISTRY (COLUMNID) 
      constraint FK_GRIDIDX_COLREG on delete cascade/*#*/;

alter table FDB_OBJECTCLASSES
   add constraint foreign key (DATASETID)
      references FDB_FEATUREDATASET (DATASETID) 
      constraint FK_OC_FDT on delete cascade/*#*/;

alter table FDB_RENDERINDEX
   add constraint foreign key (COLUMNID)
      references FDB_COLUMN_REGISTRY (COLUMNID) 
      constraint FK_RENDIDX_COLREG on delete cascade/*#*/;

alter table FDB_TABLE_REGISTRY
   add constraint foreign key (CLASSID)
      references FDB_OBJECTCLASSES (CLASSID) 
      constraint FK_TABREG_OC on delete cascade/*#*/;

alter table FDE_OBJECT_LOCKS
   add constraint foreign key (CLASSID)
      references FDB_OBJECTCLASSES (CLASSID) 
      constraint FK_OCLOCKS_OC on delete cascade/*#*/;

alter table FDE_REP_CHECKOUTINFO
   add constraint foreign key (DATASETID)
      references FDB_FEATUREDATASET (DATASETID) 
      constraint FK_REPCHECKOUT_FDT/*#*/;

alter table FDE_TABLE_LOCKS
   add constraint foreign key (CLASSID)
      references FDB_OBJECTCLASSES (CLASSID) 
      constraint FK_TABLOCKS_OC on delete cascade/*#*/;

alter table FDE_TABLE_LOCKS
   add constraint foreign key (FDEID)
      references FDE_PROCESS_INFORMATION (FDEID) 
      constraint FK_TABLOCKS_PRO/*#*/;

