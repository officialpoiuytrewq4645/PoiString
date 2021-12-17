using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes
{
    [System.Serializable]
    public class ChildNetworkPrefab
    {
        public uint parentHash;
        public NetworkPrefab Prefab;
    }
}
