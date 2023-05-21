using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class CustomItemViewFactory : IFactory<int, Transform, ItemView>
    {
        private readonly DiContainer _container;
        private ItemRegistrySettings.Set _itemRegistry;

        public CustomItemViewFactory(DiContainer container, ItemRegistrySettings.Set itemRegistry)
        {
            _container = container;
            _itemRegistry = itemRegistry;
        }
        
        public ItemView Create(int id, Transform root = null)
        {
            var itemView = _itemRegistry.GetItemView(id);
            return _container.InstantiatePrefabForComponent<ItemView>(itemView, root);
        }
    }
    
    public class ItemViewFactory : PlaceholderFactory<int, Transform, ItemView> { }
}