using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace S2fx.Utility {

    public class ProxiedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue> {

        public IDictionary<TKey, TValue> UnderlayerDictionary { get; private set; }

        public void Reset(IDictionary<TKey, TValue> dict) {
            this.UnderlayerDictionary = dict;
        }

        public ICollection<TKey> Keys => this.UnderlayerDictionary.Keys;

        public ICollection<TValue> Values => this.UnderlayerDictionary.Values;

        public int Count => this.UnderlayerDictionary.Count;

        public bool IsReadOnly => this.UnderlayerDictionary.IsReadOnly;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.UnderlayerDictionary.Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => this.UnderlayerDictionary.Values;

        public TValue this[TKey key] {
            get => this.UnderlayerDictionary[key];
            set => this.UnderlayerDictionary[key] = value;
        }

        public void Add(TKey key, TValue value) {
            this.UnderlayerDictionary.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item) {
            this.UnderlayerDictionary.Add(item);
        }

        public void Clear() {
            this.UnderlayerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            this.UnderlayerDictionary.Contains(item);

        public bool ContainsKey(TKey key) =>
            this.UnderlayerDictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            this.UnderlayerDictionary.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            this.UnderlayerDictionary.GetEnumerator();

        public bool Remove(TKey key) =>
            this.UnderlayerDictionary.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item) =>
            this.UnderlayerDictionary.Remove(item);

        public bool TryGetValue(TKey key, out TValue value) =>
            this.UnderlayerDictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() =>
            this.UnderlayerDictionary.GetEnumerator();
    }

}
