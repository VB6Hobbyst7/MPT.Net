Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.ObjectModel

Imports CSiTester.cUnitsEnum
Imports CSiTester.cLibLists

''' <summary>
''' Aggregate object of unit objects, which defines the schema of composite units and possibly contains the values used in the schema.
''' </summary>
''' <remarks></remarks>
Public Class cUnits
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
    Private Const _POWER_DENOMINATOR As String = "-"
#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' List of all of the schema components in the units object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _schemaComponents As New List(Of String)
#End Region

#Region "Properties: Friend"
    Private _unitsNumerator As New ObservableCollection(Of cUnit)
    ''' <summary>
    ''' List of unit objects that are to be placed in the numerator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsNumerator As ObservableCollection(Of cUnit)
        Get
            Return New ObservableCollection(Of cUnit)(_unitsNumerator)
        End Get
    End Property

    Private _unitsDenominator As New ObservableCollection(Of cUnit)
    ''' <summary>
    ''' List of unit objects that are to be placed in the denominator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsDenominator As ObservableCollection(Of cUnit)
        Get
            Return New ObservableCollection(Of cUnit)(_unitsDenominator)
        End Get
    End Property

    ''' <summary>
    ''' List of all unit objects, both numerators and deniminators.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property unitsAll As ObservableCollection(Of cUnit)
        Get
            Dim completeList As New ObservableCollection(Of cUnit)(unitsNumerator)
            For Each unit As cUnit In unitsDenominator
                completeList.Add(unit)
            Next
            Return completeList
        End Get
    End Property

    ''' <summary>
    ''' Optional property. Label for the shorthand term to refer to for the unit schema &amp; value combination. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property shorthandLabel As String
#End Region

#Region "Initialization"
    Friend Sub New()
        InitializeData()
        SetDefaults()
    End Sub

    Private Sub InitializeData()

    End Sub

    Private Sub SetDefaults()

    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cUnits

        With myClone
            ._schemaComponents = _schemaComponents

            For Each unitNumerator As cUnit In unitsNumerator
                .unitsNumerator.Add(CType(unitNumerator.Clone, cUnit))
            Next

            For Each unitDenominator As cUnit In unitsDenominator
                .unitsDenominator.Add(CType(unitDenominator.Clone, cUnit))
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="p_object">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal p_object As Object) As Boolean
        If Not (TypeOf p_object Is cUnits) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cUnits = TryCast(p_object, cUnits)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If ListsAreDifferent(._schemaComponents, _schemaComponents) Then Return False

            For Each unitOuter As cUnit In unitsNumerator
                isMatch = False
                For Each unitInner As cUnit In .unitsNumerator
                    If unitInner.Equals(unitOuter) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            For Each unitOuter As cUnit In unitsDenominator
                isMatch = False
                For Each unitInner As cUnit In .unitsDenominator
                    If unitInner.Equals(unitOuter) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    'Add/Remove Methods
    ''' <summary>
    ''' Adds the unit object provided to the units object.
    ''' </summary>
    ''' <param name="p_unit">Unit object to add.</param>
    ''' <remarks></remarks>
    Friend Sub AddUnit(ByVal p_unit As cUnit,
                       Optional ByVal p_index As Integer = -1)
        If p_unit.numerator Then
            AddUnitsNumerator(p_unit, p_index)
        Else
            AddUnitsDenominator(p_unit, p_index)
        End If
    End Sub

    ''' <summary>
    ''' Removes the specified unit object from the units object.
    ''' </summary>
    ''' <param name="p_unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub RemoveUnit(ByVal p_unit As cUnit)
        If p_unit.numerator Then
            RemoveUnitsNumerator(p_unit)
        Else
            RemoveUnitsDenominator(p_unit)
        End If
    End Sub
    ''' <summary>
    ''' Removes the unit object specified by position from the units object.
    ''' </summary>
    ''' <param name="p_index">The index within the numerator or denominator set from which to remove a unit.</param>
    ''' <param name="p_isNumerator">If true, then the unit will be removed from the numerator set, if it exists.
    ''' Else, the unit will be removed from the denominator set if it exists.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub RemoveUnit(ByVal p_index As Integer,
                                    ByVal p_isNumerator As Boolean)
        If p_isNumerator Then
            RemoveUnitsNumerator(p_index)
        Else
            RemoveUnitsDenominator(p_index)
        End If
    End Sub

    ''TODO: Test
    ''' <summary>
    ''' Combines two units objects into one units object as if they have been multiplied/divided together.
    ''' A resulting conversion factor is returned.
    ''' </summary>
    ''' <param name="p_units">Units object to combine with the current units object.</param>
    ''' <param name="p_divideUnits">If true, then the provided set of units will be inverted before being multiplied by the current units.
    ''' Else, the two sets of units will be assumed multiplied together.</param>
    ''' <param name="p_simplifyUnits">If true, the resulting combined units will be simplified between numerator/denominator, and also between different units of the same type if a base list is provided.</param>
    ''' <param name="p_simplifiedUnitsList">If provided, the list allows simplification of different units of the same type.</param>
    ''' <remarks></remarks>
    Friend Function CombineUnits(ByVal p_units As cUnits,
                                Optional ByVal p_divideUnits As Boolean = False,
                                Optional ByVal p_simplifyUnits As Boolean = False,
                                Optional ByVal p_simplifiedUnitsList As List(Of String) = Nothing) As Double
        Dim unitConversionFactor As Double = 1

        If p_divideUnits Then p_units.SwapNumeratorsDenominators()

        If p_simplifyUnits Then
            unitConversionFactor = CombineUnitsSet(p_units.unitsNumerator, _unitsNumerator, p_simplifiedUnitsList)
            unitConversionFactor /= CombineUnitsSet(p_units.unitsDenominator, _unitsDenominator, p_simplifiedUnitsList)
            unitConversionFactor *= SimplifyNumeratorsDenominators(p_simplifiedUnitsList)
        Else
            CombineUnitsSet(p_units.unitsNumerator, _unitsNumerator)
            CombineUnitsSet(p_units.unitsDenominator, _unitsDenominator)
        End If

        Return unitConversionFactor
    End Function

    ''' <summary>
    ''' Parses a string into a units object composed of unit objects.
    ''' </summary>
    ''' <param name="p_unitsString">String to parse into a composite units object.</param>
    ''' <param name="p_addToExisting">If 'true', then the parsed string objects will be added to the existing class state. 
    ''' The default 'false' means that the class state will be overwritten.</param>
    ''' <remarks></remarks>
    Friend Sub ParseStringToUnits(ByVal p_unitsString As String,
                                  Optional ByVal p_addToExisting As Boolean = False)
        Dim symbolicParser As New cSymbolicParser
        Dim units As New List(Of cUnit)

        units = symbolicParser.ParseStringToUnits(p_unitsString)

        If Not p_addToExisting Then
            _unitsNumerator.Clear()
            _unitsDenominator.Clear()
        End If

        For Each unitItem As cUnit In units
            If unitItem.numerator Then
                _unitsNumerator.Add(unitItem)
            Else
                _unitsDenominator.Add(unitItem)
            End If
        Next
    End Sub

    'Schema Methods
    ''' <summary>
    ''' Returns a string of the units schema with the unit types used.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSchemaLabel() As String
        Dim numeratorString As String = ParseUnitsString(unitsNumerator, True)
        Dim denominatorString As String = ParseUnitsString(unitsDenominator, True)

        Return CombineNumeratorAndDenominator(numeratorString, denominatorString)
    End Function

    ''' <summary>
    ''' Returns a list of all schema elements used, including powers.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSchemaList() As List(Of String)
        Dim schemaList As New List(Of String)
        Dim schemaEntry As String

        For Each unitItem As cUnit In _unitsNumerator
            schemaEntry = unitItem.GetUnitLabel(True, True)
            schemaList.Add(schemaEntry)
        Next
        For Each unitItem As cUnit In _unitsDenominator
            schemaEntry = unitItem.GetUnitLabel(True, True)
            schemaList.Add(schemaEntry)
        Next

        Return schemaList
    End Function

    ''' <summary>
    ''' Determines if a set of units matches the schema of the current object.
    ''' </summary>
    ''' <param name="p_unitsCompare">Units object to compare to the current object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SchemasMatch(ByVal p_unitsCompare As cUnits) As Boolean

        _schemaComponents = GetSchemaList()

        If Not p_unitsCompare.unitsNumerator.Count = unitsNumerator.Count Then Return False
        For Each unitNumeratorCompare As cUnit In p_unitsCompare.unitsNumerator
            If Not NumeratorMatch(unitNumeratorCompare) Then Return False
        Next

        If Not p_unitsCompare.unitsDenominator.Count = unitsDenominator.Count Then Return False
        For Each unitDenominatorCompare As cUnit In p_unitsCompare.unitsDenominator
            If Not DenominatorMatch(unitDenominatorCompare) Then Return False
        Next

        Return True
    End Function

    'Units Methods
    ''' <summary>
    ''' Returns a string of the composite units label with the unit names used.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetUnitsLabel() As String
        Dim numeratorString As String = ParseUnitsString(unitsNumerator)
        Dim denominatorString As String = ParseUnitsString(unitsDenominator)

        Return CombineNumeratorAndDenominator(numeratorString, denominatorString)
    End Function

    ''' <summary>
    ''' Returns the list of all base units used in the composite unit.
    ''' </summary>
    ''' <returns></returns>
    ''' <param name="p_withPowers">If true, then the unit names also have the power listed. 
    ''' Otherwise, only the list of units is returned.</param>
    ''' <remarks></remarks>
    Friend Function GetUnitsList(Optional ByVal p_withPowers As Boolean = False) As List(Of String)
        Dim unitList As New List(Of String)
        Dim unitEntry As String = ""

        For Each unitItem As cUnit In _unitsNumerator
            unitEntry = unitItem.GetUnitLabel(False, p_withPowers)
            unitList.Add(unitEntry)
        Next
        For Each unitItem As cUnit In _unitsDenominator
            unitEntry = unitItem.GetUnitLabel(False, p_withPowers)
            unitList.Add(unitEntry)
        Next

        Return unitList

    End Function

    'Manipulate/Convert Methods
    ''' <summary>
    ''' Swaps the numerator and denominator unit sets.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SwapNumeratorsDenominators()
        Dim swapUnits As New ObservableCollection(Of cUnit)
        For Each unit As cUnit In _unitsNumerator
            swapUnits.Add(unit)
        Next

        _unitsNumerator.Clear()
        For Each unit As cUnit In _unitsDenominator
            _unitsNumerator.Add(unit)
        Next

        _unitsDenominator = swapUnits
    End Sub


    ''' <summary>
    ''' Returns the conversion factor to convert the supplied units object to the current units object.
    ''' </summary>
    ''' <param name="p_unitsConvert">Units object that is to be converted to the current units object.
    ''' It is assumed that the schemas of the two units objects match. If they don't, 0 is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function Convert(ByVal p_unitsConvert As cUnits) As Double
        Dim unitsConversionFactor As Double = 1
        Dim unitConvert As New cUnit

        For Each unitNumeratorCompare As cUnit In p_unitsConvert.unitsNumerator
            unitConvert = GetUnitByMatchingTypeNumerator(unitNumeratorCompare)
            If unitConvert Is Nothing Then Return 0
            unitsConversionFactor *= (unitConvert.Convert(unitNumeratorCompare) ^ CDbl(unitNumeratorCompare.power))
        Next

        For Each unitDenominatorCompare As cUnit In p_unitsConvert.unitsDenominator
            unitConvert = GetUnitByMatchingTypeDenominator(unitDenominatorCompare)
            If unitConvert Is Nothing Then Return 0
            unitsConversionFactor /= (unitConvert.Convert(unitDenominatorCompare) ^ CDbl(unitDenominatorCompare.power))
        Next

        Return unitsConversionFactor
    End Function

    ' Query
    ''' <summary>
    ''' Returns true if all units have a unit name set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function AreUnitsSet() As Boolean
        If (unitsNumerator.Count = 0 AndAlso
            unitsDenominator.Count = 0) Then Return False

        For Each unit As cUnit In unitsNumerator
            If String.IsNullOrEmpty(unit.name) Then Return False
        Next

        For Each unit As cUnit In unitsDenominator
            If String.IsNullOrEmpty(unit.name) Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns true if all units have a type set for the schema.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSchemaSet() As Boolean
        If (unitsNumerator.Count = 0 AndAlso
            unitsDenominator.Count = 0) Then Return False

        For Each unit As cUnit In unitsNumerator
            If unit.type = eUnitType.none Then Return False
        Next

        For Each unit As cUnit In unitsDenominator
            If unit.type = eUnitType.none Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Adds a unit to the collection of numerator unit objects.
    ''' </summary>
    ''' <param name="p_unit">Unit object to add.</param>
    ''' <param name="p_index">If specified, the unit is inserted at this index in the list if it is a valid index.</param>
    ''' <remarks></remarks>
    Private Sub AddUnitsNumerator(ByVal p_unit As cUnit,
                                  Optional ByVal p_index As Integer = -1)
        If p_index = -1 Then
            _unitsNumerator.Add(p_unit)
        Else
            _unitsNumerator.Insert(p_index, p_unit)
        End If

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsNumerator"))
    End Sub
    ''' <summary>
    ''' Removes the specified unit object from the units numerator collection.
    ''' </summary>
    ''' <param name="p_unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsNumerator(ByVal p_unit As cUnit)
        Dim tempList As New ObservableCollection(Of cUnit)

        For Each unit As cUnit In _unitsNumerator
            If Not IsMatchingUnit(p_unit, unit) Then
                tempList.Add(unit)
            End If
        Next

        _unitsNumerator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsNumerator"))
    End Sub
    ''' <summary>
    ''' Removes a unit object from the numerator collection based on the provided unit type.
    ''' </summary>
    ''' <param name="p_unitType">Type of unit to remove from the list.</param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsNumerator(ByVal p_unitType As eUnitType)
        Dim tempList As New ObservableCollection(Of cUnit)

        For Each unit As cUnit In _unitsNumerator
            If Not p_unitType = unit.type Then
                tempList.Add(unit)
            End If
        Next

        _unitsNumerator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsNumerator"))
    End Sub
    ''' <summary>
    ''' Removes a unit object from the numerator collection based on the provided index, if valid.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsNumerator(ByVal p_index As Integer)
        Dim tempList As New ObservableCollection(Of cUnit)

        If _unitsNumerator.Count = 0 Then Exit Sub

        For i = 0 To _unitsNumerator.Count - 1
            If Not i = p_index Then tempList.Add(_unitsNumerator(i))
        Next
        _unitsNumerator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsNumerator"))
    End Sub

    ''' <summary>
    ''' Adds a unit to the collection of denominator unit objects.
    ''' </summary>
    ''' <param name="p_unit">Unit object to add.</param>
    ''' <param name="p_index">If specified, the unit is inserted at this index in the list if it is a valid index.</param>
    ''' <remarks></remarks>
    Private Sub AddUnitsDenominator(ByVal p_unit As cUnit,
                                    Optional ByVal p_index As Integer = -1)
        If p_index = -1 Then
            _unitsDenominator.Add(p_unit)
        Else
            _unitsDenominator.Insert(p_index, p_unit)
        End If

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsDenominator"))
    End Sub
    ''' <summary>
    ''' Removes the specified unit object from the units denominator collection.
    ''' </summary>
    ''' <param name="p_unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsDenominator(ByVal p_unit As cUnit)
        Dim tempList As New ObservableCollection(Of cUnit)

        For Each unit As cUnit In _unitsDenominator
            If Not IsMatchingUnit(p_unit, unit) Then
                tempList.Add(unit)
            End If
        Next

        _unitsDenominator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsDenominator"))
    End Sub
    ''' <summary>
    ''' Removes a unit object from the denominator collection based on the provided unit type.
    ''' </summary>
    ''' <param name="p_unitType">Type of unit to remove from the list.</param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsDenominator(ByVal p_unitType As eUnitType)
        Dim tempList As New ObservableCollection(Of cUnit)

        For Each unit As cUnit In _unitsDenominator
            If Not p_unitType = unit.type Then
                tempList.Add(unit)
            End If
        Next

        _unitsDenominator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsDenominator"))
    End Sub
    ''' <summary>
    ''' Removes a unit object from the denominator collection based on the provided index, if valid.
    ''' </summary>
    ''' <param name="p_index"></param>
    ''' <remarks></remarks>
    Private Overloads Sub RemoveUnitsDenominator(ByVal p_index As Integer)
        Dim tempList As New ObservableCollection(Of cUnit)

        If _unitsDenominator.Count = 0 Then Exit Sub

        For i = 0 To _unitsDenominator.Count - 1
            If Not i = p_index Then tempList.Add(_unitsDenominator(i))
        Next
        _unitsDenominator = tempList

        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("unitsDenominator"))
    End Sub

    ''' <summary>
    ''' Determines if the two unit objects describe the same unit in terms of overall placement &amp; quality.
    ''' </summary>
    ''' <param name="p_unitBase">First unit to compare.</param>
    ''' <param name="p_unitCompare">Second unit to compare.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMatchingUnit(ByVal p_unitBase As cUnit,
                                    ByVal p_unitCompare As cUnit) As Boolean
        If (p_unitBase.type = p_unitCompare.type AndAlso
                p_unitBase.power = p_unitCompare.power AndAlso
                p_unitBase.name = p_unitCompare.name) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns a string representing the units object, showing either the schema structure or the units as written with unit names.
    ''' </summary>
    ''' <param name="p_unitsList">List of </param>
    ''' <param name="p_parseSchema">If true, then the parsing will be only be done based on the schema of the provided unit objects. 
    ''' If false, the parsing will be on the unit names of the provided unit objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ParseUnitsString(ByVal p_unitsList As ObservableCollection(Of cUnit),
                                      Optional ByVal p_parseSchema As Boolean = False) As String
        Dim schemaString As String = ""
        Dim count As Integer = 0

        For Each unit As cUnit In p_unitsList
            Dim unitString As String = unit.GetUnitLabel(p_parseSchema, True, False)

            Dim multiplierString As String = ""
            If Not count = 0 Then multiplierString = _MULTIPLIER

            If Not String.IsNullOrEmpty(schemaString) Then schemaString &= _MULTIPLIER
            schemaString &= unitString

            count += 1
        Next

        If p_unitsList.Count > 1 Then
            schemaString = _OPENPARENTHESIS & schemaString & _CLOSEPARENTHESIS
        End If

        Return schemaString
    End Function

    ''' <summary>
    ''' Takes the provided numerator &amp; denominator strings and depending on content, combines them as 'a', 'a/b', or '1/b'.
    ''' </summary>
    ''' <param name="p_numeratorString">String representing the numerator components of the units object.</param>
    ''' <param name="p_denominatorString">String representing the denominator components of the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CombineNumeratorAndDenominator(ByVal p_numeratorString As String,
                                                    ByVal p_denominatorString As String) As String
        If (Not String.IsNullOrEmpty(p_denominatorString) AndAlso
            Not String.IsNullOrEmpty(p_numeratorString)) Then

            Return p_numeratorString & _DIVISOR & p_denominatorString
        ElseIf (String.IsNullOrEmpty(p_numeratorString) AndAlso
                Not String.IsNullOrEmpty(p_denominatorString)) Then

            Return "1" & _DIVISOR & p_denominatorString
        ElseIf Not String.IsNullOrEmpty(p_numeratorString) Then
            Return p_numeratorString
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' For units in the same set (e.g. numerator, or denominator), combines the units by either adding the unit (if new) or adjusting the exponents (if the unit already exists).
    ''' </summary>
    ''' <param name="p_unitsAdded">Set of units to add.</param>
    ''' <param name="p_unitsExisting">Existing set of units to add to.</param>
    ''' <param name="p_unitsBase">If specified, these are the units to simplify any mismatched units of matching types to.</param>
    ''' <remarks></remarks>
    Private Function CombineUnitsSet(ByVal p_unitsAdded As ObservableCollection(Of cUnit),
                                ByRef p_unitsExisting As ObservableCollection(Of cUnit),
                                Optional ByVal p_unitsBase As List(Of String) = Nothing) As Double
        Dim unitExistsInSet As Boolean = False
        Dim newPower As String = ""
        Dim unitConversionFactor As Double = 1

        For Each addedUnit As cUnit In p_unitsAdded
            unitExistsInSet = False

            For Each existingUnit As cUnit In p_unitsExisting
                If addedUnit.type = existingUnit.type Then
                    If addedUnit.name = existingUnit.name Then
                        SimplifyUnitMultiply(existingUnit, addedUnit)
                        unitExistsInSet = True
                        Exit For
                    Else
                        If p_unitsBase IsNot Nothing Then
                            If ExistsInList(addedUnit.name, p_unitsBase) Then
                                unitConversionFactor *= addedUnit.Convert(existingUnit, addedUnit)
                                SimplifyUnitMultiply(existingUnit, addedUnit)
                                unitExistsInSet = True
                                Exit For
                            ElseIf ExistsInList(existingUnit.name, p_unitsBase) Then
                                unitConversionFactor *= existingUnit.Convert(addedUnit, existingUnit)
                                SimplifyUnitMultiply(existingUnit, addedUnit)
                                unitExistsInSet = True
                                Exit For
                            Else
                                'No simplification is performed as no matching unit was found for conversion.
                            End If
                        Else
                            'No simplication is performed among non-matching units of the same type without a specified base unit.
                        End If
                    End If
                Else
                    'No simplification is performed among non-matching unit types.
                End If
            Next

            If Not unitExistsInSet Then p_unitsExisting.Add(addedUnit)
        Next

        Return unitConversionFactor
    End Function

    ''' <summary>
    ''' Simplifies the two matching units by combining powers.
    ''' </summary>
    ''' <param name="p_existingUnit">Current unit.</param>
    ''' <param name="p_addedUnit">Unit being added.</param>
    ''' <remarks></remarks>
    Private Sub SimplifyUnitMultiply(ByRef p_existingUnit As cUnit,
                                     ByRef p_addedUnit As cUnit)
        p_existingUnit.power = p_existingUnit.MultiplyUnitPowers(p_addedUnit.power)

    End Sub

    ''' <summary>
    ''' Checks the units in the numerator and denominator sets. 
    ''' If the unit exists in both, it is simplified based on powers such that it only exists in one set.
    ''' Unless the specific unit is specified, if the units are of the same type but different unit, no simplification will be perfomed for a given unit.
    ''' </summary>
    ''' <param name="p_unitsBase">If specified, these are the units to simplify any mismatched units of matching types to.</param>
    ''' <remarks></remarks>
    Private Function SimplifyNumeratorsDenominators(Optional ByVal p_unitsBase As List(Of String) = Nothing) As Double
        Dim numeratorIsDenominator As Boolean = False
        Dim unitConversionFactor As Double = 1

        For Each numeratorUnit As cUnit In _unitsNumerator
            numeratorIsDenominator = False

            For Each denominatorUnit As cUnit In _unitsDenominator
                If numeratorUnit.type = denominatorUnit.type Then
                    If numeratorUnit.name = denominatorUnit.name Then
                        SimplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                        Exit For
                    Else
                        If p_unitsBase IsNot Nothing Then
                            If ExistsInList(numeratorUnit.name, p_unitsBase) Then
                                unitConversionFactor /= numeratorUnit.Convert(denominatorUnit, numeratorUnit)
                                SimplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                                Exit For
                            ElseIf ExistsInList(denominatorUnit.name, p_unitsBase) Then
                                unitConversionFactor *= denominatorUnit.Convert(numeratorUnit, denominatorUnit)
                                SimplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                                Exit For
                            Else
                                'No simplification is performed as no matching unit was found for conversion.
                            End If
                        Else
                            'No simplication is performed among non-matching units of the same type without a specified base unit.
                        End If
                    End If
                Else
                    'No simplification is performed among non-matching unit types.
                End If
            Next
        Next

        Return unitConversionFactor
    End Function

    ''' <summary>
    ''' Simplifies the two matching units by combining powers and swapping numerator/denominator classification if necessary.
    ''' </summary>
    ''' <param name="p_numeratorUnit">Unit in the numerator position.</param>
    ''' <param name="p_denominatorUnit">Unit in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SimplifyUnitNumeratorDenominator(ByRef p_numeratorUnit As cUnit,
                                                 ByRef p_denominatorUnit As cUnit)
        Dim power As New cSymbolicBlockPower

        p_numeratorUnit.power = p_numeratorUnit.DivideUnitPowers(p_denominatorUnit.power)

        RemoveUnitsDenominator(p_denominatorUnit)

        If power.IsPowerDenominator(p_numeratorUnit.power) Then
            'Change sign
            p_numeratorUnit.power = Right(p_numeratorUnit.power, p_numeratorUnit.power.Length - 1)

            'Swap numerator in denominator position
            AddUnitsDenominator(p_numeratorUnit)
            RemoveUnitsNumerator(p_numeratorUnit)
        End If
    End Sub

    ''' <summary>
    ''' Determines if the provided unit matches any of the numerator entities of the schema.
    ''' </summary>
    ''' <param name="p_unitCompare">Unit object to compare to the current schema.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NumeratorMatch(ByVal p_unitCompare As cUnit) As Boolean
        Dim unitSchemaComponent As String = p_unitCompare.GetUnitLabel(True, True)

        If _schemaComponents.Count = 0 Then _schemaComponents = GetSchemaList()

        For Each schemaComponent As String In _schemaComponents
            If unitSchemaComponent = schemaComponent Then Return True
        Next

        Return False
    End Function
    ''' <summary>
    ''' Determines if the provided unit matches any of the denominator entities of the schema.
    ''' </summary>
    ''' <param name="p_unitCompare">Unit object to compare to the current schema.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DenominatorMatch(ByVal p_unitCompare As cUnit) As Boolean
        Return NumeratorMatch(p_unitCompare)
    End Function

    ''' <summary>
    ''' Returns the unit object within the current collection that has the same type as the provided unit in the numerator position.
    ''' Returns Nothing if no match is found.
    ''' </summary>
    ''' <param name="p_unit">Unit object to use for finding a matching unit object by type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitByMatchingTypeNumerator(ByVal p_unit As cUnit) As cUnit
        For Each unitBase As cUnit In unitsNumerator
            If p_unit.type = unitBase.type Then Return unitBase
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Returns the unit object within the current collection that has the same type as the provided unit in the denominator position.
    ''' Returns Nothing if no match is found.
    ''' </summary>
    ''' <param name="p_unit">Unit object to use for finding a matching unit object by type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnitByMatchingTypeDenominator(ByVal p_unit As cUnit) As cUnit
        For Each unitBase As cUnit In unitsDenominator
            If p_unit.type = unitBase.type Then Return unitBase
        Next

        Return Nothing
    End Function
#End Region
End Class
