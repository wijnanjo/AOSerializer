using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace SerializeAO
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            int numLines = 100;
            DateTime t1 = DateTime.Now;
            IList<IPolyline> lines = new List<IPolyline>(numLines);
            IList<object> serializedLines = new List<object>(numLines);
            IList<IPolyline> Deserializedlines = new List<IPolyline>(numLines);
            IEsriSerializer serializer = new EsriVariantSerializer();

            Console.WriteLine(String.Format("Creating {0} lines", numLines));
            for (int i = 0; i < numLines; i++)
            {
                lines.Add(CreatePl());
                Console.CursorLeft = 0;
                Console.Write(i.ToString());
            }
            TimeSpan ts = DateTime.Now.Subtract(t1);
            Console.WriteLine();
            Console.WriteLine(String.Format("Lines construction duration = {0}. Press any key to serialize", ts));

            t1 = DateTime.Now;
            int j=0;
            foreach (IPolyline line in lines)
            {
                serializedLines.Add(serializer.Serialize(line));
                Console.CursorLeft = 0;
                Console.Write(j.ToString());
                j++;
            }
            ts = DateTime.Now.Subtract(t1);
            Console.WriteLine();
            Console.WriteLine(String.Format("Serialization duration = {0}. Press any key to deserialize", ts));

            t1 = DateTime.Now;
            j = 0;
            foreach (object o in serializedLines)
            {
                IPolyline l = (IPolyline) serializer.Deserialize(o);
                Deserializedlines.Add(l);
                Console.CursorLeft = 0;
                Console.Write(j.ToString());
                j++;
            }
            ts = DateTime.Now.Subtract(t1);
            Console.WriteLine();
            Console.WriteLine(String.Format("Deserialization duration = {0}. Press any key to compare", ts));

            int difs = 0;
            for (int i = 0; i < numLines; i++)
            {
                IClone clone = (IClone) lines[i];
                if (!clone.IsEqual((IClone)Deserializedlines[i]))
                {
                    Console.WriteLine(String.Format("Lines are not equal. index = {0}", i));
                    difs++;
                }
            }
            Console.WriteLine(String.Format("Number of non-equal serializings {0}. Press any key to compare", difs));
            Console.ReadLine();
        }

        private static IPolyline CreatePl()
        {
            Random r = new Random();
            
            IPointCollection pc = (IPointCollection)new PolylineClass();
            for (int i=0;i<500;i++)
            {
                IPoint p = new PointClass();
                p.X = r.Next(100000);
                p.Y = r.Next(100000);
                object missing = Type.Missing;
                pc.AddPoint(p, ref missing, ref missing);
            }
            return (IPolyline)pc;
        }
    }

    
}
