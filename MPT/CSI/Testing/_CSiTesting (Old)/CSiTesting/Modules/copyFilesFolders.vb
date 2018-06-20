Option Explicit On
Imports System.IO
Imports System.IO.Compression


Module copyFilesFolders
    Sub ReleasedConversion()

        Call Copy_FolderMap()
        Call BatchWrite_Copy_Rename()
        Call Copy_Folder_Convert()

    End Sub

    Sub Copy_Folder(PathSource As String, PathTarget As String)
        'Makes a duplicate folder,including sub-folders
        'Note: If PathTarget already exists it will overwrite existing files in this folder. If PathTarget does not exist, it will be made for you.

        Dim FSO As Object

        FSO = CreateObject("scripting.filesystemobject")

        If FSO.FolderExists(PathSource) = False Then
            MsgBox(PathSource & " doesn't exist")
            Exit Sub
        End If

        FSO.CopyFolder(Source:=PathSource, Destination:=PathTarget)

    End Sub

    Sub Copy_FolderMap()
        'Makes a duplicate folder of released model group selected, preserving the originals.
        'Note: If PathTarget already exists it will overwrite existing files in this folder. If PathTarget does not exist, it will be made for you.

        Dim PathSource As String    'Location & name of original folder
        Dim PathTarget As String    'Location & name of copied folder
        Dim PathSourceFolderName As String

        PathSource = "" 'Update -regTest.PathMapCopy
        PathSourceFolderName = FolderName_FromPath(PathSource)
        PathTarget = "" 'Update - regTest.PathMapSource & "\" & PathSourceFolderName

        Call Copy_Folder(PathSource, PathTarget)

        'Update -         Call AllFilesNotReadOnly(regTest.PathMapSource)  'Sets all files to not be 'read only'

        '    MsgBox "You can find the files and subfolders from " & PathSource & " in " & PathTarget

    End Sub

    Sub BatchWrite_Copy_Rename()
        'Writes Batch File that Deletes all files in the specified location and subdirectories by extension - leave off *. when specifying file extension

        '       Dim CommandLine As String
        Dim BatchName As String
        Dim BatchPath As String
        '       Dim Source As String
        '        Dim Target As String
        Dim i As Long

        BatchName = "Copy&RenameFile Released.bat"
        BatchPath = Application.StartupPath & "\" & BatchName

        ' Deletes Batch File
        Call KillFile(BatchPath)

        'Gathers mapped paths and names from an Excel sheet. Perhaps automate this in the future?
        i = 0
        'Do While regTest.MapSource_Start 'Update - .Offset(i, 0).value <> ""

        '    Source = regTest.MapSource_Start 'Update - .Offset(i, 0).value
        '    Target = regTest.MapTarget_Start 'Update - .Offset(i, 0).value

        '    CommandLine = "COPY /Y " & Chr(34) & Source & Chr(34) & " " & Chr(34) & Target & Chr(34)
        '    Call Print_Batch(CommandLine, BatchPath)

        '    i = i + 1

        'Loop

        Call Run_Batch(BatchPath, True)

    End Sub

    Sub Copy_Folder_Convert()
        'Makes a duplicate folder of models converted before clearing the target folder of all .EDB Files. The new folder is renamed with the date-time of duplication
        'Note: If PathTarget already exists it will overwrite existing files in this folder. If PathTarget does not exist, it will be made for you.

        Dim Source As String    'Location & name of original folder from which models are run
        Dim Target As String    'Location & name of copied folder of models created for run

        Source = "" 'Update -regTest.PathMapTarget
        Target = Source & " - " & Format(Now, "yyyy-mm-dd hh-mm")

        'Record Variables in Excel
        'Update -        regTest.PathSource = Source
        'Update -         regTest.PathMapConvert = Target

        Call Copy_Folder(Source, Target)

        '    MsgBox "You can find the files and subfolders from " & PathSource & " in " & PathTarget

        'Writes batch file with commands to delete any existing EDB files in the destination
        Call BatchWrite_Del_SingleFileType(Source, "EDB")

    End Sub

    Sub Copy_Folder_Run()
        'Makes a duplicate folder of models selected to be run, preserving the originals. The new folder is renamed with the date-time of duplication
        'Note: If PathTarget already exists it will overwrite existing files in this folder. If PathTarget does not exist, it will be made for you.

        Dim PathSource As String    'Location & name of original folder from which models are run
        Dim PathRun As String    'Location & name of copied folder of models created for run

        PathSource = Application.StartupPath & "\models"

        PathRun = PathSource & "_Run_" & Format(Now, "yyyy-mm-dd hh-mm")

        'Record Variables in Excel
        'Update - regTest.PathSource = PathSource
        'Update - regTest.PathRun = PathRun

        Call Copy_Folder(PathSource, PathRun)

        MsgBox("You can find the files and subfolders from " & PathSource & " in " & PathRun)

    End Sub

    Sub Copy_Folders_NoMatch()
        'Copies all examples that did not match in a run to a new folder. Original models/folders are copied, as are the ones that had been run in the verification.
        'Note: If ToPath already exist it will overwrite existing files in this folder
        'if ToPath not exist it will be made for you.

        Dim PathSource As String    'Location & name of original folder from which models are run
        Dim PathRun As String    'Location & name of copied folder of models created for run

        Dim PathReport As String            'Path to folder containing the following two variables . . .
        Dim PathReportSource As String      'Path to folder containing the original models
        Dim PathReportRun As String         'Path to folder containing run models & analyses results

        Dim FromPathModel As String     'Path to Original Model
        Dim ToPathModel As String       'Path to which Original Model is to be copied

        Dim FromPathRunModel As String           'Path to Run Model & analysis results
        Dim ToPathRunModel As String       'Path to which Run Model & analysis results are to be copied

        Dim item As String
        Dim i As Long
        Dim j As Long

        'Update - PathSource = regTest.PathSource      'Retrieves variable from storage in Excel
        'Update - PathRun = regTest.PathRun      'Retrieves variable from storage in Excel
        PathSource = ""
        PathRun = ""

        PathReport = PathSource & "_Run_Report_" & Format(Now, "yyyy-mm-dd hh-mm")
        'Update - regTest.PathReport = PathReport    'Records variable in Excel
        MkDir(PathReport)

        PathReportSource = PathReport & "\models"   '<< Change
        MkDir(PathReportSource)

        '''''Length should be static, but perhaps this should be tied in with the name generation to be sure?
        PathReportRun = PathReport & "\" & Right(PathRun, 27)    ''Right' uses Length of folder name.
        MkDir(PathReportRun)

        'Update
        Dim NoMatchArray(0) As String
        NoMatchArray(0) = ""
        Dim ModelNum As Long
        ModelNum = 1
        'Update

        For i = 0 To UBound(NoMatchArray)

            ModelNum = NoMatchArray(i)

            FromPathModel = PathSource & "\id0" & ModelNum
            ToPathModel = PathReportSource & "\id0" & ModelNum
            ReDim Preserve NoMatchArrayPath(i)
            NoMatchArrayPath(i) = ToPathModel

            FromPathRunModel = PathRun & "\id0" & ModelNum
            ToPathRunModel = PathReportRun & "\id0" & ModelNum
            ReDim Preserve NoMatchArrayPathRun(i)
            NoMatchArrayPathRun(i) = ToPathRunModel

            For j = 1 To 4
                Select Case j
                    Case 1 : item = FromPathModel
                    Case 2 : item = FromPathRunModel
                    Case 3 : item = ToPathModel
                    Case 4 : item = ToPathRunModel
                    Case Else : item = ""
                End Select

                Call Path_Slash_Truncate(item)

            Next j

            Call Copy_Folder(FromPathModel, ToPathModel)
            Call Copy_Folder(FromPathRunModel, ToPathRunModel)

        Next i

        MsgBox("You can find the files and subfolders of deviating models from the latest run in " & PathReport)

    End Sub

    Sub Del_AnalysisFiles()
        'Deletes all analysis files in the specified location and subdirectories
        Dim path As String

        'Update?
        Dim BatchName As String
        Dim BatchPath As String
        'Update?

        path = "" ' Update - regTest.PathReport      'Retrieves variable from storage in Excel
        BatchName = "DeleteFiles - Analysis.bat"
        BatchPath = Application.StartupPath & "\" & BatchName

        Call BatchWrite_Del_AnalysisFiles(path, BatchName, BatchPath)

    End Sub

    Sub BatchWrite_Del_AnalysisFiles(path As String, BatchName As String, BatchPath As String)
        'Deletes all analysis files in the specified location and subdirectories

        Dim i As Long
        Dim ftype As String
        Dim CommandLine As String

        ' Deletes Batch File
        Call KillFile(BatchPath)

        'Writes batch file with commands to delete various analysis files
        For i = 1 To 122
            Select Case i
                'Analysis Files
                Case 1 : ftype = "*.ico"
                Case 2 : ftype = "*.suo"
                Case 3 : ftype = "*.docstates.suo"
                Case 4 : ftype = "*.msh"
                Case 5 : ftype = "*.OUT"
                Case 6 : ftype = "*.$$?"
                Case 7 : ftype = "*.BRG"
                Case 8 : ftype = "*.BRH"
                Case 9 : ftype = "*.C1"
                Case 10 : ftype = "*.C2"
                Case 11 : ftype = "*.C3"
                Case 12 : ftype = "*.C4"
                Case 13 : ftype = "*.C5"
                Case 14 : ftype = "*.C6"
                Case 15 : ftype = "*.C7"
                Case 16 : ftype = "*.C8"
                Case 17 : ftype = "*.C9"
                Case 18 : ftype = "*.C10"
                Case 19 : ftype = "*.C11"
                Case 20 : ftype = "*.CSE"
                Case 21 : ftype = "*.CSG"
                Case 22 : ftype = "*.CSJ"
                Case 23 : ftype = "*.CSP"
                Case 24 : ftype = "*.D1"
                Case 25 : ftype = "*.D2"
                Case 26 : ftype = "*.D3"
                Case 27 : ftype = "*.D4"
                Case 28 : ftype = "*.D5"
                Case 29 : ftype = "*.D6"
                Case 30 : ftype = "*.D7"
                Case 31 : ftype = "*.D8"
                Case 32 : ftype = "*.D9"
                Case 33 : ftype = "*.F1"
                Case 34 : ftype = "*.F2"
                Case 35 : ftype = "*.F3"
                Case 36 : ftype = "*.F4"
                Case 37 : ftype = "*.F5"
                Case 38 : ftype = "*.F6"
                Case 39 : ftype = "*.F7"
                Case 40 : ftype = "*.F8"
                Case 41 : ftype = "*.F9"
                Case 42 : ftype = "*.F10"
                Case 43 : ftype = "*.F11"
                Case 44 : ftype = "*.FUN"
                Case 45 : ftype = "*.HX3"
                Case 46 : ftype = "*.HX4"
                Case 47 : ftype = "*.HX5"
                Case 48 : ftype = "*.HX7"
                Case 49 : ftype = "*.HX8"
                Case 50 : ftype = "*.ID"
                Case 51 : ftype = "*.IDS"
                Case 52 : ftype = "*.JCJ"
                Case 53 : ftype = "*.JCP"
                Case 54 : ftype = "*.JCT"
                Case 55 : ftype = "*.JOB"
                Case 56 : ftype = "*.L3"
                Case 57 : ftype = "*.L3M"
                Case 58 : ftype = "*.LBK"
                Case 59 : ftype = "*.LBL"
                Case 60 : ftype = "*.LBM"
                Case 61 : ftype = "*.LBN"
                Case 62 : ftype = "*.LIH"
                Case 63 : ftype = "*.LIM"
                Case 64 : ftype = "*.LIN"
                Case 65 : ftype = "*.K?"
                Case 66 : ftype = "*.K??"
                Case 67 : ftype = "*.K???"
                Case 68 : ftype = "*.K????"
                Case 69 : ftype = "*.M1"
                Case 70 : ftype = "*.M2"
                Case 71 : ftype = "*.M3"
                Case 72 : ftype = "*.M4"
                Case 73 : ftype = "*.M5"
                Case 74 : ftype = "*.M6"
                Case 75 : ftype = "*.M7"
                Case 76 : ftype = "*.M8"
                Case 77 : ftype = "*.M9"
                Case 78 : ftype = "*.M10"
                Case 79 : ftype = "*.M11"
                Case 80 : ftype = "*.MAS"
                Case 81 : ftype = "*.MTL"
                Case 82 : ftype = "*.NPR"
                Case 83 : ftype = "*.PAT"
                Case 84 : ftype = "*.P1H"
                Case 85 : ftype = "*.P1M"
                Case 86 : ftype = "*.P1S"
                Case 87 : ftype = "*.P3"
                Case 88 : ftype = "*.P4"
                Case 89 : ftype = "*.P5"
                Case 90 : ftype = "*.P6"
                Case 91 : ftype = "*.P61"
                Case 92 : ftype = "*.P62"
                Case 93 : ftype = "*.P7"
                Case 94 : ftype = "*.P8"
                Case 95 : ftype = "*.P9"
                Case 96 : ftype = "*.P9P"
                Case 97 : ftype = "*.R"
                Case 98 : ftype = "*.RSI"
                Case 99 : ftype = "*.RU"
                Case 100 : ftype = "*.SCP"
                Case 101 : ftype = "*.SEC"
                Case 102 : ftype = "*.SEV"
                Case 103 : ftype = "*.SFI"
                Case 104 : ftype = "*.T3"
                Case 105 : ftype = "*.T4"
                Case 106 : ftype = "*.T5"
                Case 107 : ftype = "*.T6"
                Case 108 : ftype = "*.T7"
                Case 109 : ftype = "*.T8"
                Case 110 : ftype = "*.U3S"
                Case 111 : ftype = "*.U4S"
                Case 112 : ftype = "*.XMJ"
                Case 113 : ftype = "*.XYZ"
                Case 114 : ftype = "*.Y"
                Case 115 : ftype = "*.Y?"
                Case 116 : ftype = "*.Y??"

                    'Stiffness Matrix Dump Files
                Case 117 : ftype = "*.TXA"
                Case 118 : ftype = "*.TXC"
                Case 119 : ftype = "*.TXE"
                Case 120 : ftype = "*.TXK"
                Case 121 : ftype = "*.TXM"

                    'Log Files
                Case 122 : ftype = "*GOModel.txt"
                    '                    Case 122: ftype = "*.LOG"
                    '                    Case 123: ftype = "*.WRN"
                    '                    Case 124: ftype = "*.EKO"
                    '                    Case 125: ftype = "*.LOH"
                    '                    Case 126: ftype = "*.OUT"
                    '                    Case 127: ftype = "*.MON"
                    '                    Case 128: ftype = "*.$OG"
                    '                    Case 129: ftype = "*.$OS"
                    '                    Case 130: ftype = "*.$OT"
                    '
                    'Other Files
                    '                    Case 131: ftype = "*.mdb"
                    '                    Case 132: ftype = "*.ebk"
                    '                    Case 133: ftype = "*.$et"
                Case Else
                    ftype = ""
            End Select

            CommandLine = "FOR /R " & Chr(34) & path & Chr(34) & " %%G IN (" & ftype & ") DO del " & Chr(34) & "%%G" & Chr(34)

            Call Print_Batch(CommandLine, BatchPath)
        Next i

        CommandLine = "exit"    'Adds command in batch file to close command line after batch run

        'Appends 'exit' command to batch file
        Call Print_Batch(CommandLine, BatchPath)

        Call Run_Batch(BatchPath, True)

    End Sub

    Sub Del_EDB_IDConvert()
        'Deletes any existing .EDB files in the file structure that the mapped files are copied to
        Dim path As String

        path = "" 'Update - regtTest.PathMapTarget      'Retrieves variable from storage in Excel

        Call BatchWrite_Del_SingleFileType(path, "EDB")

    End Sub

    Sub BatchWrite_Del_SingleFileType(path As String, FileExtension As String)
        'Writes Batch File that Deletes all files in the specified location and subdirectories by extension - leave off *. when specifying file extension

        Dim CommandLine As String
        Dim BatchName As String
        Dim BatchPath As String

        BatchName = "DeleteFiles - " & FileExtension & ".bat"
        BatchPath = Application.StartupPath & "\" & BatchName

        ' Deletes Batch File
        Call KillFile(BatchPath)

        'Writes batch file with commands to delete various analysis files

        FileExtension = "*." & FileExtension

        CommandLine = "FOR /R " & Chr(34) & path & Chr(34) & " %%G IN (" & FileExtension & ") DO del " & Chr(34) & "%%G" & Chr(34)
        Call Print_Batch(CommandLine, BatchPath)
        CommandLine = "exit"    'Adds command in batch file to close command line after batch run
        Call Print_Batch(CommandLine, BatchPath)

        Call Run_Batch(BatchPath, True)

    End Sub

    Sub Run_Batch(BatchPath As String, Optional Delete As Boolean = False)
        'Runs batch file then deletes batch file
        'Note: DO NOT use relative paths in batch files with this procedure. Use absolute paths.??? 
        'Maybe not necessary with this method

        Dim path As String

        'Update - 
        path = "C:\MPT-CSI\Regression & Verification Suites\verification_demo_2013-12-01"
        'Update - 

        'Redundant after below?
        ChDir(path)

        'Runs batch file & waits for command line to finish
        Dim psi As New ProcessStartInfo(BatchPath)
        psi.RedirectStandardError = True
        psi.RedirectStandardOutput = True
        psi.CreateNoWindow = False
        psi.WorkingDirectory = path
        psi.WindowStyle = ProcessWindowStyle.Hidden
        psi.UseShellExecute = False

        Dim process As Process = process.Start(psi)

        MsgBox("""" & BatchPath & """")

        ' Deletes Batch File
        If Delete = True Then Call KillFile(BatchPath)
    End Sub

    Sub Print_Batch(CommandLine As String, BatchPath As String)
        'Writes Batch File

        On Error GoTo Err_Handler

        Dim record As String
        Dim objWriter As New System.IO.StreamWriter(BatchPath, True)
        objWriter.WriteLine(CommandLine) ' Append file string
        objWriter.Close()

Exit_Err_Handler:
        Exit Sub

Err_Handler:
        MsgBox("The following error has occured" & vbCrLf & vbCrLf & _
            "Error Number: " & Err.Number & vbCrLf & _
            "Error Source: AppendTxt" & vbCrLf & _
            "Error Description: " & Err.Description, vbCritical, "An Error has Occured!")
        GoTo Exit_Err_Handler

    End Sub

    Sub KillFile(fileName As String)
        'Deletes File

        Dim S As String

        S = Dir(fileName)
        On Error GoTo ErrHandler
        If S <> "" Then File.Delete(fileName)

        Exit Sub

ErrHandler:

    End Sub

    Sub Zip_All_Files_in_Folder()
        'Zips all files and subfolders in report folder at the same location

        Dim FileNameZip     'DO NOT specify type
        Dim PathReport     'DO NOT specify type

        Dim oApp As Object

        PathReport = "" 'Update - regTest.PathReport      'Retrieves variable from storage in Excel

        FileNameZip = PathReport & ".zip"

        'Create empty Zip File
        NewZip(FileNameZip)

        oApp = CreateObject("Shell.Application")
        'Copy the files to the compressed folder
        oApp.Namespace(FileNameZip).CopyHere(oApp.Namespace(PathReport).self)

        'Keep script waiting until Compressing is done
        Do Until oApp.Namespace(FileNameZip).items.Count = 1
            System.Threading.Thread.Sleep(1000)
        Loop

        MsgBox("You find the zipfile here: " & FileNameZip)
    End Sub

    Sub Zip_All_SubFolders_in_Folder()
        'Zips all files and subfolders in report folder at the same location

        Dim FileNameZip
        Dim PathZip

        Dim oApp As Object
        Dim i As Long
        Dim j As Long
        Dim ArItem() As Object
        Dim item As String
        Dim PathReport As String
        Dim FileNameZipFolder As String
        Dim Status As Integer

        PathReport = "" 'Update - regTest.PathReport      'Retrieves variable from storage in Excel
        FileNameZipFolder = PathReport & "\Zipped Models"
        MkDir(FileNameZipFolder)

        For j = 1 To 2

            Select Case j
                Case 1
                    ArItem = NoMatchArrayPath
                    item = ".zip"
                Case 2
                    ArItem = NoMatchArrayPathRun
                    item = "_Run.zip"
                Case Else
                    ReDim ArItem(0)
                    ArItem(0) = ""
                    item = ""
            End Select

            For i = 0 To UBound(ArItem)
                PathZip = ArItem(i)
                FileNameZip = FileNameZipFolder & "\" & Right(PathZip, 6) & item

                'Create empty Zip File
                NewZip(FileNameZip)

                oApp = CreateObject("Shell.Application")
                oApp.Namespace(FileNameZip).CopyHere(oApp.Namespace(PathZip).self)

                'Keep script waiting until Compressing is done
                Do Until oApp.Namespace(FileNameZip).items.Count = 1
                    System.Threading.Thread.Sleep(1000)
                Loop

            Next i
        Next j

        Status = MsgBox("You find the zipfile here: " & FileNameZipFolder, vbMsgBoxSetForeground)
    End Sub

    Sub NewZip(sPath)
        'Create empty Zip File

        If Len(Dir(sPath)) > 0 Then File.Delete(sPath)

        'See http://msdn.microsoft.com/en-us/library/vstudio/system.io.compression.gzipstream
        Dim directorySelected As DirectoryInfo = New DirectoryInfo(sPath)

        For Each fileToCompress As FileInfo In directorySelected.GetFiles()
            Compress(fileToCompress)
        Next

    End Sub

    Sub Compress(ByVal fileToCompress As FileInfo)
        'Compresses files & directory into ZIP folder
        'See http://msdn.microsoft.com/en-us/library/vstudio/system.io.compression.gzipstream
        Using originalFileStream As FileStream = fileToCompress.OpenRead()
            If (File.GetAttributes(fileToCompress.FullName) And FileAttributes.Hidden) <> FileAttributes.Hidden And fileToCompress.Extension <> ".gz" Then
                Using compressedFileStream As FileStream = File.Create(fileToCompress.FullName + ".gz")
                    Using compressionStream As GZipStream = New GZipStream(compressedFileStream, CompressionMode.Compress)
                        originalFileStream.CopyTo(compressionStream)
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.", _
                                          fileToCompress.Name, fileToCompress.Length.ToString(), compressedFileStream.Length.ToString())
                    End Using
                End Using
            End If
        End Using
    End Sub
End Module
