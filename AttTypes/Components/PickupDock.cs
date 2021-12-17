using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class PickupDock : ATTSerializableComponent
    {
        public uint dockedTypeHash;
        public int quantity;
        public int childIndex;
    }
}
