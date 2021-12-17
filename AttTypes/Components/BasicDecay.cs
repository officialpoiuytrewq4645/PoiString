using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    [Version(3)]
    public class BasicDecay : ATTSerializableComponent
    {
        public bool isDisabled;
        public TimeSpan timelineEntry;
    }
}
