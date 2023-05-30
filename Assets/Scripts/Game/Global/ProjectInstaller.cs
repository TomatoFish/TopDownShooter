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

            Container.BindInterfacesTo<ApplicationManager>().AsSingle();
            Container.Bind<GameStateController>().AsSingle().NonLazy();
            Container.Bind<SceneManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().NonLazy();

            BindSignals();
        }

        private void BindSignals()
        {
            // Global
            Container.DeclareSignal<ExitGameSignal>();
            Container.DeclareSignal<RunLevelSignal>();
            
            // Unit
            Container.DeclareSignal<SpawnUnitSignal>();
            Container.DeclareSignal<UnitSpawnedSignal>();
            Container.DeclareSignal<DestroyUnitSignal>();
            
            // UI Signals
            Container.DeclareSignal<ChangeUIStateSignal>();
            Container.DeclareSignal<ShowWidgetSignal>();
            Container.DeclareSignal<HideWidgetSignal>();
            Container.DeclareSignal<UIStepBackSignal>();
            Container.DeclareSignal<HideAllWidgetsSignal>();
        }
    }
}