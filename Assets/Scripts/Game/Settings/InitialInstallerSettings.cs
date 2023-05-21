using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/InitialInstallerSettings")]
    public class InitialInstallerSettings : ScriptableObjectInstaller<InitialInstallerSettings>
    {
        public GameSettings Game;
        public DebugSettings Debug;

        public override void InstallBindings()
        {
            Container.BindInstance(Game).IfNotBound();
            Container.BindInstance(Debug).IfNotBound();
        }
    }
}
