using System;

namespace Logic.Level
{
    public interface IUnit : IDisposable
    {
        public IUnitView UnitView { get; }
        public GUID Guid { get; }
    }
}