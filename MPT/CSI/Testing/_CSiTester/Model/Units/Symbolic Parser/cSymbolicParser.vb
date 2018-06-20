Option Strict On
Option Explicit On

''' <summary>
''' Takes a string and parses it into the mathematical symbolic object specified, such as units.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicParser
#Region "Constants"
    Private Const _POWER As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _DIVISOR As String = "/"
    Private Const _OPEN_PARENTHESIS As String = "("
    Private Const _CLOSE_PARENTHESIS As String = ")"
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns a list of unit objects created from the provided string.
    ''' </summary>
    ''' <param name="p_string">String to transform into a list of unit objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ParseStringToUnits(ByVal p_string As String) As List(Of cUnit)
        Dim blocksList As List(Of cSymbolicBlock) = ParseBlockList(p_string)
        Dim unitBlocks As New List(Of cUnit)

        For Each symbolicBlock As cSymbolicBlock In blocksList
            Dim unitBlock As New cUnit

            With unitBlock
                .numerator = symbolicBlock.isNumerator
                .power = symbolicBlock.blockSuperscript
                .SetUnitName(symbolicBlock.blockName)
                .SetUnitType()
            End With
            unitBlocks.Add(unitBlock)
        Next

        Return unitBlocks
    End Function

#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns a list of symbolic block objects created from the provided string.
    ''' </summary>
    ''' <param name="p_string">String to transform into a list of symbolic block objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseBlockList(ByVal p_string As String) As List(Of cSymbolicBlock)
        Dim blocksList As New List(Of cSymbolicBlock)

        blocksList = ParseBlockListItem(p_string, True)
        If blocksList.Count > 1 Then
            blocksList = ParseBlockListAll(blocksList)
        End If

        Return blocksList
    End Function

    ''' <summary>
    ''' Parses all symbolic blocks into additional symbolic blocks until no additional parsing can be done. 
    ''' Returns a list of every symbolic block in the smalles/simplest form possible.
    ''' </summary>
    ''' <param name="p_blocksList">List of symbolic block items to further parse.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseBlockListAll(ByVal p_blocksList As List(Of cSymbolicBlock)) As List(Of cSymbolicBlock)
        Dim tempBlocksListTotal As New List(Of cSymbolicBlock)

        For Each symbolicBlock As cSymbolicBlock In p_blocksList
            Dim tempBlocksList As List(Of cSymbolicBlock) = ParseBlockListItem(symbolicBlock.blockName, symbolicBlock.isNumerator)

            If tempBlocksList.Count > 1 Then
                Dim tempBlocksSubList As List(Of cSymbolicBlock) = ParseBlockListAll(tempBlocksList)

                For Each subSymbolicBlock As cSymbolicBlock In tempBlocksSubList
                    tempBlocksListTotal.Add(subSymbolicBlock)
                Next
            Else
                tempBlocksListTotal.Add(symbolicBlock)
            End If
        Next

        Return tempBlocksListTotal
    End Function

    ''' <summary>
    ''' Parses the individual string into a symbolic block list of 1-level deep.
    ''' </summary>
    ''' <param name="p_string">String to parse into a symbolic block list.</param>
    ''' <param name="p_isGlobalNumerator">Designation of whether or not the string as a whole is considered to be in the numerator position (true) or denominator position (false).</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseBlockListItem(ByVal p_string As String,
                                        ByVal p_isGlobalNumerator As Boolean) As List(Of cSymbolicBlock)
        'Note: These are not made as private class properties because unique instances are needed in the 
        '       recursive function that calls this function!
        Dim parenthesesCount As Integer = 0
        Dim parenthesesCounting As Boolean = False
        Dim powerCounting As Boolean = False
        Dim blockBase As String = ""
        Dim blockPower As String = ""
        Dim isLocalNumerator As Boolean = True
        Dim isNumerator As Boolean = IsResultNumerator(isLocalNumerator, p_isGlobalNumerator)
        Dim blocksList As New List(Of cSymbolicBlock)

        For Each character As Char In p_string
            Select Case character
                Case CChar(_OPEN_PARENTHESIS)
                    parenthesesCount += 1
                    parenthesesCounting = True
                    If Not parenthesesCount = 1 Then
                        AggregateBlockString(character, blockBase, blockPower, powerCounting)
                    End If
                Case CChar(_CLOSE_PARENTHESIS)
                    parenthesesCount -= 1
                    If parenthesesCount = 0 Then
                        blocksList = UpdateBlocksList(blocksList, isNumerator, blockBase, blockPower)

                        blockBase = ""
                        blockPower = ""
                        parenthesesCounting = False
                    Else
                        AggregateBlockString(character, blockBase, blockPower, powerCounting)
                    End If
                Case CChar(_DIVISOR)
                    'Only consider characters outside of blocks and powers
                    If Not parenthesesCounting Then
                        blocksList = UpdateBlocksList(blocksList, isNumerator, blockBase, blockPower)

                        powerCounting = False
                        blockBase = ""
                        blockPower = ""
                        isLocalNumerator = False
                        isNumerator = IsResultNumerator(isLocalNumerator, p_isGlobalNumerator)
                    Else
                        AggregateBlockString(character, blockBase, blockPower, powerCounting)
                    End If
                Case CChar(_MULTIPLIER)
                    'Only consider characters outside of blocks and powers
                    If Not parenthesesCounting Then
                        blocksList = UpdateBlocksList(blocksList, isNumerator, blockBase, blockPower)

                        powerCounting = False
                        blockBase = ""
                        blockPower = ""
                        isLocalNumerator = True
                        isNumerator = IsResultNumerator(isLocalNumerator, p_isGlobalNumerator)
                    Else
                        AggregateBlockString(character, blockBase, blockPower, powerCounting)
                    End If
                Case CChar(_POWER)
                    'This is meant to occur only at the lowest level block
                    'It always occurs after the base is done, so the switch is never reversed 
                    'Assumed that powers are never stacked
                    If Not parenthesesCounting Then
                        powerCounting = True
                    Else
                        AggregateBlockString(character, blockBase, blockPower, powerCounting)
                    End If
                Case Else
                    AggregateBlockString(character, blockBase, blockPower, powerCounting)
            End Select
        Next

        'If the string encompasses only one block, record it
        If (blocksList.Count = 0 AndAlso p_string.Length > 0) Then
            Dim power As New cSymbolicBlockPower

            'Account for if power is negative
            If power.IsPowerDenominator(blockPower) Then
                isLocalNumerator = False
                isNumerator = IsResultNumerator(isLocalNumerator, p_isGlobalNumerator)
                blockPower = Right(blockPower, blockPower.Length - 1)
            End If

            blocksList = UpdateBlocksList(blocksList, isNumerator, blockBase, blockPower)
            blockBase = ""
        End If

        'Record any unrecorded block
        If Not String.IsNullOrEmpty(blockBase) Then blocksList = UpdateBlocksList(blocksList, isNumerator, blockBase, blockPower)

        Return blocksList
    End Function

    ''' <summary>
    ''' Adds the character to the appropriate string provided.
    ''' </summary>
    ''' <param name="p_character">Character to add to a string.</param>
    ''' <param name="p_blockName">The string that is used for the base/name of the symbolic block.</param>
    ''' <param name="p_blockPower">The string that is used for the power/exponent of the symbolic block.</param>
    ''' <param name="p_useCharForPower">If true, then character will be added to the block power parameter. 
    ''' Else, the character will be added to the block base parameter.</param>
    ''' <remarks></remarks>
    Private Sub AggregateBlockString(ByVal p_character As String,
                                     ByRef p_blockName As String,
                                     ByRef p_blockPower As String,
                                     ByVal p_useCharForPower As Boolean)
        If p_useCharForPower Then
            p_blockPower &= p_character
        Else
            p_blockName &= p_character
        End If
    End Sub

    ''' <summary>
    ''' Returns a list of symbolic block objects with the new symbolic block object added, if valid.
    ''' Purpose of the function is to include validation checks of data.
    ''' If the symbolic block object is not valid, the original list provided is returned unaltered.
    ''' </summary>
    ''' <param name="p_blocksList">List to which the symbolic block is to be added.</param>
    ''' <param name="p_isNumerator">Specifies if the symbolic block is to be treated as a numerator (true), else denominator (false).</param>
    ''' <param name="p_blockName">String to be used as the base name of the symbolic object.</param>
    ''' <param name="p_blockPower">String to be used as the exponent of the symbolic object. May be blank.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateBlocksList(ByVal p_blocksList As List(Of cSymbolicBlock),
                                      ByVal p_isNumerator As Boolean,
                                      ByVal p_blockName As String,
                                      ByVal p_blockPower As String) As List(Of cSymbolicBlock)
        Dim tempBlock As cSymbolicBlock = RecordBlock(p_isNumerator, p_blockName, p_blockPower)
        Dim blocksList As List(Of cSymbolicBlock) = p_blocksList

        If Not String.IsNullOrEmpty(tempBlock.blockName) Then blocksList.Add(tempBlock)

        Return blocksList
    End Function

    ''' <summary>
    ''' Returns the symbolic block object of the paramters provided if the parameters provided are valid.
    ''' Otherwise returns an empty object.
    ''' </summary>
    ''' <param name="p_isNumerator">Specifies if the symbolic block is to be treated as a numerator (true), else denominator (false).</param>
    ''' <param name="p_blockName">String to be used as the base name of the symbolic object. 
    ''' If blank, an empty object is returned.</param>
    ''' <param name="p_blockPower">String to be used as the exponent of the symbolic object. May be blank.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecordBlock(ByVal p_isNumerator As Boolean,
                                 ByVal p_blockName As String,
                                 ByVal p_blockPower As String) As cSymbolicBlock

        Dim newBlock As New cSymbolicBlock

        If String.IsNullOrEmpty(p_blockName) Then Return newBlock

        With newBlock
            .isNumerator = p_isNumerator
            .blockName = p_blockName
            .blockSuperscript = p_blockPower
        End With

        Return newBlock
    End Function

    ''' <summary>
    ''' Determines if the current block is in the numerator or denominator position based on the superposition of the original positiona and the locally determined position.
    ''' </summary>
    ''' <param name="p_isLocalNumerator">Position at the current block level as numerator (true) or denominator (false).</param>
    ''' <param name="p_isGlobalNumerator">Position above the current block level as numerator (true) or denominator (false).</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsResultNumerator(ByVal p_isLocalNumerator As Boolean,
                                       ByVal p_isGlobalNumerator As Boolean) As Boolean
        If (p_isLocalNumerator Xor p_isGlobalNumerator) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class
