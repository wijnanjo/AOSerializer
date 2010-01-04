using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace SerializeAO
{
    /// <summary>
    /// This is a very fast serializer but fails when use in an MTA thread
    /// In fact, you should never use AO in MTA threads
    /// </summary>
    public class EsriVariantSerializer: IEsriSerializer
    {
        private readonly IVariantStream vs;

        public EsriVariantSerializer()
        {
            vs = new VariantStreamIOClass();
        }

        ~EsriVariantSerializer()
        {
            Marshal.ReleaseComObject(vs);
        }
    
        public object Serialize(object arcObject)
        {
            IMemoryBlobStream  str = new MemoryBlobStreamClass();
            ((IVariantStreamIO)vs).Stream = str;
            vs.Write(arcObject);
            object variant;
            ((IMemoryBlobStreamVariant) str).ExportToVariant(out variant);
            return variant;
        }

        public object Deserialize(object data)
        {
            IMemoryBlobStreamVariant str = new MemoryBlobStreamClass();
            str.ImportFromVariant(data);
            ((IVariantStreamIO)vs).Stream = (IMemoryBlobStream)str;
            object o = vs.Read();
            return o;
        }
    }
}
