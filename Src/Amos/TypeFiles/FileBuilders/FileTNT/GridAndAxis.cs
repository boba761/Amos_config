using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class GridAndAxis
    {
        public double[]	majorTickInc;               // Increment between major ticks
        public short[] minorIntNum;                 // Number of intervals between major ticks 
        public short[] labelPrecision;              // Number of digits after the decimal point
        public double gaussPerCentimeter;           // Used for calculation of distance axis in frequency domain
        public short gridLines;                     // Number of horizontal grid lines to be shown in data area 
        public short axisUnits;                     // Type of units to show - see constants.h
        public bool showGrid;                       // Show or hide the grid	
        public bool showGridLabels;                 // Show or hide the labels on the grid lines
        public bool adjustOnZoom;                   // Adjust the number of ticks and the precision when zoomed in
        public bool showDistanceUnits;			    // whether to show frequency or distance units when in frequency domain
        public string axisName;				        // file name of the axis (not used as of 4/10/97)
        public byte[] space;

        public GridAndAxis(BinaryReader bReader)
        {
            majorTickInc = bReader.ReadDoubles(12);
            minorIntNum = bReader.ReadShorts(12);
            labelPrecision = bReader.ReadShorts(12);
            gaussPerCentimeter = bReader.ReadDouble();
            gridLines = bReader.ReadInt16();
            axisUnits = bReader.ReadInt16();
            showGrid = bReader.ReadInt32() == 1;
            showGridLabels = bReader.ReadInt32() == 1;
            adjustOnZoom = bReader.ReadInt32() == 1;
            showDistanceUnits = bReader.ReadInt32() == 1;
            axisName = Encoding.ASCII.GetString(bReader.ReadBytes(32));
            space = bReader.ReadBytes(52);
        }
    }
}
