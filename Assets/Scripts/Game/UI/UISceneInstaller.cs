using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UISceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiContainer;
        private UIManager _uiManager;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public override void InstallBindings()
        {
            
        }
    }
}