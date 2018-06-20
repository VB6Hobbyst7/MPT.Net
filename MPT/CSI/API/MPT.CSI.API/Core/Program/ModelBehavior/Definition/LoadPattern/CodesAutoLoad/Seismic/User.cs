// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-10-2017
// ***********************************************************************
// <copyright file="User.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern.CodesAutoLoad.Seismic
{
    /// <summary>
    /// Represents the user defined auto seismic load in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class User : CSiApiBase
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="User" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public User(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        /// <summary>
        /// This function retrieves auto seismic loading parameters for User-type seismic loading.
        /// </summary>
        /// <param name="name">The name of an existing Seismic-type load pattern that has a User-type auto seismic load assigned.</param>
        /// <param name="applicationPtType">The application point type for the user load.</param>
        /// <param name="eccentricity">Eccentricity ratio that applies to all diaphragms.
        /// This item is only applicable when <paramref name="applicationPtType" /> = <see cref="eApplicationPointType.CenterOfMassWithEccentricity" /></param>
        /// <param name="diaphragms">The names of the diaphragms that have user seismic loads.</param>
        /// <param name="Fx">The global X-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Fy">The global Y-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Mz">The global Z-axis moment assigned to the specified diaphragm. [F*L].</param>
        /// <param name="applicationCoordinate">The global X-/Y-coordinate of the point where the seismic force is applied to the diaphragm. [L].</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoad(string name,
                    ref eApplicationPointType applicationPtType,
                    ref double eccentricity,
                    ref string[] diaphragms,
                    ref double[] Fx,
                    ref double[] Fy,
                    ref double[] Mz,
                    ref Coordinate2DCartesian[] applicationCoordinate)
        {
            int csiApplicationPoint = 0;
            double[] csiX = new double[0];
            double[] csiY = new double[0];
            
            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetUserLoad(name,
                            ref csiApplicationPoint, 
                            ref  eccentricity,
                            ref _numberOfItems,
                            ref diaphragms,
                            ref Fx, ref Fy, ref Mz,
                            ref csiX, ref csiY);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            applicationPtType = (eApplicationPointType) csiApplicationPoint;

            applicationCoordinate = new Coordinate2DCartesian[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                applicationCoordinate[i].X = csiX[i];
                applicationCoordinate[i].Y = csiY[i];
            }
        }

        /// <summary>
        /// This function assigns auto seismic loading parameters for User-type seismic loading.
        /// </summary>
        /// <param name="name">The name of an existing Seismic-type load pattern that has a User-type auto seismic load assigned.</param>
        /// <param name="applicationPtType">The application point type for the user load.</param>
        /// <param name="eccentricity">Eccentricity ratio that applies to all diaphragms.
        /// This item is only applicable when <paramref name="applicationPtType" /> = <see cref="eApplicationPointType.CenterOfMassWithEccentricity" /></param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoad(string name,
                    eApplicationPointType applicationPtType,
                    double eccentricity)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetUserLoad(name,
                            (int)applicationPtType,
                            eccentricity);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function assigns auto seismic loading value parameters for User-type seismic loading.
        /// </summary>
        /// <param name="name">The name of an existing Seismic-type load pattern that has a User-type auto seismic load assigned.</param>
        /// <param name="diaphragm">The name of the diaphragms that has user seismic loads.</param>
        /// <param name="Fx">The global X-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Fy">The global Y-direction force assigned to the specified diaphragm. [F].</param>
        /// <param name="Mz">The global Z-axis moment assigned to the specified diaphragm. [F*L].</param>
        /// <param name="applicationCoordinate">The global X-/Y-coordinate of the point where the wind force is applied to the diaphragm. [L].
        /// This is only applicable if load was set with the application point time as <see cref="eApplicationPointType.UserSpecifiedApplicationPoint" /></param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadValue(string name,
                    string diaphragm,
                    double Fx,
                    double Fy,
                    double Mz,
                    Coordinate2DCartesian applicationCoordinate)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetUserLoadValue(name,
                            diaphragm,
                            Fx, Fy, Mz,
                            applicationCoordinate.X,
                            applicationCoordinate.Y);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function retrieves auto seismic loading parameters for User-coefficient seismic loading.
        /// </summary>
        /// <param name="name">The name of an existing Seismic-type load pattern that has a User-coefficient auto seismic load assigned.</param>
        /// <param name="directionFlag">The seismic load direction.</param>
        /// <param name="eccentricity">Eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="userSpecifiedHeights">True: User has specified the starting and/or ending heights of the structure.</param>
        /// <param name="topZ">Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = true.</param>
        /// <param name="bottomZ">Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = true.</param>
        /// <param name="c">The base shear coefficient, c.</param>
        /// <param name="k">The building height exponent, k.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetLoadCoefficient(string name,
                            ref eSeismicLoadDirection directionFlag,
                            ref double eccentricity,
                            ref bool userSpecifiedHeights,
                            ref double topZ,
                            ref double bottomZ,
                            ref double c,
                            ref double k)
        {
            int csiDirection = 0;

            _callCode = _sapModel.LoadPatterns.AutoSeismic.GetUserCoefficient(name,
                            ref csiDirection,
                            ref eccentricity,
                            ref userSpecifiedHeights,
                            ref topZ, ref bottomZ,
                            ref c, ref k);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            directionFlag = (eSeismicLoadDirection)csiDirection;
        }

        /// <summary>
        /// This function assigns auto seismic loading parameters for User-coefficient seismic loading.
        /// </summary>
        /// <param name="name">The name of an existing Seismic-type load pattern that has a User-coefficient auto seismic load assigned.</param>
        /// <param name="directionFlag">The seismic load direction.</param>
        /// <param name="eccentricity">Eccentricity ratio that applies to all diaphragms.</param>
        /// <param name="userSpecifiedHeights">True: User has specified the starting and/or ending heights of the structure.</param>
        /// <param name="topZ">Z-coordinate at the highest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = true.</param>
        /// <param name="bottomZ">Z-coordinate at the lowest level where auto seismic loads are applied. [L].
        /// This only applies when <paramref name="userSpecifiedHeights" /> = true.</param>
        /// <param name="c">The base shear coefficient, c.</param>
        /// <param name="k">The building height exponent, k.</param>
        /// <exception cref="CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetLoadCoefficient(string name,
                            eSeismicLoadDirection directionFlag,
                            double eccentricity,
                            bool userSpecifiedHeights,
                            double topZ,
                            double bottomZ,
                            double c,
                            double k)
        {
            _callCode = _sapModel.LoadPatterns.AutoSeismic.SetUserCoefficient(name,
                            (int)directionFlag,
                            eccentricity,
                            userSpecifiedHeights,
                            topZ, bottomZ,
                            c, k);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}

#endif