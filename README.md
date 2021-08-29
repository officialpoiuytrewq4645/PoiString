# PoiString  
<b>THIS IS AN ADVANCED TOOL IN IT'S CURRENT STATE<b/>  
This is a tool for creation and modification of ATT Strings, a save format used by the vr game A Township Tale

Example for building a weapon
```
using PoiString.AttTypes;
using PoiString.AttTypes.Components.ValueTypes;
using PoiString.Knowledge;
  static void Main(string[] args)
        {
            KnowledgeManager.InitializeStructure();
            NetworkPrefab prefab = new NetworkPrefab()
            {
                PrefabHash = 42230,
                Children = new List<ChildNetworkPrefab>() {
                    new ChildNetworkPrefab() {
                        parentHash = 6136,
                        Prefab = new NetworkPrefab() {
                            PrefabHash = 9304,
                            Components = new List<AttTypes.Components.ATTSerializableComponent>() {
                                new AttTypes.Components.PhysicalMaterialPart(){Hash = PhysicalMaterial.Gold } },
                            Children = new List<ChildNetworkPrefab>() {
                                    new ChildNetworkPrefab() {
                                        parentHash = 51896,
                                        Prefab = new NetworkPrefab() {
                                            PrefabHash = 56124,
                                            Components = new List<AttTypes.Components.ATTSerializableComponent>() {
                                            new AttTypes.Components.PhysicalMaterialPart() { Hash = PhysicalMaterial.EvinonSteelAlloy },
                                                new AttTypes.Components.DurabilityModule() { integrity = .1f }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Console.WriteLine(PoiStentions.UintArrayToString(PoiStentions.StreamToAttUintArray(PoiWriter.SerializePrefab(prefab))));
            while (true)
            {

            }
        }
  ```
Example for deconstructing a string
```
using PoiString.AttTypes;
using PoiString.AttTypes.Components.ValueTypes;
using PoiString.Knowledge;
  static void Main(string[] args)
        {
            KnowledgeManager.InitializeStructure();
            NetworkPrefab prefab = PoiDecoder.GetPrefabFromString("STRING GOES HERE");
            Console.WriteLine(PoiStentions.UintArrayToString(PoiStentions.StreamToAttUintArray(PoiWriter.SerializePrefab(prefab))));
            while (true)
            {

            }
        }
```
