using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/LevelInstallerSettings")]
    public class LevelInstallerSettings : ScriptableObjectInstaller<LevelInstallerSettings>
    {
        public UnitRegistrySettings UnitSettings;
        public ItemRegistrySettings ItemSettings;

        public override void InstallBindings()
        {
            // foreach (var unit in UnitSettings)
            // {
            //     Container.BindInstance(unit.Value.Object).WithId(unit.Key + "_object").IfNotBound();
            //     Container.BindInstance(unit.Value.BaseProperties).WithId(unit.Key + "_base_prop").IfNotBound();
            //     Container.BindInstance(unit.Value.MoveProperties).WithId(unit.Key + "_move_prop").IfNotBound();
            // }
            
            // Container.BindInstance(PlayerSettings.Objects).IfNotBound();
            // Container.BindInstance(PlayerSettings.Properties).IfNotBound();
            // Container.BindInstance(EnemySettings.Objects).IfNotBound();
            // Container.BindInstance(EnemySettings.Properties).IfNotBound();
            Container.BindInstance(UnitSettings).IfNotBound();
            Container.BindInstance(ItemSettings.Registry).IfNotBound();
        }
    }
}
