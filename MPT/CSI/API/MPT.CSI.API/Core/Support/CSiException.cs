// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="CSiException.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// Exception thrown when a CSi API function fails to return 0 indicating success.
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CSiException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiException" /> class.
        /// </summary>
        public CSiException() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CSiException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">If this exception is triggered by a caught exception, the prior exception is taken in.</param>
        public CSiException(string message,
            Exception innerException = null) : base(message, innerException) { }
    }
}
