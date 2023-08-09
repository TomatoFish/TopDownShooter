using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(menuName = "Settings/LevelInstallerSettings")]
    public class LevelInstallerSettings : ScriptableObjectInstaller<LevelInstallerSettings>
    {
        [SerializeField] private UnitRegistrySettings _unitSettings;
        [SerializeField] private ItemRegistrySettings _itemSettings;
        [SerializeField] private VFXLibrary _vfxLibrary;

        public override void InstallBindings()
        {
            Container.BindInstance(_unitSettings).IfNotBound();
            Container.BindInstance(_itemSettings.Registry).IfNotBound();
            Container.BindInstance(_vfxLibrary).IfNotBound();
        }
    }
}
