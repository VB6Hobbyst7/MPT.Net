// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="IObservableSectionFrequencyDependent.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return the property assignment name for a frequency-dependent link.
    /// </summary>
    public interface IObservableSectionFrequencyDependent
    {
        /// <summary>
        /// This function retrieves the frequency dependent property assignment to a link element.
        /// If no frequency dependent property is assigned to the link, the PropName is returned as None.
        /// </summary>
        /// <param name="name">The name of an existing link element.</param>
        /// <param name="propertyName">The name of the frequency dependent link property assigned to the link element.</param>
        void GetSectionFrequencyDependent(string name, 
            ref string propertyName);
    }
}
#endif