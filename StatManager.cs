using System;

namespace PoiString.AttTypes.Components
{
    public class StatManager : ATTSerializableComponent
    {
        public Stat[] stats;
        public TimedModifier[] modifiers;
        public IndirectStatModifier[] indirectStatModifiers;
    }

    public class Stat
    {
        public uint StatHash;
        public float baseFlat;
    }
    public class TimedModifier
    {
        public uint StatHash;
        public float value;
        public bool isMultiplier;
        public TimeSpan time;
    }
    public class IndirectStatModifier
    {
        public uint StatHash;
        public TimeSpan time;
        public IndirectModifierSaveData[] modifiers;
    }
    public class IndirectModifierSaveData
    {
        public uint valueOverDurationHash;
        public float baseValue;
        public uint duration;
        public uint tick;
    }
}

