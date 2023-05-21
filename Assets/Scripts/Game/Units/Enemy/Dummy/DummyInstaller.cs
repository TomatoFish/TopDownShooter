namespace Game.Level
{
    public class DummyInstaller : EnemyInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Dummy>().AsSingle();
        }
    }
}