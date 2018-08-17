using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schedule_Program.lib
{
    public class HashMap<K, L> : List<L>
    {
        private List<K> key;
        private List<L> value;

        public HashMap()
        {
            key = new List<K>();
            value = new List<L>();
        }
        private void update()
        {
            this.Clear();
            this.AddRange(this.value);
        }
        public void Add(K key, L value)
        {
            this.key.Add(key);
            this.value.Add(value);
            update();
        }
        public void Remove(K key)
        {
            int index = this.key.IndexOf(key);
            this.key.Remove(key);
            this.value.RemoveAt(index);
            update();
        }
        public void Remove(L value)
        {
            int index = this.value.IndexOf(value);
            this.value.Remove(value);
            this.key.RemoveAt(index);
            update();
        }
        public void Replace(K key, L value, int index)
        {
            this.key.RemoveAt(index);
            this.value.RemoveAt(index);
            this.key.Insert(index, key);
            this.value.Insert(index, value);
            update();
        }
        public L GetValue(K key)
        {
            int index = this.key.IndexOf(key);
            return this.value.ElementAt(index);
        }
        public L GetValue(int index)
        {
            return this.value.ElementAt(index);
        }
        public K GetKey(L value)
        {
            int index = this.value.IndexOf(value);
            return this.key.ElementAt(index);
        }
        public K GetKey(int index)
        {
            return this.key.ElementAt(index);
        }
        public int GetKeyIndex(K key)
        {
            return this.key.IndexOf(key);
        }
        public int GetValueIndex(L value)
        {
            return this.value.IndexOf(value);
        }
        public bool ValueExists(L value)
        {
            return this.value.Contains(value);
        }
        public bool KeyExists(K key)
        {
            return this.key.Contains(key);
        }
        public int KeyOccurence(K key)
        {
            int count = 0;
            foreach(K k in this.key)
                if (key.Equals(k))
                    count++;
            return count;
        }
        public int ValueOccurence(L value)
        {
            int count = 0;
            foreach (L l in this.value)
                if (value.Equals(l))
                    count++;
            return count;
        }
        public List<int> KeyOccurenceIndexes(K key)
        {
            List<int> temp = new List<int>();
            int count = 0;
            foreach(K k in this.key)
                if (key.Equals(k))
                {
                    count++;
                    temp.Add(count);
                }
            return count == 0 ? null : temp;
        }
        public List<int> ValueOccurenceIndexes(L value)
        {
            List<int> temp = new List<int>();
            int count = 0;
            foreach (L l in this.value)
                if (value.Equals(l))
                {
                    count++;
                    temp.Add(count);
                }
            return count == 0 ? null : temp;
        }
        public void Reset()
        {
            this.key.Clear();
            this.value.Clear();
            update();
        }
        public int Size()
        {
            return value.Count;
        }
    }
}