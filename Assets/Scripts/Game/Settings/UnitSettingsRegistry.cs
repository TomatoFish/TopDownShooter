using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class UnitRegistrySettings
    {
        [SerializeField] private Set _registry; // todo: change to serializable dictionary

        [Serializable]
        public class Set
        {
            [SerializeField] private List<Pair> _items;

            public List<Pair> Items => _items;

            [Serializable]
            public class Pair
            {
                public string Key;
                public UnitObject Value;
            }
        }

        public UnitObject GetUnitObject(string id)
        {
            return _registry.Items.Count(i => i.Key.Equals(id)) > 0 ? _registry.Items.First(i => i.Key.Equals(id)).Value : null;
        }
    }
}