
namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Multi-response case design.
    /// </summary>
    public enum eMultiResponseCaseDesign
    {
        /// <summary>
        /// Envelopes.
        /// </summary>
        Envelopes = 1,

        /// <summary>
        /// Step-by-step.
        /// </summary>
        StepByStep = 2,

        /// <summary>
        /// Last step.
        /// </summary>
        LastStep = 3,

        /// <summary>
        /// Envelopes - All.
        /// </summary>
        EnvelopesAll = 4,

        /// <summary>
        /// Step-by-step - All.
        /// </summary>
        StepByStepAll = 5
    }
}
