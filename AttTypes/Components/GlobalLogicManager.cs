using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class GlobalLogicManager : ATTSerializableComponent
    {
        public List<IATTSerializable> datas;
        public uint NextId;
        public GlobalLogicManager()
        {
            datas = new List<IATTSerializable>();
            NextId = 1;
        }
        //public void AddSender(LogicSender sender)
        //{
        //    sender.identifier = NextId++;
        //    datas.Add(sender);
        //}
    }
}
