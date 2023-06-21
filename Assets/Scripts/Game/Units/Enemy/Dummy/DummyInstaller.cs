namespace Game.Level
{
    public class DummyInstaller : EnemyInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInterfacesAndSelfTo<Dummy>().AsSingle();
        }
    }
}