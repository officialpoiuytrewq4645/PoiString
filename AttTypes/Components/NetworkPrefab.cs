using System.Collections.Generic;

using PoiString.AttTypes.Components;

namespace PoiString.AttTypes
{
    public class NetworkPrefab
    {
        public uint PrefabHash;

        public float positonX;
        public float positonY;
        public float positonZ;

        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float RotationW;

        public float Scale = 1;

        public List<ATTSerializableComponent> Components = new List<ATTSerializableComponent>();
        public List<ATTEmbededEntity> EmbededEntities = new List<ATTEmbededEntity>();
        public List<ChildNetworkPrefab> Children = new List<ChildNetworkPrefab>();
        //public string UnprocessedData;

    }
    public class ATTEmbededEntity
    {
        public uint hash;
        public uint size;
        public bool isAlive;
        public List<ATTSerializableComponent> Components = new List<ATTSerializableComponent>();
    }
}

