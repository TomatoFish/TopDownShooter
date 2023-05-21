using System;
using System.Collections.Generic;
using Logic;
using Zenject;

namespace Game.Level
{
    public class UnitRegistry : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        
        private KeyValuePair<GUID, Unit> _player;
        private Dictionary<GUID, Unit> _enemies;
        
        public void Initialize()
        {
            _signalBus.Subscribe<UnitSpawnedSignal>(UnitSpawnedSignalHandler);
            _signalBus.Subscribe<DestroyUnitSignal>(DestroyUnitSignalHandler);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<UnitSpawnedSignal>(UnitSpawnedSignalHandler);
            _signalBus.Unsubscribe<DestroyUnitSignal>(DestroyUnitSignalHandler);
        }

        public void DestroyUnitSignalHandler(DestroyUnitSignal signal)
        {
            RemoveUnit(signal.Guid);
        }
        
        public void UnitSpawnedSignalHandler(UnitSpawnedSignal signal)
        {
            var type = signal.Unit.GetType();
            switch (type)
            {
                case Type _ when type == typeof(Player):
                    AddPlayer(signal.Unit.Guid, signal.Unit);
                    break;
                case Type _ when type == typeof(Enemy):
                    AddPlayer(signal.Unit.Guid, signal.Unit);
                    break;
            }
        }

        private void AddPlayer(GUID guid, IDisposable disposable)
        {
            _player.Value?.Disposable.Dispose();
            _player = new KeyValuePair<GUID, Unit>(guid, new Unit(disposable));
        }
        
        private void AddEnemy(GUID guid, IDisposable disposable)
        {
            RemoveUnit(guid);
            _enemies.Add(guid, new Unit(disposable));
        }

        private void RemoveUnit(GUID guid)
        {
            if (_player.Key == guid)
            {
                _player.Value.Disposable.Dispose();
                _player = new KeyValuePair<GUID, Unit>();
                return;
            }

            if (_enemies.ContainsKey(guid))
            {
                _enemies[guid].Disposable.Dispose();
                _enemies.Remove(guid);
            }
        }
        
        private class Unit
        {
            public readonly IDisposable Disposable;

            public Unit(IDisposable disposable)
            {
                Disposable = disposable;
            }
        }
    }
}