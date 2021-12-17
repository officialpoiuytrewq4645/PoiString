using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class Landmark : ATTSerializableComponent
    {
        //public MarkerInfo[] Markers;
        public uint Index;
        public uint MapHash;
        public int DiscoveryStateEnum;
        public uint AwareIconHash;
        public uint DiscoverIconHash;
        public bool IsMasked;
        public uint FindActionAcheivementHash;
    }

    public enum DiscoveryState
    {
        // Token: 0x04005BDF RID: 23519
        None,
        // Token: 0x04005BE0 RID: 23520
        Aware,
        // Token: 0x04005BE1 RID: 23521
        Discovered
    }

    //public class MarkerInfo
    //{
    //    public uint mapHash;
    //    public uint Index;
    //    public Vector2 Position;
    //    public bool IsMasked;
    //    public uint DiscoveredIconHash;
    //    public uint AwareIconHash;
    //    public int DefaultStateEnum;
    //    public bool IsKeptAfterUnload;
    //}
}
