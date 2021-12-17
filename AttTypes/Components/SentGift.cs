using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    [Version(1)]
    public class SentGift : ATTSerializableComponent
    {
        public string receiversName;
        public string sendersName;
        public SavableSavedDynamicObject[] toSpawn;
        //public uint debug1;
        //public bool testA;
        //public bool testB;
        //public bool testC;
        //public bool testD;

        //public bool 
        public SentGiftTag senderTag;
    }

    [System.Serializable]
    public class SentGiftTag
    {
        public int from;
        public int to;
    }
    [System.Serializable]
    public class SavableSavedDynamicObject
    {
        public uint[] darta;
        public uint messageSizeInBytes;
        public uint hash;
        public uint[] chunkversioning;
    }
}
