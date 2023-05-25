using System.Collections.Generic;
using Logic;
using UnityEngine;

namespace Game
{
    public class GameObjectDictionaryPool : DictionaryPool<System.Type, GameObject>
    {
        public GameObjectDictionaryPool(IDictionary<System.Type, GameObject> initialDictionary) : base(initialDictionary)
        {
            foreach (var value in initialDictionary.Values)
            {
                value.SetActive(false);
            }
        }

        public override GameObject Get(System.Type key)
        {
            TryGet(key, out var value);
            return value;
        }

        public override bool TryGet(System.Type key, out GameObject value)
        {
            var haveObject = base.TryGet(key, out value);
            if (value != null)
            {
                value.SetActive(true);
            }
            
            return haveObject;
        }

        public override GameObject Release(System.Type key)
        {
            var value = base.Release(key);
            value.SetActive(false);
            return value;
        }

        public override void Dispose()
        {
            foreach (var value in Pool.Values)
            {
                UnityEngine.Object.Destroy(value);
            }
            base.Dispose();
        }
    }
}