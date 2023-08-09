using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game
{
    public class VFXManager : IInitializable, IDisposable
    {
        private readonly DiContainer _container;
        private readonly VFXLibrary _vfxLibrary;
        private Dictionary<string, List<GameObject>> _pool;

        public VFXManager(DiContainer container, VFXLibrary vfxLibrary)
        {
            _container = container;
            _vfxLibrary = vfxLibrary;
        }
        
        public void Initialize()
        {
            _pool = new Dictionary<string, List<GameObject>>();
        }

        public void Dispose()
        {
            foreach (var poolObject in _pool.Values.SelectMany(poolValue => poolValue))
            {
                UnityEngine.Object.Destroy(poolObject);
            }

            _pool = null;
        }

        public T Get<T>(string key) where T : UnityEngine.Component
        {
            if (!_pool.ContainsKey(key) || _pool[key].All(poolObject => poolObject.activeSelf))
            {
                var prefab = _vfxLibrary.Get(key);
                Add(key, prefab, 1);
            }

            var returnObject = _pool[key].First(poolObject => !poolObject.activeSelf);
            returnObject.SetActive(true);
            return returnObject.GetComponent<T>();
        }

        public T Get<T>(string key, float returnDelay) where T : UnityEngine.Component
        {
            var returnObject = Get<T>(key);
            ReturnToPoolDelayed(returnObject.gameObject, returnDelay);
            return returnObject;
        }
        
        public void Add(string key, GameObject prefab, int capacity)
        {
            if (!_pool.ContainsKey(key))
            {
                _pool.Add(key, new List<GameObject>(capacity));
            }

            var newPoolObject = _container.InstantiatePrefab(prefab);
            newPoolObject.SetActive(false);
            _pool[key].Add(newPoolObject);
        }

        public async void ReturnToPoolDelayed(GameObject go, float delay)
        {
            await Task.Delay((int)(delay * 1000));
            go.SetActive(false);
        }
    }
}