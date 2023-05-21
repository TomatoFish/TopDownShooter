using Zenject;

namespace Game.Level
{
    public class PlayerInstaller : UnitInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();

            Container.BindInterfacesAndSelfTo<Player>().AsSingle();
            
            DeclareSignals();
            Container.Bind<ItemSpawner>().AsSingle();

            Container.BindInterfacesTo<PlayerMoveComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimationComponent>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerAimComponent>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerCameraFollow>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerRigControlComponent>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInventoryComponent>().AsSingle().NonLazy();

            //#if UNITY_EDITOR
            //Container.Resolve<Game.Scenes.SceneManager>().LoadScene("");
            //#endif
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<PlayerAimSignal>();
            Container.DeclareSignal<PlayerItemChangedSignal>();
        }
    }
}
