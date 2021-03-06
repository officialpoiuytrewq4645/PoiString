using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoiString.AttTypes.Components.ValueTypes;

namespace PoiString.AttTypes.Components
{

    [System.Serializable]
    public class LiquidContainer : ATTSerializableComponent//, IHasSpecialOperation
    {
        public bool canAddTo;
        public bool canRemoveFrom;
        public int contentLevel;
        public bool hasContent;
        public bool isCustom;
        public uint PresetHash;
        public CustomData customData;
        //private string LeftOverData;
        //public bool[] data;

        //public void PerformSpecialOperation()
        //{
            
        //    EthynReader reader = new EthynReader(data);
        //    canAddTo = reader.ReadBool();
        //    canRemoveFrom = reader.ReadBool();
        //    contentLevel = reader.ReadInt();
        //    hasContent = reader.ReadBool();
        //    isCustom = reader.ReadBool();
        //    //if(!isCustom)
        //    //{
        //        PresetHash = reader.ReadUint();
        //    //}//gdfsgdfsg
        //    //else
        //    //{
        //        //customData = new CustomData
        //        //{
        //        //    color = new Color { r = reader.ReadFloat(), g = reader.ReadFloat(), b = reader.ReadFloat(), a = reader.ReadFloat() },
        //        //    isConsumableThroughSkin = reader.ReadBool(),
        //        //    visualDataHash = reader.ReadUint(),
        //        //    effects = effectsFromStream(reader.ReadArray(64)),
        //        //    foodChunks = uintArrayFromStream(reader.ReadArray(32))
        //        //};
        //    //}
        //}

        //private uint[] uintArrayFromStream(bool[][] stream)
        //{
        //    uint[] Output = new uint[stream.Length];
        //    int i = 0;
        //    foreach (bool[] data in stream)
        //    {
        //        EthynReader reader = new EthynReader(data);
        //        Output[i] = reader.ReadUint();
        //        i++;
        //    }
        //    return Output;
        //}

        //public void ReverseSpecialOperation()
        //{
        //    throw new NotImplementedException();
        //}
        //private Effect[] effectsFromStream(bool[][] stream)
        //{
        //    Effect[] Output = new Effect[stream.Length];
        //    int i = 0;
        //    foreach(bool[] data in stream)
        //    {
        //        EthynReader reader = new EthynReader(data);
        //        Output[i] = new Effect() { EffectHash = reader.ReadUint(), strengthMultiplier = reader.ReadFloat() };
        //        i++;
        //    }
        //    return Output;
        //}
    }

    [System.Serializable]
    public class CustomData
    {
        public Color color;
        public bool isConsumableThroughSkin;
        public uint visualDataHash;
        public Effect[] effects;
        //public Hash<FoodChunk>[] foodChunks;
        public uint[] foodChunks;
    }
    [System.Serializable]

    public class Effect
    {
        public uint EffectHash;
        public float strengthMultiplier;
        public override string ToString()
        {
            return $"Hash: {EffectHash}, Strength: {strengthMultiplier}";
        }
    }
}
