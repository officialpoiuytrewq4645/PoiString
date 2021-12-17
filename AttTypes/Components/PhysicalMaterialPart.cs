using PoiString.AttTypes.Components.ValueTypes;
using PoiString.Knowledge;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class PhysicalMaterialPart : ATTSerializableComponent//, IHasSpecialOperation
    {
        //public Hash<PhysicalMaterial> Hash;
        public uint Hash;
        //private string Material_Name;
        //public PhysicalMaterialPart(string matname)
        //{
        //    Material_Name = matname;
        //}
        //public void PerformSpecialOperation()
        //{
        //    if (KnownMaterialHashes.AllMats.TryGetValue(Hash, out string value))
        //    {
        //        Material_Name = value;
        //    }
        //    else
        //    {
        //        Material_Name = Hash.ToString();
        //    }

        //}

        //public void ReverseSpecialOperation()
        //{
        //    if(KnownMaterialHashes.InverseAllMats.TryGetValue(Material_Name, out uint value))
        //    {
        //        Hash = value;
        //    }
        //    else if(uint.TryParse(Material_Name, out uint value2))
        //    {
        //        Hash = value2;
        //    }
        //    else
        //    {
        //    }
        //}
    }
    
}
