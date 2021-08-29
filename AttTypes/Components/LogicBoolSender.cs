using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    public class LogicBoolSender : ATTSerializableComponent
    {
        //public LogicSaveData logicData;
        public int identifier;
        public uint testa;
        public uint testb;
        public bool value;
    }
    public class LogicSaveData
    {
        public uint identifier;
    }
}
