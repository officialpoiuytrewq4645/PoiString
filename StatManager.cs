using System;

namespace PoiString.AttTypes.Components
{
    class StatManager : ATTSerializableComponent
    {
        public Stat[] stats;
        public TimedModifier[] modifiers;
        public IndirectStatModifier[] indirectStatModifiers;
    }

    class Stat
    {
        public uint StatHash;
        public float baseFlat;
    }
    class TimedModifier
    {
        public uint StatHash;
        public float value;
        public bool isMultiplier;
        public TimeSpan time;
    }
    class IndirectStatModifier
    {
        public uint StatHash;
        public TimeSpan time;
        public IndirectModifierSaveData[] modifiers;
    }
    class IndirectModifierSaveData
    {
        public uint valueOverDurationHash;
        public float baseValue;
        public uint duration;
        public uint tick;
    }
}

