// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="CSiApiBase.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using SAP2000v16; 
#elif BUILD_SAP2000v17
using SAP2000v17;
#elif BUILD_SAP2000v18
using SAP2000v18;
#elif BUILD_SAP2000v19
using SAP2000v19;
#elif BUILD_SAP2000v20
using SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiBridge20;
#elif BUILD_ETABS2013
using ETABS2013;
#elif BUILD_ETABS2015
using ETABS2015;
#elif BUILD_ETABS2016
using ETABS2016;
#elif BUILD_ETABS2017
using ETABSv17;
#endif

namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// Stores references to the basic, fundamental objects of the program for implementing a dependency injection pattern.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CSiApiBase
    {
        #region Fields
#if BUILD_SAP2000v16
        protected SAP2000v16.SapObject _sapObject;
#else        
        /// <summary>
        /// The base application object.
        /// </summary>
        protected cOAPI _sapObject;
#endif        
        /// <summary>
        /// The base model object that contains most of the application API calls.
        /// </summary>
        protected cSapModel _sapModel;

        /// <summary>
        /// Code returned by a call to an API method.
        /// </summary>
        protected int _callCode;

        /// <summary>
        /// The API default error code.
        /// </summary>
        protected const string API_DEFAULT_ERROR_CODE = "API method returned error code 1.";

        /// <summary>
        /// The number of items, to be passed into functions where this is not needed outside of the base API.
        /// </summary>
        protected int _numberOfItems = 0;
        #endregion

        #region Properties        
        /// <summary>
        /// True: Class will throw an exception if the API call fails.
        /// </summary>
        /// <value><c>true</c> if the class will throw an exception if an API call fails; otherwise, <c>false</c>.</value>
        public bool ThrowApiExceptions { get; set; } = true;

        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApiBase" /> class.
        /// </summary>
        public CSiApiBase(){ }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApiBase" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public CSiApiBase(CSiApiSeed seed)
        {
            _sapObject = seed.SapObject;
            _sapModel = seed.SapModel;
        }

#if BUILD_SAP2000v16
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApiBase"/> class.
        /// </summary>
        /// <param name="sapObject">The sap object.</param>
        /// <param name="sapModel">The sap model.</param>
        public CSiApiBase(SAP2000v16.SapObject sapObject, cSapModel sapModel)
        {
            _sapObject = sapObject;
            _sapModel = sapModel;
        }
#else        
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApiBase" /> class.
        /// </summary>
        /// <param name="sapObject">The sap object.</param>
        /// <param name="sapModel">The sap model.</param>
        public CSiApiBase(cOAPI sapObject, cSapModel sapModel)
        {
            _sapObject = sapObject;
            _sapModel = sapModel;
        }
#endif

        #endregion

        #region Methods
        /// <summary>
        /// Returns 'true' if the API success code indicates a successful call.
        /// </summary>
        /// <param name="successCode">Code returned by the API call.
        /// 0 means the call was successful.
        /// Any other code indicates failure.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool apiCallIsSuccessful(int successCode)
        {
            return (successCode == 0);
        }

        /// <summary>
        /// Returns 'true' if the API success code indicates a failed call and exceptions are set by this object to be thrown.
        /// </summary>
        /// <param name="successCode">Code returned by the API call.
        /// 0 means the call was successful.
        /// Any other code indicates failure.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool throwCurrentApiException(int successCode)
        {
            return (ThrowApiExceptions && !apiCallIsSuccessful(successCode));
        }

        /// <summary>
        /// Confirms that the array lengths are the same.
        /// </summary>
        /// <param name="arrayLength1">The array length1.</param>
        /// <param name="arrayLength2">The array length2.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array lengths mismatch: Array1 = " + arrayLength1 + " Array 2 = " + arrayLength2</exception>
        /// <exception cref="CSiException">Array lengths mismatch: Array1 = " + arrayLength1 + " Array 2 = " + arrayLength2</exception>
        protected void arraysLengthMatch(int arrayLength1, int arrayLength2)
        {
            if (arrayLength1 != arrayLength2)
            {
                throw new CSiException("Array lengths mismatch: Array1 = " + arrayLength1 + " Array 2 = " + arrayLength2);
            }
        }

        /// <summary>
        /// Confirms that the array lengths are the same.
        /// </summary>
        /// <param name="arrayName1">The array name1.</param>
        /// <param name="arrayLength1">The array length1.</param>
        /// <param name="arrayName2">The array name2.</param>
        /// <param name="arrayLength2">The array length2.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException">Array lengths mismatch: " + arrayName1 + " = " + arrayLength1 + ", " +
        ///                     arrayName2 + " = " + arrayLength2</exception>
        /// <exception cref="CSiException">Array lengths mismatch: " + arrayName1 + " = " + arrayLength1 + ", " +
        /// arrayName2 + " = " + arrayLength2</exception>
        protected void arraysLengthMatch(string arrayName1, int arrayLength1, 
            string arrayName2, int arrayLength2)
        {
            if (arrayLength1 != arrayLength2)
            {
                throw new CSiException("Array lengths mismatch: " + arrayName1 + " = " + arrayLength1 + ", " +
                    arrayName2 + " = " + arrayLength2);
            }
        }
        #endregion
    }
}
