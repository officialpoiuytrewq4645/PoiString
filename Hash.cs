


using PoiString.AttTypes.Components;
using PoiString.Knowledge;

namespace PoiString.AttTypes.Components.ValueTypes
{
    [ClassIgnoresCustomClassRules]
    internal class Hash<T> //: Knowledge.IKnowledge
    {
        public uint hash;
        public override string ToString()
        {
            return KnowledgeManager.knowleg[typeof(T)][hash];
        }
    }
}
