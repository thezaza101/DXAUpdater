CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Facets" (
    "ID" text NOT NULL,
    "maxLength" text NULL,
    "minInclusive" text NULL,
    "minLength" text NULL,
    "pattern" text NULL,
    CONSTRAINT "PK_Facets" PRIMARY KEY ("ID")
);

CREATE TABLE "UpdatedData" (
    "DataID" text NOT NULL,
    "NextDataID" text NULL,
    CONSTRAINT "PK_UpdatedData" PRIMARY KEY ("DataID")
);

CREATE TABLE "Datatype" (
    "ID" text NOT NULL,
    "facetsID" text NULL,
    "type" text NULL,
    "values" text[] NULL,
    CONSTRAINT "PK_Datatype" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Datatype_Facets_facetsID" FOREIGN KEY ("facetsID") REFERENCES "Facets" ("ID") ON DELETE RESTRICT
);

CREATE TABLE "DataElement" (
    "ID" text NOT NULL,
    "UpdatedDataDataID" text NULL,
    "datatypeID" text NULL,
    "definition" text NULL,
    "domain" text NULL,
    "guidance" text NULL,
    "identifier" text NULL,
    "lastUpdateDate" text NULL,
    "name" text NULL,
    "sourceURL" text NULL,
    "status" text NULL,
    "usage" text[] NULL,
    "values" text[] NULL,
    "version" text NULL,
    CONSTRAINT "PK_DataElement" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_DataElement_UpdatedData_UpdatedDataDataID" FOREIGN KEY ("UpdatedDataDataID") REFERENCES "UpdatedData" ("DataID") ON DELETE RESTRICT,
    CONSTRAINT "FK_DataElement_Datatype_datatypeID" FOREIGN KEY ("datatypeID") REFERENCES "Datatype" ("ID") ON DELETE RESTRICT
);

CREATE INDEX "IX_DataElement_UpdatedDataDataID" ON "DataElement" ("UpdatedDataDataID");

CREATE INDEX "IX_DataElement_datatypeID" ON "DataElement" ("datatypeID");

CREATE INDEX "IX_Datatype_facetsID" ON "Datatype" ("facetsID");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20180518035539_InitialCreate', '2.0.3-rtm-10026');