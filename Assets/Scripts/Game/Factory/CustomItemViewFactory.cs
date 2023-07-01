using Zenject;

namespace Game.Level
{
    public class CustomItemViewFactory : CustomGameObjectFactory<ItemFactoryArgs, ItemView>
    {
        public CustomItemViewFactory(DiContainer container) : base(container)
        {
        }

        public override ItemView Create(ItemFactoryArgs args)
        {
            var view  = base.Create(args);
            
            return view;
        }
    }

    public class ItemViewFactory : PlaceholderFactory<ItemFactoryArgs, ItemView> { }
}
