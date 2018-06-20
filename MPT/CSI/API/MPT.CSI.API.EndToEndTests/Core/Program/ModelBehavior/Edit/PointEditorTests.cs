#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Edit;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Edit
{
    [TestFixture]
    public class PointEditor
    {
       
        public void Align(ePointAlignOption alignmentOption,
            double ordinate,
            ref int numberPoints,
            ref string[] pointNames)
        {
          
        }

        
        public void Connect(ref int numberPoints,
            ref string[] pointNames)
        {
          
        }

        
        public void Disconnect(ref int numberPoints,
            ref string[] pointNames)
        {
          
        }

        
        public void Merge(double tolerance,
            ref int numberPoints,
            ref string[] pointNames)
        {
          
        }

        
        public void Move(string name,
            Coordinate3DCartesian coordinate,
            bool noWindoRefresh = false)
        {
          
        }
    }
}
#endif