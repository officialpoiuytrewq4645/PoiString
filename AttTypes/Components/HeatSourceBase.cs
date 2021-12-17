using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
	[System.Serializable]
	public class HeatSourceBase : ATTSerializableComponent
    {
		public bool isLit;
		public float progress;
		public TimeSpan time;
	}
}
