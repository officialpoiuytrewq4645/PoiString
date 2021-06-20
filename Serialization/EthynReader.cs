using System;
using System.Collections.Generic;

namespace PoiString
{
    public class EthynReader
    {
        private bool[] _stream;
        public uint _position { get; private set; }
        public EthynReader(bool[] stream)
        {
            ////_stream = stream;
            _stream = new bool[stream.Length + (32 - stream.Length % 32)];

            if (stream.Length < _stream.Length)
            {
                bool[] newstream = new bool[_stream.Length];
                for (int i = 0; i < stream.Length; i++)
                {
                    newstream[i] = stream[i];
                }
                stream = newstream;
            }

            for (int i = 0; i < stream.Length; i += 32)
            {
                for (int j = 0; j < 32; j++)
                {
                    _stream[i + j] = stream[i + (31 - j)];
                }

            }

        }
        public bool[] GetStream()
        {
            return _stream;
        }
        public string StreamAsString()
        {
            return _stream.BoolArrayToString();
        }

        //public static byte[] FourBytesTEST(bool[] Bools)
        //{
        //    byte[] OutBytes = new byte[4];
        //    if (Bools.Length > 32)
        //    {
        //        throw new System.Exception("Too many bits poio");
        //    }
        //    for (int i = 0; i < 32; i += 8)
        //    {
        //        bool[] buffer = new bool[8];
        //        for (int j = 0; j < 8; j++)
        //        {
        //            buffer[j] = Bools[i + j];
        //        }
        //        OutBytes[(i + 1) / 8] = BoolToByte(buffer);
        //    }
        //    return OutBytes;
        //}
        public bool[] Read(uint Amount)
        {
            bool[] Output = new bool[Amount];
            for (uint i = _position; i < _position + Amount; i++)
            {
                Output[i - _position] = _stream[i];
            }
            _position += Amount;
            return Output;
        }
        public bool[] ReadToEnd()
        {
            return Read((uint)_stream.Length - _position);
        }
        public uint ReadUint()
        {
            return BitConverter.ToUInt32(PoiStentions.FourBytes(Read(32)), 0);

        }
        public uint ReadUint16()
        {
            return BitConverter.ToUInt16(PoiStentions.TwoBytes(Read(16)), 0);

        }
        public void SetBackPointer(uint distance)
        {
            _position -= distance;
        }
        public unsafe TimeSpan ReadTimespan()
        {
            ulong value = ReadUlong();
            return *(TimeSpan*)&value;
        }
        public unsafe ulong ReadUlong()
        {
            List<byte> eightbytes = new List<byte>();
            eightbytes.AddRange(PoiStentions.FourBytes(Read(32), 0));
            eightbytes.AddRange(PoiStentions.FourBytes(Read(32), 0));
            byte[] value = eightbytes.ToArray();
            fixed (byte* ptr = &value[0])
            {
                byte* ptr2 = ptr;
                ulong test = *(ulong*)ptr2;
                return *(ulong*)ptr2;
            }
        }
        public int ReadInt()
        {

            bool[] bits = Read(32);
            Console.WriteLine(bits.BoolArrayToString());
            //bool IsPositive = bits[0];
            //bits[0] = !bits[0];
            //bits[0] = false;

            byte[] bytes = PoiStentions.FourBytes(bits);
            bytes[3] = (byte)(bytes[3] ^ (1 << 7));
            //Console.WriteLine(PoiStentions.GetBytesAsBoolArray(bytes).BoolArrayToString());
            int Output = BitConverter.ToInt32(bytes, 0);
            //if (!IsPositive)
            //{
            //    Output *= -1;
            //}
            return Output;

        }

        public string ReadString()
        {
            string Output = "";

            uint length = ReadUint16();

            if(length == 0)
            {
                return "";
            }
            
            while (_position % 8 != 0)
            {
                _position++;
            }

            //todo this may be needed
            uint wordpos = (_position % 32) / 8;

            for (int i = 0; i < length; i++)
            {
                Output += ReadChar();
            }
            string correctedOutput = "";
            //this is working of an assumption that the first two bytes are in order because thats what i observe
            int j = 0;
            for (; wordpos < 4; j++, wordpos++)
            {
                correctedOutput += Output[j];
            }
            for (; j + 4 <= Output.Length; j += 4)
            {
                correctedOutput += Output[j + 3];
                correctedOutput += Output[j + 2];
                correctedOutput += Output[j + 1];
                correctedOutput += Output[j];
            }
            if (j != Output.Length)
            {
                switch (Output.Length - j)
                {
                    case 3:
                    {
                        correctedOutput += Output[j + 2];
                        correctedOutput += Output[j + 1];
                        correctedOutput += Output[j];
                        break;
                    }
                    case 2:
                    {
                        correctedOutput += Output[j + 1];
                        correctedOutput += Output[j];
                        break;
                    }
                    case 1:
                    {
                        correctedOutput += Output[j];
                        break;
                    }
                }
            }

            return correctedOutput;
        }
        private unsafe char ReadChar()
        {
            bool[] bits = Read(8);
            bool[] reversebits = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                reversebits[i] = bits[7 - i];
            }
            return BitConverter.ToChar(new byte[] { PoiStentions.BoolToByte(reversebits), new byte() }, 0);
        }
        public float ReadFloat()
        {
            return BitConverter.ToSingle(PoiStentions.FourBytes(Read(32)), 0);
        }
        public bool ReadBool()
        {
            return Read(1)[0];
        }

        //public void ReadBytes(byte[] bytes, int byteCount)
        //{
        //    int num = (4 - this.bitIndex / 8) % 4;
        //    if (num > byteCount)
        //    {
        //        num = byteCount;
        //    }
        //    for (int i = 0; i < num; i++)
        //    {
        //        bytes[i] = (byte)this.ReadBits(8);
        //    }
        //    if (num == byteCount)
        //    {
        //        return;
        //    }
        //    int num2 = (byteCount - num) / 4;
        //    if (num2 > 0)
        //    {
        //        Buffer.BlockCopy(this.data, (int)(this.wordIndex * 4U), bytes, num, num2 * 4);
        //        this.wordIndex += (uint)num2;
        //        this.bitsRead += (uint)(num2 * 32);
        //        this.scratch = 0UL;
        //        this.LoadData();
        //    }
        //    int num3 = num + num2 * 4;
        //    int num4 = byteCount - num3;
        //    for (int j = 0; j < num4; j++)
        //    {
        //        bytes[num3 + j] = (byte)this.ReadBits(8);
        //    }
        //}
    }
}