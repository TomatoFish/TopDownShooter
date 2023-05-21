using Game.Settings;
using Zenject;

namespace Game.Level
{
    public class CustomUnitFactory : CustomGameObjectFactory<UnitFactoryArgs, UnitView>
    {
        public CustomUnitFactory(DiContainer container) : base(container)
        {
        }
    
        public override UnitView Create(UnitFactoryArgs args)
        {
            var view = base.Create(args);
        
            view.transform.SetPositionAndRotation(args.Position, args.Rotation);
            return view;
        }
    }

    // public class UnitViewFactory : PlaceholderFactory<UnityEngine.Object, UnitSettings, UnitView>
    // {
    // }
    
    public class UnitViewFactory : GameObjectFactory<UnitFactoryArgs, UnitView>
    {
    }
}