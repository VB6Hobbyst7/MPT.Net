@echo off 
set destination=%1
set "sourceLocal=%CD%\bin"
set "sourceGlobal=%CD%\..\bin"

set "progDir=CSiTester"

REM Schemas =========================================================================
set "directory=schemas"
set "destinationComplete=%destination%\%progDir%\%directory%2"
echo %destinationComplete%
del %destinationComplete%\*.* /f /q

REM Local
REM Copy All - echo d| xcopy "%sourceLocal%\%directory%\*.*" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceLocal%\%directory%\CSiTesterSettings.xsd" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceLocal%\%directory%\CSiTester_MCSettings.xsd" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceLocal%\%directory%\outputSettings.xsd" %destinationComplete%\ /s /e /k /y

REM Global
REM Copy All - echo d| xcopy "%sourceGlobal%\%directory%\*.*" %destination%\%progDir%\%directory%2\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiResources.xsd" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiPrograms.xsd" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiCodes.xsd" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiTables.xsd" %destinationComplete%\ /s /e /k /y

REM Settings Files =========================================================================
set "directory=settings"
set "destinationComplete=%destination%\%progDir%\%directory%2"
echo deleting %destinationComplete%
del %destinationComplete%\*.* /f /q

REM Local
REM Copy All - echo d| xcopy "%sourceLocal%\%directory%\*.*" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceLocal%\%directory%\CSiTesterSettings.xml" %destinationComplete% /s /e /k /y
echo d| xcopy "%sourceLocal%\%directory%\CSiTester_MCSettings.xml" %destinationComplete% /s /e /k /y

REM Global
REM Copy All - echo d| xcopy "%sourceGlobal%\%directory%\*.*" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiPrograms.xml" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiCodes.xml" %destinationComplete%\ /s /e /k /y
echo d| xcopy "%sourceGlobal%\%directory%\CSiTables.xml" %destinationComplete%\ /s /e /k /y

REM Seed Files =========================================================================
set "directory=seed"
set "destinationComplete=%destination%\%progDir%\%directory%2"
echo %destinationComplete%
del %destinationComplete%\*.* /f /q

REM Local
echo d| xcopy "%sourceLocal%\%directory%\*.*" %destinationComplete%\ /s /e /k /y

REM Global
echo d| xcopy "%sourceGlobal%\%directory%\*.*" %destinationComplete%\ /s /e /k /y
