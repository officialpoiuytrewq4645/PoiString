using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class Door : ATTSerializableComponent
    {
        public bool IsOpen;
    }
    public class SlidingDoor : Door
    {

    }
}
