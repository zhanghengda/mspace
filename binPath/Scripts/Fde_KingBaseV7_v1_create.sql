CREATE SCHEMA FDE;/*#*/;

CREATE TABLE FDE.FDB_COLUMN_REGISTRY (
ColumnID             INT IDENTITY         not null,
DomainID             INT,
ClassID              INT                  not null,
FieldName            VARCHAR(255)         not null,
AliasName            VARCHAR(255),
DefaultValueString   VARCHAR(255),
DefaultValueNumber   NUMERIC(38,8),
IsNullable           INT                  not null,
IsEditable           INT                  not null,
IsRequired           INT,
IsSystemColumn       INT,
IsRanderIndexField   INT                  not null DEFAULT 0,
Fde_type             INT                  not null,
Column_size          INT                  not null,
Decimal_digits       INT                  not null,
CONSTRAINT PK_FDB_COLUMN_REGISTRY PRIMARY KEY (ColumnID)
)/*#*/;

CREATE  INDEX FDB_TAB_COL_Link_FK ON FDE.FDB_COLUMN_REGISTRY (
ClassID
)/*#*/;

CREATE  INDEX FDB_COL_ITEM_LINK_FK ON FDE.FDB_COLUMN_REGISTRY (
DomainID
)/*#*/;

CREATE TABLE FDE.FDB_FEATUREDATASET (
DatasetID            INT IDENTITY         not null,
DatasetName          VARCHAR(128)         not null,
DatasetAliasName     VARCHAR(255),
DatasetUUID          VARCHAR(40)          not null,
DatabaseName         VARCHAR(32),
SchemaName           VARCHAR(32),
Description          VARCHAR(1024),
SRID                 INT                  not null,
TRID                 INT                  not null,
RegisterOption       INT                  not null DEFAULT 0,
EditVersion          INT                  not null DEFAULT 0,
CreateTime           TIMESTAMP            not null,
LastUpdateTime       TIMESTAMP            not null,
CONSTRAINT PK_FDB_FEATUREDATASET PRIMARY KEY (DatasetID)
)/*#*/;

CREATE UNIQUE INDEX FDB_FEATUREDATASET_UK_NAME ON FDE.FDB_FEATUREDATASET (
DatasetName ASC
)/*#*/;

CREATE TABLE FDE.FDB_GEOCOLUMN (
ColumnID             INT                  not null,
F_TableName          VARCHAR(32)          not null,
AvgPointCnt          INT                  not null,
GeoType              INT                  not null,
HasID                INT                  not null,
HasM                 INT                  not null,
HasZ                 INT                  not null,
StorageType          INT                  not null,
MaxX                 DOUBLE               not null,
MaxY                 DOUBLE               not null,
MaxZ                 DOUBLE               not null,
MaxM                 DOUBLE               not null,
MinX                 DOUBLE               not null,
MinY                 DOUBLE               not null,
MinZ                 DOUBLE               not null,
MinM                 DOUBLE               not null,
CONSTRAINT PK_FDB_GEOCOLUMN PRIMARY KEY (ColumnID)
)/*#*/;

CREATE TABLE FDE.FDB_GRIDINDEX (
ColumnID             INT                  not null,
S_TableName          VARCHAR(32)          not null,
IndexName            VARCHAR(255)         not null,
CenterX              DOUBLE               not null,
CenterY              DOUBLE               not null,
L1                   DOUBLE               not null,
L2                   DOUBLE               not null,
L3                   DOUBLE               not null,
Radio                DOUBLE               not null,
CONSTRAINT PK_FDB_GRIDINDEX PRIMARY KEY (ColumnID)
)/*#*/;

CREATE TABLE FDE.FDB_ITEMS (
ID                   INT IDENTITY         not null,
NAME                 VARCHAR(128)         not null,
FULLNAME             VARCHAR(255)         not null,
GUID                 VARCHAR(40)          not null,
TYPE                 VARCHAR(40)          not null,
BASETYPE             VARCHAR(40)          not null,
DEFAULTS             BYTEA,
DEFINITION           BYTEA,
DOCUMENTATION        BYTEA,
CONSTRAINT PK_FDB_ITEMS PRIMARY KEY (ID)
)/*#*/;

CREATE TABLE FDE.FDB_ITEMTYPES (
ID                   INT IDENTITY         not null,
NAME                 VARCHAR(255)         not null,
GUID                 VARCHAR(40)          not null,
PARENTTYPEGUID       VARCHAR(40)          not null,
CONSTRAINT PK_FDB_ITEMTYPES PRIMARY KEY (ID),
CONSTRAINT AK_IDENTIFIER_2_FDB_ITEM UNIQUE (NAME)
)/*#*/;

CREATE TABLE FDE.FDB_OBJECTCLASSES (
ClassID              INT IDENTITY         not null,
DatasetID            INT                  not null,
DatabaseName         VARCHAR(32)          not null,
SchemaName           VARCHAR(32)          not null,
ClassName            VARCHAR(128)         not null,
AliasName            VARCHAR(255)         not null,
ClsUUID              VARCHAR(40)          not null,
OidColumn            VARCHAR(32)          not null,
ClassType            INT                  not null,
Description          VARCHAR(1024),
CreateTime           TIMESTAMP            not null,
LastUpdateTime       TIMESTAMP            not null,
RegisterOption       INT                  not null DEFAULT 0,
CONSTRAINT PK_FDB_OBJECTCLASSES PRIMARY KEY (ClassID)
)/*#*/;

CREATE  INDEX FeatureDatasetClassLink_FK ON FDE.FDB_OBJECTCLASSES (
DatasetID
)/*#*/;

CREATE UNIQUE INDEX FDB_OBJECTCLASS_UK_NAME ON FDE.FDB_OBJECTCLASSES (
DatasetID ASC,
ClassName ASC
)/*#*/;

CREATE TABLE FDE.FDB_RENDERINDEX (
ColumnID             INT                  not null,
FB_TABLENAME         VARCHAR(255),
IndexName            VARCHAR(255)         not null,
CenterX              DOUBLE               not null,
CenterY              DOUBLE               not null,
L1                   DOUBLE               not null,
L2                   DOUBLE               not null,
L3                   DOUBLE               not null,
Radio                DOUBLE               not null,
CONSTRAINT PK_FDB_RENDERINDEX PRIMARY KEY (ColumnID)
)/*#*/;

CREATE TABLE FDE.FDB_REPLICATION_STEP (
ID                   INT                  not null,
DATASETID            INT                  not null,
OpType               INT                  not null,
STEP                 INT                  not null,
SUBSTEP              INT                  DEFAULT 0,
DESCRIPTION          VARCHAR(1024),
ISDONE               INT                  not null,
DATA                 BYTEA,
CONSTRAINT PK_FDB_REPLICATION_STEP PRIMARY KEY (ID)
)/*#*/;

CREATE TABLE FDE.FDB_SERVERCONFIG (
PARAMETERNAME        VARCHAR(128)         not null,
PARAMETERVALUE       VARCHAR(255)         not null,
CONSTRAINT PK_FDB_SERVERCONFIG PRIMARY KEY (PARAMETERNAME)
)/*#*/;

CREATE TABLE FDE.FDB_SERVERINFO (
DataBaseName         VARCHAR(255),
InstanceName         VARCHAR(255),
UserName             VARCHAR(255)         not null,
CreateTime           TIMESTAMP            not null,
FDBVersionMajor      INT                  not null,
FDBVersionMinor      INT                  not null,
FDBVersionBugfix     INT                  not null,
FDEProviderName      VARCHAR(255)         not null,
FDEDescription       VARCHAR(1024),
"UID"                VARCHAR(64)          not null
)/*#*/;

CREATE TABLE FDE.FDB_TABLE_REGISTRY (
TableRegistration_id INT IDENTITY         not null,
ClassID              INT,
DatabaseName         VARCHAR(32)          not null,
SchemaName           VARCHAR(32)          not null,
TableName            VARCHAR(32)          not null,
TableType            INT                  not null,
isSystemTable        INT                  not null,
Description          VARCHAR(1024),
RegistrationDate     TIMESTAMP            not null,
LastUpdateTime       TIMESTAMP            not null,
CONSTRAINT PK_FDB_TABLE_REGISTRY PRIMARY KEY (TableRegistration_id)
)/*#*/;

CREATE  INDEX objectattachlink_FK ON FDE.FDB_TABLE_REGISTRY (
ClassID
)/*#*/;

CREATE TABLE FDE.FDE_OBJECT_LOCKS (
FDEID                INT                  not null,
LOCKTYPE             CHAR(1)              not null,
CLASSID              INT                  not null,
OBJECTID             INT                  not null,
CONSTRAINT PK_FDE_OBJECT_LOCKS PRIMARY KEY (FDEID, LOCKTYPE, CLASSID, OBJECTID)
)/*#*/;

CREATE  INDEX FDE_PROCESS_OBJECT_LOCK_FK ON FDE.FDE_OBJECT_LOCKS (
FDEID
)/*#*/;

CREATE  INDEX FDE_OBJECT_OBJECTLOCK_LINK_FK ON FDE.FDE_OBJECT_LOCKS (
CLASSID
)/*#*/;

CREATE TABLE FDE.FDE_PROCESS_INFORMATION (
fdeid                INT IDENTITY         not null,
spid                 INT                  not null,
pid                  INT                  not null,
starttime            TIMESTAMP            not null,
owner                VARCHAR(32)          not null,
directconnect        CHAR(1)              not null,
CONSTRAINT PK_FDE_PROCESS_INFORMATION PRIMARY KEY (fdeid)
)/*#*/;

CREATE TABLE FDE.FDE_REP_CHECKOUTINFO (
id                   INT IDENTITY         not null,
DatasetID            INT,
checkoutName         VARCHAR(40)          not null,
checkoutDatetime     TIMESTAMP            not null,
bCheckout            INT                  not null,
bMaster              INT                  not null DEFAULT 0,
Conn                 VARCHAR(1024),
CONSTRAINT PK_FDE_REP_CHECKOUTINFO PRIMARY KEY (id),
CONSTRAINT AK_CHECKOUTNAME_FDE_REP_ UNIQUE (checkoutName)
)/*#*/;

CREATE  INDEX CheckoutDatasetLink_FK ON FDE.FDE_REP_CHECKOUTINFO (
DatasetID
)/*#*/;

CREATE TABLE FDE.FDE_REP_FULLREPLICATIONTABLE (
id                   INT                  not null,
registerclassid      INT                  not null,
datasetid            INT                  not null,
CONSTRAINT PK_FDE_REP_FULLREPLICATIONTABL PRIMARY KEY (id)
)/*#*/;

CREATE TABLE FDE.FDE_REP_TABLE_MODIFIED (
id                   INT                  not null,
tableregisterID      INT                  not null,
fieldId              INT                  not null,
optype               INT                  not null,
bKeep                INT                  not null,
CONSTRAINT PK_FDE_REP_TABLE_MODIFIED PRIMARY KEY (id)
)/*#*/;

CREATE TABLE FDE.FDE_SPATIAL_REFERENCES (
SRID                 INT IDENTITY         not null,
Description          VARCHAR(1024),
auth_name            VARCHAR(255),
auth_srid            INT,
falsex               DOUBLE               not null,
falsey               DOUBLE               not null,
xyunits              DOUBLE               not null,
falsez               DOUBLE               not null,
zunits               DOUBLE               not null,
falsem               DOUBLE               not null,
munits               DOUBLE               not null,
xycluster_tol        DOUBLE,
zcluster_tol         DOUBLE,
mcluster_tol         DOUBLE,
srtext               VARCHAR(1024)        not null,
CONSTRAINT PK_FDE_SPATIAL_REFERENCES PRIMARY KEY (SRID)
)/*#*/;

CREATE TABLE FDE.FDE_TABLE_LOCKS (
fdeid                INT                  not null,
locktype             CHAR(1)              not null,
ClassID              INT                  not null,
CONSTRAINT PK_FDE_TABLE_LOCKS PRIMARY KEY (fdeid, locktype, ClassID)
)/*#*/;

CREATE  INDEX FDE_processinfo_tablelocks_FK ON FDE.FDE_TABLE_LOCKS (
fdeid
)/*#*/;

CREATE  INDEX tabletablelocklink_FK ON FDE.FDE_TABLE_LOCKS (
ClassID
)/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_FDE_LINK__FDB_ITEM FOREIGN KEY (DomainID)
      REFERENCES FDE.FDB_ITEMS (ID)/*#*/;

ALTER TABLE FDE.FDB_COLUMN_REGISTRY
   ADD CONSTRAINT FK_FDB_COLU_TABLECOLU_FDB_OBJE FOREIGN KEY (ClassID)
      REFERENCES FDE.FDB_OBJECTCLASSES (ClassID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDB_GEOCOLUMN
   ADD CONSTRAINT FK_FDB_GEOC_FDB_GEOCO_FDB_COLU FOREIGN KEY (ColumnID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (ColumnID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDB_GRIDINDEX
   ADD CONSTRAINT FK_FDB_GRID_GEOCOLUMN_FDB_COLU FOREIGN KEY (ColumnID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (ColumnID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDB_OBJECTCLASSES
   ADD CONSTRAINT FK_FDB_OBJE_FEATUREDA_FDB_FEAT FOREIGN KEY (DatasetID)
      REFERENCES FDE.FDB_FEATUREDATASET (DatasetID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDB_RENDERINDEX
   ADD CONSTRAINT FK_FDB_REND_COLUMN_RE_FDB_COLU FOREIGN KEY (ColumnID)
      REFERENCES FDE.FDB_COLUMN_REGISTRY (ColumnID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDB_TABLE_REGISTRY
   ADD CONSTRAINT FK_FDB_TABL_OBJECTATT_FDB_OBJE FOREIGN KEY (ClassID)
      REFERENCES FDE.FDB_OBJECTCLASSES (ClassID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJE_FDE_OBJEC_FDB_OBJE FOREIGN KEY (CLASSID)
      REFERENCES FDE.FDB_OBJECTCLASSES (ClassID)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDE_OBJECT_LOCKS
   ADD CONSTRAINT FK_FDE_OBJE_FDE_PROCE_FDE_PROC FOREIGN KEY (FDEID)
      REFERENCES FDE.FDE_PROCESS_INFORMATION (fdeid)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDE_REP_CHECKOUTINFO
   ADD CONSTRAINT FK_FDE_REP__CHECKOUTD_FDB_FEAT FOREIGN KEY (DatasetID)
      REFERENCES FDE.FDB_FEATUREDATASET (DatasetID)/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_FDE_PROCE_FDE_PROC FOREIGN KEY (fdeid)
      REFERENCES FDE.FDE_PROCESS_INFORMATION (fdeid)
      ON DELETE cascade/*#*/;

ALTER TABLE FDE.FDE_TABLE_LOCKS
   ADD CONSTRAINT FK_FDE_TABL_TABLETABL_FDB_OBJE FOREIGN KEY (ClassID)
      REFERENCES FDE.FDB_OBJECTCLASSES (ClassID)
      ON DELETE cascade/*#*/;
	  
CREATE OR REPLACE FUNCTION FDE.LOCK_FDE_TABLE(FDE_ID INTEGER, CFLAG CHARACTER(1), CLASS_ID INTEGER) 
RETURNS INTEGER 
AS
DECLARE   
	FDE_LOCK_CURSOR REFCURSOR;   
	BCONFLICT BOOLEAN;   
	FOUND_LOCK INTEGER;
BEGIN   
	BCONFLICT := FALSE;  
	DELETE FROM FDE.FDE_PROCESS_INFORMATION WHERE FDEID NOT IN (SELECT PROCPID FROM SYS_STAT_ACTIVITY) AND FDEID != FDE_ID;
	   
	OPEN FDE_LOCK_CURSOR  FOR   
	SELECT FDEID FROM FDE.FDE_TABLE_LOCKS WHERE CLASSID=CLASS_ID AND (LOCKTYPE = 'X' OR CFLAG = 'X') AND FDEID != FDE_ID;   
    	FETCH FDE_LOCK_CURSOR INTO FOUND_LOCK;   
    	IF FDE_LOCK_CURSOR%FOUND THEN  
		BCONFLICT := TRUE;  
	END IF;   
	CLOSE FDE_LOCK_CURSOR;   

	IF BCONFLICT = TRUE THEN   
		RETURN -1;
    	ELSE
		DELETE FROM FDE.FDE_TABLE_LOCKS WHERE FDEID=FDE_ID AND CLASSID=CLASS_ID;
		INSERT INTO FDE.FDE_TABLE_LOCKS(FDEID, LOCKTYPE, CLASSID) VALUES(FDE_ID, CFLAG, CLASS_ID);   
		RETURN 0; 
	END IF;
END
;/*#*/;

CREATE OR REPLACE FUNCTION FDE.LOCK_FDE_OBJECT(FDE_ID INTEGER, CLASS_ID INTEGER, OBJECT_ID INTEGER) 
RETURNS INTEGER 
AS
DECLARE   
	FDE_LOCK_CURSOR REFCURSOR;   
	BCONFLICT BOOLEAN;
	FOUND_LOCK INTEGER;
BEGIN   
	BCONFLICT := FALSE;  
	DELETE FROM FDE.FDE_PROCESS_INFORMATION WHERE FDEID NOT IN (SELECT PROCPID FROM SYS_STAT_ACTIVITY) AND FDEID != FDE_ID;
	   
	OPEN FDE_LOCK_CURSOR  FOR   
	SELECT FDEID FROM FDE.FDE_OBJECT_LOCKS WHERE FDEID!=FDE_ID AND CLASSID=CLASS_ID AND OBJECTID=OBJECT_ID; 
    	FETCH FDE_LOCK_CURSOR INTO FOUND_LOCK;   
    	IF FDE_LOCK_CURSOR%FOUND THEN  
		BCONFLICT := TRUE;  
	END IF;   
	CLOSE FDE_LOCK_CURSOR;   

	IF BCONFLICT = TRUE THEN   
		RETURN -1;
    	ELSE
		DELETE FROM FDE.FDE_OBJECT_LOCKS WHERE FDEID=FDE_ID AND CLASSID=CLASS_ID AND OBJECTID=OBJECT_ID;
		INSERT INTO FDE.FDE_OBJECT_LOCKS(FDEID, LOCKTYPE, CLASSID, OBJECTID) VALUES(FDE_ID, 'X', CLASS_ID, OBJECT_ID);   
		RETURN 0; 
	END IF;
END
;/*#*/;

