// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="JointPatterns.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Represents the joint patterns in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.IJointPatterns" />
    public class JointPatterns : CSiApiBase, IJointPatterns
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="JointPatterns" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public JointPatterns(CSiApiSeed seed) : base(seed) { }
        #endregion

        #region Methods: Public

        /// <summary>
        /// Renames the specified joint pattern.
        /// </summary>
        /// <param name="nameJointPattern">The existing name of a defined joint pattern.</param>
        /// <param name="newName">The new name for the joint pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string nameJointPattern,
            string newName)
        {
            // TODO: Determine what happens to renaming a nonexisting pattern.

            _callCode = _sapModel.PatternDef.ChangeName(nameJointPattern, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The function returns the number of defined joint patterns.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.PatternDef.Count();
        }

        /// <summary>
        /// The function deletes the specified joint pattern.
        /// At least one joint pattern must be defined.
        /// The function will return an error if an attempt is made to delete the last joint pattern.
        /// </summary>
        /// <param name="nameJointPattern">The name of an existing joint pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameJointPattern)
        {
            _callCode = _sapModel.PatternDef.Delete(nameJointPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined joint patterns.
        /// </summary>
        /// <param name="namesJointPatterns">Joint pattern names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] namesJointPatterns)
        {
            namesJointPatterns = new string[0];
            _callCode = _sapModel.PatternDef.GetNameList(ref _numberOfItems, ref namesJointPatterns);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// The function defines a new joint pattern.
        /// It returns an error if the specified joint pattern name is already in use
        /// </summary>
        /// <param name="nameJointPattern">The name of a new joint pattern.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetPattern(string nameJointPattern)
        {
            _callCode = _sapModel.PatternDef.SetPattern(nameJointPattern);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
#endif