using UnityEngine.UIElements;

namespace Game.UI
{
    public class LoadingScreenWidget : UIWidget
    {
        protected override string WidgetName => "LoadingScreen";
        public override bool IsAdditive => false;
        
        public LoadingScreenWidget(UIDocument uiDocument) : base(uiDocument)
        {
        }

    }
}