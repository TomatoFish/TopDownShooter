using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class DebugSettings
    {
        [SerializeField] private StartInventory _inventory;

        public StartInventory Inventory => _inventory;
        
        [Serializable]
        public class StartInventory
        {
            [SerializeField] private List<int> _itemsId;

            public List<int> ItemsId => _itemsId;
        }
    }
}