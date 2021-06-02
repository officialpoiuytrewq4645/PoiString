using System.Collections.Generic;

namespace PoiString.Knowledge
{
    public class KnownMaterialHashes
    {
        public static Dictionary<uint, string> AllMats = new Dictionary<uint, string>
    {
        {61790,"Canvas"},
        {29972,"Redwood Gotera Core Material"},
        {35204,"Rope"},
        {16882,"Straw"},
        {33384,"Dais Leather"},
        {37850,"Dais Red Leather"},
        {42316,"Unknown Leather"},
        {63538,"Wyrm Face Leather"},
        {20178,"Aluminium"},
        {11420,"Brass"},
        {24094,"Carsi Alloy"},
        {20186,"Copper"},
        {31502,"Evinon Steel Alloy"},
        {56394,"Gold"},
        {1    ,"Iron"},
        {31174,"Mythril"},
        {16222,"Orchi Alloy"},
        {31468,"Red Iron Alloy"},
        {28618,"Silver"},
        {54964,"Tin"},
        {16064,"TrainingMetalCold"},
        {3288,"TrainingMetalHot"},
        {24028,"White Gold Alloy"},
        {19966,"Ash"},
        {37260,"Birch"},
        {24722,"Oak"},
        {10962,"Redwood"},
        {15500,"Walnut"}
    };
        public static Dictionary<string, uint> InverseAllMats = new Dictionary<string, uint>();
    }
}
