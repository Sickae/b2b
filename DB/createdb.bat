@SETLOCAL ENABLEEXTENSIONS

@set PGHOSTADDR=127.0.0.1
@set PGPORT=5432
@set PGUSER=postgres
@set PGPASSWORD=admin
@set PGPATH=".\psql_9.4"
@set DBNAME=b2b

@if not "%1"=="" (
	@set PGHOSTADDR=%1
)

@if not "%2"=="" (
	@set PGPORT=%2
)

@if not "%3"=="" (
	@set DBNAME=%3
)

@if not "%4"=="" (
	@set PGUSER=%4
)

@if not "%5"=="" (
	@set PGPASSWORD=%5
)

echo on
echo "start drop db"
%PGPATH%\psql -v dat_name='%DBNAME%' -v db_name=%DBNAME% -f 00_DropDB.sql -d postgres
echo "start create db"
%PGPATH%\psql -v db_name=%DBNAME% -v owner=%PGUSER% -f 01_CreateDB.sql -d postgres
%PGPATH%\psql -d %DBNAME% -f 02_CreateSchema.sql

@ENDLOCAL
