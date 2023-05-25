using Game.Input;
using Game.Level;
using Game.Scenes;
using Game.UI;
using Logic;
using Zenject;

namespace Game
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Bind<GameStateController>().AsSingle().NonLazy();
            Container.Bind<SceneManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();

            BindSignals();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<SpawnUnitSignal>();
            Container.DeclareSignal<UnitSpawnedSignal>();
            Container.DeclareSignal<DestroyUnitSignal>();
        }
    }
}