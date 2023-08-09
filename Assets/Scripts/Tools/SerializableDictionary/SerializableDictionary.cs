using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    [Serializable]
    public class SerializableDictionary<TK, TV> : IDictionary<TK, TV>, ISerializationCallbackReceiver
    {
        private Dictionary<TK, TV> _dictionary;
        [SerializeField] private List<KeyValuePairSerializable> _pairs;

        public SerializableDictionary()
        {
            _dictionary = new Dictionary<TK, TV>();
            if (Application.isEditor)
            {
                _pairs = new List<KeyValuePairSerializable>();
            }
        }
        
        public SerializableDictionary(Dictionary<TK, TV> initialDictionary) : this()
        {
            foreach (var pair in initialDictionary)
            {
                _dictionary.Add(pair.Key, pair.Value);
            }
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            CheckForNull();
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            CheckForNull();
            return GetEnumerator();
        }

        void ICollection<KeyValuePair<TK, TV>>.Add(KeyValuePair<TK, TV> item)
        {
            CheckForNull();
            (_dictionary as ICollection<KeyValuePair<TK, TV>>).Add(item);
        }

        public void Clear()
        {
            CheckForNull();
            _dictionary.Clear();
            
            if (Application.isEditor)
            {
                _pairs?.Clear();
            }
        }

        bool ICollection<KeyValuePair<TK, TV>>.Contains(KeyValuePair<TK, TV> item)
        {
            CheckForNull();
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TK, TV>>.CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            CheckForNull();
            (_dictionary as ICollection<KeyValuePair<TK, TV>>).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TK, TV>>.Remove(KeyValuePair<TK, TV> item)
        {
            CheckForNull();
            return _dictionary.Contains(item) && _dictionary.Remove(item.Key);
        }

        public int Count
        {
            get
            {
                CheckForNull();
                return _dictionary.Count();
            }
        }
        public bool IsReadOnly
        {
            get
            {
                CheckForNull();
                return (_dictionary as ICollection<KeyValuePair<TK, TV>>).IsReadOnly;
            }
        }
        
        public void Add(TK key, TV value)
        {
            CheckForNull();
            key = GetDefaultValue(key);
            _dictionary.Add(key, value);
            
            if (Application.isEditor)
            {
                _pairs?.Add(new KeyValuePairSerializable(key, value));
            }
        }

        public bool ContainsKey(TK key)
        {
            CheckForNull();
            return _dictionary.ContainsKey(key);
        }

        public bool Remove(TK key)
        {
            CheckForNull();
            if (Application.isEditor && _pairs != null)
            {
                for (int i = 0; i < _pairs.Count; i++)
                {
                    if (_pairs[i].Key.Equals(key))
                    {
                        _pairs.RemoveAt(i);
                        break;
                    }
                }
            }

            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TK key, out TV value)
        {
            CheckForNull();
            return _dictionary.TryGetValue(key, out value);
        }

        public TV this[TK key]
        {
            get
            {
                CheckForNull();
                return _dictionary[key];
            }
            set
            {
                CheckForNull();
                _dictionary[key] = value;
            }
        }

        public ICollection<TK> Keys
        {
            get
            {
                CheckForNull();
                return _dictionary.Keys;
            }
        }
        public ICollection<TV> Values
        {
            get
            {
                CheckForNull();
                return _dictionary.Values;
            }
        }
        
        public void OnBeforeSerialize()
        {
            var count = Count;
            if (count == 0)
            {
                _pairs = null;
            }
            else
            {
                _pairs = new List<KeyValuePairSerializable>(count);

                foreach (var pair in _dictionary)
                {
                    _pairs.Add(new KeyValuePairSerializable(pair.Key, pair.Value));
                }
            }
        }

        public void OnAfterDeserialize()
        {
            CheckForNull();
            _dictionary.Clear();

            for (int i = 0; i < _pairs.Count; i++)
            {
                if (_pairs[i].Key == null)
                {
                    var key = System.Activator.CreateInstance<TK>();
                    _pairs[i] = new KeyValuePairSerializable(key, _pairs[i].Value);
                }

                _dictionary[_pairs[i].Key] = _pairs[i].Value;
            }
        }

        private void CheckForNull()
        {
            _dictionary ??= new Dictionary<TK, TV>();
            _pairs ??= new List<KeyValuePairSerializable>();
        }

        private T GetDefaultValue<T>(T val)
        {
            if (val != null) return val;
            
            if (typeof(System.ValueType).IsAssignableFrom(typeof(T)))
            {
                return default(T);
            }
            else if (typeof(T) == typeof(string))
            {
                object emptyStr = string.Empty;
                return (T)emptyStr;
            }
            return System.Activator.CreateInstance<T>();
        }

        [Serializable]
        public class KeyValuePairSerializable
        {
            [SerializeField] private TK _key;
            [SerializeField] private TV _value;

            public KeyValuePairSerializable(TK key, TV value)
            {
                _key = key;
                _value = value;
            }

            public TK Key => _key;
            public TV Value => _value;
        }
    }
}