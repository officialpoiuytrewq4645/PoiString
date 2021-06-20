using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    public class WoodcutPointCollection : ATTSerializableComponent
    {
        public Element[] elements;
    }
    public class Element
    {
        public float progress;
        public override string ToString()
        {
            return progress.ToString();
        }
    }
}
