using Game.UI;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game
{
    public class InitialInstaller : MonoInstaller
    {
        [SerializeField] private UIDocument _uiDocument;

        private UIManager _uiManager;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public override void InstallBindings()
        {
            _uiManager.SetUIDocument(_uiDocument);
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();
        }
    }
}
