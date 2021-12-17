using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using PoiString.Knowledge;

namespace PoiString.AttTypes.Components
{
    
    public class ATTSerializableComponent : IATTSerializable
    {
        public uint BitSize;

        public FieldInfo[] GetSerializableFields()
        {
            List<FieldInfo> output = new List<FieldInfo>();

            if (this is IHasSpecialOperation)
            {
                (this as IHasSpecialOperation).ReverseSpecialOperation();
            }

            foreach (FieldInfo val in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {

                if (val.FieldType == typeof(uint) && val.Name == nameof(BitSize))
                {
                    //do nuffin
                }
                else
                {
                    output.Add(val);
                }
            }


            return output.ToArray();
        }


        public string ReflectValues()
        {
            string output = "";
            output += nameof(BitSize) + ": " + BitSize + "\n";
            if (this is IHasSpecialOperation)
            {
                foreach (FieldInfo val in this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (val.FieldType == typeof(uint) && val.Name == nameof(BitSize))
                    {
                        //do nuffin
                    }
                    else
                    {
                        object field = val.GetValue(this);
                        output += val.Name + ": " + field.ToString() + "\n";
                    }
                }
            }
            else
            {
                foreach (FieldInfo val in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (val.FieldType == typeof(uint) && val.Name == nameof(BitSize))
                    {
                        //do nuffin
                    }
                    else
                    {
                        object field = val.GetValue(this);
                        output += val.Name + ": " + field.ToString() + "\n";
                    }
                }
            }

            return output;
        }
        public void InitFromReader(EthynReader reader)
        {
            BitSize = reader.ReadUint();
            uint remainingdata = BitSize;
            foreach (FieldInfo val in this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                AnalyzeField(val, reader, this, ref remainingdata);

                //val.SetValue()
                //output += val.Name + ": " + proeprty.ToString();
            }
            if (this is IHasSpecialOperation)
            {
                (this as IHasSpecialOperation).PerformSpecialOperation();
            }
            BitSize -= remainingdata;
        }
        public void AnalyzeField(FieldInfo val, EthynReader reader, object target, ref uint remainingdata)
        {
            Type valtype = val.FieldType;
            if (valtype == typeof(uint) && val.Name == nameof(BitSize))
            {
                //do nuffin
            }
            else
            {
                val.SetValue(target, AnalyzeValue(valtype, val.Name, reader, ref remainingdata));
            }
        }
        public object AnalyzeValue(Type type, string isdata, EthynReader reader, ref uint remainingdata)
        {

            if (type == typeof(uint))
            {
                remainingdata -= 32;
                return reader.ReadUint();
            }
            else if (type == typeof(float))
            {
                remainingdata -= 32;
                return reader.ReadFloat();
            }
            else if (type == typeof(bool))
            {
                remainingdata -= 1;
                return reader.ReadBool();
            }
            else if (type == typeof(int))
            {
                remainingdata -= 32;
                return reader.ReadInt();
            }
            else if (type == typeof(bool[]) && isdata == "data")
            {
                return reader.Read(remainingdata);
            }
            else if (type == typeof(string))
            {
                string value = reader.ReadString();
                remainingdata -= (uint)((value.Length * 8) + 16);
                return value;
            }
            else if (type == typeof(TimeSpan))
            {
                remainingdata -= 32;
                return reader.ReadTimespan();
            }
            else if (type == typeof(List<IATTSerializable>))
            {
                return ReadBsery(type, reader, ref remainingdata);
            }
            else if (type.IsArray /*&& type.Namespace == type.Namespace*/)
            {
                return ReadArray(type.GetElementType(), reader, ref remainingdata);
            }
            else if (true/*type.Namespace == type.Namespace*/)
            {
                return ReadCustomType(type, reader, ref remainingdata);
            }
            //else
            //{
            //    throw new Exception($"no deserializer found for type {type.Name}");
            //}
        }

        private object ReadBsery(Type type, EthynReader reader, ref uint remainingdata)
        {
            remainingdata -= 32;
            int num = reader.ReadInt();
            List<IATTSerializable> Output = new List<IATTSerializable>();
            for (int i = 0; i < num; i++)
            {
                uint test = reader.ReadUint();
                if (KnownComponents.Knowledge.TryGetValue(test, out string ATTTypeName))
                {


                    if (KnownComponents.StructuredTypes.TryGetValue(ATTTypeName, out Type typer))
                    {
                        ATTSerializableComponent Component = (Activator.CreateInstance(typer) as ATTSerializableComponent);
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
                    Output.Add(new AttTypes.Components.FallbackSerializedType(reader, test.ToString()));
                }
            }
            return Output;
        }

        public System.Collections.IList ReadArray(Type type, EthynReader reader, ref uint remainingdata)
        {
            uint size = reader.ReadUint();
            remainingdata -= 32;
            System.Collections.IList data = (System.Collections.IList)Array.CreateInstance(type, size);

            for (int i = 0; i < size; i++)
            {

                data[i] = ReadCustomType(type, reader, ref remainingdata);

            }
            return data;
        }
        public object ReadCustomType(Type type, EthynReader reader, ref uint remainingdata)
        {
            bool HasNullCheck = type.IsClass && Attribute.GetCustomAttribute(type, typeof(ClassIgnoresCustomClassRules)) == null;// && !typeof(ATTSerializableComponent).IsAssignableFrom(type);
            if (HasNullCheck)
            {
                bool IsNull = reader.ReadBool();
                remainingdata -= 1;
                if (IsNull)
                {
                    return null;
                }
            }
            //if type isnt class do thing
            object data = Activator.CreateInstance(type);
            if (type.IsClass)
            {
                foreach (FieldInfo val in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
                {
                    AnalyzeField(val, reader, data, ref remainingdata);
                }
            }
            else
            {
                data = AnalyzeValue(type, "", reader, ref remainingdata);
            }
            return data;
        }

    }
    public interface IHasSpecialOperation
    {
        void PerformSpecialOperation();
        void ReverseSpecialOperation();
    }
    public interface IATTSerializable
    {
        FieldInfo[] GetSerializableFields();
    }
    public class ClassIgnoresCustomClassRules : Attribute
    {

    }
    public class Version : Attribute
    {
        public int ver;
        public Version(int ver)
        {
            this.ver = ver;
        }
    }

}