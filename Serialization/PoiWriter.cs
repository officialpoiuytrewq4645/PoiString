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
                    if (Knowledge.KnownComponents.InverseKnowledge.TryGetValue((component as FallbackSerializedType).FALLBACKNAMEAAA, out uint value))
                    {
                        SerializeUInt(stream, value);
                    }
                    else
                    {
                        SerializeUInt(stream, uint.Parse((component as FallbackSerializedType).FALLBACKNAMEAAA));
                    }
                    //SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[(component as FallbackSerializedType).name]);
                }
                else
                {
                    string name = component.GetType().Name;
                    SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[name]);
                }

                foreach (FieldInfo val in component.GetSerializableFields())
                {
                    SerializeField(substream, val, component, ref ComponentSize, stream.Count);
                }
                SerializeUInt(stream, ComponentSize);
                component.BitSize = ComponentSize;
                stream.AddRange(substream);
            }
            //end of components identifier
            SerializeUInt(stream, 0);
        }
        private static void SerializeField(List<bool> stream, FieldInfo val, object component, ref uint ComponentSize, int StringPos)
        {
            Type valtype = val.FieldType;
            SerializeValue(stream, val.Name, val.GetValue(component), valtype, ref ComponentSize, StringPos);
        }
        private static void SerializeValue(List<bool> stream, string fieldname, object value, Type type, ref uint ComponentSize, int StringPos)
        {

            if (type == typeof(uint))
            {
                SerializeUInt(stream, (uint)value);
                ComponentSize += 32;
            }
            else if (type == typeof(float))
            {
                SerializeFloat(stream, (float)value);
                ComponentSize += 32;
            }
            else if (type == typeof(int))
            {
                SerializeInt(stream, (int)value);
                ComponentSize += 32;
            }
            else if (type == typeof(bool))
            {
                SerializeBool(stream, (bool)value);
                ComponentSize += 1;
            }
            else if (type == typeof(bool[]) && fieldname == "data")
            {
                //val.SetValue(this, reader.Read(remainingdata).BoolArrayToString());
                stream.AddRange((bool[])value);
                ComponentSize += (uint)((bool[])value).Length;
            }
            else if (type == typeof(string) && fieldname == "FALLBACKNAMEAAA")
            {
                //ignore
            }
            else if (type == typeof(string))
            {
                ComponentSize += SerializeString(stream, (string)value, StringPos);

            }
            else if (type == typeof(TimeSpan))
            {
                SerializeTimeSpan(stream, (TimeSpan)value);
                ComponentSize += 64;
            }
            else if (type == typeof(List<IATTSerializable>))
            {
                SerializeBsery(stream, type, (List<IATTSerializable>)value, ref ComponentSize, StringPos);
            }
            else if (type.IsArray && type.Namespace == type.Namespace)
            {
                SerializeArray(stream, type, (System.Collections.IList)value, ref ComponentSize, StringPos);  //valtype.GetElementType(), reader, ref remainingdata));
            }
            else if (type.Namespace == type.Namespace)
            {
                SerializeCustomType(stream, type, (object)value, ref ComponentSize, StringPos);
            }
            else
            {
                throw new Exception($"no serializer found for type {type.Name}");
            }
        }
        private static void SerializeBsery(List<bool> stream, Type type, List<IATTSerializable> ToSerialize, ref uint componentSize, int StringPos)
        {
            SerializeInt(stream, ToSerialize.Count);
            componentSize += 32;

            for (int i = 0; i < ToSerialize.Count; i++)
            {
                uint ComponentSize = 0;
                List<bool> substream = new List<bool>();
                if (ToSerialize[i] is FallbackSerializedType)
                {
                    if (Knowledge.KnownComponents.InverseKnowledge.TryGetValue((ToSerialize[i] as FallbackSerializedType).FALLBACKNAMEAAA, out uint value))
                    {
                        SerializeUInt(stream, value);
                        componentSize += 32;
                    }
                    else
                    {
                        SerializeUInt(stream, uint.Parse((ToSerialize[i] as FallbackSerializedType).FALLBACKNAMEAAA));
                        componentSize += 32;
                    }
                    //SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[(component as FallbackSerializedType).name]);
                }
                else
                {
                    string name = ToSerialize[i].GetType().Name;
                    SerializeUInt(stream, Knowledge.KnownComponents.InverseKnowledge[name]);
                    componentSize += 32;
                }

                foreach (FieldInfo val in ToSerialize[i].GetSerializableFields())
                {
                    SerializeField(substream, val, ToSerialize[i], ref ComponentSize, stream.Count);
                }
                SerializeUInt(stream, ComponentSize);
                componentSize += 32;
                (ToSerialize[i] as ATTSerializableComponent).BitSize = ComponentSize;
                stream.AddRange(substream);

                componentSize += ComponentSize;
            }
        }
        private static void SerializeArray(List<bool> stream, Type type, System.Collections.IList ToSerialize, ref uint componentSize, int StringPos)
        {
            //array length
            uint length = 0;
            if (ToSerialize != null)
            {
                length = (uint)ToSerialize.Count;
            }
            SerializeUInt(stream, length);
            componentSize += 32;
            if (ToSerialize.Count > 0)
            {
                foreach (object item in ToSerialize)
                {
                    SerializeCustomType(stream, type.GetElementType(), item, ref componentSize, StringPos);
                }
            }
        }
        private static void SerializeCustomType(List<bool> stream, Type type, object ToSerialize, ref uint componentSize, int StringPos)
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
                    SerializeField(stream, val, ToSerialize, ref componentSize, StringPos);
                }
            }
            else
            {
                //if you dont include a unity field
                if (ToSerialize == null)
                {
                    ToSerialize = Activator.CreateInstance(type);
                }
                if (type.IsClass)
                {
                    foreach (FieldInfo val in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                    {
                        SerializeField(stream, val, ToSerialize, ref componentSize, StringPos);
                    }
                }
                else
                {
                    SerializeValue(stream, "", ToSerialize, type, ref componentSize, StringPos);
                }

            }


        }



        private unsafe static void SerializeTimeSpan(List<bool> substream, TimeSpan timeSpan)
        {
            SerializeUInt64(substream, *(ulong*)&timeSpan);
        }

        private static uint SerializeString(List<bool> stream, string ToSerialize, int StringPos)
        {
            uint ComponentLength = 0;
            //length
            SerializeUInt16(stream, (ushort)ToSerialize.Length);
            ComponentLength += 16;

            //uint length = ReadUint16();

            if (ToSerialize.Length == 0)
            {
                return ComponentLength;
            }
            //align
            while ((StringPos + stream.Count) % 8 != 0)
            {
                stream.Add(false);
                ComponentLength += 1;
            }

            ////this may be needed
            uint wordpos = (uint)((StringPos + stream.Count) % 32) / 8;

            //for (int i = 0; i < length; i++)
            //{
            //    Output += ReadChar();
            //}
            string correctedOutput = "";

            int j = 0;
            //finish current word
            for (; wordpos < 4; j++, wordpos++)
            {
                if (ToSerialize.Length > j)
                {
                    correctedOutput += ToSerialize[j];
                }
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
            //i think its better now
            if (j != ToSerialize.Length)
            {

                for (; j < ToSerialize.Length; j++)
                {
                    correctedOutput += ToSerialize[j];
                }
                //switch (ToSerialize.Length - j)
                //{

                //    case 3:
                //    {
                //        correctedOutput += ToSerialize[j + 2];
                //        correctedOutput += ToSerialize[j + 1];
                //        correctedOutput += ToSerialize[j];
                //        break;
                //    }
                //    case 2:
                //    {
                //        correctedOutput += ToSerialize[j + 1];
                //        correctedOutput += ToSerialize[j];
                //        break;
                //    }
                //    case 1:
                //    {
                //        correctedOutput += ToSerialize[j];
                //        break;
                //    }
                //}
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
