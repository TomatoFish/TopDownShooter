using UnityEngine;
using Zenject;

namespace Game
{
    public class InitialInstaller : MonoInstaller
    {
        [SerializeField] private Transform _uiContainer;
        
        public override void InstallBindings()
        {
            //Container.Bind<Transform>().WithId("UIContainer").FromInstance(_uiContainer).AsSingle();
            Container.BindInstance(_uiContainer).WithId("UIContainer");
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();
        }
    }
}
