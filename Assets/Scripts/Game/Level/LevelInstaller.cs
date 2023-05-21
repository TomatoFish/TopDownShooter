using Logic;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<UnitFactoryArgs, UnitView, UnitViewFactory>().FromFactory<CustomUnitFactory>();
            Container.BindFactory<int, Transform, ItemView, ItemViewFactory>().FromFactory<CustomItemViewFactory>();
            Container.BindFactory<int, Transform, Item, ItemFactory>().FromFactory<CustomItemFactory>();

            Container.BindInterfacesTo<LevelCameraController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UnitSpawnController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UpdateManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UnitRegistry>().AsSingle().NonLazy();

            SignalsDeclare();
            InjectSpawners();
        }
        
        private void InjectSpawners()
        {
            var spawners = FindObjectsOfType<SpawnPoint>();

            foreach (var spawner in spawners)
            {
                Container.Bind<SpawnPoint>().FromInstance(spawner);
            }
        }
        
        private void SignalsDeclare()
        {
            Container.DeclareSignal<LevelTriggerSignal>();
        }
    }
}
