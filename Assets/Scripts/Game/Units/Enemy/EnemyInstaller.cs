namespace Game.Level
{
    public class EnemyInstaller : UnitInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<Enemy>().AsSingle();
        }
    }
}
