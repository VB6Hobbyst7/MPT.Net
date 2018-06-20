#if !BUILD_ETABS2015 && !BUILD_ETABS2016
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Aluminum material types available in the program.
    /// </summary>
    public enum eMaterialPropertyTypeAluminum
    {
        SubType_6061_T6 = 1,
        SubType_6063_T6 = 2,  
        SubType_5052_H34 = 3 
    }
}
#endif