// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-08-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="ConcreteBoxGirder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.BridgeAdvanced.BridgeType
{


    /// <summary>
    /// Represents the concrete box girder bridge superstructure in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class ConcreteBoxGirder : CSiApiBase, IConcreteBoxGirder
    {

#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteBoxGirder"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public ConcreteBoxGirder(CSiApiSeed seed) : base(seed) { }


#endregion

#region Methods: Public
        /// <summary>
        /// This function returns the number of stress points at the specified web of the specified superstructure section cut.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="Superstructure.CountSuperstructureCut"/>.</param>
        /// <param name="webIndex">The index number of the web in this section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="GetSuperstructureCutSectionValues"/>.</param>
        /// <param name="count">The number of stress points in this web for this section cut in this bridge object. <para/>
        /// They will be identified in subsequent API functions using the indices 0 to Count-1.</param>
        public void CountSuperstructureCutWebStressPoint(string name,
            int cutIndex,
            int webIndex,
            ref int count)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.CountSuperCutWebStressPoint(name, cutIndex, webIndex, ref count);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns section properties for the region above or below a given Y coordinate value at a single superstructure section cut in a bridge object. <para/>
        /// These properties are calculated for the section before skew, grade, and superelevation are applied. <para/>
        /// Coordinate values are measured from the lower-left corner of the section bounding-box. <para/>
        /// X is positive to the right when looking upstation, and Y is positive upward.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="yCoordinate">The Y coordinate in the section local coordinate system above or below which the section properties are calculated.</param>
        /// <param name="isAboveY">True: Properties are to be computed for the region above the specified coordinate Y.
        /// False: Properties are to be computed for the region below the specified coordinate Y.</param>
        /// <param name="yCentroidOfRegion">Y coordinate of the centroid of the region above/below specified coordinate Y. [L].</param>
        /// <param name="areaOfRegion">Area of the region above/below specified coordinate Y. [L^2].</param>
        /// <param name="momentOfInertiaOfRegion">Moment of inertia of the region above/below specified coordinate Y, taken about a horizontal axis at <paramref name="yCentroidOfRegion"/>. [L^4]</param>
        public void GetSuperstructureCutSectionPropertiesAtY(string name,
            int cutIndex,
            double yCoordinate,
            bool isAboveY,
            ref double yCentroidOfRegion,
            ref double areaOfRegion,
            ref double momentOfInertiaOfRegion)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutSectionPropsAtY(name, cutIndex, yCoordinate, isAboveY, ref yCentroidOfRegion, ref areaOfRegion, ref momentOfInertiaOfRegion);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns an individual section property at a single superstructure section cut in a bridge object.<para/>
        /// These properties are calculated for the section before skew, grade, and superstructure elevation are applied. <para/>
        /// Coordinate values are measured from the lower-left corner of the section bounding-box. <para/>
        /// X is positive to the right when looking upstation, and Y is positive upward.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="cutSectionItem">The type of property value to be gotten.</param>
        /// <param name="sectionCutValue">The value of the requested item:<para/>
        /// <see cref="eSectionCutItem.NumberOfGirdersOrWebs"/>: <paramref name="sectionCutValue"/> &gt;= 0, integral.<para/>
        /// <see cref="eSectionCutItem.DesignAreaOfTopSlab"/>: <paramref name="sectionCutValue"/> &gt; 0, [L^2].<para/>
        /// <see cref="eSectionCutItem.DesignAreaOfBottomSlab"/>: <paramref name="sectionCutValue"/> &gt;= 0, [L^2].<para/>
        /// <see cref="eSectionCutItem.WidthOfTopSlab"/>: <paramref name="sectionCutValue"/> &gt; 0, [L].<para/>
        /// <see cref="eSectionCutItem.WidthOfBottomSlab"/>: <paramref name="sectionCutValue"/> &gt;= 0, [L].<para/>
        /// <see cref="eSectionCutItem.XCoordinateOfTopSlabCentroid"/>: Any value is valid. [L].<para/>
        /// <see cref="eSectionCutItem.XCoordinateOfBottomSlabCentroid"/>: Any value is valid. [L].<para/>
        /// <see cref="eSectionCutItem.YCoordinateOfTopSlabCentroid"/>: <paramref name="sectionCutValue"/> &gt;= 0, [L].<para/>
        /// <see cref="eSectionCutItem.YCoordinateOfBottomSlabCentroid"/>: <paramref name="sectionCutValue"/> &lt;= 0, [L].<para/>
        /// <see cref="eSectionCutItem.AreaInsideTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt;= 0, [L^2].<para/>
        /// <see cref="eSectionCutItem.LengthOfTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt;= 0, [L].<para/>
        /// <see cref="eSectionCutItem.NumberOfTendons"/>: <paramref name="sectionCutValue"/> &gt;= 0.<para/>
        /// <see cref="eSectionCutItem.TopOutsideWidthOfTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt; 0, [L].<para/>
        /// <see cref="eSectionCutItem.BottomOutsideWidthOfTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt; 0, [L].<para/>
        /// <see cref="eSectionCutItem.LeftOutsideLengthOfTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt; 0, [L].<para/>
        /// <see cref="eSectionCutItem.RightOutsideLengthOfTorsionCircuit"/>: <paramref name="sectionCutValue"/> &gt; 0, [L].<para/></param>
        public void GetSuperstructureCutSectionValues(string name,
            int cutIndex,
            eSectionCutItem cutSectionItem,
            ref double sectionCutValue)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutSectionValues(name, cutIndex, (int)cutSectionItem, ref sectionCutValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns information about the box girder slab thicknesses at a given horizontal location across the box girder section.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="xCoordinate">The X coordinate in the section local coordinate system at which a vertical line is passed through the section and the slab coordinates are returned.</param>
        /// <param name="status">Status of what is cut.</param>
        /// <param name="yTopmostSectionCut">The topmost Y coordinate where the vertical line cuts the section. <para/>  
        /// This item is returned as zero when <paramref name="status"/> = <see cref="eCutStatus.NoCut"/>.</param>
        /// <param name="yBottommostSectionCut">The bottommost Y coordinate where the vertical line cuts the section.<para/>  
        /// This item is returned as zero when <paramref name="status"/> = <see cref="eCutStatus.NoCut"/>.</param>
        /// <param name="yTopmostInteriorCellCut">The topmost Y coordinate where the vertical line cuts an interior cell.<para/>  
        /// This item is returned as zero when <paramref name="status"/> = <see cref="eCutStatus.NoCut"/> or <see cref="eCutStatus.SectionAndInteriorCellCut"/>.</param>
        /// <param name="yBottommostInteriorCellCut">The bottommost Y coordinate where the vertical line cuts an interior cell.<para/>  
        /// This item is returned as zero when <paramref name="status"/> = <see cref="eCutStatus.NoCut"/> or <see cref="eCutStatus.SectionAndInteriorCellCut"/>.</param>
        public void GetSuperstructureCutSlabCoordinatesAtX(string name,
            int cutIndex,
            double xCoordinate,
            ref eCutStatus status,
            ref double yTopmostSectionCut,
            ref double yBottommostSectionCut,
            ref double yTopmostInteriorCellCut,
            ref double yBottommostInteriorCellCut)
        {
            int csiStatus = 0;

            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutSlabCoordsAtX(name, cutIndex, xCoordinate, ref csiStatus, ref yTopmostSectionCut, ref yBottommostSectionCut, ref yTopmostInteriorCellCut, ref yBottommostInteriorCellCut);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            status = (eCutStatus) csiStatus;
        }

        /// <summary>
        /// This function returns the name of a single tendon object, giving access to tendon assignments, tendon section, and material property. <para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="tendonIndex">The index number of a tendon in this section cut of this bridge object. 
        /// This must be from 0 to CountTendon-1, where CountTendon is the number of tendons returned by function <see cref="GetSuperstructureCutSectionValues"/> using Item = <see cref="eSectionCutItem.NumberOfTendons"/>.</param>
        /// <param name="nameBridgeTendon">The name of the tendon inside of the bridge object corresponding to <paramref name="tendonIndex"/>.</param>
        /// <param name="nameTendonObject">The name of the tendon object created by the program from the bridge object tendon corresponding to <paramref name="tendonIndex"/>.</param>
        public void GetSuperstructureCutTendonNames(string name,
            int cutIndex,
            int tendonIndex,
            ref string nameBridgeTendon,
            ref string nameTendonObject)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutTendonNames(name, cutIndex, tendonIndex, ref nameBridgeTendon, ref nameTendonObject);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns an individual section property for a single tendon at a single superstructure section cut in a bridge object. <para/>
        /// These properties are calculated for the section before skew, grade, and superstructure elevation are applied. <para/>
        /// Coordinate values are measured from the lower-left corner of the section bounding-box. <para/>
        /// X is positive to the right when looking upstation, and Y is positive upward.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="tendonIndex">The index number of a tendon in this section cut of this bridge object. 
        /// This must be from 0 to CountTendon-1, where CountTendon is the number of tendons returned by the function <see cref="GetSuperstructureCutSectionValues"/> using Item = <see cref="eSectionCutItem.NumberOfTendons"/>.</param>
        /// <param name="tendonCutItem">The type of property value to be gotten.</param>
        /// <param name="tendonCutValue">The value of the requested item:<para/>
        /// <see cref="eTendonCutItem.XCoordinate"/>: Any value OK. [L]<para/>
        /// <see cref="eTendonCutItem.YCoordinate"/>: Any value OK. [L]<para/>
        /// <see cref="eTendonCutItem.Diameter"/>: Value &gt;= 0. [L]<para/>
        /// <see cref="eTendonCutItem.BondingType"/>: 1 = Bonded, 2 = Unbonded. <para/>
        /// <see cref="eTendonCutItem.Slope"/>: Any value OK. [L/L]</param>
        public void GetSuperstructureCutTendonValues(string name,
            int cutIndex,
            int tendonIndex,
            eTendonCutItem tendonCutItem,
            ref double tendonCutValue)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutTendonValues(name, cutIndex, tendonIndex, (int)tendonCutItem, ref tendonCutValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns information about the box girder web thicknesses at a given elevation in the box girder section.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="yCoordinate">The Y coordinate in the section local coordinate system at which the web coordinates are returned.</param>
        /// <param name="webIsCut">Booleans indicating if each web is cut by a horizontal line at the specified Y coordinate.</param>
        /// <param name="xCoordinatesWebLeft">This is a array of X coordinates of the left side of each web. <para/>
        /// If the web is not cut by a horizontal line at the specified Y coordinate, this value is returned as zero.</param>
        /// <param name="xCoordinatesWebRight">This is a array of X coordinates of the right side of each web. <para/>
        /// If the web is not cut by a horizontal line at the specified Y coordinate, this value is returned as zero.</param>
        public void GetSuperstructureCutWebCoordinatesAtY(string name,
            int cutIndex,
            double yCoordinate,
            ref bool[] webIsCut,
            ref double[] xCoordinatesWebLeft,
            ref double[] xCoordinatesWebRight)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutWebCoordsAtY(name, cutIndex, yCoordinate, ref _numberOfItems, ref webIsCut, ref xCoordinatesWebLeft, ref xCoordinatesWebRight);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function returns location and material information about a single stress point in a web at a superstructure section cut in a bridge object. <para/>
        /// The function returns zero if the information is successfully retrieved; otherwise it returns a nonzero value.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="Superstructure.CountSuperstructureCut"/>.</param>
        /// <param name="webIndex">The index number of the web in this section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="GetSuperstructureCutSectionValues"/>.</param>
        /// <param name="pointIndex">The index number of the stress point in this web of this section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by function <see cref="CountSuperstructureCutWebStressPoint"/>.</param>
        /// <param name="coordinateOfStressPoint">The transverse and vertical coordinates of the stress point in the section, measured from the bottom left corner of the section. <para/> 
        /// X is positive to the right when looking upstation, Y is positive upward. [L]</param>
        /// <param name="nameMaterial">The name of the material property at this stress point.</param>
        /// <param name="note">A description of the stress point that may be used for identification. <para/>
        /// Points that are pre-defined by the program will have prescribed notes.</param>
        public void GetSuperstructureCutWebStressPoint(string name,
            int cutIndex,
            int webIndex,
            int pointIndex,
            ref Coordinate2DCartesian coordinateOfStressPoint,
            ref string nameMaterial,
            ref string note)
        {
            double x = 0;
            double y = 0;

            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutWebStressPoint(name, cutIndex, webIndex, pointIndex, ref x, ref y, ref nameMaterial, ref note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            coordinateOfStressPoint.X = x;
            coordinateOfStressPoint.Y = y;
        }

        /// <summary>
        /// This function returns an individual section property for a single web at a single superstructure section cut in a bridge object. <para/>
        /// These properties are calculated for the section before skew, grade, and superstructure elevation are applied. <para/>
        /// Coordinate values are measured from the lower-left corner of the section bounding-box. X is positive to the right when looking upstation, and Y is positive upward.<para/>
        /// An error is returned for items <see cref="eWebCutItem.MinTopSlabThickness"/>, <see cref="eWebCutItem.MinBottomSlabThickness"/>, <see cref="eWebCutItem.TopWidthOfCell"/> and <see cref="eWebCutItem.BottomWidthOfCell"/> if the <paramref name="webIndex"/> is specified as 0.<para/>
        /// If the bridge object is not currently linked to existing objects in the model, an error is returned.
        /// TODO: handle this.
        /// </summary>
        /// <param name="name">The name of an existing bridge object.</param>
        /// <param name="cutIndex">The index number of the section cut in this bridge object. <para/>
        /// This must be from 0 to Count-1, where Count is the value returned by the function <see cref="Superstructure.CountSuperstructureCut"/>. <para/>
        /// Section cuts will be in order of increasing station and increasing location as defined in the <see cref="Superstructure.GetSuperstructureCutLocation"/>.</param>
        /// <param name="webIndex">The index number of a web in this section cut of this bridge object. <para/>
        /// This must be from 0 to CountWeb-1, where CountWeb is the number of webs returned by the function <see cref="GetSuperstructureCutSectionValues"/> using Item = <see cref="eSectionCutItem.NumberOfGirdersOrWebs"/>. <para/>
        /// Webs count from left to right when looking upstation.</param>
        /// <param name="webCutItem">The type of property value to be gotten.</param>
        /// <param name="webCutValue">The value of the requested item:<para/>
        /// <see cref="eWebCutItem.AngleFromVertical"/>: Abs(<paramref name="webCutValue"/>) &gt; 90. [deg]<para/>
        /// <see cref="eWebCutItem.MinHorizontalWebThickness"/>: <paramref name="webCutValue"/> &gt; 0. [L]<para/>
        /// <see cref="eWebCutItem.MinTopSlabThickness"/>: <paramref name="webCutValue"/> &gt; 0. [L]<para/>
        /// <see cref="eWebCutItem.MinBottomSlabThickness"/>: <paramref name="webCutValue"/> &gt; 0. [L]<para/>
        /// <see cref="eWebCutItem.TopWidthOfCell"/>: <paramref name="webCutValue"/> &gt;= 0. [L]<para/>
        /// <see cref="eWebCutItem.BottomWidthOfCell"/>: <paramref name="webCutValue"/> &gt;= 0. [L]</param>
        public void GetSuperstructureCutWebValues(string name,
            int cutIndex,
            int webIndex,
            eWebCutItem webCutItem,
            ref double webCutValue)
        {
            _callCode = _sapModel.BridgeAdvancedSuper.BASConcBox.GetSuperCutWebValues(name, cutIndex, webIndex, (int)webCutItem, ref webCutValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#endregion
    }
}
#endif
