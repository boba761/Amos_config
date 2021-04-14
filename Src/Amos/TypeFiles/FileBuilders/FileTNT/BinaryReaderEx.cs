using System;
using System.IO;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public static class BinaryReaderEx
    {
        public static bool[] ReadBools( this BinaryReader bReader, int count )
        {
            bool[] array = new bool[count];
            for (int i = 0; i < count; i++)
                array[i] = bReader.ReadInt32() == 1;
            return array;
        }

        public static short[] ReadShorts(this BinaryReader bReader, int count )
        {
            short[] array = new short[count];
            for (int i = 0; i < count; i++)
                array[i] = bReader.ReadInt16();
            return array;
        }

        public static int[] ReadInts(this BinaryReader bReader, int count )
        {
            int[] array = new int[count];
            for (int i = 0; i < count; i++)
                array[i] = bReader.ReadInt32();
            return array;
        }

        public static float[] ReadFloats( this BinaryReader bReader, int count )
        {
            float[] array = new float[count];
            for (int i = 0; i < count; i++)
                array[i] = bReader.ReadSingle();
            return array;
        }

        public static double[] ReadDoubles( this BinaryReader bReader, int count )
        {
            double[] array = new double[count];
            for (int i = 0; i < count; i++)
                array[i] = bReader.ReadDouble();
            return array;
        }

        public static DateTime ReadCTime( this BinaryReader bReader )
        {
            TimeSpan span = TimeSpan.FromTicks(bReader.ReadUInt32() * TimeSpan.TicksPerSecond);
            DateTime t = new DateTime(1970, 1, 1).Add(span);
            return TimeZone.CurrentTimeZone.ToLocalTime(t);
        }

        public static TimeSpan ReadCTimeSpan( this BinaryReader bReader )
        {
            return TimeSpan.FromTicks(bReader.ReadUInt32() * TimeSpan.TicksPerSecond);
        }
    }
}
