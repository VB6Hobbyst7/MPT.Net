
using System.ComponentModel;

namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Hinge state classifications used to determine where on the force/deformation curve the hinge lies. 
    /// Various actions may be taken beyond various classifications.
    /// </summary>
    public enum eHingeState
    {
        [Description("None")] None = 0,
        [Description("to B")] toB = 1,
        [Description("to C")] toC = 2,
        [Description("to D")] toD = 3,
        [Description("to E")] toE = 4,
        [Description("> E")] toEPlus = 5
    }
}
