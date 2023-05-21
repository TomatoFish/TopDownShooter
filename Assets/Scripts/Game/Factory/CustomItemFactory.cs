using Game.Level;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class CustomItemFactory : IFactory<int, Transform, Item>
    {
        private readonly DiContainer _container;
        private ItemRegistrySettings.Set _itemRegistry;

        public CustomItemFactory(DiContainer container, ItemRegistrySettings.Set itemRegistry)
        {
            _container = container;
            _itemRegistry = itemRegistry;
        }

        public Item Create(int id, Transform viewRoot = null)
        {
            var itemViewPrefab = _itemRegistry.GetItemView(id);
            var ItemConfig = _itemRegistry.GetItemConfig(id);
            var itemView = _container.InstantiatePrefabForComponent<ItemView>(itemViewPrefab, viewRoot);
            return new Item(itemView, ItemConfig);
        }
    }

    public class ItemFactory : PlaceholderFactory<int, Transform, Item> { }
}
