// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="AISC_360_10.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
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
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel
{
    /// <summary>
    /// Steel design code <see cref="AISC_360_10" />.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Steel.SteelCode" />
    /// <seealso cref="SteelCode" />
    public class AISC_360_10 : SteelCode
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AISC_360_10" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AISC_360_10(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the value of a steel design overwrite item.
        /// </summary>
        /// <param name="name">The name of a frame object with a steel frame design procedure.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="programDetermined">True: The specified value is program determined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetOverwrite(string name,
            eOverwrites_AISC_360_10 item,
            ref double value,
            ref bool programDetermined)
        {
            _callCode = _sapModel.DesignSteel.AISC360_10.GetOverwrite(name, (int)item, ref value, ref programDetermined);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the value of a steel design overwrite item.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the frame object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all frame objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected frame objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetOverwrite(string name,
            eOverwrites_AISC_360_10 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignSteel.AISC360_10.SetOverwrite(name, 
                            (int)item, value, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the value of a steel design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPreference(ePreferences_AISC_360_10 item,
            ref double value)
        {
            _callCode = _sapModel.DesignSteel.AISC360_10.GetPreference((int)item, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function sets the value of a steel design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPreference(ePreferences_AISC_360_10 item,
            double value)
        {
            _callCode = _sapModel.DesignSteel.AISC360_10.SetPreference((int)item, value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif