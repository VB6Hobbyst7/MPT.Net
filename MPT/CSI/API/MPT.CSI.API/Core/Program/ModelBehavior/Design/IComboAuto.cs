// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-03-2017
// ***********************************************************************
// <copyright file="IComboAuto.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements an interface for automatically creating code-based load combinations.
    /// </summary>
    public interface IComboAuto
    {
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        void GetComboAutoGenerate(ref bool autoGenerate);

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        void SetComboAutoGenerate(bool autoGenerate);
    }
}
#endif