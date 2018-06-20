using System;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// Converts enumerations between those used in this library and those used in the CSi program supported by a given compilation.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    internal class CSiEnumConverter
    {
        // TODO: Test these two generic functions in MPT.Enums and consider moving them there.
        // See: https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
        // diaphragmOption = CSiEnumConverter.Convert<eDiaphragmOption, CSiProgram.eDiaphragmOption>(csiDiaphragmOption);
        internal static TTo Convert<TTo, TFrom>(TFrom enumValue) 
            where TTo : struct, IConvertible
            where TFrom : struct, IConvertible
        {
            if (!typeof(TTo).IsEnum)
            {
                throw new ArgumentException("TTo must be an enumerated type");
            }
            if (!typeof(TFrom).IsEnum)
            {
                throw new ArgumentException("TFrom must be an enumerated type");
            }
            
            return (TTo)(object)enumValue;
        }

        // diaphragmOption = CSiEnumConverter.Convert(csiDiaphragmOption, diaphragmOption);
        internal static TTo Convert<TTo, TFrom>(TFrom fromEnumValue, TTo toEnumValue)
           where TTo : struct, IConvertible
           where TFrom : struct, IConvertible
        {
            if (!typeof(TTo).IsEnum)
            {
                throw new ArgumentException("TTo must be an enumerated type");
            }
            if (!typeof(TFrom).IsEnum)
            {
                throw new ArgumentException("TFrom must be an enumerated type");
            }

            return (TTo)(object)fromEnumValue;
        }


        // eItemType
        internal static CSiProgram.eItemType ToCSi(eItemType enumValue)
        {
            return (CSiProgram.eItemType) enumValue;
        }

        internal static eItemType FromCSi(CSiProgram.eItemType enumValue)
        {
            return (eItemType) enumValue;
        }

        // eItemTypeElement
        internal static CSiProgram.eItemTypeElm ToCSi(eItemTypeElement enumValue)
        {
            return (CSiProgram.eItemTypeElm) enumValue;
        }

        internal static eItemTypeElement FromCSi(CSiProgram.eItemTypeElm enumValue)
        {
            return (eItemTypeElement) enumValue;
        }

        // eUnits
        internal static CSiProgram.eUnits ToCSi(eUnits enumValue)
        {
            return (CSiProgram.eUnits) enumValue;
        }

        internal static eUnits FromCSi(CSiProgram.eUnits enumValue)
        {
            return (eUnits) enumValue;
        }

        // eLoadPatternType
        internal static CSiProgram.eLoadPatternType ToCSi(eLoadPatternType enumValue)
        {
            return (CSiProgram.eLoadPatternType) enumValue;
        }

        internal static eLoadPatternType FromCSi(CSiProgram.eLoadPatternType enumValue)
        {
            return (eLoadPatternType) enumValue;
        }

        // eLoadCaseType
        internal static CSiProgram.eLoadCaseType ToCSi(eLoadCaseType enumValue)
        {
            return (CSiProgram.eLoadCaseType) enumValue;
        }

        internal static eLoadCaseType FromCSi(CSiProgram.eLoadCaseType enumValue)
        {
            return (eLoadCaseType) enumValue;
        }

        // eLoadCaseType
        internal static CSiProgram.eCNameType ToCSi(eCaseComboType enumValue)
        {
            return (CSiProgram.eCNameType) enumValue;
        }

        internal static eCaseComboType FromCSi(CSiProgram.eCNameType enumValue)
        {
            return (eCaseComboType) enumValue;
        }

        // eConstraintType
        internal static CSiProgram.eConstraintType ToCSi(eConstraintType enumValue)
        {
            return (CSiProgram.eConstraintType) enumValue;
        }

        internal static eConstraintType FromCSi(CSiProgram.eConstraintType enumValue)
        {
            return (eConstraintType) enumValue;
        }


        // eConstraintAxis
        internal static CSiProgram.eConstraintAxis ToCSi(eConstraintAxis enumValue)
        {
            return (CSiProgram.eConstraintAxis) enumValue;
        }

        internal static eConstraintAxis FromCSi(CSiProgram.eConstraintAxis enumValue)
        {
            return (eConstraintAxis) enumValue;
        }

        // eFrameType
        internal static CSiProgram.eFramePropType ToCSi(eFrameSectionType enumValue)
        {
            return (CSiProgram.eFramePropType) enumValue;
        }

        internal static eFrameSectionType FromCSi(CSiProgram.eFramePropType enumValue)
        {
            return (eFrameSectionType) enumValue;
        }

        // eLinkType
        internal static CSiProgram.eLinkPropType ToCSi(eLinkPropertyType enumValue)
        {
            return (CSiProgram.eLinkPropType) enumValue;
        }

        internal static eLinkPropertyType FromCSi(CSiProgram.eLinkPropType enumValue)
        {
            return (eLinkPropertyType) enumValue;
        }

        // eMaterialPropertyType
        internal static CSiProgram.eMatType ToCSi(eMaterialPropertyType enumValue)
        {
            return (CSiProgram.eMatType) enumValue;
        }

        internal static eMaterialPropertyType FromCSi(CSiProgram.eMatType enumValue)
        {
            return (eMaterialPropertyType) enumValue;
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // e2DFrameType
        internal static CSiProgram.e2DFrameType ToCSi(e2DFrameType enumValue)
        {
            return (CSiProgram.e2DFrameType)enumValue;
        }

        internal static e2DFrameType FromCSi(CSiProgram.e2DFrameType enumValue)
        {
            return (e2DFrameType)enumValue;
        }
#endif
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
        // e3DFrameType
        internal static CSiProgram.e3DFrameType ToCSi(e3DFrameType enumValue)
        {
            return (CSiProgram.e3DFrameType)enumValue;
        }

        internal static e3DFrameType FromCSi(CSiProgram.e3DFrameType enumValue)
        {
            return (e3DFrameType)enumValue;
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        // eForce
        internal static CSiProgram.eForce ToCSi(eForce enumValue)
        {
            return (CSiProgram.eForce)enumValue;
        }

        internal static eForce FromCSi(CSiProgram.eForce enumValue)
        {
            return (eForce)enumValue;
        }

        // eLength
        internal static CSiProgram.eLength ToCSi(eLength enumValue)
        {
            return (CSiProgram.eLength)enumValue;
        }

        internal static eLength FromCSi(CSiProgram.eLength enumValue)
        {
            return (eLength)enumValue;
        }

        // eTemperature
        internal static CSiProgram.eTemperature ToCSi(eTemperature enumValue)
        {
            return (CSiProgram.eTemperature)enumValue;
        }

        internal static eTemperature FromCSi(CSiProgram.eTemperature enumValue)
        {
            return (eTemperature)enumValue;
        }

        // eShellType
        internal static CSiProgram.eShellType ToCSi(eShellType enumValue)
        {
            return (CSiProgram.eShellType)enumValue;
        }

        internal static eShellType FromCSi(CSiProgram.eShellType enumValue)
        {
            return (eShellType)enumValue;
        }

        // eDeckType
        internal static CSiProgram.eDeckType ToCSi(eDeckType enumValue)
        {
            return (CSiProgram.eDeckType) enumValue;
        }

        internal static eDeckType FromCSi(CSiProgram.eDeckType enumValue)
        {
            return (eDeckType) enumValue;
        }

        // eSlabType
        internal static CSiProgram.eSlabType ToCSi(eSlabType enumValue)
        {
            return (CSiProgram.eSlabType)enumValue;
        }

        internal static eSlabType FromCSi(CSiProgram.eSlabType enumValue)
        {
            return (eSlabType)enumValue;
        }

        // eWallPropertyType
        internal static CSiProgram.eWallPropType ToCSi(eWallPropertyType enumValue)
        {
            return (CSiProgram.eWallPropType)enumValue;
        }

        internal static eWallPropertyType FromCSi(CSiProgram.eWallPropType enumValue)
        {
            return (eWallPropertyType)enumValue;
        }

        // eDiaphragmOption
        internal static CSiProgram.eDiaphragmOption ToCSi(eDiaphragmOption enumValue)
        {
            return (CSiProgram.eDiaphragmOption)enumValue;
        }

        internal static eDiaphragmOption FromCSi(CSiProgram.eDiaphragmOption enumValue)
        {
            return (eDiaphragmOption)enumValue;
        }

        // eWallPierRebarLayerType
        internal static CSiProgram.eWallPierRebarLayerType ToCSi(eWallPierRebarLayerType enumValue)
        {
            return (CSiProgram.eWallPierRebarLayerType)enumValue;
        }

        internal static eWallPierRebarLayerType FromCSi(CSiProgram.eWallPierRebarLayerType enumValue)
        {
            return (eWallPierRebarLayerType)enumValue;
        }

        // eWallSpandrelRebarLayerType
        internal static CSiProgram.eWallSpandrelRebarLayerType ToCSi(eWallSpandrelRebarLayerType enumValue)
        {
            return (CSiProgram.eWallSpandrelRebarLayerType)enumValue;
        }

        internal static eWallSpandrelRebarLayerType FromCSi(CSiProgram.eWallSpandrelRebarLayerType enumValue)
        {
            return (eWallSpandrelRebarLayerType)enumValue;
        }

        // eAreaDesignOrientation
        internal static CSiProgram.eAreaDesignOrientation ToCSi(eAreaDesignOrientation enumValue)
        {
            return (CSiProgram.eAreaDesignOrientation)enumValue;
        }

        internal static eAreaDesignOrientation FromCSi(CSiProgram.eAreaDesignOrientation enumValue)
        {
            return (eAreaDesignOrientation)enumValue;
        }

        // eFrameDesignOrientation
        internal static CSiProgram.eFrameDesignOrientation ToCSi(eFrameDesignOrientation enumValue)
        {
            return (CSiProgram.eFrameDesignOrientation)enumValue;
        }

        internal static eFrameDesignOrientation FromCSi(CSiProgram.eFrameDesignOrientation enumValue)
        {
            return (eFrameDesignOrientation)enumValue;
        }

        // eSupportType
        internal static CSiProgram.eObjType ToCSi(eSupportType enumValue)
        {
            return (CSiProgram.eObjType)enumValue;
        }

        internal static eSupportType FromCSi(CSiProgram.eObjType enumValue)
        {
            return (eSupportType)enumValue;
        }
#endif
    }
}
