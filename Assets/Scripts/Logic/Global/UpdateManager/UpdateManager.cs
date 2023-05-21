using System;
using System.Collections.Generic;
using Zenject;

namespace Logic
{
    public class UpdateManager : IUpdateManager, ITickable, IFixedTickable, ILateTickable
    {
        private List<ITickable> _tickables;
        private List<IFixedTickable> _fixedTickables;
        private List<ILateTickable> _lateTickables;

        [Inject]
        public void Construct()
        {
            _tickables = new List<ITickable>();
            _fixedTickables = new List<IFixedTickable>();
            _lateTickables = new List<ILateTickable>();
        }

        public void AddContainer(DiContainer container)
        {
            container.ResolveAll<ITickable>().ForEach(c =>
            {
                if (c is not IUpdateManager) AddTickable(c);
            });
            
            container.ResolveAll<IFixedTickable>().ForEach(c =>
            {
                if (c is not IUpdateManager) AddFixedTickable(c);
            });
            
            container.ResolveAll<ILateTickable>().ForEach(c =>
            {
                if (c is not IUpdateManager) AddLateTickable(c);
            });

            var a = container.ResolveAll<IInitializable>();
            a.ForEach(initializable =>
            {
                initializable.Initialize();
            });
        }

        public void RemoveContainer(DiContainer container)
        {
            container.ResolveAll<ITickable>().ForEach(RemoveTickable);
            container.ResolveAll<IFixedTickable>().ForEach(RemoveFixedTickable);
            container.ResolveAll<ILateTickable>().ForEach(RemoveLateTickable);
            container.ResolveAll<IDisposable>().ForEach(c => c.Dispose());
        }

        #region Add
        
        public void AddTickable(ITickable newTickable)
        {
            if (_tickables.Contains(newTickable)) return;
            _tickables.Add(newTickable);
        }
        
        public void AddFixedTickable(IFixedTickable newTickable)
        {
            if (_fixedTickables.Contains(newTickable)) return;
            _fixedTickables.Add(newTickable);
        }
        
        public void AddLateTickable(ILateTickable newTickable)
        {
            if (_lateTickables.Contains(newTickable)) return;
            _lateTickables.Add(newTickable);
        }
        
        #endregion

        #region Remove
        
        public void RemoveTickable(ITickable newTickable)
        {
            if (!_tickables.Contains(newTickable)) return;
            _tickables.Remove(newTickable);
        }
        
        public void RemoveFixedTickable(IFixedTickable newTickable)
        {
            if (!_fixedTickables.Contains(newTickable)) return;
            _fixedTickables.Remove(newTickable);
        }
        
        public void RemoveLateTickable(ILateTickable newTickable)
        {
            if (!_lateTickables.Contains(newTickable)) return;
            _lateTickables.Remove(newTickable);
        }

        #endregion

        #region Tick

        public void Tick()
        {
            foreach (var tickable in _tickables)
            {
                tickable.Tick();
            }
        }

        public void FixedTick()
        {
            foreach (var tickable in _fixedTickables)
            {
                tickable.FixedTick();
            }
        }

        public void LateTick()
        {
            foreach (var tickable in _lateTickables)
            {
                tickable.LateTick();
            }
        }

        #endregion
    }
}