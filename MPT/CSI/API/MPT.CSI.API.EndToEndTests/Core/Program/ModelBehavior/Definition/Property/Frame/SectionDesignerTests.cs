using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.Property.Frame
{
    [TestFixture]
    public class SectionDesigner_Get : CsiGet
    {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        
        
        public void GetChannel(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
          
        }

        
        public void GetDoubleAngle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double w,
            ref double tf,
            ref double tw,
            ref double separation)
        {
          
        }


        
        public void GetPlate(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double thickness,
            ref double width)
        {
          
        }

        public void GetSolidSector(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double angle,
            ref double radius,
            ref int color)
        {
          
        }
        
        
        public void GetSolidSegment(string name,
            string nameShape,
            ref string nameMaterial,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double angle,
            ref double radius,
            ref int color)
        {
          
        }
        
        
        public void GetPipe(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double diameter,
            ref double thickness,
            ref int color)
        {
          
        }

        
        public void GetTube(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref double tf,
            ref double tw,
            ref int color)
        {
          
        }
                
        
        public void GetReferenceCircle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref double diameter)
        {
          
        }

        
        public void GetReferenceLine(string name,
            string nameShape,
            ref Coordinate2DCartesian startCoordinate,
            ref Coordinate2DCartesian endCoordinate)
        {
          
        }
#endif
#if !BUILD_ETABS2015


        public void GetTee(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
          
        }

        
        public void GetAngle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw)
        {
          
        }

        
        public void GetISection(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref double bfBottom,
            ref double tfBottom)
        {
          
        }
        
        
        public void GetCircle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double diameter,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar,
            ref int numberOfBars,
            ref double cover,
            ref string barSize)
        {
          
        }

        
        public void GetRectangle(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar)
        {
          
        }

        public void GetReinforcementSingle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref string barSize,
            ref string nameMaterial)
        {
          
        }

        
        public void GetReinforcedCorner(string name,
            string nameShape,
            ref int[] pointNumbers,
            ref string[] barSizes)
        {
          
        }
        
        
        public void GetReinforcedEdge(string name,
            string nameShape,
            ref int[] edgeNumbers,
            ref string[] barSizes,
            ref double[] spacing,
            ref double[] cover)
        {
          
        }

        
        public void GetReinforcedLine(string name,
            string nameShape,
            ref Coordinate2DCartesian startCoordinate,
            ref Coordinate2DCartesian endCoordinate,
            ref string barSize,
            ref double barSpacing,
            ref bool endBarsExist,
            ref string nameMaterial)
        {
          
        }

        
        public void GetReinforcedCircle(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref string barSize,
            ref double rotation,
            ref double diameter,
            ref int numberBars,
            ref string nameMaterial)
        {
          
        }


        
        public void GetReinforcedRectangular(string name,
            string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref double h,
            ref double w,
            ref string nameMaterial)
        {
          
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017

        public void GetReinforcedTee(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref bool mirrorAbout3)
        {
          
        }

        
        public void GetReinforcedL(string name,
            string nameShape,
            ref string nameMaterial,
            ref string fileName,
            ref Coordinate2DCartesian centerCoordinate,
            ref double rotation,
            ref int color,
            ref double h,
            ref double bf,
            ref double tf,
            ref double tw,
            ref bool mirrorAbout2,
            ref bool mirrorAbout3)
        {
          
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20

        public void GetPolygon(string name,
            string nameShape,
            ref string nameMaterial,
            ref string stressStrainOverwrite,
            ref int numberOfPoints,
            ref Coordinate2DCartesian[] coordinates,
            ref double[] radius,
            ref int color,
            ref bool isReinforced,
            ref string nameMaterialRebar)
        {
          
        }
#endif
    }
    
    [TestFixture]
    public class SectionDesigner_Set : CsiSet
    {
      #if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public void Delete(string name,
            string nameShape,
            bool deleteAll = false)
        {
          
        }
        
        
        public void SetTee(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
          
        }
        
        
        public void SetAngle(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
          
        }
        
        
        public void SetChannel(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4)
        {
          
        }

        
        public void SetDoubleAngle(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double w = 24,
            double tf = 2.4,
            double tw = 2.4,
            double separation = 1.2)
        {
          
        }


        
        public void SetISection(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double h = 24,
            double bf = 24,
            double tf = 2.4,
            double tw = 2.4,
            double bfBottom = 24,
            double tfBottom = 2.4)
        {
          
        }
        
        
        public void SetPlate(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            int color = -1,
            double thickness = 2.4,
            double width = 24)
        {
          
        }
        
        
        public void SetSolidSector(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double angle,
            double radius,
            int color = -1)
        {
          
        }
        
        
        public void SetSolidSegment(string name,
            ref string nameShape,
            string nameMaterial,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double angle,
            double radius,
            int color = -1)
        {
          
        }

        
        public void SetCircle(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            Coordinate2DCartesian centerCoordinate,
            double rotation = 22.5,
            double diameter = 24,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "",
            int numberOfBars = 8,
            double cover = 2,
            string barSize = "")
        {
          
        }
        
        
        public void SetPipe(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double diameter = 24,
            double thickness = 2.4,
            int color = -1)
        {
          
        }

        
        
        public void SetRectangle(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h,
            double w,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "")
        {
          
        }

        
        public void SetTube(string name,
            ref string nameShape,
            string nameMaterial,
            string fileName,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h = 24,
            double w = 24,
            double tf = 2.4,
            double tw = 2.4,
            int color = -1)
        {
          
        }
        
        
        
        public void SetReinforcementSingle(string name,
            ref string nameShape,
            ref Coordinate2DCartesian centerCoordinate,
            string barSize,
            string nameMaterial = "")
        {
          
        }
        
        
        public void SetReinforcedCorner(string name,
            ref string nameShape,
            int pointNumber,
            string barSize,
            bool applyRebarToAllCorners = false)
        {
          
        }


        
        public void SetReinforcedEdge(string name,
            ref string nameShape,
            int edgeNumber,
            string barSize,
            double spacing,
            double cover,
            bool applyRebarToAllEdges = false)
        {
          
        }
        
        
        public void SetReinforcedLine(string name,
            ref string nameShape,
            Coordinate2DCartesian startCoordinate,
            Coordinate2DCartesian endCoordinate,
            string barSize,
            double barSpacing,
            bool endBarsExist = false,
            string nameMaterial = "")
        {
          
        }
        
        
        public void SetReinforcedCircle(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            string barSize,
            double rotation,
            double diameter,
            int numberBars,
            string nameMaterial = "")
        {
          
        }

        
        public void SetReinforcedRectangular(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            double rotation,
            double h,
            double w,
            string nameMaterial = "")
        {
          
        }
        

        
        public void SetReferenceCircle(string name,
            ref string nameShape,
            Coordinate2DCartesian centerCoordinate,
            double diameter)
        {
          
        }
        
        
        public void SetReferenceLine(string name,
            ref string nameShape,
            Coordinate2DCartesian startCoordinate,
            Coordinate2DCartesian endCoordinate)
        {
          
        }
      #endif
      #if BUILD_SAP2000v19 || BUILD_SAP2000v20 || BUILD_CSiBridgev19 || BUILD_CSiBridgev20
      
        
        public void SetPolygon(string name,
            ref string nameShape,
            string nameMaterial,
            string stressStrainOverwrite,
            int numberOfPoints,
            Coordinate2DCartesian[] coordinates,
            double[] radius,
            int color = -1,
            bool isReinforced = false,
            string nameMaterialRebar = "")
        {
          
        }
      #endif
    }
}