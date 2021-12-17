using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class FallbackSerializedType : ATTSerializableComponent
    {
        public string FALLBACKNAMEAAA;
        //public string data;
        public bool[] data;
        public FallbackSerializedType(EthynReader reader, string name)
        {
            this.FALLBACKNAMEAAA = name;
            BitSize = reader.ReadUint();
            data = reader.Read(BitSize);
        }
        public FallbackSerializedType()
        {

        }


    }
}