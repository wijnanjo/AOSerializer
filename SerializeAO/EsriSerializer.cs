using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace SerializeAO
{
    /// <summary>
    /// This is a very slow serializer. It even slows down when it is used with caching
    /// So, just don't use it!!!
    /// </summary>
    public class EsriSerializer: IEsriSerializer
    {
        private IXMLSerializer serializer;

        public EsriSerializer()
        {
            serializer = new XMLSerializerClass();
        }
    
        public object Serialize(object arcObject)
        {
            string data = serializer.SaveToString(arcObject, null, null);
            return data;
        }

        public object Deserialize(object data)
        {
            object o = serializer.LoadFromString((string)data, null, null);
            return o;
        }
    }
}
