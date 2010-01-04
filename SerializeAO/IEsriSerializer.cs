using System;
using System.Collections.Generic;
using System.Text;

namespace SerializeAO
{
    public interface IEsriSerializer
    {
        object Serialize(object arcObject);
        object Deserialize(object data);
    }
}
