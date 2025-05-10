ALTER TABLE "Movies" ALTER COLUMN "UpdatedAt" SET DEFAULT (now());

ALTER TABLE "Movies" ALTER COLUMN "Title" TYPE character varying(100);

ALTER TABLE "Movies" ALTER COLUMN "CreatedAt" SET DEFAULT (now());

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250510154523_MovieConfig', '9.0.4');
