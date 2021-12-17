using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class MapBoard : ATTSerializableComponent
    {
        public uint mapHash;
        public int unlockPieceIndex;
        public Vector2 positionOffset;
        public float zoom;
    }
}
