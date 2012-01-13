@echo OFF
echo Installing MySQL Server
cd MySQL
start /wait MySQL.msi /quiet /norestart
echo Copying the Settings file.
copy my.ini "%PROGRAMFILES%\MySQL\MySQL Server 5.1\"
copy as.sql "%PROGRAMFILES%\MySQL\MySQL Server 5.1\bin"
echo Installing MySQL Service
cd "%PROGRAMFILES%\MySQL\MySQL Server 5.1\bin"
mysqld --install
cd "%PROGRAMFILES%\MySQL\MySQL Server 5.1"
echo Starting MySQL
NET START MYSQL
echo Installing MySQL .NET Connector
cd \MySQL
start /wait MySQL_Connector.msi /quiet /norestart
cd "%PROGRAMFILES%\MySQL\MySQL Server 5.1\bin"
mysqladmin -u "root" password root
mysql -u "root" -p"root" < as.sql
@echo ON