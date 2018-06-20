// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="FrameReleases.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign
{
    /// <summary>
    /// Represents the named set frame releases in the application.
    /// A named set of property modifiers can be applied to a frame or area object during staged construction to change the property modifiers that were previously assigned or applied to the object. 
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign.IFrameReleases" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class FrameReleases : CSiApiBase, IFrameReleases
    {

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameReleases" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public FrameReleases(CSiApiSeed seed) : base(seed) { }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing frame end release.
        /// </summary>
        /// <param name="currentName">The existing name of a defined frame end release.</param>
        /// <param name="newName">The new name for the frame end release</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.NamedAssign.ReleaseFrame.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns the total number of defined frame end releases in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.NamedAssign.ReleaseFrame.Count();
        }


        /// <summary>
        /// The function deletes a specified frame end release.
        /// </summary>
        /// <param name="name">The name of an existing frame end release.
        /// It returns an error if the specified end release can not be deleted; for example, if it is currently used by a staged construction load case.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.NamedAssign.ReleaseFrame.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function retrieves the names of all defined frame end releases
        /// </summary>
        /// <param name="names">Frame end release names retrieved by the program.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetNameList(out string[] names)
        {
            names = new string[0];
            _callCode = _sapModel.NamedAssign.ReleaseFrame.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        #endregion

        #region Methods: Public
        // === Get/Set

        /// <summary>
        /// This function retrieves the release assignments for a frame end release.
        /// </summary>
        /// <param name="name">The name of an existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetReleases(string name,
            out DegreesOfFreedomLocal iEndRelease,
            out DegreesOfFreedomLocal jEndRelease,
            out Fixity iEndFixity,
            out Fixity jEndFixity)
        {
            bool[] csiiEndRelease = new bool[0];
            bool[] csijEndRelease = new bool[0];
            double[] csiiEndFixity = new double[0];
            double[] csijEndFixity = new double[0];

            _callCode = _sapModel.NamedAssign.ReleaseFrame.GetReleases(name, ref csiiEndRelease, ref csijEndRelease, ref csiiEndFixity, ref csijEndFixity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            iEndRelease = new DegreesOfFreedomLocal();
            iEndRelease.FromArray(csiiEndRelease);

            jEndRelease = new DegreesOfFreedomLocal();
            jEndRelease.FromArray(csijEndRelease);

            iEndFixity = new Fixity();
            iEndFixity.FromArray(csiiEndFixity);

            jEndFixity = new Fixity();
            jEndFixity.FromArray(csijEndFixity);
        }

        /// <summary>
        /// This function defines a named frame end release.
        /// </summary>
        /// <param name="name">The name of a new or existing frame end release.</param>
        /// <param name="iEndRelease">Booleans indicating the I-End releases.</param>
        /// <param name="jEndRelease">Booleans indicating the J-End releases.</param>
        /// <param name="iEndFixity">Values indicating the I-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        /// <param name="jEndFixity">Values indicating the J-End partial fixity springs.
        /// The value will be inactive if there is not a released specified for the corresponding degree of freedom.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetReleases(string name,
            DegreesOfFreedomLocal iEndRelease,
            DegreesOfFreedomLocal jEndRelease,
            Fixity iEndFixity,
            Fixity jEndFixity)
        {
            bool[] csiiEndRelease = iEndRelease.ToArray();
            bool[] csijEndRelease = jEndRelease.ToArray();
            double[] csiiEndFixity = iEndFixity.ToArray();
            double[] csijEndFixity = jEndFixity.ToArray();

            _callCode = _sapModel.NamedAssign.ReleaseFrame.SetReleases(name, ref csiiEndRelease, ref csijEndRelease, ref csiiEndFixity, ref csijEndFixity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif
