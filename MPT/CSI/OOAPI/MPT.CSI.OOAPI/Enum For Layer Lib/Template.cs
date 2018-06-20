using MPT.CSI.API.Core.Support;

#if BUILD_SAP2000v16
using SAP2000v16;
#elif BUILD_SAP2000v17
using SAP2000v17;
#elif BUILD_SAP2000v18
using SAP2000v18;
#elif BUILD_SAP2000v19
using SAP2000v19;
#elif BUILD_CSiBridgev18
using CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiBridge19;
#elif BUILD_ETABS2013
using ETABS2013;
#elif BUILD_ETABS2015
using ETABS2015;
#elif BUILD_ETABS2016
using ETABS2016;
#endif

#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#endif

namespace MPT.CSI.API.Core
{
    /// <summary>
    /// Represents the xxxx in the application.
    /// </summary>
    public class Template : CSiApiBase
    {

        #region Fields


        #endregion


        #region Properties



        #endregion


        #region Initialization

        public Template(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        public void GetThing(ref string param)
        {
            //_callCode = _sapModel.
            if (throwCurrentApiException(_callCode)) { throw new CSiException(); }
        }


        public void SetThing(string param)
        {
            //_callCode = _sapModel.
            if (throwCurrentApiException(_callCode)) { throw new CSiException(); }
        }

        #endregion

        // Only SAP2000
#if BUILD_SAP2000v16 || BUILD_SAP2000v17 || BUILD_SAP2000v18 || BUILD_SAP2000v19

#endif

        // Only CSiBridge
#if BUILD_CSiBridgev18 || BUILD_CSiBridgev19

#endif

        // Only ETABS
#if BUILD_ETABS2013 || BUILD_ETABS2014 || BUILD_ETABS2015 || BUILD_ETABS2016

#endif


        // All but CSiBridge
#if !BUILD_CSiBridgev18 && !BUILD_CSiBridgev19

#endif

        // All but ETABS
#if !BUILD_ETABS2013 && !BUILD_ETABS2014 && !BUILD_ETABS2015 && !BUILD_ETABS2016

#endif
    }
}
