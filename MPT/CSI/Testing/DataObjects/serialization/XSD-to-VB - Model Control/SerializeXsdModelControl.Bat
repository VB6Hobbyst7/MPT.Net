REM xsd.exe "%xsdFile%" /c /out:"%outDirectory%" /l:"%language%" (default is C#)

cd %CD%

REM create an *.xsd of a known file name
COPY model_database_model_V0_6.xsd "%CD%\xmlModelControlBase.xsd"

REM Create VB Class File
"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\xsd.exe" /n:"ModelControl" "%CD%\CSiResources_V0_1.xsd" "%CD%\xmlModelControlBase.xsd" /c /out:"%CD%" /l:VB

REM clean up temporary *.xsd file
del "%CD%\xmlModelControlBase.xsd"
