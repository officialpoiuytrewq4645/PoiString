using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
	[System.Serializable]
	public class RepairBox : ATTSerializableComponent
    {
        public Item[] items;
        public bool isRepeatable;
        public bool isDone;
        public Vector3 spawnPosition;
		public Quaternion spawnRotation;
		public uint spawning;
		public uint staticHash;
		public bool canBeUndone;
		public uint spawningDataHash;

	}
	[System.Serializable]
	public class Item
	{
		public uint itemHash;
		public bool usePhysMat;
		public uint physMatHash;
		public uint cost;
		public bool isIgnoringMaterialOnSimpleServer;
	}
}
