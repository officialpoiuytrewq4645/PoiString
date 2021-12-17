using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoiString.AttTypes.Components
{
    [System.Serializable]
    public class LogicBoolSender : ATTSerializableComponent
    {
        public uint UpdatedValueWEIRDOTHING;
        public uint ChangedExternallyWEIRDOTHING;
        public uint identifier;
        public bool value;
    }
    [System.Serializable]
    public class LogicFloatSender : ATTSerializableComponent
    {
        public uint UpdatedValueWEIRDOTHING;
        public uint ChangedExternallyWEIRDOTHING;
        public uint identifier;
        public float value;
    }
    [System.Serializable]
    public class LogicIntSender : ATTSerializableComponent
    {
        public uint UpdatedValueWEIRDOTHING;
        public uint ChangedExternallyWEIRDOTHING;
        public uint identifier;
        public int value;
    }
    [System.Serializable]
    public class LogicVector3Sender : ATTSerializableComponent
    {
        public uint UpdatedValueWEIRDOTHING;
        public uint ChangedExternallyWEIRDOTHING;
        public uint identifier;
        public Vector3 value;
    }
    
    //public class LogicSender : ATTSerializableComponent
    //{
    //    public uint UpdatedValueWEIRDOTHING;
    //    public uint ChangedExternallyWEIRDOTHING;
    //    public uint identifier;
    //}
}
