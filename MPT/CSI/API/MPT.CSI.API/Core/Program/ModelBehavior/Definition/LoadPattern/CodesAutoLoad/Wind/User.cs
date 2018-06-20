// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="User.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Wind
{
    /// <summary>
    /// Represents the user defined auto wind load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class User : CSiApiBase
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public User(CSiApiSeed seed) : base(seed){}


        #endregion

        #region Methods: Public        
        /// <summary>
        /// This function retrieves auto wind loading parameters for User-type wind loading.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load pattern that has a User-type auto wind load assigned.</param>
        /// <param name="diaphragms">The names of the diaphragms that have user wind loads.</param>
        /// <param name="Fx">The global X-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Fy">The global Y-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Mz">The global Z-axis moment assigned to the specified diaphragm. [F*L].</param>
        /// <param name="applicationCoordinate">The global X-/Y-coordinate of the point where the wind force is applied to the diaphragm. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
                    ref string[] diaphragms,
                    ref double[] Fx,
                    ref double[] Fy,
                    ref double[] Mz,
                    ref Coordinate2DCartesian[] applicationCoordinate)
        {
            double[] csiX = new double[0];
            double[] csiY = new double[0];

            _callCode = _sapModel.LoadPatterns.AutoWind.GetUserLoad(name,
                            ref _numberOfItems,
                            ref diaphragms,
                            ref Fx, ref Fy, ref Mz,
                            ref csiX, ref csiY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            applicationCoordinate = new Coordinate2DCartesian[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                applicationCoordinate[i].X = csiX[i];
                applicationCoordinate[i].Y = csiY[i];
            }
        }

        /// <summary>
        /// This function assigns auto wind loading parameters for User-type wind loading.
        /// </summary>
        /// <param name="name">The name of an existing Wind-type load pattern that has a User-type auto wind load assigned.</param>
        /// <param name="diaphragm">The name of the diaphragms that has user wind loads.</param>
        /// <param name="Fx">The global X-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Fy">The global Y-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Mz">The global Z-axis moment assigned to the specified diaphragm. [F*L].</param>
        /// <param name="applicationCoordinate">The global X-/Y-coordinate of the point where the wind force is applied to the diaphragm. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
                    string diaphragm,
                    double Fx,
                    double Fy,
                    double Mz,
                    Coordinate2DCartesian applicationCoordinate)
        {
            _callCode = _sapModel.LoadPatterns.AutoWind.SetUserLoad(name,
                            diaphragm,
                            Fx, Fy, Mz,
                            applicationCoordinate.X,
                            applicationCoordinate.Y);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
#endif
