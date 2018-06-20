REM xsd.exe "%xsdFile%" /c /out:"%outDirectory%" /l:"%language%" (default is C#)

cd %CD%

REM create an *.xsd of a known file name
COPY CSiResources_V0_1.xsd "%CD%\xmlCSiResourcesBase.xsd"

REM Create VB Class File
"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\xsd.exe" "%CD%\xmlCSiResourcesBase.xsd" /c /out:"%CD%" /l:VB

REM clean up temporary *.xsd file
del "%CD%\xmlCSiResourcesBase.xsd"
