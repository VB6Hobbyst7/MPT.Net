// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-15-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="ISolidObject.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Represents the Solid Object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IAddableObject" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGroupAssignable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISelectable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IObservableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableLocalAxes" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILocalAxesAdvancedWithPoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IGUID" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ISolidAutoMesh" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IEdgeConstraints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ISurfaceSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletableSpring" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableTransformationMatrix" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservablePoints" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableElement" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableSection" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.IChangeableMaterialTemperature" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadGravity" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadPorePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadStrain" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadSurfacePressure" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.ILoadTemperatureConstant" />
    public interface ISolidObject :
        IAddableObject,
        ICountable, IListableNames, IChangeableName, IGroupAssignable, ISelectable, IDeletable,
        IObservableLocalAxes, IChangeableLocalAxes,
        ILocalAxesAdvancedWithPoints, IGUID, ISolidAutoMesh, IEdgeConstraints, 
        ISurfaceSpring, IDeletableSpring,

        IObservableTransformationMatrix, IObservableTransformationMatrixObject,
        IObservablePoints, IObservableElement,
             
        IObservableSection, IChangeableSection, 
        IObservableMaterialTemperature, IChangeableMaterialTemperature,

        // Loads
        ILoadGravity, 
        ILoadPorePressure, 
        ILoadStrain, 
        ILoadSurfacePressure, 
        ILoadTemperatureConstant 
    {
        
    }
}
#endif
