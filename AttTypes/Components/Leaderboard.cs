using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class Leaderboard : ATTSerializableComponent
    {
        public LeaderboardDefinitionData definition;
    }
    [System.Serializable]
    public class LeaderboardDefinitionData
    {
        public uint referencehash;
        public uint hash;
        public string coursename;
        public int size;
        public bool islowest;
    }
    [System.Serializable]
    public class CheckPoint : ATTSerializableComponent
    {
        public LeaderboardDefinitionData[] beginningDatas;

        public LeaderboardDefinitionData[] endingDatas;
    }
    [System.Serializable]
    public class SafePositionSetter : ATTSerializableComponent
    {
        public Vector3 offset;
    }
}
