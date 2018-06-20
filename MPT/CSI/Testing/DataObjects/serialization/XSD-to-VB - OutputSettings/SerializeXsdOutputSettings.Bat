REM xsd.exe "%xsdFile%" /c /out:"%outDirectory%" /l:"%language%" (default is C#)

cd %CD%

REM Create VB Class File
"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\xsd.exe" "%CD%\xmlOutputSettingsBase.xsd" /c /out:"%CD%" /l:VB
