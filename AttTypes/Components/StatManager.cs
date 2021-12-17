using System;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class StatManager : ATTSerializableComponent
    {
        public Stat[] stats;
        public TimedModifier[] modifiers;
        public IndirectStatModifier[] indirectStatModifiers;
    }

    [System.Serializable]
    public class Stat
    {
        public uint StatHash;
        public float baseFlat;
    }
    [System.Serializable]
    public class TimedModifier
    {
        public uint StatHash;
        public float value;
        public bool isMultiplier;
        public TimeSpan time;
    }
    [System.Serializable]
    public class IndirectStatModifier
    {
        public uint StatHash;
        public TimeSpan time;
        public IndirectModifierSaveData[] modifiers;
    }
    [System.Serializable]
    public class IndirectModifierSaveData
    {
        public uint valueOverDurationHash;
        public float baseValue;
        public uint duration;
        public uint tick;
    }
}

