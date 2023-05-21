using System;
using System.Collections.Generic;
using Game.Config;
using Game.Level;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class ItemRegistrySettings
    {
        [SerializeField] private Set _registry;

        public Set Registry => _registry;

        [Serializable]
        public class Set
        {
            [SerializeField] private List<ItemSettings> _items;

            public List<ItemSettings> Items => _items;

            public ItemView GetItemView(int id)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Id.Equals(id))
                    {
                        return _items[i].View;
                    }
                }

                return null;
            }

            public ItemConfig GetItemConfig(int id)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Id.Equals(id))
                    {
                        return _items[i].Config;
                    }
                }

                return null;
            }
        }
        
        [Serializable]
        public class ItemSettings
        {
            [SerializeField] private int _id;
            [SerializeField] private ItemView _view;
            [SerializeField] private ItemConfig _config;

            public int Id => _id;
            public ItemView View => _view;
            public ItemConfig Config => _config;
        }
    }
}