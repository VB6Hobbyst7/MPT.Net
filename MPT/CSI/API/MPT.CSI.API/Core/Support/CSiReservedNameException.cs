// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="CSiReservedNameException.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.CSI.API.Core.Support
{
    /// <summary>
    /// Exception thrown when a CSi API function fails to return 0 indicating success, due to the use of a reserved name in the application.
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class CSiReservedNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiReservedNameException" /> class.
        /// </summary>
        public CSiReservedNameException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiReservedNameException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CSiReservedNameException(string message) : base(message) { }
    }
}
