// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="SolidEditor.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Edit
{
    /// <summary>
    /// Represents the solid editor in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Edit.ISolidEditor" />
    public class SolidEditor : CSiApiBase, ISolidEditor
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidEditor" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SolidEditor(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        /// <summary>
        /// This function meshes solid objects.
        /// </summary>
        /// <param name="name">The name of an existing solid object.</param>
        /// <param name="number2_4">This is the number of objects created between faces 2 and 4 of the solid object.</param>
        /// <param name="number1_3">This is the number of objects created between faces 1 and 3 of the solid object.</param>
        /// <param name="number5_6">This is the number of objects created between faces 5 and 6 of the solid object.</param>
        /// <param name="numberSolids">The number of solid objects created when the specified solid object is divided.</param>
        /// <param name="solidNames">This is an array of the name of each solid object created when the specified solid object is divided.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Divide(string name,
            int number2_4,
            int number1_3,
            int number5_6,
            int numberSolids,
            string[] solidNames)
        {
            _callCode = _sapModel.EditSolid.Divide(name, number2_4, number1_3, number5_6, ref numberSolids, ref solidNames);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
#endif
