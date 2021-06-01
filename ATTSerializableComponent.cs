using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        }
        public void AnalyzeField(FieldInfo val, EthynReader reader, object target, ref uint remainingdata)
        {
            Type valtype = val.FieldType;
            if (valtype == typeof(uint) && val.Name == nameof(BitSize))
            {
                //do nuffin
            }
            else if (valtype == typeof(uint))
            {
                val.SetValue(target, reader.ReadUint());
                remainingdata -= 32;
            }
            else if (valtype == typeof(float))
            {
                val.SetValue(target, reader.ReadFloat());
                remainingdata -= 32;
            }
            else if (valtype == typeof(bool))
            {
                val.SetValue(target, reader.ReadBool());
                remainingdata -= 1;
            }
            else if (valtype == typeof(int))
            {
                val.SetValue(target, reader.ReadInt());
                remainingdata -= 32;
            }
            else if (valtype == typeof(bool[]) && val.Name == "data")
            {
                val.SetValue(target, reader.Read(remainingdata));
            }
            else if (valtype == typeof(string))
            {
                string value = reader.ReadString();
                val.SetValue(target, value);
                remainingdata -= (uint)((value.Length * 8) + 16);
            }
            else if (valtype == typeof(TimeSpan))
            {
                val.SetValue(target, reader.ReadTimespan());

                remainingdata -= 32;
            }
            else if (valtype.IsArray && valtype.Namespace == valtype.Namespace)
            {
                val.SetValue(target, ReadArray(valtype.GetElementType(), reader, ref remainingdata));
            }
            else if (valtype.Namespace == valtype.Namespace)
            {
                val.SetValue(target, ReadCustomType(valtype, reader, ref remainingdata));
            }
            else
            {
                throw new Exception($"no deserializer found for type {valtype.Name}");
            }
        }
        public object[] ReadArray(Type type, EthynReader reader, ref uint remainingdata)
        {
            uint size = reader.ReadUint();
            remainingdata -= 32;
            object[] data = (object[])Array.CreateInstance(type, size);

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
            object data = Activator.CreateInstance(type);
            foreach (FieldInfo val in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                AnalyzeField(val, reader, data, ref remainingdata);
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