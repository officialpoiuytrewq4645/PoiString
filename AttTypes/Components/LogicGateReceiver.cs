using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
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
	public enum LogicOperationType
	{
		And,
		Or,
		Xor,
		Nand,
		Toggle
	}
}
