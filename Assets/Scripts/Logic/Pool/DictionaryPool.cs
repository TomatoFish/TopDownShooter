using System;
using System.Collections.Generic;

namespace Logic
{
    public class DictionaryPool<T1, T2> : IDictionaryPool<T1, T2>, IDisposable
    {
        private readonly Dictionary<T1, T2> _pool;
        private readonly Dictionary<T1, T2> _activeObjects;

        protected Dictionary<T1, T2> Pool => _pool;
        
        public DictionaryPool()
        {
            _pool = new Dictionary<T1, T2>();
            _activeObjects = new Dictionary<T1, T2>();
        }
        
        public DictionaryPool(IDictionary<T1, T2> initialDictionary) : this()
        {
            foreach (var initialPair in initialDictionary)
            {
                _pool.Add(initialPair.Key, initialPair.Value);
            }
        }

        public virtual T2 Get(T1 key)
        {
            TryGet(key, out var value);
            return value;
        }

        public virtual bool TryGet(T1 key, out T2 value)
        {
            var isActive = _activeObjects.TryGetValue(key, out value);
            if (isActive)
            {
                return true;
            }
            
            var haveValue = _pool.TryGetValue(key, out value);
            if (haveValue)
            {
                _activeObjects.Add(key, value);
            }

            return haveValue;
        }

        public virtual T2 Release(T1 key)
        {
            var isActive = _activeObjects.TryGetValue(key, out T2 value);
            if (isActive)
            {
                _activeObjects.Remove(key);
            }

            return value;
        }
        
        public virtual bool HaveInPool(T1 key)
        {
            return _pool.ContainsKey(key);
        }
        
        public virtual bool IsActive(T1 key)
        {
            return _activeObjects.ContainsKey(key);
        }

        public virtual void Dispose()
        {
            _pool.Clear();
            _activeObjects.Clear();
        }
    }
}