using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    class NetworkRigidbody : ATTSerializableComponent
    {
        public float PosX;
        public float PosY;
        public float PosZ;

        public float RotX;
        public float RotY;
        public float RotZ;
        public float RotW;

        public bool isKinematic;
        public bool isServerSleeping;

        public float VelX;
        public float VelY;
        public float VelZ;

        public float AngVelX;
        public float AngVelY;
        public float AngVelZ;


    }
}