using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
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
