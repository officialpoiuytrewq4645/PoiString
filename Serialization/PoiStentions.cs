using System;
using System.Collections.Generic;

namespace PoiString
{
    public static class PoiStentions
    {
        //public PoiString.AttTypes.Components.Color RGBToColor(string rgb)
        //{

        //}
        public static string GetAsATTString(AttTypes.NetworkPrefab prefab)
        {
            return UintArrayToString(StreamToAttUintArray(PoiWriter.SerializePrefab(prefab)));
        }
        public static uint HashComponentName(string Name)
        {
            int hash1 = 5381;
            int hash2 = hash1;

            int current;

            for (int i = 0; i < Name.Length; i++)
            {
                current = Name[i];

                if (i % 2 == 0)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ current;
                }
                else
                {
                    hash2 = ((hash2 << 5) + hash2) ^ current;
                }
            }
            return (uint)(hash1 + (hash2 * 1566083941));
        }
        public static string BoolArrayToString(this bool[] self)
        {
            string output = "";
            foreach (bool bit in self)
            {
                output += bit ? "1" : "0";
            }
            return output;
        }
        public static bool[] BinStringToBoolArray(string BinString)
        {
            bool[] stream = new bool[BinString.Length];
            for (int i = 0; i < BinString.Length; i++)
            {
                stream[i] = BinString[i] == '1';
            }
            return stream;
        }
        public static string UintToBoolString(uint value)
        {
            string output = "";
            foreach (byte bla in BitConverter.GetBytes(value))
            {
                output += GetByteAsBoolArray(bla).BoolArrayToString();
            }
            return output;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string GetBytesAsString(byte Byte)
        {
            string output = "";
            for (int i = 0; i < 8; i++)
            {
                output += ((Byte & (1 << i)) == 0 ? "0" : "1");
            }
            return output;
        }
        public static bool[] GetByteAsBoolArray(byte Byte)
        {
            bool[] output = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                output[i] = ((Byte & (1 << i)) != 0);
            }
            return output;
        }
        public static bool[] GetBytesAsBoolArray(byte[] Bytes)
        {
            bool[] output = new bool[8 * Bytes.Length];
            for (int j = 0; j < Bytes.Length; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    output[i + j * 8] = ((Bytes[j] & (1 << i)) != 0);
                }
            }
            return output;
        }
        public static byte BoolToByte(bool[] Bools)
        {
            if (Bools.Length > 8)
            {
                throw new System.Exception("Too many bits either use FourBytes or fix yo code poio");
            }

            byte OutByte = new byte();
            int bitIndex = 0;
            for (int i = 0; i < Bools.Length; i++)
            {
                if (Bools[i])
                {
                    OutByte |= (byte)(((byte)1) << bitIndex);
                }
                bitIndex++;
            }
            return OutByte;
        }
        public static byte[] FourBytes(bool[] Bools, uint StartIndex = 0)
        {
            byte[] OutBytes = new byte[4];
            //if (Bools.Length > 32)
            //{
            //    throw new System.Exception("Too many bits poio");
            //}
            for (int i = 0; i < 32; i += 8)
            {
                bool[] buffer = new bool[8];
                for (int j = 0; j < 8; j++)
                {
                    buffer[j] = Bools[31 - (i + j) + StartIndex];
                }
                OutBytes[(i + 1) / 8] = BoolToByte(buffer);
            }
            return OutBytes;
        }
        public static byte[] TwoBytes(bool[] Bools, uint StartIndex = 0)
        {
            byte[] OutBytes = new byte[2];
            //if (Bools.Length > 32)
            //{
            //    throw new System.Exception("Too many bits poio");
            //}
            for (int i = 0; i < 16; i += 8)
            {
                bool[] buffer = new bool[8];
                for (int j = 0; j < 8; j++)
                {
                    buffer[j] = Bools[15 - (i + j) + StartIndex];
                }
                OutBytes[(i + 1) / 8] = BoolToByte(buffer);
            }
            return OutBytes;
        }
        public static string UintArrayToString(uint[] uints)
        {
            string Output = "";
            foreach(uint value in uints)
            {
                Output += $"{value},";
            } 
            return Output;
        }
        
        public static uint[] StreamToAttUintArray(List<bool> stream)
        {
            uint[] ATTString = new uint[(int)Math.Ceiling((double)stream.Count / 32) + 2];
            ATTString[1] = (uint)Math.Ceiling((double)stream.Count / 8);

            if (stream.Count < stream.Count + (32 - stream.Count % 32))
            {
                int toadd = (32 - stream.Count % 32);
                for (int i = 0; i < toadd; i++)
                {
                    stream.Add(false);
                }

            }

            bool[] FinalStream = stream.ToArray();
            for (uint i = 0; i < stream.Count; i += 32)
            {
                ATTString[i / 32 + 2] = BitConverter.ToUInt32(PoiStentions.FourBytes(FinalStream, i), 0);
            }
            ATTString[0] = ATTString[2];
            return ATTString;
        }
    }
}