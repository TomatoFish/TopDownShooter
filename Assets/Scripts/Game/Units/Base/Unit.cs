using System;
using Logic;
using Logic.Level;
using Logic.Settings;
using Zenject;

namespace Game.Level
{
    public class Unit<T1, T2> : IDisposable, IUnit where T1 : IUnitView where T2 : IUnitSettings
    {
        public readonly T1 View;
        public readonly T2 Settings;

        private SignalBus _signalBus;
        private GUID _guid;
        
        public IUnitView UnitView => View;
        public GUID Guid => _guid;
        
        public virtual IUnitBaseProperties BaseProperties => Settings.BaseProperties;
        public float WalkSpeed => Settings.MoveProperties.WalkSpeed;
        public float WalkAcceleration => Settings.MoveProperties.WalkAcceleration;
        public float RotationSpeed => Settings.MoveProperties.RotationSpeed;
        public float AimSpeed => Settings.MoveProperties.AimSpeed;
        public float Height => Settings.MoveProperties.Height;
        
        public Unit(T1 view, T2 settings, SignalBus signalBus)
        {
            View = view;
            Settings = settings;
            _guid = GuidGenerator.GenerateGuid();
            signalBus.Fire(new UnitSpawnedSignal(this));
        }

        public virtual void Dispose()
        {
        }
    }
}
