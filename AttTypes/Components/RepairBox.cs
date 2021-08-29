using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    public class RepairBox
    {
        public Item[] items;
        public bool isRepeatable;
        public bool isDone;
        public Vector3 spawnPosition;
		public Quaternion spawnRotation;
		public NetworkPrefab spawning;
		public uint staticHash;
		public bool canBeUndone;
		public uint spawningDataHash;
		
	}
	public class Item
	{
		public uint itemHash;
		public bool usePhysMat;
		public uint physMatHash;
		public uint cost;
		public bool isIgnoringMaterialOnSimpleServer;
	}
}
