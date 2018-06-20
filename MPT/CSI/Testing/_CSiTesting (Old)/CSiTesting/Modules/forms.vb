Option Explicit On

Module forms
    Function IsUserFormLoaded(ByVal UFName As String) As Boolean
        'checks whether a specified form is loaded
        'From http://stackoverflow.com/questions/15439440/check-if-form-is-opened

        Dim frmCol As New FormCollection()
        Dim Cnt As Integer = 0

        frmCol = Application.OpenForms
        For Each f As Form In frmCol
            If f.Name = UFName Then Cnt += 1
        Next
        Return IIf(Cnt > 0, True, False)

    End Function

    Sub closeFormUpdateOpenForms(ByVal formClose As Form, ByVal formOpen As Object)
        'If closing one form should cause updates in another form, such as passing data, actions are done in this function
        Dim i As Long
        Dim programFileType As String()

        formClose.Close()

        If formOpen.Name = "frmRunSetup" Then
            'Program Name Label Update
            formOpen.lblProgram.text = regTest.program_name
            'Combo Box Update. Defaults to standard file type for program selected.
            ' regTest.fileType = regTest.program_name 'Update?

            formOpen.cmbbxFileType.Items.clear()    'Clears combo box
            programFileType = checkDropDownList()   'Determines and assigns new combo list
            formOpen.cmbbxFileType.Items.AddRange(programFileType)  'Populates listbox from corresponding list

            'Combo Box Update.
            For i = 0 To formOpen.cmbbxFileType.Items.Count - 1    'Selects initial cell
                If i = 0 Then
                    If formOpen.cmbbxFileType.Items(i) <> regTest.fileType Then  'Defaults to standard file type for program selected if there is a mismatch between lists.
                        regTest.fileType = regTest.getDefaultFileType
                    End If
                End If
                If formOpen.cmbbxFileType.Items(i) = regTest.fileType Then
                    formOpen.cmbbxFileType.SelectedItem = regTest.fileType
                    Exit For
                End If
            Next i

        End If
    End Sub

    Sub CheckFieldNumeric(ByRef strField As TextBox)
        'Enforces criteria that only numerical entries are allowed in a textbox field

        If Not IsNumeric(strField.Text) Then
            strField.Text = ""
            MsgBox("Please enter numerical characters only")
        End If
    End Sub

    Function checkEntryExistsListBox(ByRef listBoxName As ListBox, ByRef textBoxName As TextBox) As Boolean
        'Checks whether the text in a textbox matches an entry in a listbox.
        Dim i As Long
        checkEntryExistsListBox = False

        For i = 0 To listBoxName.Items.Count - 1  'Selects initial cell
            If listBoxName.Items(i) = textBoxName.Text Then
                checkEntryExistsListBox = True
                MsgBox("Entry already exists.")
                Exit For
            End If
        Next i
    End Function

    Function checkValidEmail(ByRef textBoxName As TextBox) As Boolean
        'checks if a textbox entry e-mail is valid by searching for the "@" character, and then whether a "." character follows
        Dim i As Long
        Dim j As Long
        Dim strEmail As String
        Dim strMatch As Boolean

        strMatch = False
        strEmail = textBoxName.Text

        For i = 1 To Len(strEmail)
            If Mid(strEmail, i, 1) = "@" Then
                strMatch = True
            End If
            If strMatch = True Then
                For j = i To Len(strEmail)
                    If Not Mid(strEmail, j, 1) = "." Then
                        strMatch = False
                    Else
                        strMatch = True
                        checkValidEmail = True
                        Exit Function
                    End If
                Next j
            End If
        Next i
        MsgBox("Please enter a valid e-mail address.")
        checkValidEmail = False
    End Function

    '
    'Sub autoInitialize()
    '    'Fill Text Fields
    '    TextBox_TimeMultiplier.Text = Range("")
    '
    '    'Fill Checkboxes
    '    If Range("") = "yes" Then
    '        CheckBox_LogFiles.CheckState = CheckState.Checked
    '    Else
    '        CheckBox_LogFiles.CheckState = CheckState.UnChecked
    '    End If
    '
    '    'Fill Radio Buttons
    '    If Range("") = "absolute" Then
    '        Option_SourceAbsolute.checked = True
    '        Option_SourceRelative.checked = False
    '    Else
    '        Option_SourceAbsolute.checked = False
    '        Option_SourceRelative.checked = True
    '    End If
    'End Sub

End Module
