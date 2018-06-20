
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Options for how concrete frame.
    /// Used in GetSummaryResultsColumn.
    /// </summary>
    public enum eConcreteDesignOption
    {
        /// <summary>
        /// Concrete frame rebar will be checked for adequacy for demands.
        /// </summary>
        Check = 1,

        /// <summary>
        /// Concrete frame rebar will be designed for demands.
        /// </summary>
        Design = 2
    }
}
