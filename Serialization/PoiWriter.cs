using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using PoiString.AttTypes;
using PoiString.AttTypes.Components;

namespace PoiString
{
    public static class PoiWriter
    {
        public static List<bool> SerializePrefab(NetworkPrefab prefab)
        {
            //List<FieldInfo> fields = new List<FieldInfo>();
            List<bool> stream = new List<bool>();


            foreach (FieldInfo val in prefab.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                //fields.Add(val);
                Type valtype = val.FieldType;
                if (valtype == typeof(uint))
                {
                    SerializeUInt(stream, (uint)val.GetValue(prefab));
                }
                else if (valtype == typeof(float))
                {
                    SerializeFloat(stream, (float)val.GetValue(prefab));
                }
                else if (valtype == typeof(List<ATTSerializableComponent>))
                {
                    SerializeComponents(stream, (List<ATTSerializableComponent>)val.GetValue(prefab));
                }
                else if (valtype == typeof(List<ATTEmbededEntity>))
                {
                    SerializeEmbededEntities(stream, (List<ATTEmbededEntity>)val.GetValue(prefab));
                }
                else if (valtype == typeof(List<ChildNetworkPrefab>))
                {
                    SerializeChildPrefabs(stream, (List<ChildNetworkPrefab>)val.GetValue(prefab));
                }
                else if (valtype == typeof(string) && val.Name == "UnprocessedData")
                {
                    stream.AddRange(PoiStentions.BinStringToBoolArray((string)val.GetValue(prefab)));
                    // val.SetValue(this, reader.Read(remainingdata).BoolArrayToString());
                }
                else
                {
                    throw new Exception($"no serializer found for type {valtype.Name}");
                }


            }

            //foreach (ATTSerializableComponent component in prefab.Components)
            //{
            //    fields.AddRange(component.GetSerializableFields());
            //}

            return stream;
        }

        private static void SerializeChildPrefabs(List<bool> stream, List<ChildNetworkPrefab> ToSerialize)
        {
            foreach (ChildNetworkPrefab child in ToSerialize)
            {
                //exists bit
                SerializeBool(stream, true);
                SerializeUInt(stream, child.parentHash);
                stream.AddRange(SerializePrefab(child.Prefab));
            }
            //i assume this belongs here
            SerializeBool(stream, false);
        }

        private static void SerializeEmbededEntities(List<bool> stream, List<ATTEmbededEntity> ToSerialize)
        {
            foreach (ATTEmbededEntity entity in ToSerialize)
            {
                SerializeUInt(stream, entity.hash);
                SerializeUInt(stream, entity.size);
                SerializeBool(stream, entity.isAlive);
                SerializeComponents(stream, entity.Components);
            }
            //end of Entities identifier
            SerializeUInt(stream, 0);
        }

        private unsafe static void SerializeComponents(List<bool> stream, List<ATTSerializableComponent> ToSerialize)
        {
            if (ToSerialize == null)
            {
                //end of components identifier
                SerializeUInt(stream, 0);
                return;
            }
            foreach (ATTSerializableComponent component in ToSerialize)
            {
                uint ComponentSize = 0;
                List<bool> substream = new List<bool>();
                if (component is FallbackSerializedType)
                {
                    SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[(component as FallbackSerializedType).name]);
                }
                else
                {
                    string name = component.GetType().Name;
                    SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[name]);
                }

                foreach (FieldInfo val in component.GetSerializableFields())
                {
                    SerializeField(substream, val, component, ref ComponentSize);
                }
                SerializeUInt(stream, ComponentSize);
                stream.AddRange(substream);
            }
            //end of components identifier
            SerializeUInt(stream, 0);
        }
        private static void SerializeField(List<bool> stream, FieldInfo val, object component, ref uint ComponentSize)
        {
            Type valtype = val.FieldType;
            if (valtype == typeof(uint))
            {
                SerializeUInt(stream, (uint)val.GetValue(component));
                ComponentSize += 32;
            }
            else if (valtype == typeof(float))
            {
                SerializeFloat(stream, (float)val.GetValue(component));
                ComponentSize += 32;
            }
            else if (valtype == typeof(int))
            {
                SerializeInt(stream, (int)val.GetValue(component));
                ComponentSize += 32;
            }
            else if (valtype == typeof(bool))
            {
                SerializeBool(stream, (bool)val.GetValue(component));
                ComponentSize += 1;
            }
            else if (valtype == typeof(bool[]) && val.Name == "data")
            {
                //val.SetValue(this, reader.Read(remainingdata).BoolArrayToString());
                stream.AddRange((bool[])val.GetValue(component));
                ComponentSize += (uint)((bool[])val.GetValue(component)).Length;
            }
            else if (valtype == typeof(string) && val.Name == "name" && component is FallbackSerializedType)
            {
                //ignore
            }
            //else if (valtype == typeof(string))
            //{
            //    ComponentSize += SerializeString(substream, (string)val.GetValue(component));

            //}
            else if (valtype == typeof(TimeSpan))
            {
                SerializeTimeSpan(stream, (TimeSpan)val.GetValue(component));
                ComponentSize += 64;
            }
            else if (valtype.IsArray && valtype.Namespace == valtype.Namespace)
            {
                SerializeArray(stream, valtype, (object[])val.GetValue(component), ref ComponentSize);  //valtype.GetElementType(), reader, ref remainingdata));
            }
            else if (valtype.Namespace == valtype.Namespace)
            {
                SerializeCustomType(stream, valtype, (object)val.GetValue(component), ref ComponentSize);
            }
            else
            {
                throw new Exception($"no serializer found for type {valtype.Name}");
            }
        }
        private static void SerializeArray(List<bool> stream, Type type, object[] ToSerialize, ref uint componentSize)
        {
            //array length
            SerializeUInt(stream, (uint)ToSerialize.Length);
            componentSize += 32;
            foreach (object item in ToSerialize)
            {
                SerializeCustomType(stream, type.GetElementType(), item, ref componentSize);
            }
        }
        private static void SerializeCustomType(List<bool> stream, Type type, object ToSerialize, ref uint componentSize)
        {
            bool HasNullCheck = type.IsClass && Attribute.GetCustomAttribute(type, typeof(ClassIgnoresCustomClassRules)) == null;
            if (HasNullCheck)
            {
                SerializeBool(stream, ToSerialize == null);
                componentSize += 1;
                if (ToSerialize == null)
                {
                    return;
                }
                foreach (FieldInfo val in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    SerializeField(stream, val, ToSerialize, ref componentSize);
                }
            }
        }



        private unsafe static void SerializeTimeSpan(List<bool> substream, TimeSpan timeSpan)
        {
            SerializeUInt64(substream, *(ulong*)&timeSpan);
        }

        private static uint SerializeString(List<bool> stream, string ToSerialize)
        {
            uint ComponentLength = 0;
            //length
            SerializeUInt16(stream, (ushort)ToSerialize.Length);
            ComponentLength += 16;

            //uint length = ReadUint16();


            while (stream.Count % 8 != 0)
            {
                stream.Add(false);
            }

            ////this may be needed
            uint wordpos = (uint)(stream.Count % 32) / 8;


            //for (int i = 0; i < length; i++)
            //{
            //    Output += ReadChar();
            //}
            string correctedOutput = "";

            int j = 0;
            //finish current word
            for (; wordpos < 4; j++, wordpos++)
            {
                correctedOutput += ToSerialize[j];
            }
            //serialize in reverse order
            for (; j + 4 <= ToSerialize.Length; j += 4)
            {
                correctedOutput += ToSerialize[j + 3];
                correctedOutput += ToSerialize[j + 2];
                correctedOutput += ToSerialize[j + 1];
                correctedOutput += ToSerialize[j];
            }
            //this could be done better
            if (j != ToSerialize.Length)
            {
                switch (ToSerialize.Length - j)
                {
                    case 3:
                    {
                        correctedOutput += ToSerialize[j + 2];
                        correctedOutput += ToSerialize[j + 1];
                        correctedOutput += ToSerialize[j];
                        break;
                    }
                    case 2:
                    {
                        correctedOutput += ToSerialize[j + 1];
                        correctedOutput += ToSerialize[j];
                        break;
                    }
                    case 1:
                    {
                        correctedOutput += ToSerialize[j];
                        break;
                    }
                }
            }

            foreach (char letter in correctedOutput)
            {
                stream.AddRange(PoiStentions.GetByteAsBoolArray(BitConverter.GetBytes(letter)[0]).Reverse().ToArray());
                ComponentLength += 8;
            }
            return ComponentLength;
        }

        private static void SerializeFloat(List<bool> stream, float ToSerialize)
        {
            stream.AddRange(PoiStentions.GetBytesAsBoolArray(BitConverter.GetBytes(ToSerialize)).Reverse().ToArray());
        }
        private static void SerializeUInt(List<bool> stream, uint ToSerialize)
        {
            stream.AddRange(PoiStentions.GetBytesAsBoolArray(BitConverter.GetBytes(ToSerialize)).Reverse().ToArray());
        }
        private static void SerializeUInt16(List<bool> stream, ushort ToSerialize)
        {
            stream.AddRange(PoiStentions.GetBytesAsBoolArray(BitConverter.GetBytes(ToSerialize)).Reverse().ToArray());
        }
        private static unsafe void SerializeUInt64(List<bool> stream, ulong ToSerialize)
        {
            byte[] bytes = BitConverter.GetBytes(ToSerialize);
            byte[] newbytes = new byte[8];
            newbytes[0] = bytes[4];
            newbytes[1] = bytes[5];
            newbytes[2] = bytes[6];
            newbytes[3] = bytes[7];
            newbytes[4] = bytes[0];
            newbytes[5] = bytes[1];
            newbytes[6] = bytes[2];
            newbytes[7] = bytes[3];

            stream.AddRange(PoiStentions.GetBytesAsBoolArray(newbytes).Reverse().ToArray());
        }
        private static void SerializeInt(List<bool> stream, int ToSerialize)
        {

            //bool IsPositive = true;
            ////if negative un fuk
            //if (ToSerialize < 0)
            //{
            //    IsPositive = false;
            //    ToSerialize *= -1;
            //}


            bool[] bits = PoiStentions.GetBytesAsBoolArray(BitConverter.GetBytes(ToSerialize)).Reverse().ToArray();
            bits[0] = !bits[0];

            stream.AddRange(bits);

            //stream.AddRange(PoiStentions.GetBytesAsBoolArray(BitConverter.GetBytes(ToSerialize)).Reverse().ToArray());
        }
        private static void SerializeBool(List<bool> stream, bool ToSerialize)
        {
            stream.Add(ToSerialize);
        }
    }
}
