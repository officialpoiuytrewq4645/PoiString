using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class Lever : ATTSerializableComponent
    {
        public bool toggleSaved;
        public bool syncWithSenderSaved;
    }
}
