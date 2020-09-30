/*==============================================================*/
/* DBMS name:      InterBase 6.x                                */
/* Created on:     2012/9/18 16:27:59                           */
/*==============================================================*/

create generator S_FDB_COLUMN_REGISTRY/*#*/;

create generator S_FDB_FEATUREDATASET/*#*/;

create generator S_FDB_ITEMS/*#*/;

create generator S_FDB_ITEMTYPES/*#*/;

create generator S_FDB_OBJECTCLASSES/*#*/;

create generator S_FDB_REPLICATION_STEP/*#*/;

create generator S_FDB_TABLE_REGISTRY/*#*/;

create generator S_FDE_REP_CHECKOUTINFO/*#*/;

create generator S_FDE_REP_FULLREPLICATIONTABLE/*#*/;

create generator S_FDE_SPATIAL_REFERENCES/*#*/;

/*==============================================================*/
/* Table: FDB_FEATUREDATASET                                    */
/*==============================================================*/
create table FDB_FEATUREDATASET (
DatasetID            INTEGER                        not null,
DatasetName          VARCHAR(128)                   not null,
DatasetAliasName     VARCHAR(255),
DatasetUUID          CHAR(40) CHARACTER SET ASCII   not null,
DatabaseName         VARCHAR(32),
SchemaName           VARCHAR(32),
Description          VARCHAR(1024),
SRID                 INTEGER                        not null,
TRID                 INTEGER                        not null,
RegisterOption       INTEGER                        default 0 not null,
EditVersion          INTEGER                        default 0 not null,
CreateTime           TIMESTAMP                      not null,
LastUpdateTime       TIMESTAMP                      not null,
constraint PK_FDB_FEATUREDATASET primary key (DatasetID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_FEATUREDATASET_UK_NAME                            */
/*==============================================================*/
create unique asc index FDB_FEATUREDATASET_UK_NAME on FDB_FEATUREDATASET (
DatasetName
)/*#*/;

/*==============================================================*/
/* Table: FDB_OBJECTCLASSES                                     */
/*==============================================================*/
create table FDB_OBJECTCLASSES (
ClassID              INTEGER                        not null,
DatasetID            INTEGER                        not null,
DatabaseName         VARCHAR(32)                    not null,
SchemaName           VARCHAR(32)                    not null,
ClassName            VARCHAR(128)                   not null,
AliasName            VARCHAR(255)                   not null,
ClsUUID              CHAR(40) CHARACTER SET ASCII   not null,
OidColumn            VARCHAR(32)                    not null,
ClassType            INTEGER                        not null,
Description          VARCHAR(1024),
CreateTime           TIMESTAMP                      not null,
LastUpdateTime       TIMESTAMP                      not null,
RegisterOption       INTEGER                        default 0 not null,
constraint PK_FDB_OBJECTCLASSES primary key (ClassID)
)/*#*/;

/*==============================================================*/
/* Index: FDB_OBJECTCLASS_UK_NAME                               */
/*==============================================================*/
create unique asc index FDB_OBJECTCLASS_UK_NAME on FDB_OBJECTCLASSES (
DatasetID,
ClassName
)/*#*/;

/*==============================================================*/
/* Table: FDB_COLUMN_REGISTRY                                   */
/*==============================================================*/
create table FDB_COLUMN_REGISTRY (
ColumnID             INTEGER                        not null,
DomainID             INTEGER,
ClassID              INTEGER                        not null,
FieldName            VARCHAR(255)                   not null,
AliasName            VARCHAR(255),
DefaultValueString   VARCHAR(255),
DefaultValueNumber   NUMERIC(18,8),
IsNullable           INTEGER                        not null,
IsEditable           INTEGER                        not null,
IsRequired           INTEGER,
IsSystemColumn       INTEGER,
IsRanderIndexField   INTEGER                        default 0 not null,
Fde_type             INTEGER                        not null,
Column_size          INTEGER                        not null,
Decimal_digits       INTEGER                        not null,
constraint PK_FDB_COLUMN_REGISTRY primary key (ColumnID),
constraint AK_FDB_COLUMN_REGISTR_FDB_COLU unique (ClassID, FieldName)
)/*#*/;

/*==============================================================*/
/* Table: FDB_GEOCOLUMN                                         */
/*==============================================================*/
create table FDB_GEOCOLUMN (
ColumnID             INTEGER                        not null,
F_TableName          VARCHAR(64)                    not null,
AvgPointCnt          INTEGER                        not null,
GeoType              INTEGER                        not null,
HasID                INTEGER                        not null,
HasM                 INTEGER                        not null,
HasZ                 INTEGER                        not null,
StorageType          INTEGER                        not null,
MaxX                 DOUBLE PRECISION               not null,
MaxY                 DOUBLE PRECISION               not null,
MaxZ                 DOUBLE PRECISION               not null,
MaxM                 DOUBLE PRECISION               not null,
MinX                 DOUBLE PRECISION               not null,
MinY                 DOUBLE PRECISION               not null,
MinZ                 DOUBLE PRECISION               not null,
MinM                 DOUBLE PRECISION               not null,
constraint PK_FDB_GEOCOLUMN primary key (ColumnID)
)/*#*/;

/*==============================================================*/
/* Table: FDB_GRIDINDEX                                         */
/*==============================================================*/
create table FDB_GRIDINDEX (
ColumnID             INTEGER                        not null,
IndexName            VARCHAR(255)                   not null,
S_TableName          VARCHAR(32)                    not null,
CenterX              DOUBLE PRECISION               not null,
CenterY              DOUBLE PRECISION               not null,
L1                   DOUBLE PRECISION               not null,
L2                   DOUBLE PRECISION               not null,
L3                   DOUBLE PRECISION               not null,
Radio                DOUBLE PRECISION               not null,
constraint PK_FDB_GRIDINDEX primary key (ColumnID),
constraint AK_FDB_GRIDINDEX_UC_S_FDB_GRID unique (S_TableName),
constraint AK_FDB_GRIDINDEX_UC_N_FDB_GRID unique (IndexName)
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMS                                             */
/*==============================================================*/
create table FDB_ITEMS (
ID                   INTEGER                        not null,
NAME                 VARCHAR(128)                   not null,
FULLNAME             VARCHAR(255)                   not null,
GUID                 VARCHAR(40)                    not null,
TYPE                 VARCHAR(40)                    not null,
BASETYPE             VARCHAR(40)                    not null,
DEFAULTS             BLOB SUB_TYPE BINARY,
DEFINITION           BLOB SUB_TYPE BINARY,
DOCUMENTATION        BLOB SUB_TYPE BINARY,
constraint PK_FDB_ITEMS primary key (ID),
constraint AK_FDB_FDB_ITEMS_COLU unique (FULLNAME, BASETYPE)
)/*#*/;

/*==============================================================*/
/* Index: UK_ITEMTYPES_FNAME_TYPE                               */
/*==============================================================*/
create asc index UK_ITEMTYPES_FNAME_TYPE on FDB_ITEMS (
FULLNAME,
TYPE
)/*#*/;

/*==============================================================*/
/* Table: FDB_ITEMTYPES                                         */
/*==============================================================*/
create table FDB_ITEMTYPES (
ID                   INTEGER                        not null,
NAME                 VARCHAR(255)                   not null,
GUID                 VARCHAR(40)                    not null,
PARENTTYPEGUID       VARCHAR(40)                    not null,
constraint PK_FDB_ITEMTYPES primary key (ID),
constraint AK_IDENTIFIER_2_FDB_ITEM unique (NAME)
)/*#*/;

/*==============================================================*/
/* Table: FDB_RENDERINDEX                                       */
/*==============================================================*/
create table FDB_RENDERINDEX (
ColumnID             INTEGER                        not null,
IndexName            VARCHAR(255)                   not null,
CenterX              DOUBLE PRECISION               not null,
CenterY              DOUBLE PRECISION               not null,
L1                   DOUBLE PRECISION               not null,
L2                   DOUBLE PRECISION               not null,
L3                   DOUBLE PRECISION               not null,
Radio                DOUBLE PRECISION               not null,
FB_TABLENAME		 VARCHAR(255)					not null,
constraint PK_FDB_RENDERINDEX primary key (ColumnID),
constraint AK_FDB_GRIDINDEX_UC_N_FDB_REND unique (IndexName)
)/*#*/;

/*==============================================================*/
/* Table: FDB_REPLICATION_STEP                                  */
/*==============================================================*/
create table FDB_REPLICATION_STEP (
ID                   INTEGER                        not null,
DATASETID            INTEGER                        not null,
OpType               INTEGER                        not null,
STEP                 INTEGER                        not null,
SUBSTEP              INTEGER                        default 0,
DESCRIPTION          VARCHAR(1024),
ISDONE               INTEGER                        not null,
DATA                 BLOB SUB_TYPE BINARY,
constraint PK_FDB_REPLICATION_STEP primary key (ID)
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERCONFIG                                      */
/*==============================================================*/
create table FDB_SERVERCONFIG (
PARAMETERNAME        VARCHAR(128)                   not null,
PARAMETERVALUE       VARCHAR(255)                   not null,
constraint PK_FDB_SERVERCONFIG primary key (PARAMETERNAME)
)/*#*/;

/*==============================================================*/
/* Table: FDB_SERVERINFO                                        */
/*==============================================================*/
create table FDB_SERVERINFO (
DataBaseName         VARCHAR(255),
InstanceName         VARCHAR(255),
UserName             VARCHAR(255)                   not null,
CreateTime           TIMESTAMP                      not null,
FDBVersionMajor      INTEGER                        not null,
FDBVersionMinor      INTEGER                        not null,
FDBVersionBugfix     INTEGER                        not null,
FDEProviderName      VARCHAR(255)                   not null,
FDEDescription       VARCHAR(1024),
UID                  VARCHAR(64)                    not null
)/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_REGISTRY                                    */
/*==============================================================*/
create table FDB_TABLE_REGISTRY (
TableRegistration_id INTEGER                        not null,
ClassID              INTEGER,
DatabaseName         VARCHAR(32)                    not null,
SchemaName           VARCHAR(32)                    not null,
TableName            VARCHAR(32)                    not null,
TableType            INTEGER                        not null,
isSystemTable        INTEGER                        not null,
Description          VARCHAR(1024),
RegistrationDate     TIMESTAMP                      not null,
LastUpdateTime       TIMESTAMP                      not null,
constraint PK_FDB_TABLE_REGISTRY primary key (TableRegistration_id)
)/*#*/;

/*==============================================================*/
/* Table: FDB_TABLE_SEQ_LINK                                    */
/*==============================================================*/
create table FDB_TABLE_SEQ_LINK (
TableRegistration_id INTEGER                        not null,
SeqName              VARCHAR(32)                    not null,
constraint PK_FDB_TABLE_SEQ_LINK primary key (TableRegistration_id)
)/*#*/;

/*==============================================================*/
/* Table: FDE_PROCESS_INFORMATION                               */
/*==============================================================*/
create table FDE_PROCESS_INFORMATION (
fdeid                INTEGER                        not null,
spid                 INTEGER                        not null,
pid                  INTEGER                        not null,
startTime            TIMESTAMP                      not null,
owner                VARCHAR(32)                    not null,
directconnect        CHAR(1)                        not null,
constraint PK_FDE_PROCESS_INFORMATION primary key (fdeid)
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_CHECKOUTINFO                                  */
/*==============================================================*/
create table FDE_REP_CHECKOUTINFO (
id                   INTEGER                        not null,
DatasetID            INTEGER,
checkoutName         VARCHAR(40)                    not null,
checkoutDatetime     TIMESTAMP                      not null,
bCheckout            INTEGER                        not null,
bMaster              INTEGER                        default 0 not null,
Conn                 VARCHAR(1024),
constraint PK_FDE_REP_CHECKOUTINFO primary key (id),
constraint AK_CHECKOUTNAME_FDE_REP_ unique (checkoutName)
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_FULLREPLICATIONTABLE                          */
/*==============================================================*/
create table FDE_REP_FULLREPLICATIONTABLE (
id                   INTEGER                        not null,
registerclassid      INTEGER                        not null,
datasetid            INTEGER                        not null,
constraint PK_FDE_REP_FULLREPLICATIONTABL primary key (id)
)/*#*/;

/*==============================================================*/
/* Table: FDE_REP_TABLE_MODIFIED                                */
/*==============================================================*/
create table FDE_REP_TABLE_MODIFIED (
id                   INTEGER                        not null,
tableregisterID      INTEGER                        not null,
fieldId              INTEGER                        not null,
optype               INTEGER                        not null,
bKeep                INTEGER                        not null,
constraint PK_FDE_REP_TABLE_MODIFIED primary key (id)
)/*#*/;

/*==============================================================*/
/* Table: FDE_SPATIAL_REFERENCES                                */
/*==============================================================*/
create table FDE_SPATIAL_REFERENCES (
SRID                 INTEGER                        not null,
Description          VARCHAR(1024),
auth_name            VARCHAR(255),
auth_srid            INTEGER,
falsex               DOUBLE PRECISION               not null,
falsey               DOUBLE PRECISION               not null,
xyunits              DOUBLE PRECISION               not null,
falsez               DOUBLE PRECISION               not null,
zunits               DOUBLE PRECISION               not null,
falsem               DOUBLE PRECISION               not null,
munits               DOUBLE PRECISION               not null,
xycluster_tol        DOUBLE PRECISION,
zcluster_tol         DOUBLE PRECISION,
mcluster_tol         DOUBLE PRECISION,
srtext               VARCHAR(1024)                  not null,
constraint PK_FDE_SPATIAL_REFERENCES primary key (SRID)
)/*#*/;

/*==============================================================*/
/* Table: FDE_TABLE_LOCKS                                       */
/*==============================================================*/
create table FDE_TABLE_LOCKS (
fdeid                INTEGER                        not null,
locktype             CHAR(1)                        not null,
ClassID              INTEGER                        not null,
constraint PK_FDE_TABLE_LOCKS primary key (fdeid, locktype, ClassID)
)/*#*/;

alter table FDB_COLUMN_REGISTRY
   add constraint FK_FDB_COLU_RELATIONS_FDB_ITEM foreign key (DomainID)
      references FDB_ITEMS (ID)/*#*/;

alter table FDB_COLUMN_REGISTRY
   add constraint FK_FDB_COLU_TABLECOLU_FDB_OBJE foreign key (ClassID)
      references FDB_OBJECTCLASSES (ClassID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_GEOCOLUMN
   add constraint FK_FDB_GEOC_FDB_GEOCO_FDB_COLU foreign key (ColumnID)
      references FDB_COLUMN_REGISTRY (ColumnID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_GRIDINDEX
   add constraint FK_FDB_GRID_GEOCOLUMN_FDB_COLU foreign key (ColumnID)
      references FDB_COLUMN_REGISTRY (ColumnID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_OBJECTCLASSES
   add constraint FK_FDB_OBJE_FEATUREDA_FDB_FEAT foreign key (DatasetID)
      references FDB_FEATUREDATASET (DatasetID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_RENDERINDEX
   add constraint FK_FDB_REND_COLUMN_RE_FDB_COLU foreign key (ColumnID)
      references FDB_COLUMN_REGISTRY (ColumnID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_TABLE_REGISTRY
   add constraint FK_FDB_TABL_OBJECTATT_FDB_OBJE foreign key (ClassID)
      references FDB_OBJECTCLASSES (ClassID)
      on delete cascade
      on update cascade/*#*/;

alter table FDB_TABLE_SEQ_LINK
   add constraint FK_FDB_TABL_FDB_TABLE_FDB_TABL foreign key (TableRegistration_id)
      references FDB_TABLE_REGISTRY (TableRegistration_id)
      on delete cascade
      on update cascade/*#*/;

alter table FDE_REP_CHECKOUTINFO
   add constraint FK_FDE_REP__CHECKOUTD_FDB_FEAT foreign key (DatasetID)
      references FDB_FEATUREDATASET (DatasetID)/*#*/;

alter table FDE_TABLE_LOCKS
   add constraint FK_FDE_TABL_FDE_PROCE_FDE_PROC foreign key (fdeid)
      references FDE_PROCESS_INFORMATION (fdeid)/*#*/;

alter table FDE_TABLE_LOCKS
   add constraint FK_FDE_TABL_TABLETABL_FDB_OBJE foreign key (ClassID)
      references FDB_OBJECTCLASSES (ClassID)/*#*/;


SET TERM ^;
create trigger ti_fdb_column_registry for FDB_COLUMN_REGISTRY
before insert as
declare variable numrows integer;
begin
    /*  Column "ColumnID" uses sequence S_FDB_COLUMN_REGISTRY  */
     new.ColumnID = GEN_ID(S_FDB_COLUMN_REGISTRY, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_featuredataset for FDB_FEATUREDATASET
before insert as
declare variable numrows integer;
begin
    /*  Column "DatasetID" uses sequence S_FDB_FEATUREDATASET  */
     new.DatasetID = GEN_ID(S_FDB_FEATUREDATASET, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_items for FDB_ITEMS
before insert as
declare variable numrows integer;
begin
    /*  Column "ID" uses sequence S_FDB_ITEMS  */
     new.ID = GEN_ID(S_FDB_ITEMS, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_itemtypes for FDB_ITEMTYPES
before insert as
declare variable numrows integer;
begin
    /*  Column "ID" uses sequence S_FDB_ITEMTYPES  */
     new.ID = GEN_ID(S_FDB_ITEMTYPES, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_objectclasses for FDB_OBJECTCLASSES
before insert as
declare variable numrows integer;
begin
    /*  Column "ClassID" uses sequence S_FDB_OBJECTCLASSES  */
     new.ClassID = GEN_ID(S_FDB_OBJECTCLASSES, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_replication_step for FDB_REPLICATION_STEP
before insert as
declare variable numrows integer;
begin
    /*  Column "ID" uses sequence S_FDB_REPLICATION_STEP  */
     new.ID = GEN_ID(S_FDB_REPLICATION_STEP, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fdb_table_registry for FDB_TABLE_REGISTRY
before insert as
declare variable numrows integer;
begin
    /*  Column "TableRegistration_id" uses sequence S_FDB_TABLE_REGISTRY  */
     new.TableRegistration_id = GEN_ID(S_FDB_TABLE_REGISTRY, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fde_rep_checkoutinfo for FDE_REP_CHECKOUTINFO
before insert as
declare variable numrows integer;
begin
    /*  Column "id" uses sequence S_FDE_REP_CHECKOUTINFO  */
     new.id = GEN_ID(S_FDE_REP_CHECKOUTINFO, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fde_rep_fullreplicationtable for FDE_REP_FULLREPLICATIONTABLE
before insert as
declare variable numrows integer;
begin
    /*  Column "id" uses sequence S_FDE_REP_FULLREPLICATIONTABLE  */
     new.id = GEN_ID(S_FDE_REP_FULLREPLICATIONTABLE, 1);
end;^
SET TERM ;^/*#*/;


SET TERM ^;
create trigger ti_fde_spatial_references for FDE_SPATIAL_REFERENCES
before insert as
declare variable numrows integer;
begin
    /*  Column "SRID" uses sequence S_FDE_SPATIAL_REFERENCES  */
     new.SRID = GEN_ID(S_FDE_SPATIAL_REFERENCES, 1);
end;^
SET TERM ;^/*#*/;

