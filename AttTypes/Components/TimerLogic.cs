using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class TimerLogic : ATTSerializableComponent
    {
        public bool activeOnLoadSaved;
        public bool timerResetsWhenRetriggeredSaved;
        public float activeDurationSaved;
    }
}
