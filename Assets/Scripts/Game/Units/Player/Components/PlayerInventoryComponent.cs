using Game.Input;
using System;
using System.Collections.Generic;
using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class PlayerInventoryComponent : IDisposable, ILateTickable
    {
        [Inject] private InputController _input;
        [Inject] private PlayerView _playerView;
        [Inject] private PlayerInventoryView _inventoryView;
        [Inject] private DebugSettings _debugSettings;
        [Inject] private SignalBus _signalBus;
        [Inject] private ItemViewFactory _itemViewFactory;
        [Inject] private ItemRegistrySettings.Set _itemRegistry;

        private bool _isAiming;
        private int _currentItemId = 0;
        private Dictionary<int, Item> _items;

        public Dictionary<int, Item> Items => _items;
        public Item CurrentItem => _items[_currentItemId];
        public float WeaponHeight => CurrentItem.Transform.position.y - _playerView.MovementTransform.position.y;
        
        [Inject]
        public void Construct()
        {
            InstallWeapons(_inventoryView.WeaponContainer);
            _inventoryView.LaserAim.gameObject.SetActive(false);

            _input.ItemChange += OnItemChanged;
            _input.Fire += UseItem;
            _signalBus.Subscribe<PlayerAimSignal>(PrepareItem);
        }
        
        public void Dispose()
        {
            _input.ItemChange -= OnItemChanged;
            _input.Fire -= UseItem;
            _signalBus.TryUnsubscribe<PlayerAimSignal>(PrepareItem);
        }

        public void LateTick()
        {
            if (_isAiming)
            {
                _inventoryView.LaserAim.SetPosition(0, CurrentItem.AimSightTransform.position);
                _inventoryView.LaserAim.SetPosition(1, CurrentItem.AimSightTransform.position + CurrentItem.AimSightTransform.forward * 10);
            }
        }
        
        private void InstallWeapons(Transform weaponContainer)
        {
            _items = new Dictionary<int, Item>();
            foreach (var itemId in _debugSettings.Inventory.ItemsId)
            {
                var newWeapon = CreateItem(itemId, weaponContainer);
                newWeapon.SetActive(itemId == _currentItemId);
                _items.Add(itemId, newWeapon);
            }
        }

        private void PrepareItem(PlayerAimSignal s)
        {
            _isAiming = s.IsAiming;
            _inventoryView.LaserAim.gameObject.SetActive(s.IsAiming);
        }
        
        private void OnItemChanged(int id)
        {
            var prevItem = CurrentItem;
            CurrentItem.SetActive(false);
            _currentItemId = id;
            CurrentItem.SetActive(true);
            _signalBus.TryFire(new PlayerItemChangedSignal(prevItem, CurrentItem));
        }
        
        private void UseItem(bool isActive)
        {
            if (!isActive) return;
            TryUseItem();
        }

        public bool TryUseItem()
        {
            if (!_isAiming) return false;
            return CurrentItem.Use();
        }

        private Item CreateItem(int itemId, Transform root)
        {
            var prefab = _itemRegistry.GetItemView(itemId);
            var config = _itemRegistry.GetItemConfig(itemId);
            var args = new ItemFactoryArgs(prefab.gameObject, root, false);
            var itemView = _itemViewFactory.Create(args);

            return new Item(itemView, config);
        }
    }
}
