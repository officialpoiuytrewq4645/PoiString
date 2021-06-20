using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    public class Leaderboard : ATTSerializableComponent
    {
        public LeaderboardDefinitionData definition;
    }
    public class LeaderboardDefinitionData
    {
        public uint referencehash;
        public uint hash;
        public string coursename;
        public int size;
        public bool islowest;
    }
    public class CheckPoint : ATTSerializableComponent
    {
        public LeaderboardDefinitionData[] beginningDatas;

        public LeaderboardDefinitionData[] endingDatas;
    }
    public class SafePositionSetter : ATTSerializableComponent
    {
        public Vector3 offset;
    }
}
