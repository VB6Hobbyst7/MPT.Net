Option Explicit On
Option Strict On

Module Excel
    Public Sub ClearOutput()
        Dim rowOffset As Integer
        rowOffset = 1

        wksht = ThisWorkbook.Worksheets(WKSHT_NAME)

        Call ClearColumn(RANGE_NAME_RET_SET, RANGE_NAME_PARAMETER)
        Call ClearColumn(RANGE_NAME_VALUE_GET, RANGE_NAME_PARAMETER)
        Call ClearColumn(RANGE_NAME_RET_GET, RANGE_NAME_PARAMETER)
        Call ClearColumn(RANGE_NAME_VALUE_TABLE, RANGE_NAME_PARAMETER)
    End Sub

    Private Sub ClearColumn(ByVal p_rangeHeaderName As String, _
                            Optional ByVal p_referenceColumnHeaderName As String = "")
        Dim rowOffset As Integer
        rowOffset = 1

        If p_referenceColumnHeaderName = "" Then
            p_referenceColumnHeaderName = p_rangeHeaderName
        End If

        While Not wksht.Range(p_referenceColumnHeaderName).Offset(rowOffset, 0).value = ""
            wksht.Range(p_rangeHeaderName).Offset(rowOffset, 0).value = ""
            rowOffset = rowOffset + 1
        End While
    End Sub

    '==========For Sequence Sheet Only=============
    '' Other global parameters
    'Dim CaseNumber As Integer
    'Dim lookupIndex As Integer
    'Dim col As Integer
    'Dim row As Integer
    'Dim rowStart As Integer
    'Dim colStart As Integer
    'Const wkshtName As String = "Test"
    '
    'Public Sub ClearSequence()
    '    rowStart = 4
    '    colStart = 6
    '
    '    Dim col As Integer
    '    col = colStart
    '    row = rowStart
    '
    '    For i = 0 To 5
    '        For j = 0 To 11
    '            Worksheets(wkshtName).Cells(row + j, col + i).value = ""
    '        Next j
    '    Next i
    'End Sub
    '=======================================

    Public Sub GetSheetParameters(ByRef p_parameters() As String, _
                              ByRef p_caseRows() As Integer, _
                              ByRef p_parametersInputs() As String, _
                              ByRef p_parametersInputsStartRows() As Integer)
        p_parameters = GetParameters()
        p_caseRows = GetCaseRows(p_parameters)
        p_parametersInputs = GetParameterInputs()
        p_parametersInputsStartRows = GetParametersInputsStartRows(p_parametersInputs, p_caseRows)
    End Sub

    Private Function GetParameters() As String()
        Dim parameters() As String
        ReDim Preserve parameters(0)

        Dim rowOffset As Integer
        rowOffset = 1

        Dim index As Integer
        Dim tempItem As String
        tempItem = ""
        While Not wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value = ""
            index = UBound(parameters)
            If rowOffset > 1 Then
                index = index + 1
            End If

            tempItem = wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value

            ' Only add entry if it is unique
            If Not tempItem = "" And Not ExistsInArray(tempItem, parameters) Then
                ReDim Preserve parameters(index)
                parameters(index) = tempItem
            End If

            rowOffset = rowOffset + 1
        End While

        GetParameters = parameters
    End Function

    Private Function GetCaseRows(p_parameters() As String) As Integer()
        Dim cases As Integer
        cases = 0

        Dim rowOffset As Integer
        rowOffset = 1

        While Not wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value = ""
            If p_parameters(0) = wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value Then
                cases = cases + 1
            End If
            rowOffset = rowOffset + 1
        End While

        Dim caseRows() As Integer
        ReDim Preserve caseRows(0)
        If cases > 0 Then
            caseRows(0) = wksht.Range(RANGE_NAME_PARAMETER).row + 1
        End If
        If cases > 1 Then
            Dim i As Integer
            For i = 1 To cases - 1
                ReDim Preserve caseRows(i)
                caseRows(i) = caseRows(i - 1) + UBound(p_parameters) + 1
            Next i
        End If

        GetCaseRows = caseRows
    End Function

    Private Function GetParameterInputs() As String()
        Dim parameterInputs() As String
        ReDim Preserve parameterInputs(0)

        Dim rowOffset As Integer
        rowOffset = 1

        Dim index As Integer
        While Not wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value = ""
            If Not wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value = "" Then
                index = UBound(parameterInputs)
                If Not parameterInputs(0) = "" Then
                    index = index + 1
                End If
                ReDim Preserve parameterInputs(index)

                parameterInputs(index) = wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value
            End If
            rowOffset = rowOffset + 1
        End While

        GetParameterInputs = parameterInputs
    End Function

    ' Gets the row of the first occurrence of each parameter input name provided
    Private Function GetParametersInputsStartRows(p_parametersInputs() As String, _
                                                  p_cases() As Integer) As Integer()
        Dim parametersInputsStartRows() As Integer
        ReDim Preserve parametersInputsStartRows(0)

        Dim index As Integer
        index = 0

        Dim parametersPerCase As Integer
        parametersPerCase = (UBound(p_parametersInputs) + 1) / (UBound(p_cases) + 1)

        Dim i As Integer
        For i = 0 To parametersPerCase - 1
            Dim matchParameter As Boolean
            matchParameter = False

            Dim rowOffset As Integer
            rowOffset = 1


            Do While Not wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).value = ""
                If p_parametersInputs(i) = wksht.Range(RANGE_NAME_INPUT).Offset(rowOffset, 0).value Then
                    ReDim Preserve parametersInputsStartRows(index)

                    parametersInputsStartRows(index) = wksht.Range(RANGE_NAME_PARAMETER).Offset(rowOffset, 0).row
                    matchParameter = True
                    index = index + 1
                End If
                If matchParameter Then Exit Do
                rowOffset = rowOffset + 1
            Loop
        Next i

        GetParametersInputsStartRows = parametersInputsStartRows
    End Function



    Private Function ExistsInArray(ByVal p_item As String, _
                                         p_array() As String) As Boolean
        ExistsInArray = False
        Dim i As Integer
        For i = 0 To UBound(p_array)
            If p_array(i) = p_item Then
                ExistsInArray = True
                Exit For
            End If
        Next i
    End Function
End Module
