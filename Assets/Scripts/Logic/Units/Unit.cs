using System;
using Logic.Settings;

namespace Logic.Level
{
    public class Unit<T1, T2> : IDisposable, IUnit where T1 : IUnitView where T2 : IUnitSettings
    {
        public readonly T1 View;
        public readonly T2 Settings;

        private GUID _guid;
        
        public IUnitView UnitView => View;
        public GUID Guid => _guid;
        
        public virtual IUnitBaseProperties BaseProperties => Settings.BaseProperties;

        public Unit(T1 view, T2 settings)
        {
            View = view;
            Settings = settings;
            _guid = GuidGenerator.GenerateGuid();
        }

        public virtual void Dispose()
        {
        }
    }
}
