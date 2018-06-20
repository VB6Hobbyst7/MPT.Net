// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-10-2017
//
// Last Modified By : Mark
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="AA_ASD_2000.cs" company="">
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

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum
{
    /// <summary>
    /// Aluminum design Code <see cref="AA_ASD_2000" />.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Design.CodesDesign.Aluminum.AluminumCode" />
    /// <seealso cref="AluminumCode" />
    public class AA_ASD_2000 : AluminumCode
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AA_ASD_2000" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AA_ASD_2000(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
        /// <summary>
        /// This function retrieves the value of an aluminum design overwrite item.
        /// </summary>
        /// <param name="name">The name of a frame object with an aluminum frame design procedure.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="programDetermined">True: The specified value is program determined.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetOverwrite(string name,
            eOverwrites_AA_ASD_2000 item,
            ref double value,
            ref bool programDetermined)
        {        
            _callCode = _sapModel.DesignAluminum.AA_ASD_2000.GetOverwrite(name, (int)item, ref value, ref programDetermined);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }            
        }

        /// <summary>
        /// This function sets the value of an aluminum design overwrite item.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="item">The overwrite item considered.</param>
        /// <param name="value">The value of the considered overwrite item.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignment is made to the frame object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignment is made to all frame objects in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, assignment is made to all selected frame objects, and the <paramref name="name" /> item is ignored.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetOverwrite(string name,
            eOverwrites_AA_ASD_2000 item,
            double value,
            eItemType itemType = eItemType.Object)
        {
            _callCode = _sapModel.DesignAluminum.AA_ASD_2000.SetOverwrite(name, 
                            (int)item, value, 
                            EnumLibrary.Convert<eItemType, CSiProgram.eItemType>(itemType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function retrieves the value of an aluminum design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetPreference(ePreferences_AA_ASD_2000 item,
            ref double value)
        {          
            _callCode = _sapModel.DesignAluminum.AA_ASD_2000.GetPreference((int)item, ref value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }            
        }

        /// <summary>
        /// This function sets the value of an aluminum design preference item.
        /// </summary>
        /// <param name="item">The preference item considered.</param>
        /// <param name="value">The value of the considered preference item.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPreference(ePreferences_AA_ASD_2000 item,
            double value)
        {
            _callCode = _sapModel.DesignAluminum.AA_ASD_2000.SetPreference((int)item, value);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif