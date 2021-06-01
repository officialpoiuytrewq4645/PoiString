using System;
using System.Reflection;
using System.Collections.Generic;
using PoiString.AttTypes.Components;
using PoiString.AttTypes.Components.ValueTypes;

namespace PoiString.Knowledge
{
    public class KnowledgeManager
    {
        public static Dictionary<Type, Dictionary<uint, string>> knowleg = new Dictionary<Type, Dictionary<uint, string>>();
        public static void InitializeStructure()
        {
            //    MethodInfo KnowledgeInitMethod = typeof(Knowledge).GetMethod(nameof(Knowledge.Init));
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(ATTSerializableComponent)))
                    {
                        KnownComponents.StructuredTypes.Add(type.Name, type);
                    }
                    //really outside the scope poi
                    //else if (typeof(IKnowledge).IsAssignableFrom(type) && type != typeof(IKnowledge))
                    //{
                    //    Dictionary<uint, string> CurrentType = new Dictionary<uint, string>();
                    //    knowleg.Add(type, CurrentType);
                    //    foreach (FieldInfo val in type.GetFields(BindingFlags.Static | BindingFlags.Public))
                    //    {
                    //        CurrentType.Add((uint)val.GetValue(null), val.Name);
                    //    }
                    //}
                }
            }
            foreach (KeyValuePair<uint, string> keyValuePair in KnownComponents.Knowledge)
            {
                KnownComponents.InverseKnowledge.Add(keyValuePair.Value, keyValuePair.Key);
            }
            //foreach (KeyValuePair<uint, string> keyValuePair in KnownMaterialHashes.AllMats)
            //{
            //    KnownMaterialHashes.InverseAllMats.Add(keyValuePair.Value, keyValuePair.Key);
            //}
        }

    }
    //public interface IKnowledge
    //{

    //}
    //public class Knowledge
    //{

    //}
}