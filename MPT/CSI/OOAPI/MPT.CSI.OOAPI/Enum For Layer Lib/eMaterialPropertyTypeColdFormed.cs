#if !BUILD_ETABS2015 && !BUILD_ETABS2016
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Cold-formed material types available in the program.
    /// </summary>
    public enum eMaterialPropertyTypeColdFormed
    {
        ASTM_A653SQGr33 = 1,
        ASTM_A653SQGr50 = 2 
    }
}
#endif
