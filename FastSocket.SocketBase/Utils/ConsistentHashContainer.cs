using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FastSocket.SocketBase.Utils
{
    public sealed class ConsistentHashContainer<T>
    {
        private readonly Dictionary<uint, T> _dic = new Dictionary<uint, T>();
        private readonly T[] _arr = null;
        private readonly uint[] _keys = null;

        public ConsistentHashContainer(IDictionary<string,T> source)
        {
            if (source == null) throw new ArgumentNullException("source");
            var servers = new List<T>();
            var keys = new List<uint>();

            foreach (var child in source)
            {
                for (int i = 0; i < 250; i++)
                {
                    uint key = BitConverter.ToUInt32(
                        new ModifiedFNV1_32().ComputeHash(Encoding.ASCII.GetBytes(child.Key + "-" + i)), 0);
                    if (!this._dic.ContainsKey(key))
                    {
                        this._dic[key] = child.Value;
                        keys.Add((key));
                    }
                }
                servers.Add(child.Value);
            }

            this._arr = servers.ToArray();
            keys.Sort();
            this._keys = keys.ToArray();
        }

        #region Private Methods

        private T Get(uint consistentKey)
        {
            int i = Array.BinarySearch(this._keys, consistentKey);
            if (i < 0)
            {
                i = ~i;
                if (i >= this._keys.Length) i = 0;
            }

            return this._dic[this._keys[i]];
        }

        #endregion

        #region Public Methods

        public T Get(byte[] consistentKey)
        {
            if (this._arr.Length == 0) return default(T);
            if (this._arr.Length == 1) return this._arr[0];
            return Get(BitConverter.ToUInt32(new ModifiedFNV1_32().ComputeHash(consistentKey), 0));
        }

        #endregion
    }

    public class FNV1_32 : HashAlgorithm
    {
        private  static  readonly uint FNV_prime = 16777619;
        private static readonly uint offset_basis = 2166136261;

        protected uint hash;

        public FNV1_32()
        {
            HashSizeValue = 32;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            int length = ibStart + cbSize;
            for (int i = ibStart; i < length; i++) hash = (hash * FNV_prime) ^ array[i];
        }

        protected override byte[] HashFinal()
        {
            return BitConverter.GetBytes(hash);
        }

        public override void Initialize()
        {
            hash = offset_basis;
        }
    }

    public class ModifiedFNV1_32 : FNV1_32
    {
        protected override byte[] HashFinal()
        {
            hash += hash << 13;
            hash ^= hash >> 7;
            hash += hash << 3;
            hash ^= hash >> 17;
            hash += hash << 5;
            return BitConverter.GetBytes(hash);
        }
    }
}