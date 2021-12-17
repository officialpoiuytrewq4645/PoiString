using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class LogicBoolReceiver : LogicReceiver
    {
    }
    [System.Serializable]
    public class LogicIntReceiver : LogicReceiver
    {
    }
    [System.Serializable]
    public class LogicFloatReceiver : LogicReceiver
    {
    }
    [System.Serializable]
    public class LogicGateReceiver : ATTSerializableComponent
    {
        public uint[] Datas;
        public int operationtype;
        public bool isInversedOutputSaved;
        public override string ToString()
        {
            return Enum.GetName(typeof(LogicOperationType), operationtype);
        }
    }
    [System.Serializable]
    public class LogicReceiver : ATTSerializableComponent
    {
        public uint sender;
    }
    public enum LogicOperationType
    {
        And,
        Or,
        Xor,
        Nand,
        Toggle
    }
}
