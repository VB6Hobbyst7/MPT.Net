// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="Superstructure.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced
{

    /// <summary>
    /// Represents the bridge superstructure in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Superstructure : CSiApiBase, ISuperstructure
    {

#region Fields
        private readonly CSiApiSeed _seed;

        private ConcreteBoxGirder _concreteBoxGirder;
#endregion


#region Properties        
        /// <summary>
        /// Gets the concrete box girder.
        /// </summary>
        /// <value>The concrete box girder.</value>
        public ConcreteBoxGirder ConcreteBoxGirder => _concreteBoxGirder ?? (_concreteBoxGirder = new ConcreteBoxGirder(_seed));


#endregion


#region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="Superstructure"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Superstructure(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }


#endregion

#region Methods: Public        

        /// <summary>
        /// This function returns location and material information about a single stress point at a superstructure section cut in a bridge object. <para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="CountSuperstructureCut"/>.</param>
        /// <param name="pointIndex">The index number of the stress point in this section cut in this bridge object. 
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="CountSuperstructureCutStressPoint"/></param>
        /// <param name="stressPointCoordinate">The transverse and vertical coordinates of the stress point in the section, measured from the bottom left corner of the section.  <para/>
        /// X is positive to the right when looking upstation, Y is positive upward. [L]</param>
        /// <param name="nameMaterial">The name of the material property at this stress point.</param>
        /// <param name="note">A description of the stress point that may be used for identification. 
        /// Points that are pre-defined by the program will have prescribed notes.</param>
        public void GetSuperstructureCutStressPoint(string name,
                    int cutIndex,
                    int pointIndex,
                    ref Coordinate2DCartesian stressPointCoordinate,
                    ref string nameMaterial,
                    ref string note)
        {
            double x = 0;
            double y = 0;

            _callCode = _sapModel.BridgeAdvancedSuper.GetSuperCutStressPoint(name, cutIndex, pointIndex, ref x, ref y, ref nameMaterial, ref note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            stressPointCoordinate.X = x;
            stressPointCoordinate.Y = y;
        }

        /// <summary>
        /// This function returns the number of stress points at the specified superstructure section cut.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle the error?
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. <para/>
        /// This must be from 0 to <paramref name="numberOfStressPoints"/>-1, where <paramref name="numberOfStressPoints"/> is the value returned by function <see cref="CountSuperstructureCut"/>.</param>
        /// <param name="numberOfStressPoints">The number of stress points for this section cut in this bridge object. <para/>
        /// They will be identified in subsequent API functions using the indices 0 to <paramref name="numberOfStressPoints"/>-1.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void CountSuperstructureCutStressPoint(string name,
            int cutIndex,
            ref int numberOfStressPoints)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.CountSuperCutStressPoint(name, cutIndex, ref numberOfStressPoints);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function returns location and orientation information about a single superstructure section cut in a bridge object. <para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. 
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="CountSuperstructureCut"/>. 
        /// Section cuts will be in order of increasing <paramref name="station"/> and increasing <paramref name="location"/>.</param>
        /// <param name="location">Indicates whether the <paramref name="cutIndex"/> section cut occurs before or after the associated station</param>
        /// <param name="station">The station ordinate of the <paramref name="cutIndex"/> section cut at the reference line of the superstructure. [L]</param>
        /// <param name="referencePointCoordinate">The transverse and vertical coordinates in the section of the reference point that corresponds to the layout line in the bridge object. <para/>
        /// X is positive to the right when looking upstation, Y is positive upward.  <para/>
        /// Coordinates are measured from the lower-left corner of the section bounding-box before skew, grade, and superelevation are applied.  <para/>
        /// The rotations of the section due to skew, grade, and superelevation occur about the reference point. [L]</param>
        /// <param name="skew">The skew angle, in degrees, of the section cut, measured from the horizontal normal to the superstructure reference line, with positive being about the upward vertical axis.</param>
        /// <param name="grade">The grade, as a slope (abs(Grade) &lt; 1.0), giving the vertical rise per distance along the superstructure reference line.</param>
        /// <param name="superstructureElevation">The superelevation, as a slope (abs(SuperElev) &lt; 1.0), giving the vertical rise per distance along the transverse normal to the superstructure reference line.</param>
        public void GetSuperstructureCutLocation(string name,
            int cutIndex,
            ref eStation location,
            ref double station,
            ref Coordinate2DCartesian referencePointCoordinate,
            ref double skew,
            ref double grade,
            ref double superstructureElevation)
        {
            double x = 0;
            double y = 0;
            int csiLocation = 0;

            _callCode = _sapModel.BridgeAdvancedSuper.GetSuperCutLocation(name, cutIndex, ref csiLocation, ref station, ref x, ref y, ref skew, ref grade, ref superstructureElevation);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            location = (eStation) csiLocation;
            referencePointCoordinate.X = x;
            referencePointCoordinate.Y = y;
        }

        /// <summary>
        /// This function returns the number of superstructure section cuts that are available for getting analysis results and performing design.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle?
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="count">The number of section cuts in this bridge object. <para/>
        /// They will be identified in subsequent API functions using the indices 0 to <paramref name="count"/>-1. <para/>
        /// There may be one or two section cuts at each output station along the length of the superstructure.</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void CountSuperstructureCut(string name,
            ref int count)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.CountSuperCut(name, ref count);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif
