ECHO off

rem batch file to run a script to create a db
rem 2020/10/26

sqlcmd -S localhost -E -i disclone_db.sql

rem sqlcmd -S localhost\sqlexpress -E -i camp_db_pm.sql
rem sqlcmd -S localhost\mssqlserver -E -i camp_db_pm.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE