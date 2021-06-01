using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PoiString.AttTypes;
using PoiString.AttTypes.Components;
using PoiString.Knowledge;

namespace PoiString
{
    static class PoiDecoder
    {

        public static NetworkPrefab GetPrefabFromString(string ATTString)
        {
            //ethyn's approach
            int ArrayPos = 0;

            string[] splitstring = ATTString.Split('|');

            string[] StringData = splitstring[0].Split(',');
            string[] Versioning = null;
            if (splitstring.Length > 1)
            {
                Versioning = splitstring[1].Split(',');
            }
            uint[] UintStringData = new uint[StringData.Length];

            Dictionary<uint, uint> VersioningDict = new Dictionary<uint, uint>();


            for (int i = 0; i < StringData.Length; i++)
            {
                if (StringData[i] == "")
                {
                    break;
                }
                UintStringData[i] = uint.Parse(StringData[i]);
            }

            uint PrefabHash = UintStringData[ArrayPos++];
            uint SizeOfStringInBits = 8 * UintStringData[ArrayPos++];

            bool[] Stream = new bool[SizeOfStringInBits];
            int StreamPos = 0;
            foreach (uint sectioniebitthing in UintStringData.Skip(ArrayPos))
            {
                foreach (byte Byte in BitConverter.GetBytes(sectioniebitthing))
                {
                    //handling for half bit uint things at end of stream
                    if (Stream.Length < StreamPos + 8)
                    {
                        break;
                    }
                    PoiStentions.GetByteAsBoolArray(Byte).CopyTo(Stream, StreamPos);
                    StreamPos += 8;
                }

            }
            if (Versioning != null)
            {
                int VersioningEntries = int.Parse(Versioning[0]) * 2;
                for (int i = 1; i < VersioningEntries; i += 2)
                {
                    VersioningDict.Add(uint.Parse(Versioning[i]), uint.Parse(Versioning[i + 1]));
                }
            }
            EthynReader reader = new EthynReader(Stream);


            return DecodePrefab(reader);
        }



        private static NetworkPrefab DecodePrefab(EthynReader reader)
        {
            NetworkPrefab Output = new NetworkPrefab();
            Output.PrefabHash = reader.ReadUint();
            Output.positonX = reader.ReadFloat();
            Output.positonY = reader.ReadFloat();
            Output.positonZ = reader.ReadFloat();
            Output.RotationX = reader.ReadFloat();
            Output.RotationY = reader.ReadFloat();
            Output.RotationZ = reader.ReadFloat();
            Output.RotationW = reader.ReadFloat();
            Output.Scale = reader.ReadFloat();
            //Output += "Component: " + reader.ReadUint() + "\n";
            //uint componentsize = reader.ReadUint();
            //Output += "ComponentSize: " + componentsize + "\n";


            //Output += "Data: " + reader.Read((int)componentsize).BoolArrayToString() + "\n";
            List<ATTSerializableComponent> components = DecodeComponent(reader);
            if (components != null)
            {
                Output.Components.AddRange(components);
            }
            List<ATTEmbededEntity> entities = DecodeEmbededEntities(reader);
            if (entities != null)
            {
                Output.EmbededEntities.AddRange(entities);
            }
            List<ChildNetworkPrefab> children = DecodeChildPrefab(reader);
            if (children != null)
            {
                Output.Children.AddRange(children);
            }


            //Output.UnprocessedData = reader.ReadToEnd().BoolArrayToString();
            //Output += DecodeComponent(reader);
            //Output += DecodeComponent(reader);


            return Output;
        }

        private static List<ChildNetworkPrefab> DecodeChildPrefab(EthynReader reader)
        {
            List<ChildNetworkPrefab> Output = new List<ChildNetworkPrefab>();

            bool ExistsBit = reader.ReadBool();

            if (ExistsBit)
            {
                ChildNetworkPrefab ThisChild = new ChildNetworkPrefab();

                ThisChild.parentHash = reader.ReadUint();
                ThisChild.Prefab = DecodePrefab(reader);

                Output.Add(ThisChild);

                List<ChildNetworkPrefab> Children = DecodeChildPrefab(reader);

                if (Children != null)
                {
                    Output.AddRange(Children);
                }

                return Output;
            }
            else
            {
                return null;
            }


        }

        private static List<ATTEmbededEntity> DecodeEmbededEntities(EthynReader reader)
        {
            List<ATTEmbededEntity> Output = new List<ATTEmbededEntity>();
            uint EntityHash = reader.ReadUint();
            if (EntityHash != 0)
            {
                ATTEmbededEntity entity = new ATTEmbededEntity();
                entity.hash = EntityHash;
                entity.size = reader.ReadUint();
                entity.isAlive = reader.ReadBool();

                if (!entity.isAlive)
                {
                    reader.Read(entity.size);
                }
                else
                {
                    entity.Components = DecodeComponent(reader);
                }

                Output.Add(entity);

                List<ATTEmbededEntity> OtherEntities = DecodeEmbededEntities(reader);

                if (OtherEntities != null)
                {
                    Output.AddRange(OtherEntities);
                }

                return Output;
            }
            else
            {
                return null;
            }
        }

        private static List<ATTSerializableComponent> DecodeComponent(EthynReader reader)
        {
            List<ATTSerializableComponent> Output = new List<ATTSerializableComponent>();
            uint componenthash = reader.ReadUint();
            if (componenthash != 0)
            {

                if (KnownComponents.Knowledge.TryGetValue(componenthash, out string ATTTypeName))
                {


                    if (KnownComponents.StructuredTypes.TryGetValue(ATTTypeName, out Type type))
                    {
                        ATTSerializableComponent Component = (Activator.CreateInstance(type) as ATTSerializableComponent);
                        Component.InitFromReader(reader);
                        Output.Add(Component);
                    }
                    else
                    {
                        Output.Add(new AttTypes.Components.FallbackSerializedType(reader, ATTTypeName));
                    }
                }
                else
                {
                    Output.Add(new AttTypes.Components.FallbackSerializedType(reader, componenthash.ToString()));
                }
                List<ATTSerializableComponent> OtherComponents = DecodeComponent(reader);
                if (OtherComponents != null)
                {
                    Output.AddRange(OtherComponents);
                }


                return Output;
            }
            else
            {
                //reader.SetBackPointer(32);
                return null;
            }




        }

    }



}
//public static string DecodePrefab(EthynReader reader)
//{
//    string Output = "";
//    Output += "Prefab Hash: " + reader.ReadUint() + "\n";
//    Output += "Pos X: " + reader.ReadFloat() + "\n";
//    Output += "Pos Y: " + reader.ReadFloat() + "\n";
//    Output += "Pos Z: " + reader.ReadFloat() + "\n";
//    Output += "Rot X: " + reader.ReadFloat() + "\n";
//    Output += "Rot Y: " + reader.ReadFloat() + "\n";
//    Output += "Rot Z: " + reader.ReadFloat() + "\n";
//    Output += "Rot W: " + reader.ReadFloat() + "\n";
//    Output += "Scale: " + reader.ReadFloat() + "\n";
//    //Output += "Component: " + reader.ReadUint() + "\n";
//    //uint componentsize = reader.ReadUint();
//    //Output += "ComponentSize: " + componentsize + "\n";


//    //Output += "Data: " + reader.Read((int)componentsize).BoolArrayToString() + "\n";

//    Output += DecodeComponent(reader);
//    //Output += DecodeComponent(reader);
//    //Output += DecodeComponent(reader);


//    return Output;
//}
//public static string DecodeComponent(EthynReader reader)
//{
//    string Output = "";
//    uint componenthash = reader.ReadUint();
//    if (componenthash != 0)
//    {

//        if (KnownComponents.Knowledge.TryGetValue(componenthash, out string ATTTypeName))
//        {

//            Output += "\nComponent: " + ATTTypeName + "\n";
//            if (KnownComponents.StructuredTypes.TryGetValue(ATTTypeName, out AttTypes.Components.ATTSerializableComponent type))
//            {
//                type.InitFromReader(reader);
//                Output += type.ReflectValues();
//            }
//            else
//            {
//                Output += new AttTypes.Components.FallbackSerializedType(reader).ReflectValues();
//            }
//        }
//        else
//        {
//            Output += new AttTypes.Components.FallbackSerializedType(reader).ReflectValues();
//        }
//        Output += DecodeComponent(reader);



//        //Output += "Component: " + reader.ReadUint() + "\n";
//        //componentsize = reader.ReadUint();
//        //Output += "ComponentSize: " + componentsize + "\n";

//        //Output += "Data: " + reader.Read((int)componentsize).BoolArrayToString() + "\n";
//    }


//    return Output;


//}