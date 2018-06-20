Option Explicit On
Option Strict On

''' <summary>
''' Various options of converting program data to/from XML node values.
''' </summary>
''' <remarks></remarks>
Public Enum eReadWriteConversion
    readWriteOnly
    convertToEnum
    convertPathStoredRelative
    convertPathStoredRelativeFromList
    convertPathStoredUnknown
    convertBooleanYesNo
    convertBooleanTrueFalse
    convertInteger
    convertObservableCollection
    convertObservableCollectionFromList
    convertObservableCollectionFromUniqueList
    aggregateAndConvertObservableCollectionStart
    aggregateAndConvertObservableCollection
    aggregateAndConvertObservableCollectionEnd
End Enum