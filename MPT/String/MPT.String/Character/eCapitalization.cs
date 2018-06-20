
namespace MPT.String.Character
{
    /// <summary>
    /// Enumeration of how to return the capitalization pattern of a string.
    /// </summary>
    public enum eCapitalization
    {
        /// <summary>
        /// Every character is capitalized.
        /// </summary>
        ALLCAPS = 0,

        /// <summary>
        /// Every character is lower-case.
        /// </summary>
        alllower = 1,

        /// <summary>
        /// The first character of the first word is capitalized, with all other characters as lower-case.
        /// </summary>
        Firstupper = 2,
    }
}
