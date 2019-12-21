using System;

namespace ConsoleApp.Riddles
{
    public class CustomDictionary<TKey, TValue>
    {
        struct Entry
        {
            internal TKey key;
            internal TValue value;
        }

        Entry[] entries;
        int entryLength;

        public CustomDictionary(int length)
        {
            this.entryLength = length;
            this.entries = new Entry[length];
        }

        public TKey[] Keys
        {
            get
            {
                var keys = new TKey[entries.Length];
                for (var i = 0; i < entries.Length; i++)
                {
                    keys[i] = entries[i].key;
                }
                return keys;
            }
        }

        public TValue[] Values
        {
            get
            {
                var values = new TValue[entries.Length];
                for(var i = 0; i < entries.Length; i++)
                {
                    values[i] = entries[i].value;
                }
                return values;
            }
        }

        private int IndexOf(TKey key)
        {
            var hashCode = key.GetHashCode();
            if(hashCode < this.entryLength)
            {
                return hashCode;
            }
            else
            {
                // Detect index collision and resolve it
                int index = hashCode / this.entryLength;
                while(index > this.entryLength)
                {
                    index = index / this.entryLength;
                }
                var indexOccupied = !this.entries[index].key.Equals(default(TKey));
                // Temporary code to identify free index slot
                if(indexOccupied)
                {
                    index -= 1;
                }
                return index;
            }
        }

        public bool ContainsKey(TKey key)
        {
            var index = this.IndexOf(key);
            var defaultKey = default(TKey);
            var keyName = this.entries[index].key;
            return !keyName.Equals(defaultKey);
        }

        public TValue this[TKey key]
        {
            get
            {
                var index = this.IndexOf(key);
                if(index < 0)
                {
                    return default(TValue);
                }
                else
                {
                    return this.entries[index].value;
                }
            }
            set
            {
                this.Add(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            var index = this.IndexOf(key);
            this.entries[index].key = key;
            this.entries[index].value = value;
        }        
    }
}
