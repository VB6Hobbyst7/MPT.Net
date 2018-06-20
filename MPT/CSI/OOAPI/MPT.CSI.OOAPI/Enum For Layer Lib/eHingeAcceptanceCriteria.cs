
using System.ComponentModel;

namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Hinge acceptance criteria used to determine where on the force/deformation curve the hinge lies. 
    /// Various actions may be taken beyond various criteria. 
    /// The criteria corresponds to Performance Based Design standards.
    /// </summary>
    public enum eHingeAcceptanceCriteria
    {
        [Description("None")] None = 0,

        /// [summary]
        /// Immediate Occupancy
        /// [/summary]
        /// [remarks][/remarks]
        [Description("to IO")] toIO = 1,

        /// [summary]
        /// Life Safety
        /// [/summary]
        /// [remarks][/remarks]
        [Description("to LS")] toLS = 2,

        /// [summary]
        /// Collapse Prevention
        /// [/summary]
        /// [remarks][/remarks]
        [Description("to CP")] toCP = 3,

        /// [summary]
        /// Collapse
        /// [/summary]
        /// [remarks][/remarks]
        [Description("> CP")] toCPPlus = 4
    }
}
