using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    [ClassIgnoresCustomClassRules]
    public class Color
    {
        public float r;
        public float g;
        public float b;
        public float a;
    }
    [System.Serializable]
    [ClassIgnoresCustomClassRules]
    public class Vector2
    {
        public float x;
        public float y;
    }
    [System.Serializable]
    [ClassIgnoresCustomClassRules]
    public class Vector3
    {
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    [ClassIgnoresCustomClassRules]
    public class Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

}
