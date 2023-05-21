using Game.Settings;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private UnitSettings _unitSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitSettings>().FromInstance(_unitSettings).AsSingle();
        }
    }
}