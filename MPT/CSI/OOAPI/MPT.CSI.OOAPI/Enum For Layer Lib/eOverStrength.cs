
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Method by which the overstrength factor is determined in seismic design strength D/C cases.
    /// </summary>
    public enum eOverStrength
    {
        None = 1,

        ///' <summary>
        ///' The value specified in the code, if available.
        ///' </summary>
        ///' <remarks></remarks>
        CodeDefault = 2,

        UserSpecified = 3
    }
}
