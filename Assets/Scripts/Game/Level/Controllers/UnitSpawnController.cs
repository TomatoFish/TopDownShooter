using System;
using Game.Settings;
using Logic.Level;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class UnitSpawnController : IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private UnitRegistrySettings _unitRegistry;
        [Inject] private UnitViewFactory _unitViewFactory;

        public void Initialize()
        {
            _signalBus.Subscribe<SpawnUnitSignal>(SpawnUnit);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SpawnUnitSignal>(SpawnUnit);
        }

        private async void SpawnUnit(SpawnUnitSignal s)
        {
            var settings = _unitRegistry.GetUnitObject(s.UnitId);
            var obj = await settings.LoadObject();

            var args = new UnitFactoryArgs((GameObject)obj, null, false, s.Position, s.Rotation);
            var go = _unitViewFactory.Create(args);
            
            go.transform.SetPositionAndRotation(s.Position, s.Rotation);
        }
    }
}