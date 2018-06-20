// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-10-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="AISI_ASD_96.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19 || BUILD_SAP2000v20
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
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed
{
    /// <summary>
    /// Cold-formed steel design code <see cref="AISI_ASD_96" />.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.ColdFormed.ColdFormedSteelCode" />
    /// <seealso cref="ColdFormedSteelCode" />
    public class AISI_ASD_96 : ColdFormedSteelCode
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AISI_ASD_96" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AISI_ASD_96(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the value of a cold formed design overwrite item.
        /// </summary>
        /// <param name="name">The name of a frame object with a cold formed frame design procedure.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="programDetermined">True: The specified value is program determined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetOverwrite(string name,
            eOverwrites_AISI_ASD_96 item,
            ref double value,
            ref bool programDetermined)
        {
            _callCode = _sapModel.DesignColdFormed.AISI_ASD96.GetOverwrite(name, (int)item, ref value, ref programDetermined);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }            
        }

        /// <summary>
        /// This function sets the value of a cold formed design overwrite item.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the frame object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all frame objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected frame objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetOverwrite(string name,
            eOverwrites_AISI_ASD_96 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignColdFormed.AISI_ASD96.SetOverwrite(name, 
                            (int)item, value, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the value of a cold formed design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPreference(ePreferences_AISI_ASD_96 item,
            ref double value)
        {           
            _callCode = _sapModel.DesignColdFormed.AISI_ASD96.GetPreference((int)item, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }            
        }

        /// <summary>
        /// This function sets the value of a cold formed design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPreference(ePreferences_AISI_ASD_96 item,
            double value)
        {
            _callCode = _sapModel.DesignColdFormed.AISI_ASD96.SetPreference((int)item, value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif