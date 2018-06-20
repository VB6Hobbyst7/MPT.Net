// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-21-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ExtendedEntityData.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program
{
    /// <summary>
    /// Represents the extended entity data associated with the application.<para />
    /// Can be used to store metadata for a model, or any other data specific to your application
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ExtendedEntityData : CSiApiBase
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedEntityData" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ExtendedEntityData(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public        
        /// <summary>
        /// This function can be used to retrieve metadata for a model, or any other data specific to your application.
        /// </summary>
        /// <param name="applicationName">This is an application name of your choice under which your application previously stored some data. <para />
        /// It is recommended you choose a unique name to guarantee no other third-party application resets the data that was set by your application.  <para />
        /// Application names are stored in their original capitalization and character set, but are compared in a case insensitive and culturally invariant manner for retrieval purposes.</param>
        /// <param name="key">This is an entry name under which the data provided in the remaining arguments was stored. <para />
        /// Entry names are stored in their original capitalization and character set, but are compared in a case insensitive and culturally invariant manner for retrieval purposes.</param>
        /// <param name="numberValues">The number of strings stored for the given application name and entry name.</param>
        /// <param name="values">String values previously stored under the application name <paramref name="applicationName" /> and entry name <paramref name="key" />, and returned to the API user.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetKeyStrings(string applicationName,
            string key,
            ref int numberValues,
            ref string[] values)
        {
            _callCode = _sapModel.GetKeyStringsExtendedEntityData(applicationName, key, ref numberValues, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function can be used to store metadata for a model, or any other data specific to your application.
        /// </summary>
        /// <param name="applicationName">This is an application name of your choice under which your application previously stored some data. <para />
        /// It is recommended you choose a unique name to guarantee no other third-party application resets the data that was set by your application.  <para />
        /// Application names are stored in their original capitalization and character set, but are compared in a case insensitive and culturally invariant manner for retrieval purposes.</param>
        /// <param name="key">This is an entry name under which the data provided in the remaining arguments was stored. <para />
        /// This data can later be retrieved, reset, or deleted by providing a valid application name and entry name.<para />
        /// Entry names are stored in their original capitalization and character set, but are compared in a case insensitive and culturally invariant manner for retrieval purposes.</param>
        /// <param name="values">Strings.<para />
        /// These strings replace any strings previously stored for the given application name and entry name. <para />
        /// Calling this function with <paramref name="values" /> length = zero is equivalent to erasing any previously stored data.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetKeyStrings(string applicationName,
            string key,
            string[] values)
        {
            _callCode = _sapModel.SetStringsExtendedEntityData(applicationName, key, values.Length, ref values);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Gets the keys with strings that are associated with metadata for a model, or any other data specific to your application.
        /// </summary>
        /// <param name="applicationName">This is an application name of your choice under which your application previously stored some data. <para />
        /// It is recommended you choose a unique name to guarantee no other third-party application resets the data that was set by your application.  <para />
        /// Application names are stored in their original capitalization and character set, but are compared in a case insensitive and culturally invariant manner for retrieval purposes.</param>
        /// <param name="keys">The entry names for which data was previously stored under the application name <paramref name="applicationName" />, and returned to the API user.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetKeysWithStrings(string applicationName,
            ref string[] keys)
        {
            _callCode = _sapModel.GetKeysWithStringsExtendedEntityData(applicationName, ref _numberOfItems, ref keys);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

    }
}
#endif