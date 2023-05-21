using Zenject;

namespace Logic
{
    public interface IUpdateManager
    {
        public void AddContainer(DiContainer container);
        public void RemoveContainer(DiContainer container);
        public void AddTickable(ITickable newTickable);
        public void AddFixedTickable(IFixedTickable newTickable);
        public void AddLateTickable(ILateTickable newTickable);
        public void RemoveTickable(ITickable newTickable);
        public void RemoveFixedTickable(IFixedTickable newTickable);
        public void RemoveLateTickable(ILateTickable newTickable);
    }
}