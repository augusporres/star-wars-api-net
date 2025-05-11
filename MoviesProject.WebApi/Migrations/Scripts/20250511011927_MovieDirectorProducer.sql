START TRANSACTION;
ALTER TABLE "Movies" ALTER COLUMN "Title" DROP NOT NULL;

ALTER TABLE "Movies" ALTER COLUMN "OpenningCrawl" DROP NOT NULL;

ALTER TABLE "Movies" ADD "Director" character varying(255);

ALTER TABLE "Movies" ADD "Producer" character varying(255);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250511011927_MovieDirectorProducer', '9.0.4');

COMMIT;