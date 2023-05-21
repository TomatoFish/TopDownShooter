using UnityEngine;
using Zenject;

namespace Game
{
    public class InitialInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();
        }
    }
}
