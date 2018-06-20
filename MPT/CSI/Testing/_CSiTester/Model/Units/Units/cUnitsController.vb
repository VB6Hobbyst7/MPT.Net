Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports CSiTester.cEnumerations
Imports CSiTester.cUnitsEnum

Imports CSiTester.cLibLists

''' <summary>
''' Handles the highest level controlling of units including:
''' Sets up schema based on selections from pre-defined lists, such as stress.
''' Sets up lists of available pre-defined shorthand labels (such as MPa) for the current units schema.
''' Translates between the base or derived units and other representations of the units, such as psi instead lb/in^2, or MPa instead of N/mm^2.
''' </summary>
''' <remarks></remarks>
Public Class cUnitsController
    Implements ICloneable
    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#Region "Enumerations"
    ''' <summary>
    ''' List of standard unit types available for selection where the schema is already defined in the classes.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnitTypeStandard
        <Description("")> none
        ''' <summary>
        ''' Not included in any type lists, as it indicates that a custom type of cUnits object is to be created.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Custom")> custom
        <Description("D/C")> D_C
        <Description("Strain")> strain
        <Description("Length")> length
        <Description("Area")> area
        <Description("Volume")> volume
        <Description("Displacement")> displacement
        <Description("Displacement (Rotation)")> displacementRotational
        <Description("Velocity")> velocity
        <Description("Acceleration")> acceleration
        <Description("Rotation")> rotation
        <Description("Angular Velocity")> angularVelocity
        <Description("Angular Acceleration")> angularAcceleration
        <Description("Mass")> mass
        <Description("Weight")> weight
        <Description("Density (Mass)")> density_mass
        <Description("Density (Weight)")> density_weight
        <Description("Temperature")> temperature
        <Description("Time")> time
        <Description("Period")> period
        <Description("Frequency")> frequency
        <Description("Force")> force
        <Description("Moment")> moment
        <Description("Stress")> stress
        <Description("Pressure")> pressure
        <Description("Pressure (Line)")> pressure_Line
        <Description("Work")> work
        <Description("Power")> power
        <Description("Rotational Inertia")> rotationalInertia
        <Description("Section Modulus")> sectionModulus
        <Description("Radius of Gyration")> radiusOfGyration
        <Description("Unitless")> unitless
    End Enum

    ''' <summary>
    ''' List of schema types that contain shorthand labels.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnitTypeShorthand
        <Description("")> none
        <Description("Linearly Distributed Force")> forceLineDistribution
        <Description("Pressure or Stress")> pressureOrStress
        <Description("Work")> work
        <Description("Power")> power
        <Description("Speed")> speed
        <Description("Angular Speed")> speedAngular
    End Enum
#End Region

#Region "Properties: Friend"
    Private _quickUnitTypes As New List(Of String)
    ''' <summary>
    ''' List of a subset of 'all unit types' for the most commonly defined units. 
    ''' Used for autogenerating pre-defined unit schemas.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property quickUnitTypes As List(Of String)
        Get
            Return _quickUnitTypes
        End Get
    End Property

    Private _allUnitTypes As New List(Of String)
    ''' <summary>
    ''' List of all unit types that have pre-defined schemas available.
    ''' Used for autogenerating pre-defined unit schemas.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property allUnitTypes As List(Of String)
        Get
            Return _allUnitTypes
        End Get
    End Property

    Private _shorthandUnitsAvailable As New List(Of String)
    ''' <summary>
    ''' List of all of the shorthand units currently available for selection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property shorthandUnitsAvailable As List(Of String)
        Get
            Return _shorthandUnitsAvailable
        End Get
    End Property

    ''' <summary>
    ''' Units object that contains a collection of unit objects that defines a complete unit, such as stress, speed, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property units As New cUnits

    Private _type As eUnitTypeStandard
    ''' <summary>
    ''' The pre-edfined unit type from which the units object is set up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property type As eUnitTypeStandard
        Get
            Return _type
        End Get
        Set(value As eUnitTypeStandard)
            If Not value = type Then
                _type = value
                SetUnitsSchemaByType()
                IsShorthandTypesAvailable()
            End If
        End Set
    End Property

    Private _typeShorthand As eUnitTypeShorthand
    ''' <summary>
    ''' The pre-defined shorthand schema type from which the units object is set up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property typeShorthand As eUnitTypeShorthand
        Get
            Return _typeShorthand
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        Initialization()
    End Sub

    Private Sub Initialization()
        _allUnitTypes = GetEnumDescriptionList(eUnitTypeStandard.none)
        _allUnitTypes.Remove(GetEnumDescription(eUnitTypeStandard.none))
        _allUnitTypes.Remove(GetEnumDescription(eUnitTypeStandard.custom))
        _allUnitTypes.Sort()

        With _quickUnitTypes
            .Add(GetEnumDescription(eUnitTypeStandard.unitless))
            .Add(GetEnumDescription(eUnitTypeStandard.D_C))
            .Add(GetEnumDescription(eUnitTypeStandard.strain))
            .Add(GetEnumDescription(eUnitTypeStandard.displacement))
            .Add(GetEnumDescription(eUnitTypeStandard.displacementRotational))
            .Add(GetEnumDescription(eUnitTypeStandard.force))
            .Add(GetEnumDescription(eUnitTypeStandard.moment))
            .Add(GetEnumDescription(eUnitTypeStandard.stress))
            .Add(GetEnumDescription(eUnitTypeStandard.time))
            .Add(GetEnumDescription(eUnitTypeStandard.period))
            .Add(GetEnumDescription(eUnitTypeStandard.frequency))
        End With
        _quickUnitTypes.Sort()
    End Sub

    Friend Function Clone() As Object Implements System.ICloneable.Clone
        Dim myClone As New cUnitsController

        With myClone
            For Each shorthandUnit As String In shorthandUnitsAvailable
                .shorthandUnitsAvailable.Add(shorthandUnit)
            Next

            .units = CType(units.Clone, cUnits)
            .type = type
            ._typeShorthand = typeShorthand
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
        If Not (TypeOf p_object Is cUnitsController) Then Return False

        Dim comparedObject As cUnitsController = TryCast(p_object, cUnitsController)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If ListsAreDifferent(.shorthandUnitsAvailable, shorthandUnitsAvailable) Then Return False

            If Not .units.Equals(units) Then Return False

            If Not .type = type Then Return False
            If Not .typeShorthand = typeShorthand Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Friend"
    'Set Class Type
    ''' <summary>
    ''' Sets the schema type based on the string name of the schema. 
    ''' If no match is found between the enum descriptions and the supplied string, the type is unchanged.
    ''' </summary>
    ''' <param name="p_schemaType">Name of the schema type to set the class to.</param>
    ''' <remarks></remarks>
    Friend Sub SetTypeByDescription(ByVal p_schemaType As String)
        type = ConvertStringToEnumByDescription(Of eUnitTypeStandard)(p_schemaType)
    End Sub

    ''' <summary>
    ''' Sets the schema type based on the shorthand type provided.
    ''' For shorthands used for multiple types, a default is chosen unless a schema type string name is provided.
    ''' If no match is found, or the shorthand type is 'none' the type is unchanged.
    ''' </summary>
    ''' <param name="p_shorthandType">Shorthand type that the class is to be using.</param>
    ''' <param name="p_schemaType">Name of the schema type to set the class to.</param>
    ''' <remarks></remarks>
    Friend Sub SetTypeByShorthand(ByVal p_shorthandType As eUnitTypeShorthand,
                                  Optional ByVal p_schemaType As String = "")
        Select Case p_shorthandType
            Case eUnitTypeShorthand.none
                If String.IsNullOrEmpty(p_schemaType) Then
                    'No action is taken
                Else
                    SetTypeByDescription(p_schemaType)
                End If
            Case eUnitTypeShorthand.forceLineDistribution
                type = eUnitTypeStandard.pressure_Line
            Case eUnitTypeShorthand.pressureOrStress
                If String.IsNullOrEmpty(p_schemaType) Then
                    type = eUnitTypeStandard.stress
                Else
                    SetTypeByDescription(p_schemaType)
                End If
            Case eUnitTypeShorthand.work
                type = eUnitTypeStandard.work
            Case eUnitTypeShorthand.power
                type = eUnitTypeStandard.power
            Case eUnitTypeShorthand.speed
                type = eUnitTypeStandard.velocity
            Case eUnitTypeShorthand.speedAngular
                If String.IsNullOrEmpty(p_schemaType) Then
                    type = eUnitTypeStandard.frequency
                Else
                    SetTypeByDescription(p_schemaType)
                End If
        End Select
    End Sub

    'Set Units Object
    ''' <summary>
    ''' Sets the type, units object, and other related properties to match the specified shorthand name that is provided as a string.
    ''' If no match is found for unit values, they will be left empty.
    ''' If no match is found for the unit type, the units object will be empty.
    ''' </summary>
    ''' <param name="p_shorthandName">Name of the shorthand unit to set.</param>
    ''' <remarks></remarks>
    Friend Sub ParseStringToShorthandUnits(ByVal p_shorthandName As String)
        AssignShorthandUnits(p_shorthandName)
    End Sub

    ''' <summary>
    ''' Sets the type, units object, and other related properties to match the specified unit string that is provided.
    ''' If no match is found for unit values, they will be left empty.
    ''' If no match is found for the unit type, the units object will be empty.
    ''' </summary>
    ''' <param name="p_units"></param>
    ''' <remarks></remarks>
    Friend Sub ParseStringToUnits(ByVal p_units As String)
        Dim shorthandType As cUnitsController.eUnitTypeShorthand = GetShorthandTypeByName(p_units)

        If shorthandType = cUnitsController.eUnitTypeShorthand.none Then
            units.ParseStringToUnits(p_units)
        Else
            ParseStringToShorthandUnits(p_units)
        End If
    End Sub

    ''' <summary>
    ''' Sets the type, units object, and other related properties to match the specified shorthand type and name.
    ''' A schema type name should be provided for ambiguous cases where multiple types might apply to the same shorthand type.
    ''' </summary>
    ''' <param name="p_shorthandName">Name of the shorthand unit to set.</param>
    ''' <param name="p_schemaType">Name of the schema type, in cases where multiple types might apply to the same shorthand type.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub AssignShorthandUnits(ByVal p_shorthandName As String,
                                              Optional ByVal p_schemaType As String = "")
        If String.IsNullOrEmpty(p_shorthandName) Then
            RemoveShorthandUnits()
            Exit Sub
        End If

        Dim shorthandType As eUnitTypeShorthand = GetShorthandTypeByName(p_shorthandName)
        AssignShorthandUnits(shorthandType, p_shorthandName, p_schemaType)
    End Sub
    ''' <summary>
    ''' Sets the type, units object, and other related properties to match the specified shorthand type and name.
    ''' A schema type name should be provided for ambiguous cases where multiple types might apply to the same shorthand type.
    ''' </summary>
    ''' <param name="p_shorthandType">Type of the shorthand unit to set.</param>
    ''' <param name="p_shorthandName">Name of the shorthand unit to set.</param>
    ''' <param name="p_schemaType">Name of the schema type, in cases where multiple types might apply to the same shorthand type.</param>
    ''' <remarks></remarks>
    Friend Overloads Sub AssignShorthandUnits(ByVal p_shorthandType As eUnitTypeShorthand,
                                                ByVal p_shorthandName As String,
                                                Optional ByVal p_schemaType As String = "")
        Dim tempUnits As cUnits = GetShorthandUnits(p_shorthandType, p_shorthandName)
        If tempUnits Is Nothing Then Exit Sub

        _typeShorthand = p_shorthandType
        If type = eUnitTypeStandard.none Then SetTypeByShorthand(_typeShorthand, p_schemaType)

        units = tempUnits
        units.shorthandLabel = p_shorthandName
    End Sub

    ''' <summary>
    ''' Removes the shorthand-specific properties of the units object, while leaving the rest of the properties intact.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RemoveShorthandUnits()
        _typeShorthand = eUnitTypeShorthand.none
        units.shorthandLabel = ""
    End Sub

    'Shorthand names list
    ''' <summary>
    ''' Returns the list of possible shorthand names based on the current shorthand type assigned to the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandNamesList() As List(Of String)
        Return GetShorthandNamesList(typeShorthand)
    End Function
    ''' <summary>
    ''' Returns the list of possible shorthand names based on the provided shorthand type enumeration.
    ''' </summary>
    ''' <param name="p_shorthandType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandNamesList(ByVal p_shorthandType As eUnitTypeShorthand) As List(Of String)
        Dim shorthandNames As New List(Of String)

        Select Case p_shorthandType
            Case eUnitTypeShorthand.forceLineDistribution
                Dim units As New cUnitForceLineDistribution
                shorthandNames = units.unitsList

            Case eUnitTypeShorthand.pressureOrStress
                Dim units As New cUnitPressureStress
                shorthandNames = units.unitsList

            Case eUnitTypeShorthand.work
                Dim units As New cUnitWork
                shorthandNames = units.unitsList

            Case eUnitTypeShorthand.power
                Dim units As New cUnitPower
                shorthandNames = units.unitsList

            Case eUnitTypeShorthand.speed
                Dim units As New cUnitSpeed
                shorthandNames = units.unitsList

            Case eUnitTypeShorthand.speedAngular
                Dim units As New cUnitSpeedAngular
                shorthandNames = units.unitsList

            Case Else
                'No action is taken, leaving an empty list.
        End Select

        Return shorthandNames
    End Function


    'Determine Shorthand Type
    ''' <summary>
    ''' Returns the shorthand enumeration based on the string name provided.
    ''' </summary>
    ''' <param name="p_shorthandName">Name of the shorthand unit to match to an enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetShorthandTypeByName(ByVal p_shorthandName As String) As eUnitTypeShorthand
        Dim linearForce As New cUnitForceLineDistribution
        For Each item As String In linearForce.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.forceLineDistribution
        Next

        Dim stress As New cUnitPressureStress
        For Each item As String In stress.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.pressureOrStress
        Next

        Dim work As New cUnitWork
        For Each item As String In work.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.work
        Next

        Dim power As New cUnitPower
        For Each item As String In power.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.power
        Next

        Dim speed As New cUnitSpeed
        For Each item As String In speed.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.speed
        Next

        Dim frequency As New cUnitSpeedAngular
        For Each item As String In frequency.unitsList
            If p_shorthandName = item Then Return eUnitTypeShorthand.speedAngular
        Next

        Return eUnitTypeShorthand.none
    End Function

    ''' <summary>
    '''  Returns the shorthand enumeration based on the unit type enumeration of the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandTypeByUnitsType() As eUnitTypeShorthand
        Return GetShorthandTypeByUnitsType(type)
    End Function
    ''' <summary>
    ''' Returns the shorthand enumeration based on the unit type enumeration provided. 
    ''' </summary>
    ''' <param name="p_unitType">Unit type enumeration to be used to determine the shorthand enumeration type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandTypeByUnitsType(ByVal p_unitType As eUnitTypeStandard) As eUnitTypeShorthand
        Select Case p_unitType
            Case eUnitTypeStandard.stress,
                 eUnitTypeStandard.pressure
                Return eUnitTypeShorthand.pressureOrStress

            Case eUnitTypeStandard.pressure_Line
                Return eUnitTypeShorthand.forceLineDistribution

            Case eUnitTypeStandard.work
                Return eUnitTypeShorthand.work

            Case eUnitTypeStandard.power
                Return eUnitTypeShorthand.power

            Case eUnitTypeStandard.angularVelocity,
                 eUnitTypeStandard.frequency
                Return eUnitTypeShorthand.speedAngular

            Case eUnitTypeStandard.velocity
                Return eUnitTypeShorthand.speed

            Case Else
                Return eUnitTypeShorthand.none
        End Select
    End Function

    ''' <summary>
    ''' Returns the shorthand enumeration based on which one, if any, the current schema matches.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandTypeByUnits() As eUnitTypeShorthand
        Return GetShorthandTypeByUnits(units)
    End Function
    ''' <summary>
    ''' Returns the shorthand enumeration based on which one, if any, the schema of the provided units object matches.
    ''' </summary>
    ''' <param name="p_units">Units object to use for determining the shorthand enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetShorthandTypeByUnits(ByVal p_units As cUnits) As eUnitTypeShorthand
        Dim linearForce As New cUnitForceLineDistribution
        If p_units.SchemasMatch(linearForce.GetUnits) Then Return eUnitTypeShorthand.forceLineDistribution

        Dim stress As New cUnitPressureStress
        If p_units.SchemasMatch(stress.GetUnits) Then Return eUnitTypeShorthand.pressureOrStress

        Dim work As New cUnitWork
        If p_units.SchemasMatch(work.GetUnits) Then Return eUnitTypeShorthand.work

        Dim power As New cUnitPower
        If p_units.SchemasMatch(power.GetUnits) Then Return eUnitTypeShorthand.power

        Dim speed As New cUnitSpeed
        If p_units.SchemasMatch(speed.GetUnits) Then Return eUnitTypeShorthand.speed

        Dim angularVelocity As New cUnitSpeedAngular
        If p_units.SchemasMatch(angularVelocity.GetUnits) Then Return eUnitTypeShorthand.speedAngular

        Return eUnitTypeShorthand.none
    End Function

    ''' <summary>
    ''' Returns 'true' if shorthand units are available for a given schema. The list of available shorthand units is also updated in the process.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsShorthandTypesAvailable() As Boolean
        If type = eUnitTypeStandard.custom Then 'Get list by matching schema to known schema types.
            _shorthandUnitsAvailable = GetShorthandNamesList(GetShorthandTypeByUnits(units))
        Else
            _shorthandUnitsAvailable = GetShorthandNamesList(GetShorthandTypeByUnitsType())
        End If

        If _shorthandUnitsAvailable.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns the units object corresponding to the shorthand type and shorthand name. 
    ''' Schema and values are filled where matches are found. 
    ''' Unit values are returned as empty where no matches are found.
    ''' Units is returned as 'Nothing' if no shorthand type match is found.
    ''' </summary>
    ''' <param name="p_shorthandType">Type of shorthand label that is being used (pressure, speed, etc.)</param>
    ''' <param name="p_shorthandName">Name of the shorthand label used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetShorthandUnits(ByVal p_shorthandType As eUnitTypeShorthand,
                                     ByVal p_shorthandName As String) As cUnits
        Dim tempUnits As cUnits = Nothing

        Select Case p_shorthandType
            Case eUnitTypeShorthand.forceLineDistribution
                Dim linearForce As New cUnitForceLineDistribution
                tempUnits = linearForce.GetUnits(p_shorthandName)

            Case eUnitTypeShorthand.pressureOrStress
                Dim stress As New cUnitPressureStress
                tempUnits = stress.GetUnits(p_shorthandName)

            Case eUnitTypeShorthand.work
                Dim work As New cUnitWork
                tempUnits = work.GetUnits(p_shorthandName)

            Case eUnitTypeShorthand.power
                Dim power As New cUnitPower
                tempUnits = power.GetUnits(p_shorthandName)

            Case eUnitTypeShorthand.speed
                Dim speed As New cUnitSpeed
                tempUnits = speed.GetUnits(p_shorthandName)

            Case eUnitTypeShorthand.speedAngular
                Dim frequency As New cUnitSpeedAngular
                tempUnits = frequency.GetUnits(p_shorthandName)

        End Select

        Return tempUnits
    End Function

    ''' <summary>
    ''' Sets the schema of the units object according to a preset arrangement based on specified type.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnitsSchemaByType()
        If type = eUnitTypeStandard.custom Then Exit Sub 'This preserves the current units when switiching to 'custom' from another type.

        units = New cUnits

        Select Case type
            Case eUnitTypeStandard.acceleration
                SetUnitsLength()
                SetUnitsTime("2", False)
            Case eUnitTypeStandard.angularAcceleration
                SetUnitsRotation()
                SetUnitsTime("2", False)
            Case eUnitTypeStandard.angularVelocity
                SetUnitsRotation()
                SetUnitsTime(, False)
            Case eUnitTypeStandard.area
                SetUnitsLength("2")
            Case eUnitTypeStandard.custom
                'No action taken as properties are to be customized
            Case eUnitTypeStandard.D_C
                SetUnitsUnitless()
            Case eUnitTypeStandard.density_mass
                SetUnitsMass()
                SetUnitsLength("3", False)
            Case eUnitTypeStandard.density_weight
                SetUnitsForce()
                SetUnitsLength("3", False)
            Case eUnitTypeStandard.displacement
                SetUnitsLength()
            Case eUnitTypeStandard.displacementRotational
                SetUnitsRotation()
            Case eUnitTypeStandard.force
                SetUnitsForce()
            Case eUnitTypeStandard.frequency
                SetUnitsRotation()
                SetUnitsTime(, False)
            Case eUnitTypeStandard.length
                SetUnitsLength()
            Case eUnitTypeStandard.mass
                SetUnitsMass()
            Case eUnitTypeStandard.moment
                SetUnitsForce()
                SetUnitsLength()
            Case eUnitTypeStandard.none
                'No action taken as properties are to be customized
            Case eUnitTypeStandard.period
                SetUnitsTime()
            Case eUnitTypeStandard.power
                SetUnitsForce()
                SetUnitsLength()
                SetUnitsTime(, False)
            Case eUnitTypeStandard.pressure
                SetUnitsForce()
                SetUnitsLength("2", False)
            Case eUnitTypeStandard.pressure_Line
                SetUnitsForce()
                SetUnitsLength(, False)
            Case eUnitTypeStandard.radiusOfGyration
                SetUnitsLength()
            Case eUnitTypeStandard.rotation
                SetUnitsRotation()
            Case eUnitTypeStandard.rotationalInertia
                SetUnitsLength("4")
            Case eUnitTypeStandard.sectionModulus
                SetUnitsLength("3")
            Case eUnitTypeStandard.strain
                SetUnitsLength()
                SetUnitsLength(, False)
            Case eUnitTypeStandard.stress
                SetUnitsForce()
                SetUnitsLength("2", False)
            Case eUnitTypeStandard.temperature
                SetUnitsTemperature()
            Case eUnitTypeStandard.time
                SetUnitsTime()
            Case eUnitTypeStandard.unitless
                SetUnitsUnitless()
            Case eUnitTypeStandard.velocity
                SetUnitsLength()
                SetUnitsTime(, False)
            Case eUnitTypeStandard.volume
                SetUnitsLength("3")
            Case eUnitTypeStandard.weight
                SetUnitsForce()
            Case eUnitTypeStandard.work
                SetUnitsForce()
                SetUnitsLength()
        End Select
    End Sub


    'The methods below set up the units object according to the correlated base unit.
    ''' <summary>
    ''' Adds a force unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsForce(Optional ByVal p_power As String = "1",
                            Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.force, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a length unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsLength(Optional ByVal p_power As String = "1",
                               Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.length, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a mass unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsMass(Optional ByVal p_power As String = "1",
                            Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.mass, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a rotation unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsRotation(Optional ByVal p_power As String = "1",
                           Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.rotation, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a temperature unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsTemperature(Optional ByVal p_power As String = "1",
                            Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.temperature, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a time unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsTime(Optional ByVal p_power As String = "1",
                            Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.time, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a unitless unit object to the units object.
    ''' </summary>
    ''' <param name="p_power">Power to which the unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="p_isNumerator">If true, the unit is in the numerator position. If false, the unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub SetUnitsUnitless(Optional ByVal p_power As String = "1",
                            Optional ByVal p_isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.unitless, p_power, p_isNumerator)
        units.AddUnit(unit)
    End Sub

#End Region

End Class
